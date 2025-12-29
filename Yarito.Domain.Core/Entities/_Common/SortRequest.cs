using Yarito.Domain.Core.Enums._Common;

namespace Yarito.Domain.Core.Entities._Common
{
    public class SortRequest<T>
    {
        public T? SortBy { get; init; }
        public SortDirectionEnum Direction { get; init; } = SortDirectionEnum.Ascending;
    }
}
