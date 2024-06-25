using System;
using Services.Helpers;
using Services.Helpers.Interfaces;

namespace Services.Helpers
{
    public class DateTimeHelperService : IDateTimeHelperService
    {
        public string ConvertBDDateStringToDateTimeObject(string dateStringBDFormat)
        {
            string convertedDate = string.Empty;
            int month = 0;
            int day = 0;
            int year = 0;
            var currentTime = DateTime.Now;
            convertedDate = new DateTime(Convert.ToInt32(currentTime.Year), Convert.ToInt32(currentTime.Month), Convert.ToInt32(currentTime.Day)).ToString("dd/MM/yyyy");
            if (dateStringBDFormat != null && dateStringBDFormat != "")
            {
                string[] splitData = dateStringBDFormat.Split('/', '-');
                if (splitData != null && splitData.Length > 1)
                {
                    day = int.Parse(splitData[0]);
                    month = int.Parse(splitData[1]);
                    if (splitData[2].Contains(" "))
                    {
                        string[] splitYearTime = splitData[2] != null ? splitData[2].Split(' ') : new string[] { "0" };
                        if (splitYearTime != null && splitYearTime.Length > 1)
                            year = int.Parse(splitYearTime[0]);
                    }
                    else
                    {
                        year = int.Parse(splitData[2]);
                    }

                }
                else
                {
                    day = int.Parse(dateStringBDFormat.Split('-')[0]);
                    month = int.Parse(dateStringBDFormat.Split('-')[1]);
                    if (splitData[2].Contains(" "))
                    {
                        string[] splitYearTime = splitData[2] != null ? splitData[2].Split(' ') : new string[] { "0" };
                        if (splitYearTime != null && splitYearTime.Length > 1)
                            year = int.Parse(splitYearTime[0]);
                    }
                    else
                    {
                        year = int.Parse(splitData[2]);
                    }
                }
                DateTime newDate = new DateTime(year, month, day);
                convertedDate = newDate.ToString();
            }
            return convertedDate;
        }

        public string ConvertUSDateStringToDateTimeObject(string dateStringUKFormat)
        {
            string convertedDate = string.Empty;
            int month = 0;
            int day = 0;
            int year = 0;
            var currentTime = DateTime.Now;
            convertedDate = new DateTime(Convert.ToInt32(currentTime.Year), Convert.ToInt32(currentTime.Month), Convert.ToInt32(currentTime.Day)).ToString("dd/MM/yyyy");
            if (dateStringUKFormat != null && dateStringUKFormat != "")
            {
                string[] splitData = dateStringUKFormat.Split('/', '-');
                if (splitData != null && splitData.Length > 1)
                {
                    month = int.Parse(splitData[0]);
                    day = int.Parse(splitData[1]);
                    if (splitData[2].Contains(" "))
                    {
                        string[] splitYearTime = splitData[2] != null ? splitData[2].Split(' ') : new string[] { "0" };
                        if (splitYearTime != null && splitYearTime.Length > 1)
                            year = int.Parse(splitYearTime[0]);
                    }
                    else
                    {
                        year = int.Parse(splitData[2]);
                    }

                }
                else
                {
                    month = int.Parse(dateStringUKFormat.Split('-')[1]);
                    day = int.Parse(dateStringUKFormat.Split('-')[0]);

                    if (splitData[2].Contains(" "))
                    {
                        string[] splitYearTime = splitData[2] != null ? splitData[2].Split(' ') : new string[] { "0" };
                        if (splitYearTime != null && splitYearTime.Length > 1)
                            year = int.Parse(splitYearTime[0]);
                    }
                    else
                    {
                        year = int.Parse(splitData[2]);
                    }
                }
                DateTime newDate = new DateTime(year, month, day);
                convertedDate = newDate.ToString();
            }
            return convertedDate;
        }

        public DateTime Now()
        {
            TimeZoneInfo tst = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
            var offset = new DateTimeOffset(DateTime.UtcNow);
            return offset.AddHours(6).DateTime;
        }

        public DateTime Today()
        {
            TimeZoneInfo tst = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
            var offset = new DateTimeOffset(DateTime.UtcNow);
            return offset.AddHours(6).DateTime.Date;
        }

        public DateTime FirstDayOfCurrentMonth()
        {
            var now = Now();
            return new DateTime(now.Year, now.Month, 1);
        }

        public DateTime LastDayOfCurrentMonth()
        {
            var now = Now();
            return new DateTime(now.Year, now.Month, 1).AddMonths(1).AddMilliseconds(-1);
        }

        public int CurrentMonthNoOfDays()
        {
            return DateTime.DaysInMonth(Today().Year, Today().Month); ;
        }

        public int GetNoOfDaysInMonthForGivenDate(DateTime date)
        {
            return DateTime.DaysInMonth(date.Year, date.Month); ;
        }
    }
}
