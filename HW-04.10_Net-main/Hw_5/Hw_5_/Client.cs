using System;
using static System.Console;
using Hw_5_CustomDate;

class Hw_5_Client
{
    static void Main()
    {
        try
        {
            Date date1 = new Date();
            WriteLine("Дата по умолчанию:");
            date1.PrintDate();
            WriteLine($"День недели: {date1.Day_Of_Week}");

            WriteLine("\nВведите дату (день месяц год) вводить отдельно:");
            int day = int.Parse(ReadLine());
            int month = int.Parse(ReadLine());
            int year = int.Parse(ReadLine());

            Date date2 = new Date(day, month, year);
            WriteLine("Введенная дата:");
            date2.PrintDate();
            WriteLine($"День недели: {date2.Day_Of_Week}");

            int difference = date1 - date2;
            WriteLine($"\nРазница между датами в днях: {difference}");

            WriteLine("\nВведите количество дней для изменения даты:");
            int daysToAdd = int.Parse(ReadLine());
            date2 += daysToAdd;
            WriteLine($"Новая дата после изменения на {daysToAdd} дней:");
            date2.PrintDate();
            WriteLine($"День недели: {date2.Day_Of_Week}");

            WriteLine("\nИспользование оператора '++' для добавления одного дня к дате:");
            date2++;
            date2.PrintDate();
            WriteLine($"День недели: {date2.Day_Of_Week}");

            WriteLine("\nИспользование оператора '--' для вычитания одного дня из даты:");
            date2--;
            date2.PrintDate();
            WriteLine($"День недели: {date2.Day_Of_Week}");

            WriteLine("\nСравнение дат:");
            Date date3 = new Date(15, 8, 2025);
            WriteLine("Дата 1:");
            date1.PrintDate();
            WriteLine("Дата 2:");
            date2.PrintDate();
            WriteLine("Дата 3:");
            date3.PrintDate();

            if (date1 > date2)
            {
                WriteLine("Дата 1 больше Даты 2");
            }
            else
            {
                WriteLine("Дата 1 меньше или равна Дате 2");
            }

            if (date1 < date3)
            {
                WriteLine("Дата 1 меньше Даты 3");
            }

            if (date1 == date2)
            {
                WriteLine("Дата 1 идентична Дате 2");
            }
            else
            {
                WriteLine("Дата 1 не идентична Дате 2");
            }

            if (date1 != date2)
            {
                WriteLine("Дата 1 не равна Дате 2");
            }
            else
            {
                WriteLine("Дата 1 равна Дате 2");
            }
        }
        catch (Exception ex)
        {
            WriteLine("Ошибка: " + ex.Message);
        }
    }
}
