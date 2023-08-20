using System.Globalization;

namespace PersianDatePickerMAUI.Common;

public static class CommonHelpers
{
    public static bool IsLeapYear(this int year)
    {
        var persianCal = new PersianCalendar();
        return persianCal.IsLeapYear(year);
    }
    public static string GetMonthName(this int month) =>
   month switch
   {
       1 => "فروردین",
       2 => "اردیبهشت",
       3 => "خرداد",
       4 => "تیر",
       5 => "مرداد",
       6 => "شهریور",
       7 => "مهر",
       8 => "آبان",
       9 => "آذر",
       10 => "دی",
       11 => "بهمن",
       12 => "اسفند",
       _ => "نامشخص",
   };
    public static string GetWeekName(this DayOfWeek week) =>
        week switch
        {
            DayOfWeek.Saturday => "شنبه",
            DayOfWeek.Sunday => "یکشنبه",
            DayOfWeek.Monday => "دوشنبه",
            DayOfWeek.Tuesday => "سه شنبه",
            DayOfWeek.Wednesday => "چهارشنبه",
            DayOfWeek.Thursday => "پنجشنبه",
            DayOfWeek.Friday => "جمعه",
            _ => "نامشخص"
        };
    public static int GetWeekSpan(this DayOfWeek week)
    {
        return week switch
        {
            DayOfWeek.Saturday => 0,
            DayOfWeek.Sunday => 1,
            DayOfWeek.Monday => 2,
            DayOfWeek.Tuesday => 3,
            DayOfWeek.Wednesday => 4,
            DayOfWeek.Thursday => 5,
            DayOfWeek.Friday => 6,
            _ => 0
        };
    }
    public static string NormalizePersianDate(this string persianDate)
    {
        if (string.IsNullOrWhiteSpace(persianDate))
        {
            return GetPersianDateNow();
        }
        PersianCalendar persianCalendar = new PersianCalendar();

        string[] dateParts = persianDate.Split('/');

        if (dateParts.Length != 3)
        {
            return GetPersianDateNow();
        }
        if (int.TryParse(dateParts[0], out int year) &&
            int.TryParse(dateParts[1], out int month) &&
            int.TryParse(dateParts[2], out int day))
        {
            // Check if the parsed values form a valid Persian date
            try
            {
                persianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);
                return persianDate;
            }
            catch (ArgumentOutOfRangeException)
            {
                return GetPersianDateNow();
            }
        }

        return GetPersianDateNow();
    }
    public static string GetPersianDateNow()
    {
        var date = DateTime.Now;
        var calendar = new PersianCalendar();
        var persianDate = new DateTime(calendar.GetYear(date), calendar.GetMonth(date), calendar.GetDayOfMonth(date));
        var result = persianDate.ToString("yyyy/MM/dd", new CultureInfo("fa-IR"));
        return result;
    }
}
