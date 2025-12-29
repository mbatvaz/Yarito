namespace Yarito.Domain.Core.Entities._Common
{
    public class PageRequest
    {
        private const int MaxPageSize = 100;
        private readonly int _basePageSize = 10;

        public int Page { get; init; } = 1;

        public int PageSize
        {
            get => _basePageSize;
            init => _basePageSize = value <= 0 
                ? 10 
                : (value > MaxPageSize 
                    ? MaxPageSize 
                    : value);
        }
    }
}
