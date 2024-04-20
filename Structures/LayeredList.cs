using HospitalInformationSystem.Models;
using System;
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

            // если узел с данными проката найден, удалить его из списка
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

        // Вспомогательный метод для сортировки QuickSort
        private ListNode QuickSort(ListNode head)
        {
            if (head == null || head.Next == null)
                return head;

            // Разделение списка на две части по pivot
            ListNode[] partitions = Partition(head);

            // Рекурсивная сортировка двух частей
            partitions[0] = QuickSort(partitions[0]); // Левая часть
            partitions[1] = QuickSort(partitions[1]); // Правая часть

            // Объединение отсортированных частей
            ListNode sortedList = partitions[0];

            ListNode tail = sortedList;
            while (tail != null && tail.Next != null)
            {
                tail = tail.Next;
            }

            if (tail != null)
            {
                tail.NextGroup = partitions[1];
            }

            return sortedList;
        }

        // Вспомогательный метод для разделения списка на две части
        private ListNode[] Partition(ListNode head)
        {
            if (head == null || head.Next == null)
                return new ListNode[] { head, null };

            ListNode pivot = head;
            ListNode smallerHead = null;
            ListNode smallerTail = null;
            ListNode equalHead = null;
            ListNode equalTail = null;
            ListNode largerHead = null;
            ListNode largerTail = null;

            ListNode current = head;
            while (current != null)
            {
                if (string.Compare(current.Referral.DoctorSurnameInitials, pivot.Referral.DoctorSurnameInitials) < 0)
                {
                    if (smallerHead == null)
                    {
                        smallerHead = current;
                        smallerTail = current;
                    }
                    else
                    {
                        smallerTail.Next = current;
                        smallerTail = smallerTail.Next;
                    }
                }
                else if (string.Compare(current.Referral.DoctorSurnameInitials, pivot.Referral.DoctorSurnameInitials) == 0)
                {
                    if (equalHead == null)
                    {
                        equalHead = current;
                        equalTail = current;
                    }
                    else
                    {
                        equalTail.Next = current;
                        equalTail = equalTail.Next;
                    }
                }
                else
                {
                    if (largerHead == null)
                    {
                        largerHead = current;
                        largerTail = current;
                    }
                    else
                    {
                        largerTail.Next = current;
                        largerTail = largerTail.Next;
                    }
                }

                current = current.Next;
            }

            // Сборка разделенных частей
            ListNode[] partitions = new ListNode[2];
            if (smallerHead != null)
            {
                partitions[0] = smallerHead;
                smallerTail.Next = null;
            }
            else
            {
                partitions[0] = null;
            }

            if (equalHead != null)
            {
                partitions[1] = equalHead;
                equalTail.Next = null;
            }
            else
            {
                partitions[1] = null;
            }

            if (largerHead != null)
            {
                largerTail.Next = null;
            }

            return partitions;
        }
        // Метод для поиска Referral по фамилии врача
        public ObservableCollection<Referral> FindByDoctorName(string doctorSurname)
        {
            ObservableCollection<Referral> foundReferrals = new ObservableCollection<Referral>();

            ListNode currentGroup = Head;

            while (currentGroup != null)
            {
                ListNode currentNode = currentGroup;

                while (currentNode != null)
                {
                    if (currentNode.Referral.DoctorSurnameInitials.Equals(doctorSurname, StringComparison.OrdinalIgnoreCase))
                    {
                        // Добавляем найденный Referral в коллекцию
                        foundReferrals.Add(currentNode.Referral);
                    }

                    currentNode = currentNode.Next;
                }

                currentGroup = currentGroup.NextGroup; // Переходим к следующей группе
            }

            return foundReferrals;
        }

        // Метод для поиска Referral по регистрационному номеру пациента
        public Referral FindByPatientRegistrationNumber(string registrationNumber)
        {
            ListNode currentGroup = Head;

            while (currentGroup != null)
            {
                ListNode currentNode = currentGroup;

                while (currentNode != null)
                {
                    if (currentNode.Referral.PatientRegistrationNumber.Equals(registrationNumber, StringComparison.OrdinalIgnoreCase))
                    {
                        // Возвращаем найденный Referral
                        return currentNode.Referral;
                    }

                    currentNode = currentNode.Next;
                }

                currentGroup = currentGroup.NextGroup; // Переходим к следующей группе
            }

            return null; // Если не найден Referral с указанным регистрационным номером
        }
    }
}
