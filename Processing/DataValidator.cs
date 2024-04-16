using System;
using System.Text.RegularExpressions;

namespace HospitalInformationSystem.Processing
{
    public static class DataValidator
    {
        public static bool ValidateRegistrationNumber(string input)
        {
            // Паттерн для полного регистрационного номера в формате "MM-NNNNNN", где MM – номер участка (цифры); NNNNNN – порядковый номер (цифры);
            string pattern = @"^\d{2}-\d{6}$";
            return Regex.IsMatch(input, pattern);
        }
        public static bool ValidateFullName(string input)
        {
            // Паттерн для полного имени кириллицей в формате "Иванов-Сидоров Иван Иванович"
            string pattern = @"^(?=.{8,64}$)[А-ЯЁа-яё-]+\s[А-ЯЁа-яё-]+(?:\s[А-ЯЁа-яё]+)?$";
            return Regex.IsMatch(input, pattern);
        }
        public static bool ValidateYearOfBirth(string input)
        {
            // Паттерн для года рождения в формате "2004" или "1991"
            string pattern = @"^(19\d{2}|20\d{2})$";
            return Regex.IsMatch(input, pattern);
        }
        public static bool ValidateAddress(string input)
        {
            // Паттерн для адреса на кириллице с допустимыми символами ".", ",", "-"
            string pattern = @"^[А-ЯЁа-яё\d\s"".,-]{1,256}$";
            return Regex.IsMatch(input, pattern);
        }
        public static bool ValidateWorkplace(string input)
        {
            // Паттерн для места работы/учёбы кириллицей, с тире, цифрами, пробелами и кавычками
            string pattern = @"^[А-ЯЁа-яё\d\s"".№,-]{1,256}$";
            return Regex.IsMatch(input, pattern);
        }
        public static bool ValidateSurnameInitials(string input)
        {
            // Паттерн Фамилии И.О. формата "Иванов-Сидоров И. И."
            string pattern = @"^(?=.{8,25}$)[А-ЯЁа-яё-]+\s[А-ЯЁа-яё]\.\s[А-ЯЁа-яё]\.$";
            return Regex.IsMatch(input, pattern);
        }
        public static bool ValidatePosition(string input)
        {
            // Паттерн позиции формата "Врач-Трансфузиолог"
            string pattern = @"^[А-ЯЁа-яё\s-]{1,64}$";
            return Regex.IsMatch(input, pattern);
        }
        public static bool ValidateOfficeNumber(string input)
        {
            // Паттерн номера офиса формата "123", "1", "90", "9625"
            string pattern = @"^[\d]{1,4}$";
            return Regex.IsMatch(input, pattern);
        }
        public static bool ValidateSchedule(string input)
        {
            // Паттерн для проверки расписания в формате "пн:20.00-22.00 вт:10.00-12.00 ср:16.00-18.00"
            string dayOfWeekPattern = @"(пн|вт|ср|чт|пт|сб|вс)";
            string timePattern = @"([01][0-9]|2[0-3])\.[0-5][0-9]";
            string timeIntervalPattern = $@"{timePattern}-{timePattern}";
            string pattern = $@"^{dayOfWeekPattern}:{timeIntervalPattern}( {dayOfWeekPattern}:{timeIntervalPattern}){{0,5}}$";
            return Regex.IsMatch(input, pattern);
        }
        public static bool ValidateDate(string input)
        {
            // Паттерн для проверки даты в формате "дд.мм.гггг"
            string pattern = @"^(\d{2})\.(\d{2})\.(\d{4})$";
            return Regex.IsMatch(input, pattern) && DateTime.TryParse(input + " 00:00", out _);
        }
        public static bool ValidateTime(string input)
        {
            // Паттерн для проверки времени в формате "чч:мм"
            string pattern = @"^([01][0-9]|2[0-3]):[0-5][0-9]$";
            return Regex.IsMatch(input, pattern);
        }
    }
}
