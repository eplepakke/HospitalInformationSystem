using NUnit.Framework;
using HospitalInformationSystem.Processing;

namespace HospitalInformationSystem.Tests
{
    [TestFixture]
    public class DataValidatorTests
    {
        [TestCase("12-345678", ExpectedResult = true)]
        [TestCase("AB-123456", ExpectedResult = false)]
        [TestCase("12-34567", ExpectedResult = false)]
        [TestCase("123-456789", ExpectedResult = false)]
        public bool ValidateRegistrationNumber_ValidatesCorrectly(string input)
        {
            return DataValidator.ValidateRegistrationNumber(input);
        }

        [TestCase("Иванов Иван Иванович", ExpectedResult = true)]
        [TestCase("Петров-Сидоров Петр", ExpectedResult = true)]
        [TestCase("Сидоров", ExpectedResult = false)]
        [TestCase("Иванов Петр Сергеевич", ExpectedResult = true)]
        [TestCase("Иванов-Петров Иван", ExpectedResult = true)]
        [TestCase("Иванов-Петров Иван Сергеевич", ExpectedResult = true)]
        public bool ValidateFullName_ValidatesCorrectly(string input)
        {
            return DataValidator.ValidateFullName(input);
        }

        [TestCase("2000", ExpectedResult = true)]
        [TestCase("1995", ExpectedResult = true)]
        [TestCase("2022", ExpectedResult = true)]
        [TestCase("99", ExpectedResult = false)]
        [TestCase("20000", ExpectedResult = false)]
        [TestCase("20222", ExpectedResult = false)]
        public bool ValidateYearOfBirth_ValidatesCorrectly(string input)
        {
            return DataValidator.ValidateYearOfBirth(input);
        }

        [TestCase("ул. Пушкина, д. 10, кв. 25", ExpectedResult = true)]
        [TestCase("Московский пр-т, 123", ExpectedResult = true)]
        [TestCase("Санкт-Петербург, Невский проспект, д. 1", ExpectedResult = true)]
        [TestCase("ул. Ленина", ExpectedResult = true)]
        [TestCase("ул. Гагарина, 5", ExpectedResult = true)]
        [TestCase("ул. 50-летия Октября, 100, кв. 1", ExpectedResult = true)]
        [TestCase("пр-т Ленина, 100, офис 25", ExpectedResult = true)]
        [TestCase("Сидоровка", ExpectedResult = true)]
        public bool ValidateAddress_ValidatesCorrectly(string input)
        {
            return DataValidator.ValidateAddress(input);
        }

        [TestCase("Госпиталь №1", ExpectedResult = true)]
        [TestCase("Университет им. Петра Великого", ExpectedResult = true)]
        [TestCase("Клиника доктора Сидорова", ExpectedResult = true)]
        [TestCase("Поликлиника 25", ExpectedResult = true)]
        [TestCase("Школа №5", ExpectedResult = true)]
        [TestCase("Институт педиатрии", ExpectedResult = true)]
        public bool ValidateWorkplace_ValidatesCorrectly(string input)
        {
            return DataValidator.ValidateWorkplace(input);
        }

        [TestCase("Иванов И. И.", ExpectedResult = true)]
        [TestCase("Петров-Сидоров П. С.", ExpectedResult = true)]
        [TestCase("Иванов И.И.", ExpectedResult = false)]
        [TestCase("Сидоров Петр Сергеевич", ExpectedResult = false)]
        [TestCase("Иванов-Петров Иван", ExpectedResult = false)]
        public bool ValidateSurnameInitials_ValidatesCorrectly(string input)
        {
            return DataValidator.ValidateSurnameInitials(input);
        }

        [TestCase("Врач-терапевт", ExpectedResult = true)]
        [TestCase("Хирург", ExpectedResult = true)]
        [TestCase("Главный врач", ExpectedResult = true)]
        [TestCase("Медсестра", ExpectedResult = true)]
        [TestCase("Педиатр-неонатолог", ExpectedResult = true)]
        [TestCase("Стоматолог ортопед", ExpectedResult = true)]
        [TestCase("Операционная медсестра", ExpectedResult = true)]
        [TestCase("Доктор", ExpectedResult = true)]
        [TestCase("Профессор-доктор медицинских наук", ExpectedResult = true)]
        [TestCase("Иванов И.И.", ExpectedResult = false)]
        public bool ValidatePosition_ValidatesCorrectly(string input)
        {
            return DataValidator.ValidatePosition(input);
        }

        [TestCase("123", ExpectedResult = true)]
        [TestCase("1", ExpectedResult = true)]
        [TestCase("90", ExpectedResult = true)]
        [TestCase("9625", ExpectedResult = true)]
        [TestCase("12345", ExpectedResult = false)]
        [TestCase("abc", ExpectedResult = false)]
        [TestCase("12-34", ExpectedResult = false)]
        public bool ValidateOfficeNumber_ValidatesCorrectly(string input)
        {
            return DataValidator.ValidateOfficeNumber(input);
        }

        [TestCase("пн:20.00-22.00 вт:10.00-12.00 ср:16.00-18.00", ExpectedResult = true)]
        [TestCase("пн:20.00-22.00", ExpectedResult = true)]
        [TestCase("пн:20.00-22.00 вт:10.00-12.00 ср:16.00-18.00 пт:09.00-11.00 сб:14.00-16.00", ExpectedResult = true)]
        [TestCase("пн:20.00-25.00 вт:10.00-12.00 ср:16.00-18.00", ExpectedResult = false)] // Некорректное время
        [TestCase("пн:20.00-22.00 вт:10.00-12.00 ср:16.00-18.00 пт:09.00-11.00 сб:14.00", ExpectedResult = false)] // Некорректное время
        [TestCase("пн:20.00-22.00 вт:10.00", ExpectedResult = false)] // Недостаточное количество дней
        public bool ValidateSchedule_ValidatesCorrectly(string input)
        {
            return DataValidator.ValidateSchedule(input);
        }

        [TestCase("01.01.2022", ExpectedResult = true)]
        [TestCase("31.12.2022", ExpectedResult = true)]
        [TestCase("29.02.2024", ExpectedResult = true)] // Високосный год
        [TestCase("29.02.2021", ExpectedResult = false)] // Невисокосный год
        [TestCase("32.01.2022", ExpectedResult = false)] // Некорректный день
        [TestCase("01.13.2022", ExpectedResult = false)] // Некорректный месяц
        [TestCase("1.1.2022", ExpectedResult = false)] // Некорректный формат
        public bool ValidateDate_ValidatesCorrectly(string input)
        {
            return DataValidator.ValidateDate(input);
        }

        [TestCase("10:30", ExpectedResult = true)]
        [TestCase("23:59", ExpectedResult = true)]
        [TestCase("00:00", ExpectedResult = true)]
        [TestCase("25:00", ExpectedResult = false)] // Некорректные часы
        [TestCase("12:60", ExpectedResult = false)] // Некорректные минуты
        [TestCase("9:5", ExpectedResult = false)] // Некорректный формат
        public bool ValidateTime_ValidatesCorrectly(string input)
        {
            return DataValidator.ValidateTime(input);
        }
    }
}
