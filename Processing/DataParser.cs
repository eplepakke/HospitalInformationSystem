using System;
using System.Globalization;

namespace HospitalInformationSystem.Processing
{
    public class DataParser
    {
        public int ParseYearOfBirth(string input)
        {
            int year = -1;
            if (DataValidator.ValidateYearOfBirth(input))
            {
                year = int.Parse(input);
            }
            return year;
        }
        public int ParseOfficeNumber(string input)
        {
            int number = -1;
            if (DataValidator.ValidateOfficeNumber(input)) 
            { 
                number = int.Parse(input); 
            }
            return number;
        }
        public DateTime ParseDateAndTime(string inputDate, string inputTime)
        {
            CultureInfo culture = CultureInfo.CreateSpecificCulture("ru-RU");

            string dateTimeString = inputDate + " " + inputTime;
            DateTime dateTime = DateTime.MinValue;

            if (DataValidator.ValidateDate(inputDate) && DataValidator.ValidateTime(inputTime))
            {
                dateTime = DateTime.Parse(dateTimeString, culture);
            }
            return dateTime;
        }
        public bool TryParseTimeRange(string timeRangeStr, out TimeSpan startTime, out TimeSpan endTime)
        {
            startTime = TimeSpan.Zero;
            endTime = TimeSpan.Zero;

            var parts = timeRangeStr.Split('-', (char)StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2)
                return false;

            if (!TimeSpan.TryParseExact(parts[0], "h\\.mm", CultureInfo.InvariantCulture, out startTime))
                return false;

            if (!TimeSpan.TryParseExact(parts[1], "h\\.mm", CultureInfo.InvariantCulture, out endTime))
                return false;

            return true;
        }
        public bool TryParseDayOfWeek(string dayOfWeekStr, out DayOfWeek dayOfWeek)
        {
            switch(dayOfWeekStr) 
            {
                case "вс":
                    dayOfWeek = DayOfWeek.Sunday;
                    return true;
                case "пн":
                    dayOfWeek = DayOfWeek.Monday;
                    return true;
                case "вт":
                    dayOfWeek = DayOfWeek.Tuesday;
                    return true;
                case "ср":
                    dayOfWeek = DayOfWeek.Wednesday;
                    return true;
                case "чт":
                    dayOfWeek = DayOfWeek.Thursday;
                    return true;
                case "пт":
                    dayOfWeek = DayOfWeek.Friday;
                    return true;
                case "сб":
                    dayOfWeek = DayOfWeek.Saturday;
                    return true;
                default:
                    dayOfWeek = 0;
                    return false;
            }
        }
        public bool TryParseDateTimeWithSchedule(DateTime dateTime, string schedule)
        {
            // Разбиваем строку расписания на составляющие
            var daySchedules = schedule.Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);

            foreach (var daySchedule in daySchedules)
            {
                // Ожидаем формат: "день_недели:начало_времени-конец_времени"
                var parts = daySchedule.Split(':');
                if (parts.Length != 2)
                    continue;

                var dayOfWeekStr = parts[0].Trim().ToLower();
                var timeRangeStr = parts[1].Trim();

                // Пытаемся распарсить день недели
                if (!TryParseDayOfWeek(dayOfWeekStr, out DayOfWeek dayOfWeek))
                    continue;

                // Пытаемся распарсить интервал времени
                if (!TryParseTimeRange(timeRangeStr, out TimeSpan startTime, out TimeSpan endTime))
                    continue;

                // Проверяем, соответствует ли день недели и время расписанию
                if (dateTime.DayOfWeek == dayOfWeek &&
                    dateTime.TimeOfDay >= startTime &&
                    dateTime.TimeOfDay < endTime)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
