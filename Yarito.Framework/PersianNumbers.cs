namespace Yarito.Framework
{
    public static class PersianNumbers
    {
        public static string ToPersianNum(this string? input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            var persianDigits = new[] { '۰', '۱', '۲', '۳', '۴', '۵', '۶', '۷', '۸', '۹' };
            return string.Concat(input.Select(c => char.IsDigit(c)
                ? persianDigits[c - '0']
                : c));
        }
    }
}
