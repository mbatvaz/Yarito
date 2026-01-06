using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Yarito.Framework
{
    public static class Validation
    {
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;
            var digitsOnly = new string(phoneNumber.Where(char.IsDigit).ToArray());

            if(!digitsOnly.StartsWith("09"))
                return false;

            return digitsOnly.Length == 11;
        }

        public static bool IsValidText(string text, int len)
        {
            if (string.IsNullOrWhiteSpace(text))
                return false;

            text = text.Trim();

            if (text.Length > len)
                return false;

            return true;
        }

        public static bool IsValidName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            name = name.Trim();

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

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            email = email.Trim();

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

        public static bool IsValidBaseWalletBalance(decimal amount)
        {
            if (amount < 0)
                return false;

            return decimal.Truncate(amount) == amount;
        }

        public static bool IsValidAddress(string address)
        {
            address = address.Trim();
            return address.Length <= 500;
        }

        public static bool IsValidImageUrlOrFileName(string? filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return false;

            var ext = Path.GetExtension(filePath.Trim())?.ToLowerInvariant();
            var validFormat = new List<string> { ".jpg", ".jpeg", ".png", ".webp", ".gif", ".bmp" };
            return !string.IsNullOrWhiteSpace(ext) && validFormat.Contains(ext);
        }

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
