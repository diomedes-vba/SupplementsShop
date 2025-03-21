namespace SupplementsShop.Domain.Interfaces;

public interface IPagedList<T> : IList<T>
{
    int PageIndex { get; }
    int PageSize { get; }
    int TotalPages { get; }
    int TotalCount { get; }
    bool HasPreviousPage { get; }
    bool HasNextPage { get; }
}