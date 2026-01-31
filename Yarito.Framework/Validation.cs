using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Yarito.Framework
{
    public static class Validation
    {
        /// <summary>
        /// درستی ساختار شماره موبایل را بررسی میکند
        /// شماره موبایل نباید null باشد
        /// باید با 09 شروع شود و طول آن دقیقا 11 کاراکتر باشد
        /// </summary>
        /// <param name="phoneNumber">شماره موبایل  وارد شده</param>
        /// <returns>مقدار True در صورتی درستی ساختار شماره موبایل</returns>
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;
            var digitsOnly = new string(phoneNumber.Where(char.IsDigit).ToArray());

            if(!digitsOnly.StartsWith("09"))
                return false;

            return digitsOnly.Length == 11;
        }

        /// <summary>
        /// درستی ساختار متن های را بررسی میکند
        /// مقدار متن نباید خالی یا NULL باشد
        /// و پس از حذف فضاهای خالی اضافی طول آن نباید از طول تعریف شده بیشتر شود
        /// </summary>
        /// <param name="text">متن مورد نظر</param>
        /// <param name="len">حداکثر طول مجاز</param>
        /// <returns>مقدار true در صورتی که درستی ساختار متن</returns>
        public static bool IsValidText(string text, int len)
        {
            if (string.IsNullOrWhiteSpace(text))
                return false;

            if (text.Length > len)
                return false;

            return true;
        }

        /// <summary>
        /// اعتبار سنجی ساختار نام ها
        /// نام باید بعد از حذف فضا های خالی اضافی حداکثر 50 کاراکتر باشد
        /// و فقط ساختار حروفات فارسی و انگلیسی مجاز است
        /// </summary>
        /// <param name="name">نام مورد نظر</param>
        /// <returns>مقدار True در صورتی که نام ساختار صحیحی داشته باشد</returns>
        public static bool IsValidName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            if (name.Length > 50)
                return false;

            foreach (var ch in name)
            {
                switch (ch)
                {
                    case ' ':
                    case >= '\u0600' and <= '\u06FF':
                    case >= '\u0750' and <= '\u077F':
                    case >= '\u08A0' and <= '\u08FF':
                    case >= 'A' and <= 'Z':
                    case >= 'a' and <= 'z':
                        continue;
                    default:
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// اعتبار ستجی ساختار ایمیل ها
        /// ایمیل پس از حذف فاصله های اضافی ابتدا و انتها باید نباید هیچ فاصله ای در ساختار خود داشته باشد
        /// و حالت استاندارد یک ایمیل را داشته باشد
        /// </summary>
        /// <param name="email">ایمیل وارد شده</param>
        /// <returns>مقدار True در صورتی که ساختار ایمیل صحیح باشد</returns>
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            if (email.Contains(' '))
                return false;

            try
            {
                var addr = new MailAddress(email);
                return string.Equals(addr.Address, email, StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// مقدار اولیه موجودی حساب را اعتبار سنجی میکند
        /// مقدار حساب باید عددی صحیح و بیشتر از 0 باشد 
        /// </summary>
        /// <param name="amount">موجودی اولیه حساب</param>
        /// <returns>مقدار True در صورت صحیح بودن موجودی اولیه</returns>
        public static bool IsValidBaseWalletBalance(decimal amount)
        {
            if (amount < 0)
                return false;

            return decimal.Truncate(amount) == amount;
        }

        /// <summary>
        /// اعتبار سنجی درستی آدرس وارد شده
        /// در صورتی که ادرس صحیح است که طول آن کمتر از 500 کاراکتر باشد
        /// </summary>
        /// <param name="address">آدرس وارد شده</param>
        /// <returns>مقدار True در صورت صحیح بودم آدرس وارد شده</returns>
        public static bool IsValidAddress(string address)
        {
            address = address.Trim();
            return address.Length <= 500;
        }

        /// <summary>
        /// اعتبار سنجی فرمت تصاویر وارد شده
        /// فقط فرمت های زیر معتبر است
        /// ".jpg", ".jpeg", ".png", ".webp", ".gif", ".bmp" 
        /// </summary>
        /// <param name="filePath">آدرس تصویر</param>
        /// <returns>مقدار True در صورت درستی مقدار ووارد شده</returns>
        public static bool IsValidImageUrlOrFileName(string? filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return false;

            var ext = Path.GetExtension(filePath.Trim())?.ToLowerInvariant();
            var validFormat = new List<string> { ".jpg", ".jpeg", ".png", ".webp", ".gif", ".bmp" };
            return !string.IsNullOrWhiteSpace(ext) && validFormat.Contains(ext);
        }

        /// <summary>
        /// استاندارد سازی متن وارد شده
        /// متن وارد شده نباید خالی یا null باشد
        /// در صورت داشتن فضای خالی اصلاح شده و عبارات فارسی اصلاح میگردد
        /// </summary>
        /// <param name="input">متن ورودی</param>
        /// <returns>مثدار متن نرمال سازی شده</returns>
        public static string NormalizeText(string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            var s = input.Trim();
            s = Regex.Replace(s, @"\s+", " ");

            s = s.Replace('ي', 'ی')
                .Replace('ك', 'ک')
                .Replace('\u200C', ' ');

            return s;
        }

        //public static bool IsValidProfileImage(Stream imageStream)
        //{
        //    if (!imageStream.CanRead)
        //        return false;

        //    if (!imageStream.CanSeek)
        //        return false;

        //    var pos = imageStream.Position;
        //    try
        //    {
        //        return IsAllowedImageStreamInternal(imageStream);
        //    }
        //    finally
        //    {
        //        imageStream.Position = pos;
        //    }
        //}

        //private static bool IsAllowedImageStreamInternal(Stream s)
        //{
        //    // حداقل 12 بایت برای webp لازم داریم
        //    Span<byte> header = stackalloc byte[12];
        //    int read = ReadExactlyUpTo(s, header);

        //    if (read < 2) return false;

        //    // JPEG: FF D8 FF
        //    if (read >= 3 && header[0] == 0xFF && header[1] == 0xD8 && header[2] == 0xFF)
        //        return true;

        //    // PNG: 89 50 4E 47 0D 0A 1A 0A
        //    if (read >= 8 &&
        //        header[0] == 0x89 && header[1] == 0x50 && header[2] == 0x4E && header[3] == 0x47 &&
        //        header[4] == 0x0D && header[5] == 0x0A && header[6] == 0x1A && header[7] == 0x0A)
        //        return true;

        //    // GIF: "GIF87a" or "GIF89a"
        //    if (read >= 6 &&
        //        header[0] == (byte)'G' && header[1] == (byte)'I' && header[2] == (byte)'F' &&
        //        header[3] == (byte)'8' && (header[4] == (byte)'7' || header[4] == (byte)'9') &&
        //        header[5] == (byte)'a')
        //        return true;

        //    // BMP: "BM"
        //    if (header[0] == (byte)'B' && header[1] == (byte)'M')
        //        return true;

        //    // WEBP: "RIFF" .... "WEBP" (offset 8)
        //    if (read >= 12 &&
        //        header[0] == (byte)'R' && header[1] == (byte)'I' && header[2] == (byte)'F' && header[3] == (byte)'F' &&
        //        header[8] == (byte)'W' && header[9] == (byte)'E' && header[10] == (byte)'B' && header[11] == (byte)'P')
        //        return true;

        //    return false;
        //}

        //private static int ReadExactlyUpTo(Stream s, Span<byte> buffer)
        //{
        //    int total = 0;
        //    while (total < buffer.Length)
        //    {
        //        int n = s.Read(buffer.Slice(total));
        //        if (n <= 0) break;
        //        total += n;
        //    }
        //    return total;
        //}

    }
}
