using NUnit.Framework;
using HospitalInformationSystem.Processing;
using System;
using NUnit.Framework.Legacy;
using NUnit.Framework.Constraints;

namespace HospitalInformationSystem.Tests
{
    [TestFixture]
    public class DataParserTests
    {
        [TestCase("1990", 1990)]
        [TestCase("2000", 2000)]
        [TestCase("invalid", -1)]
        [TestCase("", -1)]
        public void ParseYearOfBirth_ValidAndInvalidInputs_ReturnsExpectedResult(string input, int expectedResult)
        {
            int year = DataParser.ParseYearOfBirth(input);

            ClassicAssert.AreEqual(expectedResult, year);
        }

        [TestCase("123", 123)]
        [TestCase("456", 456)]
        [TestCase("001", 1)]
        [TestCase("invalid", -1)]
        [TestCase("", -1)]
        public void ParseOfficeNumber_ValidAndInvalidInputs_ReturnsExpectedResult(string input, int expectedResult)
        {
            int number = DataParser.ParseOfficeNumber(input);

            ClassicAssert.AreEqual(expectedResult, number);
        }

        [TestCase("20.04.2024", "10:30", 2024, 4, 20, 10, 30)]
        [TestCase("29.02.2024", "20:31", 2024, 2, 29, 20, 31)]
        [TestCase("29.02.2023", "20:31", 1, 1, 1, 0, 0)]
        [TestCase("invalid", "10:30", 1, 1, 1, 0, 0)] 
        [TestCase("20.04.2024", "invalid", 1, 1, 1, 0, 0)]
        public void ParseDateAndTime_ValidAndInvalidInputs_ReturnsExpectedDateTime(string inputDate, string inputTime, int year, int month, int day, int hour, int minute)
        {
            DateTime dateTime = DataParser.ParseDateAndTime(inputDate, inputTime);

            ClassicAssert.AreEqual(new DateTime(year, month, day, hour, minute, 0), dateTime);
        }

        [TestCase("20.00-22.00", true, 20, 0, 22, 0)]
        [TestCase("09.12-12.23", true, 9, 12, 12, 23)]
        [TestCase("0.00-0.00", true, 0, 0, 0, 0)]
        [TestCase("20:00-22:00", false, 0, 0, 0, 0)]
        [TestCase("aa.00-22.00", false, 0, 0, 0, 0)]
        [TestCase("20.00-24.00", false, 20, 0, 0, 0)] // Некорректный формат времени (24.00)
        public void ParseDateAndTime_ValidAndInvalidInputs_TestTryParseTimeRange(string timeRangeStr, bool expectedResult, int startHours, int startMinutes, int endHours, int endMinutes)
        {
            TimeSpan startTime, endTime;
            TimeSpan startExpected = new TimeSpan(startHours, startMinutes, 0);
            TimeSpan endExpected = new TimeSpan(endHours, endMinutes, 0);

            bool result = DataParser.TryParseTimeRange(timeRangeStr, out startTime, out endTime);

            ClassicAssert.AreEqual(expectedResult, result);
            ClassicAssert.AreEqual(startExpected, startTime);
            ClassicAssert.AreEqual(endExpected, endTime);
        }

        [TestCase("пн", true, DayOfWeek.Monday)]
        [TestCase("вт", true, DayOfWeek.Tuesday)]
        [TestCase("ср", true, DayOfWeek.Wednesday)]
        [TestCase("ыаыа", false, 0)]
        [TestCase("123", false, 0)]
        [TestCase("0", false, 0)] 
        public void ParseDateAndTime_ValidAndInvalidInputs_TestTryParseDayOfWeek(string dayOfWeekStr, bool expectedResult, DayOfWeek dayOfWeekExpected)
        {
            DayOfWeek dayOfWeek;

            bool result = DataParser.TryParseDayOfWeek(dayOfWeekStr, out dayOfWeek);

            ClassicAssert.AreEqual(expectedResult, result);
            ClassicAssert.AreEqual(dayOfWeekExpected, dayOfWeek);
        }

        [TestCase("2024-04-22T20:30:00", "пн:20.00-22.00", ExpectedResult = true)]
        [TestCase("2024-04-23T20:10:00", "пн:20.00-22.00", ExpectedResult = false)]
        [TestCase("2024-04-24T16:10:00", "пн:20.00-22.00 вт:10.00-12.00 ср:16.00-18.00", ExpectedResult = true)]
        public bool ParseDateAndTime_ValidAndInvalidInputs_TestTryParseDateTimeWithSchedule(string dateTimeStr, string schedule)
        {
            DateTime dateTimeToCheck = DateTime.Parse(dateTimeStr);

            bool result = DataParser.TryParseDateTimeWithSchedule(dateTimeToCheck, schedule);

            return result;
        }
    }
}
