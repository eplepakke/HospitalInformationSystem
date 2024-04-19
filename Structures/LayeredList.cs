using HospitalInformationSystem.Models;
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
                Head = new ListNode(referral);
                return;
            }

            ListNode currNode = Head;
            ListNode nodeToAdd = new ListNode(referral);

            while (!currNode.Referral.DoctorSurnameInitials.Equals(nodeToAdd.Referral.DoctorSurnameInitials))
            {
                if (currNode.NextGroup == null)
                {
                    break;
                }
                currNode = currNode.NextGroup;
            }

            if (currNode.Referral.DoctorSurnameInitials.Equals(nodeToAdd.Referral.DoctorSurnameInitials))
            {
                // идем до конца группы
                while (currNode.Referral.DoctorSurnameInitials.Equals(nodeToAdd.Referral.DoctorSurnameInitials))
                {
                    if (currNode.Next == null)
                    {
                        currNode.Next = nodeToAdd;
                        return;
                    }
                    if (!currNode.Next.Referral.DoctorSurnameInitials.Equals(nodeToAdd.Referral.DoctorSurnameInitials))
                    {
                        nodeToAdd.Next = currNode.Next;
                        currNode.Next = nodeToAdd;
                        return;
                    }
                    currNode = currNode.Next;
                }

            }
            else if (currNode.NextGroup == null)
            {
                currNode.NextGroup = nodeToAdd;
                // идем до конца группы
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
    }
}
