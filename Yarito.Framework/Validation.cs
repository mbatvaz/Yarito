using Microsoft.Identity.Client;

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
    }
}
