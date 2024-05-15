using HospitalInformationSystem.Models;
using HospitalInformationSystem.Processing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HospitalInformationSystem.Structures
{
    public class HashNode
    {
        public string Key { get; set; }
        public Patient Patient { get; set; }
        public HashNode(Patient patient)
        {
            Key = patient.RegistrationNumber;
            Patient = patient;
        }
    }
    public class HashTable
    {
        private static int capacity = 500;
        public HashNode[] Table;

        public HashTable()
        {
            Table = new HashNode[capacity];
        }
        public static int HashFunction1(string key)
        {
            int hash = 0;
            foreach (char c in key)
            {
                hash += c * c;
            }
            return hash % capacity;

        }
        public static int HashFunction2(string key)
        {
            int hash = 0;
            foreach (char c in key)
            {
                hash = (hash * 31 + c) % capacity;
            }
            return hash;
        }
        public bool IsEmpty()
        {
            foreach (HashNode node in Table)
            {
                // если хотя бы один элемент не равен null, таблица не пуста
                if (node != null)
                {
                    return false;
                }
            }
            return true;
        }
        private bool IsFull()
        {
            for (int i = 0; i < capacity; i++)
            {
                if (Table[i] == null)
                {
                    return false;
                }
            }
            return true;
        }
        private void ResizeAndRehash()
        {
            int newCapacity = capacity * 2;
            HashNode[] newTable = new HashNode[newCapacity];

            foreach (HashNode node in Table)
            {
                if (node != null)
                {
                    int hashCode = HashFunction1(node.Key);
                    if (newTable[hashCode] == null)
                    {
                        newTable[hashCode] = node;
                    }
                    else
                    {
                        int secondHashCode = HashFunction2(node.Key);
                        int i = 1;
                        while (newTable[(hashCode + i * secondHashCode) % newCapacity] != null)
                        {
                            i++;
                        }
                        newTable[(hashCode + i * secondHashCode) % newCapacity] = node;
                    }
                }
            }

            Table = newTable;
            capacity = newCapacity;
        }
        public ObservableCollection<Patient> GetAllPatients()
        {
            ObservableCollection<Patient> patients = new ObservableCollection<Patient>();

            for (int i = 0; i < capacity; i++)
            {
                if (Table[i] != null)
                {
                    patients.Add(Table[i].Patient);
                }
            }

            return patients;
        }
        public void Put(Patient patient)
        {
            // если таблица переполнена
            if (IsFull())
            {
                ResizeAndRehash();
            }

            HashNode newNode = new HashNode(patient);
            int hashCode = HashFunction1(newNode.Key);

            if (Table[hashCode] == null)
            {
                Table[hashCode] = newNode;
            }
            else
            {
                // двойное хеширование
                int secondHashCode = HashFunction2(newNode.Key);
                int i = 1;
                while (Table[(hashCode + i * secondHashCode) % capacity] != null)
                {
                    i++;
                }
                Table[(hashCode + i * secondHashCode) % capacity] = newNode;
            }
        }
        public void Clear()
        {
            for (int i = 0; i < capacity; i++)
            {
                Table[i] = null; // Set each hash table entry to null
            }
        }
        public Patient GetPatientByNumber(string key)
        {
            int hashCode1 = HashFunction1(key);
            int hashCode2 = HashFunction2(key);

            int i = 0;
            while (true)
            {
                // ищем через двойное хеширование
                int index = (hashCode1 + i * hashCode2) % capacity;
                if (Table[index] != null && Table[index].Key == key)
                {
                    return Table[index].Patient;
                }
                else if (Table[index] == null)
                {
                    // если достигли пустой ячейки, значит, элемента с таким ключом нет в таблице
                    return null;
                }
                i++;
            }
        }
        public List<Patient> SearchByPartiallyFullName(string partiallyFullName)
        {
            List<Patient> patients = new List<Patient>();
            ObservableCollection<Patient> collection = GetAllPatients();
            foreach (var patient in collection)
            {
                if (DataSearch.ContainsIgnoreCase(patient.FullName, partiallyFullName))
                {
                    patients.Add(patient);
                }
            }
            return patients;
        }
        public Patient RemovePatientByNumber(string key)
        {
            int hashCode1 = HashFunction1(key);
            int hashCode2 = HashFunction2(key);

            int i = 0;
            while (i < capacity)
            {
                int index = (hashCode1 + i * hashCode2) % capacity;
                if (Table[index] != null && Table[index].Key == key)
                {
                    // удаляем машину из ячейки и возвращаем ее
                    Patient removedPatient = Table[index].Patient;
                    Table[index] = null;
                    return removedPatient;
                }
                else if (Table[index] == null)
                {
                    // если достигли пустой ячейки, то удаляемого элемента нет
                    break;
                }
                i++;
            }

            // возвращаем null, если элемент не найден
            return null;
        }
    }
}
