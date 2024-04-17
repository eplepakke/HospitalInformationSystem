using System;
using System.Collections.Generic;
using HospitalInformationSystem.Models;
using HospitalInformationSystem.Processing;

namespace HospitalInformationSystem.Structures
{
    public class Node
    {
        public Doctor data;
        public Node left, right;
        public int height;

        public Node(Doctor item)
        {
            data = item;
            left = right = null;
            height = 1;
        }
    }

    public class AVLTree
    {
        Node root;
        int Height(Node N)
        {
            return N == null ? -1 : N.height;
        }

        int Max(int a, int b)
        {
            return (a > b) ? a : b;
        }

        Node RightRotate(Node y)
        {
            Node x = y.left;
            Node T2 = x.right;

            x.right = y;
            y.left = T2;

            y.height = Max(Height(y.left), Height(y.right)) + 1;
            x.height = Max(Height(x.left), Height(x.right)) + 1;

            return x;
        }

        Node LeftRotate(Node x)
        {
            Node y = x.right;
            Node T2 = y.left;

            y.left = x;
            x.right = T2;

            x.height = Max(Height(x.left), Height(x.right)) + 1;
            y.height = Max(Height(y.left), Height(y.right)) + 1;

            return y;
        }

        int GetBalance(Node N)
        {
            return N == null ? 0 : Height(N.left) - Height(N.right);
        }

        Node Insert(Node node, Doctor data)
        {
            if (node == null)
                return new Node(data);

            if (String.Compare(data.SurnameInitials, node.data.SurnameInitials) < 0)
                node.left = Insert(node.left, data);
            else if (String.Compare(data.SurnameInitials, node.data.SurnameInitials) > 0)
                node.right = Insert(node.right, data);
            else
                return node;
            node.height = 1 + Max(Height(node.left), Height(node.right));

            int balance = GetBalance(node);

            if (balance > 1 && String.Compare(data.SurnameInitials, node.left.data.SurnameInitials) < 0)
                return RightRotate(node);

            if (balance < -1 && String.Compare(data.SurnameInitials, node.right.data.SurnameInitials) > 0)
                return LeftRotate(node);

            if (balance > 1 && String.Compare(data.SurnameInitials, node.left.data.SurnameInitials) > 0)
            {
                node.left = LeftRotate(node.left);
                return RightRotate(node);
            }

            if (balance < -1 && String.Compare(data.SurnameInitials, node.right.data.SurnameInitials) < 0)
            {
                node.right = RightRotate(node.right);
                return LeftRotate(node);
            }

            return node;
        }

        Node MinValueNode(Node node)
        {
            Node current = node;

            while (current.left != null)
                current = current.left;

            return current;
        }

        Node Delete(Node root, Doctor data)
        {
            if (root == null)
                return root;

            if (String.Compare(data.SurnameInitials, root.data.SurnameInitials) < 0)
                root.left = Delete(root.left, data);
            else if (String.Compare(data.SurnameInitials, root.data.SurnameInitials) > 0)
                root.right = Delete(root.right, data);
            else
            {
                if (root.left == null || root.right == null)
                {
                    Node temp = null;
                    if (temp == root.left)
                        temp = root.right;
                    else
                        temp = root.left;

                    if (temp == null)
                    {
                        temp = root;
                        root = null;
                    }
                    else
                        root = temp;
                }
                else
                {
                    Node temp = MinValueNode(root.right);
                    root.data = temp.data;
                    root.right = Delete(root.right, temp.data);
                }
            }

            if (root == null)
                return root;

            root.height = 1 + Max(Height(root.left), Height(root.right));

            int balance = GetBalance(root);

            if (balance > 1 && GetBalance(root.left) >= 0)
                return RightRotate(root);

            if (balance > 1 && GetBalance(root.left) < 0)
            {
                root.left = LeftRotate(root.left);
                return RightRotate(root);
            }

            if (balance < -1 && GetBalance(root.right) <= 0)
                return LeftRotate(root);

            if (balance < -1 && GetBalance(root.right) > 0)
            {
                root.right = RightRotate(root.right);
                return LeftRotate(root);
            }

            return root;
        }

        Node Search(Node root, Doctor data)
        {
            if (root == null || String.Compare(data.SurnameInitials, root.data.SurnameInitials) == 0)
                return root;

            if (String.Compare(data.SurnameInitials, root.data.SurnameInitials) < 0)
                return Search(root.left, data);

            return Search(root.right, data);
        }

        void InOrder(Node root, Queue<Doctor> resultList)
        {
            if (root != null)
            {
                InOrder(root.left, resultList);
                resultList.Enqueue(root.data);
                InOrder(root.right, resultList);
            }
        }

        Queue<Doctor> InOrderTraversal()
        {
            Queue<Doctor> result = new Queue<Doctor>();
            InOrder(root, result);
            return result;
        }

        void PrintTree(Node root, string prefix, bool isLeft)
        {
            if (root != null)
            {
                Console.Write(prefix);
                Console.Write(isLeft ? "├──" : "└──");
                Console.WriteLine(root.data);

                PrintTree(root.left, prefix + (isLeft ? "│   " : "    "), true);
                PrintTree(root.right, prefix + (isLeft ? "│   " : "    "), false);
            }
        }

        public void Add(Doctor data)
        {
            root = Insert(root, data);
        }

        public void Remove(Doctor data)
        {
            root = Delete(root, data);
        }

        public Doctor SearchBySurnameInitials(string surnameInitials)
        {
            if (root == null)
                return null;

            Doctor searchDoctor = new Doctor
            {
                SurnameInitials = surnameInitials
            };

            return Search(root, searchDoctor).data;
        }

        public List<Doctor> SearchByPartiallyPosition(string position)
        {
            List<Doctor> doctors = new List<Doctor>();
            Queue<Doctor> queue = InOrderTraversal();
            while(queue.Count > 0)
            {
                Doctor doctor = queue.Dequeue();
                if (DataSearch.ContainsIgnoreCase(doctor.Position, position))
                {
                    doctors.Add(doctor);
                }
            }
            return doctors;
        }

        public void PrintTree()
        {
            Console.WriteLine("Дерево:");
            PrintTree(root, "", false);
        }
    }
}
