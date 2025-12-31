using System;
using System.Globalization;

namespace Yarito.Framework
{
    public static class ShamsiDate
    {
        private static readonly PersianCalendar Pc = new ();

        private static readonly string[] DayNames =
        ["شنبه", "یکشنبه", "دوشنبه", "سه‌شنبه", "چهارشنبه", "پنجشنبه", "جمعه"];

        private static readonly string[] MonthNames =
        ["فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند"];

        extension(DateTime date)
        {
            /// <summary>
            /// تبدیل DayOfWeek میلادی به شماره روز هفته شمسی (شنبه=0 ... جمعه=6)
            /// </summary>
            public int ShamsiDayOfWeek()
                => ((int)date.DayOfWeek + 1) % 7;

            public string ShamsiDayNameOfWeek()
            {
                var dayNumber = date.ShamsiDayOfWeek();
                return dayNumber is >= 0 and <= 6 
                    ? DayNames[dayNumber] 
                    : "نامعتبر";
            }

            public string ShamsiMonthName()
            {
                var monthNumber = Pc.GetMonth(date);
                return monthNumber is >= 1 and <= 12 
                    ? MonthNames[monthNumber - 1] 
                    : "نامعتبر";
            }

            public string CompleteShamsiDate(string? format = null)
            {
                var dayName = date.ShamsiDayNameOfWeek();
                var day = Pc.GetDayOfMonth(date).ToString("00").ToPersianNum();
                var monthName = date.ShamsiMonthName();
                var monthNum = Pc.GetMonth(date).ToString("00").ToPersianNum();
                var year = Pc.GetYear(date).ToString("0000").ToPersianNum();
                var time = date.ToString("HH:mm").ToPersianNum();

                return format switch
                {
                    "dn, dd mn yyyy" => $"{dayName}، {day} {monthName} {year}",
                    "dn, dd mn yyyy - HH:MM" => $"{dayName}، {day} {monthName} {year} ساعت {time}",
                    "dd mn yyyy" => $"{day} {monthName} {year}",
                    "yyyy/mm/dd" => $"{ year}/{monthNum}/{day}",
                    "yyyy/mm/dd - HH:MM" => $"{year}/{monthNum}/{day} - {time}",
                    _ => $"{day} {monthName} {year}"
                };
            }
        }
    }
}
