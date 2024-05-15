using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HospitalInformationSystem.Models;
using HospitalInformationSystem.Processing;
using HospitalInformationSystem.Structures;

namespace HospitalInformationSystem
{
    class Program
    {
        public static HashTable hashTable = new HashTable();
        public static AVLTree avlTree = new AVLTree();
        public static LayeredList layeredList = new LayeredList();
        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в информационную систему больницы!");
            using (var context = new ApplicationDbContext())
            {
                context.Patients.ToList().ForEach(p => { hashTable.Put(p); });
                context.Doctors.ToList().ForEach(d => { avlTree.Add(d); });
                context.Referrals.ToList().ForEach(r => { layeredList.Insert(r); });
            }

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Регистрация нового пациента");
                Console.WriteLine("2. Удаление данных о пациенте");
                Console.WriteLine("3. Просмотр всех зарегистрированных пациентов");
                Console.WriteLine("4. Очистка всех имеющихся пациентов");
                Console.WriteLine("5. Поиск пациента по регистрационному номеру");
                Console.WriteLine("6. Поиск пациентов по ФИО");
                Console.WriteLine("7. Добавление нового врача");
                Console.WriteLine("8. Удаление данных о враче");
                Console.WriteLine("9. Просмотр всех имеющихся врачей");
                Console.WriteLine("10. Очистка всех имеющихся врачей");
                Console.WriteLine("11. Поиск врача по фамилии и инициалам");
                Console.WriteLine("12. Поиск врача по фрагментам должности");
                Console.WriteLine("13. Регистрация выдачи направления больному к врачу");
                Console.WriteLine("14. Регистрация возврата направления врачом или больным к врачу");
                Console.WriteLine("15. Просмотр всех имеющихся направлений");
                Console.WriteLine("16. Поиск направления по фамилии и инициалам врача");
                Console.WriteLine("17. Поиск направления по регистрационному номеру пациенту");
                Console.WriteLine("0. Выход");

