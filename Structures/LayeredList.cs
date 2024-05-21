using HospitalInformationSystem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HospitalInformationSystem.Structures
{
    public class ListNode
    {
        public Referral Referral { get; set; }
        public ListNode NextGroup { get; set; }
        public ListNode Next { get; set; }

        public ListNode(Referral referral)
        {
            Referral = referral;
            NextGroup = null;
            Next = null;
        }
    }
    public class LayeredList
    {
        public ListNode Head { get; set; }
        public void Insert(Referral referral)
        {
            if (Head == null)
            {
                // Если список пуст, создаем головной узел
                Head = new ListNode(referral);
                return;
            }

            ListNode currNode = Head;
            ListNode nodeToAdd = new ListNode(referral);

            // Поиск нужной группы (по фамилии врача) или места для вставки
            while (currNode.Referral.DoctorSurnameInitials[0] != nodeToAdd.Referral.DoctorSurnameInitials[0])
            {
                if (currNode.NextGroup == null)
                {
                    // Если не найдена группа, выходим из цикла
                    break;
                }
                currNode = currNode.NextGroup;
            }

            if (currNode.Referral.DoctorSurnameInitials[0] == nodeToAdd.Referral.DoctorSurnameInitials[0])
            {
                // Найдена группа, идем до конца группы
                while (currNode.Referral.DoctorSurnameInitials[0] == nodeToAdd.Referral.DoctorSurnameInitials[0])
                {
                    if (currNode.Next == null)
                    {
                        // Добавляем новый узел в конец текущей группы
                        currNode.Next = nodeToAdd;
                        return;
                    }
                    if (currNode.Next.Referral.DoctorSurnameInitials[0] != nodeToAdd.Referral.DoctorSurnameInitials[0])
                    {
                        // Вставляем новый узел перед первым узлом с другой фамилией врача
                        nodeToAdd.Next = currNode.Next;
                        currNode.Next = nodeToAdd;
                        return;
                    }
                    currNode = currNode.Next;
                }

            }
            else if (currNode.NextGroup == null)
            {
                // Создаем новую группу, если не найдена группа с текущей фамилией врача
                currNode.NextGroup = nodeToAdd;
                // Идем до конца группы
                while (currNode.Next != null)
                {
                    currNode = currNode.Next;
                }
                currNode.Next = nodeToAdd;
            }
        }
        public void Delete(Referral referral)
        {
            if (Head == null)
                return;

            if (Head.Referral.Equals(referral))
            {
                Head = Head.Next;
                return;
            }

            // поиск удаляемого элемента и предыдущего узла
            ListNode prevNode = null;
            ListNode currNode = Head;
            while (currNode != null && !currNode.Referral.Equals(referral))
            {
                prevNode = currNode;
                currNode = currNode.Next;
            }

            // если узел с данными найден, удалить его из списка
            if (currNode != null)
            {
                // если узел является началом группы
                if (prevNode != null && prevNode.NextGroup == currNode)
                {
                    prevNode.NextGroup = currNode.NextGroup;
                }
                // если узел не начало группы
                else if (prevNode != null)
                {
                    prevNode.Next = currNode.Next;
                }
            }
        }
        public ObservableCollection<Referral> GetAllReferrals()
        {
            Sort();
            ObservableCollection<Referral> referrals = new ObservableCollection<Referral>();

            ListNode curr = Head;
            while (curr != null)
            {
                referrals.Add(curr.Referral);
                curr = curr.Next;
            }

            return referrals;
        }
        // Метод для сортировки списка с использованием QuickSort
        public void Sort()
        {
            Head = QuickSort(Head);
        }
        private ListNode QuickSort(ListNode head)
        {
            if (head == null || head.Next == null)
                return head;

            ListNode pivot = head;
            ListNode lessHead = null, lessTail = null;
            ListNode greaterHead = null, greaterTail = null;
            ListNode curr = head.Next;

            while (curr != null)
            {
                if (string.Compare(curr.Referral.DoctorSurnameInitials, pivot.Referral.DoctorSurnameInitials, StringComparison.OrdinalIgnoreCase) <= 0)
                {
                    if (lessHead == null)
                    {
                        lessHead = curr;
                        lessTail = curr;
                    }
                    else
                    {
                        lessTail.Next = curr;
                        lessTail = curr;
                    }
                }
                else
                {
                    if (greaterHead == null)
                    {
                        greaterHead = curr;
                        greaterTail = curr;
                    }
                    else
                    {
                        greaterTail.Next = curr;
                        greaterTail = curr;
                    }
                }
                curr = curr.Next;
            }

            if (lessTail != null)
                lessTail.Next = null;
            if (greaterTail != null)
                greaterTail.Next = null;

            ListNode sortedLess = QuickSort(lessHead);
            ListNode sortedGreater = QuickSort(greaterHead);

            return Concatenate(sortedLess, pivot, sortedGreater);
        }
        private ListNode Concatenate(ListNode less, ListNode pivot, ListNode greater)
        {
            ListNode head = less;

            if (less == null)
            {
                head = pivot;
            }
            else
            {
                ListNode temp = less;
                while (temp.Next != null)
                {
                    temp = temp.Next;
                }
                temp.Next = pivot;
            }

            pivot.Next = greater;

            return head;
        }
        // Метод для поиска Referral по фамилии врача
        public List<Referral> FindByDoctorName(string doctorSurname)
        {
            List<Referral> foundReferrals = new List<Referral>();

            ListNode currentNode = Head;

            while (currentNode != null)
            {
                if (currentNode.Referral.DoctorSurnameInitials.Equals(doctorSurname, StringComparison.OrdinalIgnoreCase))
                {
                    // Добавляем найденный Referral в коллекцию
                    foundReferrals.Add(currentNode.Referral);
                }

                currentNode = currentNode.Next;
            }

            return foundReferrals;
        }
        // Метод для поиска Referral по регистрационному номеру пациента
        public List<Referral> FindByPatientRegistrationNumber(string registrationNumber)
        {
            List<Referral> foundReferrals = new List<Referral>();

            ListNode currentNode = Head;

            while (currentNode != null)
            {
                if (currentNode.Referral.PatientRegistrationNumber.Equals(registrationNumber, StringComparison.OrdinalIgnoreCase))
                {
                    // Добавляем найденный Referral в коллекцию
                    foundReferrals.Add(currentNode.Referral);
                }

                currentNode = currentNode.Next;
            }

            return foundReferrals;
        }
        public void Clear()
        {
            Head = null; // Обнуляем головной узел, чтобы список стал пустым

            // Очищаем память, проходя по всем узлам и удаляя их
            ListNode currentGroup = Head;
            while (currentGroup != null)
            {
                ListNode currentNode = currentGroup;
                while (currentNode != null)
                {
                    ListNode nextNode = currentNode.Next; // Сохраняем ссылку на следующий узел
                    currentNode.Next = null; // Отсоединяем текущий узел от списка
                    currentNode.NextGroup = null; // Отсоединяем текущий узел от группы
                    currentNode = nextNode; // Переходим к следующему узлу
                }
                currentGroup = currentGroup.NextGroup; // Переходим к следующей группе
            }
        }
    }
}
