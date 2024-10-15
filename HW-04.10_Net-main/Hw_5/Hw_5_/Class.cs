using System;
using static System.Console;

namespace Hw_5_CustomDate
{
    class Date
    {
        private int day;
        private int month;
        private int year;

        public int Day
        {
            get => day;
            set
            {
                if (IsValidDate(value, month, year))
                {
                    day = value;
                    WriteLine($"Day set to {day:D2}.");
                }
                else
                {
                    WriteLine($"Invalid day: {value} for month {month:D2}, year {year}.");
                }
            }
        }

        public int Month
        {
            get => month;
            set
            {
                if (IsValidDate(day, value, year))
                {
                    month = value;
                    WriteLine($"Month set to {month:D2}.");
                }
                else
                {
                    WriteLine($"Invalid month: {value} for day {day:D2}, year {year}.");
                }
            }
        }

        public int Year
        {
            get => year;
            set
            {
                if (IsValidDate(day, month, value))
                {
                    year = value;
                    WriteLine($"Year set to {year}.");
                }
                else
                {
                    WriteLine($"Invalid year: {value}. Please check again.");
                }
            }
        }

        public string DayOfWeek
        {
            get
            {
                int adjustedMonth = month < 3 ? month + 12 : month;
                int adjustedYear = month < 3 ? year - 1 : year;

                int h = (day + (13 * (adjustedMonth + 1)) / 5 + adjustedYear % 100 + 
                         (adjustedYear % 100) / 4 + (adjustedYear / 100) / 4 - 2 * (adjustedYear / 100)) % 7;

                return h switch
                {
                    0 => "Saturday",
                    1 => "Sunday",
                    2 => "Monday",
                    3 => "Tuesday",
                    4 => "Wednesday",
                    5 => "Thursday",
                    6 => "Friday",
                    _ => "Unknown day"
                };
            }
        }

        public Date() : this(1, 1, 2000)
        {
            WriteLine("Default date set to 01.01.2000.");
        }

        public Date(int day, int month, int year)
        {
            if (IsValidDate(day, month, year))
            {
                this.day = day;
                this.month = month;
                this.year = year;
                WriteLine($"New date: {day:D2}.{month:D2}.{year}");
            }
            else
            {
                this.day = 1;
                this.month = 1;
                this.year = 2000;
                WriteLine("Invalid date. Reset to default: 01.01.2000.");
            }
        }

        private bool IsValidDate(int day, int month, int year)
        {
            if (year < 1 || month < 1 || month > 12) return false;

            int[] daysInMonth = { 31, IsLeapYear(year) ? 29 : 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            return day >= 1 && day <= daysInMonth[month - 1];
        }

        private bool IsLeapYear(int year) =>
            (year % 400 == 0) || (year % 100 != 0 && year % 4 == 0);

        public int DifferenceInDays(Date other) =>
            Math.Abs(CountDays(this) - CountDays(other));

        private int CountDays(Date date)
        {
            int days = date.day;
            for (int y = 1; y < date.year; y++)
                days += IsLeapYear(y) ? 366 : 365;

            int[] daysInMonth = { 31, IsLeapYear(date.year) ? 29 : 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            for (int m = 1; m < date.month; m++)
                days += daysInMonth[m - 1];

            return days;
        }

        public void AddDays(int daysToAdd)
        {
            int totalDays = CountDays(this) + daysToAdd;
            int newYear = 1;

            while (true)
            {
                int daysInYear = IsLeapYear(newYear) ? 366 : 365;
                if (totalDays > daysInYear)
                {
                    totalDays -= daysInYear;
                    newYear++;
                }
                else break;
            }

            int[] daysInMonth = { 31, IsLeapYear(newYear) ? 29 : 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            int newMonth = 1;

            while (totalDays > daysInMonth[newMonth - 1])
            {
                totalDays -= daysInMonth[newMonth - 1];
                newMonth++;
            }

            day = totalDays;
            month = newMonth;
            year = newYear;
            WriteLine($"New date after adding {daysToAdd} days: {day:D2}.{month:D2}.{year}");
        }

        public void PrintDate() => WriteLine($"Current date: {day:D2}.{month:D2}.{year}");

        public static int operator -(Date d1, Date d2) => d1.DifferenceInDays(d2);

        public static Date operator +(Date d, int days)
        {
            Date result = new(d.day, d.month, d.year);
            result.AddDays(days);
            return result;
        }

        public static Date operator ++(Date d) => d + 1;
        public static Date operator --(Date d) => d + (-1);

        public static bool operator >(Date d1, Date d2) => d1.DifferenceInDays(d2) > 0;
        public static bool operator <(Date d1, Date d2) => d1.DifferenceInDays(d2) < 0;

        public static bool operator ==(Date d1, Date d2) =>
            d1.day == d2.day && d1.month == d2.month && d1.year == d2.year;

        public static bool operator !=(Date d1, Date d2) => !(d1 == d2);

        public override bool Equals(object obj) =>
            obj is Date date && this == date;

        public override int GetHashCode() => HashCode.Combine(day, month, year);
    }
}