                Console.Write("Введите номер действия: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        RegisterNewPatient();
                        break;
                    case "2":
                        RemovePatient();
                        break;
                    case "3":
                        ViewAllPatients();
                        break;
                    case "4":
                        ClearAllPatients();
                        break;
                    case "5":
                        SearchPatientByRegistrationNumber();
                        break;
                    case "6":
                        SearchPatientsByFullName();
                        break;
                    case "7":
                        AddNewDoctor();
                        break;
                    case "8":
                        RemoveDoctor();
                        break;
                    case "9":
                        ViewAllDoctors();
                        break;
                    case "10":
                        ClearAllDoctors();
                        break;
                    case "11":
                        SearchDoctorByFullName();
                        break;
                    case "12":
                        SearchDoctorByPosition();
                        break;
                    case "13":
                        RegisterReferralToDoctor();
                        break;
                    case "14":
                        RegisterReferralReturn();
                        break;
                    case "15":
                        ViewAllReferrals();
                        break;
                    case "16":
                        SearchReferralByDoctor();
                        break;
                    case "17":
                        SearchReferralByPatient();
                        break;
                    case "0":
                        Console.WriteLine("До свидания!");
                        return;
                    default:
                        Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                        break;
                }
            }
        }
        static void AddPatientToDB(Patient patient)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Patients.Add(patient);
                int res = context.SaveChanges();
                if (res > 0)
                {
                    Console.WriteLine("Пациент сохранен в БД.");
                }
                else
                {
                    Console.WriteLine("Произошла ошибка.");
                }
            }
        }
        static void AddDoctorToDB(Doctor doctor)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Doctors.Add(doctor);
                int res = context.SaveChanges();
                if (res > 0)
                {
                    Console.WriteLine("Доктор сохранен в БД.");
                }
                else
                {
                    Console.WriteLine("Произошла ошибка.");
                }
            }
        }
        static void AddReferralToDB(Referral referral)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Referrals.Add(referral);
                int res = context.SaveChanges();
                if (res > 0)
                {
                    Console.WriteLine("Направление сохранено в БД.");
                }
                else
                {
                    Console.WriteLine("Произошла ошибка.");
                }
            }
        }
        static void DeletePatientFromDB(Patient patient)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Patients.Remove(patient);
                int res = context.SaveChanges();
                if (res > 0)
                {
                    Console.WriteLine("Пациент удален из БД.");
                }
                else
                {
                    Console.WriteLine("Произошла ошибка.");
                }
            }
        }
        static void DeleteDoctorFromDB(Doctor doctor)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Doctors.Remove(doctor);
                int res = context.SaveChanges();
                if (res > 0)
                {
                    Console.WriteLine("Доктор удален из БД.");
                }
                else
                {
                    Console.WriteLine("Произошла ошибка.");
                }
            }
        }
        static void DeleteReferralFromDB(Referral referral)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Referrals.Remove(referral);
                int res = context.SaveChanges();
                if (res > 0)
                {
                    Console.WriteLine("Направление удалено из БД.");
                }
                else
                {
                    Console.WriteLine("Произошла ошибка.");
                }
            }
        }
        static void ClearAllPatientsFromDB()
        {
            using (var context = new ApplicationDbContext())
            {
                var patients = context.Patients.ToList();
                foreach (var pat in patients) 
                { 
                    context.Patients.Remove(pat);
                }
                context.SaveChanges();
            }
        }
        static void ClearAllDoctorsFromDB()
        {
            using (var context = new ApplicationDbContext())
            {
                var doctors = context.Doctors.ToList();
                foreach (var doc in doctors)
                {
                    context.Doctors.Remove(doc);
                }
                context.SaveChanges();
            }
        }
        static void ClearAllReferralsFromDB()
        {
            using (var context = new ApplicationDbContext())
            {
                var referrals = context.Referrals.ToList();
                foreach (var referr in referrals)
                {
                    context.Referrals.Remove(referr);
                }
                context.SaveChanges();
            }
        }
        static void PrintObservableCollection<T>(ObservableCollection<T> collection)
        {
            foreach (var item in collection)
            {
                Console.WriteLine(item.ToString());
            }
        }
        static void RegisterNewPatient()
        {
            Console.Write("Введите регистрационный номер пациента: ");
            string registrationNumber = Console.ReadLine();
            while (!DataValidator.ValidateRegistrationNumber(registrationNumber))
            {
                Console.WriteLine("Неверный формат, повторите ввод.");
                Console.Write("Введите регистрационный номер пациента: ");
                registrationNumber = Console.ReadLine();
            }

            Console.Write("Введите ФИО пациента: ");
            string fullName = Console.ReadLine();
            while (!DataValidator.ValidateFullName(fullName))
            {
                Console.WriteLine("Неверный формат, повторите ввод.");
                Console.Write("Введите ФИО пациента: ");
                fullName = Console.ReadLine();
            }

            Console.Write("Введите год рождения: ");
            int yearOfBirth = DataParser.ParseYearOfBirth(Console.ReadLine());
            while (yearOfBirth == -1)
            {
                Console.WriteLine("Неверный формат, повторите ввод.");
                Console.Write("Введите год рождения: ");
                yearOfBirth = DataParser.ParseYearOfBirth(Console.ReadLine());
            }

            Console.Write("Введите адрес пациента: ");
            string address = Console.ReadLine();
            while (!DataValidator.ValidateAddress(address))
            {
                Console.WriteLine("Неверный формат, повторите ввод.");
                Console.Write("Введите адрес пациента: ");
                address = Console.ReadLine();
            }

            Console.Write("Введите место работы/учёбы пациента: ");
            string workplace = Console.ReadLine();
            while (!DataValidator.ValidateWorkplace(workplace))
            {
                Console.WriteLine("Неверный формат, повторите ввод.");
                Console.Write("Введите место работы/учёбы пациента: ");
                workplace = Console.ReadLine();
            }

            Patient newPatient = new Patient
            {
                RegistrationNumber = registrationNumber,
                FullName = fullName,
                YearOfBirth = yearOfBirth,
                Address = address,
                Workplace = workplace
            };

            hashTable.Put(newPatient);
            AddPatientToDB(newPatient);

            Console.WriteLine("Пациент успешно зарегистрирован.");
        }
        static void RemovePatient()
        {
            Console.Write("Введите регистрационный номер пациента для удаления: ");
            string registrationNumber = Console.ReadLine();
            while (!DataValidator.ValidateRegistrationNumber(registrationNumber))
            {
                Console.WriteLine("Неверный формат, повторите ввод.");
                Console.Write("Введите регистрационный номер пациента для удаления: ");
                registrationNumber = Console.ReadLine();
            }

            Patient deleted = hashTable.RemovePatientByNumber(registrationNumber);
            DeletePatientFromDB(deleted);
            if (deleted != null)
            {
                Console.WriteLine("Данные о пациенте удалены.");
                Console.WriteLine(deleted.ToString());
                List<Referral> founded = layeredList.FindByPatientRegistrationNumber(registrationNumber);
                foreach (Referral referral in founded)
                {
                    layeredList.Delete(referral);
                    DeleteReferralFromDB(referral);
                }
            }
            else
            {
                Console.WriteLine("Пациента с таким номером не найдено.");
            }
        }
        static void ViewAllPatients()
        {
            Console.WriteLine("Список зарегистрированных пациентов:");
            PrintObservableCollection(hashTable.GetAllPatients());
        }
        static void ClearAllPatients()
        {
            Console.WriteLine("Очистка пациентов завершена.");
            hashTable.Clear();
            layeredList.Clear();
            ClearAllPatientsFromDB();
            ClearAllReferralsFromDB();
        }
        static void SearchPatientByRegistrationNumber()
        {
            Console.Write("Введите регистрационный номер пациента для поиска: ");
            string registrationNumber = Console.ReadLine();
            while (!DataValidator.ValidateRegistrationNumber(registrationNumber))
            {
                Console.WriteLine("Неверный формат, повторите ввод.");
                Console.Write("Введите регистрационный номер пациента для поиска: ");
                registrationNumber = Console.ReadLine();
            }

            Patient founded = hashTable.GetPatientByNumber(registrationNumber);
            if (founded != null)
            {
                Console.WriteLine("Данные о пациенте найдены.");
                Console.WriteLine(founded.ToString());
            }
            else
            {
                Console.WriteLine("Пациента с таким номером не найдено.");
            }
        }
        static void SearchPatientsByFullName()
        {
            Console.Write("Введите ФИО пациента для поиска: ");
            string fullName = Console.ReadLine();

            List<Patient> founded = hashTable.SearchByPartiallyFullName(fullName);
            if (founded != null && founded.Count > 0)
            {
                Console.WriteLine("Данные о пациентах найдены.");

                foreach (Patient patient in founded)
                {
                    Console.WriteLine(patient.ToString());
                }
            }
            else
            {
                Console.WriteLine("Пациентов с таким ФИО не найдено.");
            }
        }
        static void AddNewDoctor()
        {
            Console.Write("Введите фамилию инициалы нового врача: ");
            string surnameInitials = Console.ReadLine();
            while (!DataValidator.ValidateSurnameInitials(surnameInitials))
            {
                Console.WriteLine("Неверный формат, повторите ввод.");
                Console.Write("Введите фамилию инициалы нового врача: ");
                surnameInitials = Console.ReadLine();
            }

            Console.Write("Введите должность нового врача: ");
            string position = Console.ReadLine();
            while (!DataValidator.ValidatePosition(position))
            {
                Console.WriteLine("Неверный формат, повторите ввод.");
                Console.Write("Введите должность нового врача: ");
                position = Console.ReadLine();
            }

            Console.Write("Введите номер кабинета нового врача: ");
            int officeNumber = DataParser.ParseOfficeNumber(Console.ReadLine());
            while (officeNumber == -1)
            {
                Console.WriteLine("Неверный формат, повторите ввод.");
                Console.Write("Введите номер кабинета нового врача: ");
                officeNumber = DataParser.ParseOfficeNumber(Console.ReadLine());
            }

            Console.Write("Введите график приема нового врача: ");
            string schedule = Console.ReadLine();
            while (!DataValidator.ValidateSchedule(schedule))
            {
                Console.WriteLine("Неверный формат, повторите ввод.");
                Console.Write("Введите график приема нового врача: ");
                schedule = Console.ReadLine();
            }

            Doctor doctor = new Doctor()
            {
                SurnameInitials = surnameInitials,
                Position = position,
                OfficeNumber = officeNumber,
                Schedule = schedule
            };

            avlTree.Add(doctor);
            AddDoctorToDB(doctor);

            Console.WriteLine("Новый врач успешно добавлен.");
        }
        static void RemoveDoctor()
        {
            Console.Write("Введите фамилию инициалы врача для удаления: ");
            string surnameInitials = Console.ReadLine();
            while (!DataValidator.ValidateSurnameInitials(surnameInitials))
            {
                Console.WriteLine("Неверный формат, повторите ввод.");
                Console.Write("Введите фамилию инициалы врача для удаления: ");
                surnameInitials = Console.ReadLine();
            }

            Doctor deleted = avlTree.SearchBySurnameInitials(surnameInitials);
            avlTree.Remove(deleted);
            DeleteDoctorFromDB(deleted);
            if (deleted != null) 
            {
                Console.WriteLine("Данные о враче удалены.");
                Console.WriteLine(deleted.ToString());
                List<Referral> founded = layeredList.FindByDoctorName(surnameInitials);
                foreach (Referral referral in founded)
                {
                    layeredList.Delete(referral);
                    DeleteReferralFromDB(referral);
                }
            }
            else
            {
                Console.WriteLine("Врача с такой фамилией не найдено.");
            }
            
        }
        static void ViewAllDoctors()
        {
            Console.WriteLine("Список имеющихся врачей:");
            PrintObservableCollection(avlTree.GetAllDoctors());
        }
        static void ClearAllDoctors()
        {
            Console.WriteLine("Очистка врачей завершена.");
            avlTree.Clear();
            layeredList.Clear();
            ClearAllDoctorsFromDB();
            ClearAllReferralsFromDB();
        }
        static void SearchDoctorByFullName()
        {
            Console.Write("Введите фамилию инициалы врача для поиска: ");
            string surnameInitials = Console.ReadLine();
            while (!DataValidator.ValidateSurnameInitials(surnameInitials))
            {
                Console.WriteLine("Неверный формат, повторите ввод.");
                Console.Write("Введите фамилию инициалы врача для поиска: ");
                surnameInitials = Console.ReadLine();
            }

            Doctor founded = avlTree.SearchBySurnameInitials(surnameInitials);
            if (founded != null)
            {
                Console.WriteLine("Данные о враче найдены.");
                Console.WriteLine(founded.ToString());
            }
            else
            {
                Console.WriteLine("Врача с такой фамилией не найдено.");
            }
        }
        static void SearchDoctorByPosition()
        {
            Console.Write("Введите фрагмент должности для поиска врачей: ");
            string positionFragment = Console.ReadLine();

            List<Doctor> founded = avlTree.SearchByPartiallyPosition(positionFragment);
            if (founded != null && founded.Count > 0)
            {
                Console.WriteLine("Данные о врачах найдены:");

                foreach (Doctor doctor in founded)
                {
                    Console.WriteLine(doctor.ToString());
                }
            }
            else
            {
                Console.WriteLine("Врачей с такой должностью не найдено.");
            }
        }
        static void RegisterReferralToDoctor()
        {
            Console.Write("Введите регистрационный номер пациента: ");
            string registrationNumber = Console.ReadLine();
            while (!DataValidator.ValidateRegistrationNumber(registrationNumber) && hashTable.GetPatientByNumber(registrationNumber) != null)
            {
                if (!DataValidator.ValidateRegistrationNumber(registrationNumber))
                {
                    Console.WriteLine("Неверный формат, повторите ввод.");
                }
                else
                {
                    Console.WriteLine("Нет пациента с таким номером, повторите ввод.");
                }
                Console.Write("Введите регистрационный номер пациента: ");
                registrationNumber = Console.ReadLine();
            }

            Console.Write("Введите фамилию инициалы врача, к которому направляется пациент: ");
            string surnameInitials = Console.ReadLine();
            while (!DataValidator.ValidateSurnameInitials(surnameInitials) && avlTree.SearchBySurnameInitials(surnameInitials) != null)
            {
                if (!DataValidator.ValidateSurnameInitials(surnameInitials))
                {
                    Console.WriteLine("Неверный формат, повторите ввод.");
                }
                else
                {
                    Console.WriteLine("Нет врача с таким именем, повторите ввод.");
                }
                Console.Write("Введите фамилию инициалы врача, к которому направляется пациент: ");
                surnameInitials = Console.ReadLine();
            }
            Doctor doctor = avlTree.SearchBySurnameInitials(surnameInitials);

            Console.Write("Введите дату приема: ");
            string dateStr = Console.ReadLine();
            while (!DataValidator.ValidateDate(dateStr))
            {
                Console.WriteLine("Неверный формат, повторите ввод.");
                Console.Write("Введите дату приема: ");
                dateStr = Console.ReadLine();
            }

            Console.Write("Введите время приема: ");
            string timeStr = Console.ReadLine();
            while (!DataValidator.ValidateTime(timeStr))
            {
                Console.WriteLine("Неверный формат, повторите ввод.");
                Console.Write("Введите время приема: ");
                timeStr = Console.ReadLine();
            }

            DateTime dateTime = DataParser.ParseDateAndTime(dateStr, timeStr);

            if (!DataParser.TryParseDateTimeWithSchedule(dateTime, doctor.Schedule))
            {
                Console.WriteLine("Направление выдано пациенту.");
                Referral referral = new Referral() 
                {
                    PatientRegistrationNumber = registrationNumber,
                    DoctorSurnameInitials = surnameInitials,
                    Datetime = dateTime,
                };
                layeredList.Insert(referral);
                AddReferralToDB(referral);
            }
            else 
            {
                Console.WriteLine("В эту дату и время недоступна запись к этому врачу.");
            }
        }
        static void RegisterReferralReturn()
        {
            Console.Write("Введите регистрационный номер пациента: ");
            string registrationNumber = Console.ReadLine();
            while (!DataValidator.ValidateRegistrationNumber(registrationNumber) && hashTable.GetPatientByNumber(registrationNumber) != null)
            {
                if (!DataValidator.ValidateRegistrationNumber(registrationNumber))
                {
                    Console.WriteLine("Неверный формат, повторите ввод.");
                }
                else
                {
                    Console.WriteLine("Нет пациента с таким номером, повторите ввод.");
                }
                Console.Write("Введите регистрационный номер пациента: ");
                registrationNumber = Console.ReadLine();
            }
            List<Referral> referralList = layeredList.FindByPatientRegistrationNumber(registrationNumber);

            Console.Write("Введите фамилию инициалы врача, к которому направляется пациент: ");
            string surnameInitials = Console.ReadLine();
            while (!DataValidator.ValidateSurnameInitials(surnameInitials) && avlTree.SearchBySurnameInitials(surnameInitials) != null)
            {
                if (!DataValidator.ValidateSurnameInitials(surnameInitials))
                {
                    Console.WriteLine("Неверный формат, повторите ввод.");
                }
                else
                {
                    Console.WriteLine("Нет врача с таким именем, повторите ввод.");
                }
                Console.Write("Введите фамилию инициалы врача, к которому направляется пациент: ");
                surnameInitials = Console.ReadLine();
            }
            List<Referral> founded = new List<Referral>();
            foreach(Referral referral in referralList)
            {
                if (referral.DoctorSurnameInitials == surnameInitials)
                {
                    founded.Add(referral);
                }
            }

            if (founded.Count <= 0)
            {
                Console.WriteLine("Не найдено таких направлений.");
                return;
            }

            Console.Write("Введите дату приема: ");
            string dateStr = Console.ReadLine();
            while (!DataValidator.ValidateDate(dateStr))
            {
                Console.WriteLine("Неверный формат, повторите ввод.");
                Console.Write("Введите дату приема: ");
                dateStr = Console.ReadLine();
            }

            Console.Write("Введите время приема: ");
            string timeStr = Console.ReadLine();
            while (!DataValidator.ValidateTime(timeStr))
            {
                Console.WriteLine("Неверный формат, повторите ввод.");
                Console.Write("Введите время приема: ");
                timeStr = Console.ReadLine();
            }

            DateTime dateTime = DataParser.ParseDateAndTime(dateStr, timeStr);

            foreach (Referral referral1 in founded)
            {
                if (referral1.Datetime == dateTime)
                {
                    Console.WriteLine("Направление возвращено.");
                    layeredList.Delete(referral1);
                    DeleteReferralFromDB(referral1);
                    return;
                }
            }

            Console.WriteLine("Таких направлений не найдено.");
        }
        static void ViewAllReferrals()
        {
            Console.WriteLine("Список зарегистрированных направлений:");
            PrintObservableCollection(layeredList.GetAllReferrals());
        }
        static void SearchReferralByDoctor()
        {
            Console.Write("Введите фамилию инициалы врача: ");
            string surnameInitials = Console.ReadLine();
            while (!DataValidator.ValidateSurnameInitials(surnameInitials))
            {
                Console.WriteLine("Неверный формат, повторите ввод.");
                Console.Write("Введите фамилию инициалы врача: ");
                surnameInitials = Console.ReadLine();
            }
            List<Referral> founded = layeredList.FindByDoctorName(surnameInitials);
            if (founded != null && founded.Count > 0)
            {
                Console.WriteLine("Данные о направлениях найдены:");

                foreach (Referral referral in founded)
                {
                    Console.WriteLine(referral.ToString());
                }
            }
            else
            {
                Console.WriteLine("Направлений у этого врача не найдено.");
            }
        }
        static void SearchReferralByPatient()
        {
            Console.Write("Введите регистрационный номер пациента: ");
            string registrationNumber = Console.ReadLine();
            while (!DataValidator.ValidateRegistrationNumber(registrationNumber))
            {
                Console.WriteLine("Неверный формат, повторите ввод.");
                Console.Write("Введите регистрационный номер пациента: ");
                registrationNumber = Console.ReadLine();
            }
            List<Referral> founded = layeredList.FindByPatientRegistrationNumber(registrationNumber);
            if (founded != null && founded.Count > 0)
            {
                Console.WriteLine("Данные о направлениях найдены:");

                foreach (Referral referral in founded)
                {
                    Console.WriteLine(referral.ToString());
                }
            }
            else
            {
                Console.WriteLine("Направлений у этого пациента не найдено.");
            }
        }
    }
}
