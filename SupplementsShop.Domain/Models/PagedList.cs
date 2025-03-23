using SupplementsShop.Domain.Interfaces;

namespace SupplementsShop.Domain.Models;

[Serializable]
public partial class PagedList<T> : List<T>, IPagedList<T>
{
    public int PageIndex { get; }
    public int PageSize { get; }
    public int TotalPages { get;  }
    public int TotalCount { get; }
    public bool HasPreviousPage => PageIndex > 0;
    public bool HasNextPage => PageIndex + 1 < TotalCount;
    
    public PagedList(IList<T> source, int pageIndex, int pageSize, int? totalCount = null)
    {
        // Min page size is 1
        pageSize = Math.Max(1, pageSize);
        
        TotalCount = totalCount ?? source.Count;
        TotalPages = TotalCount / pageSize;
        if (TotalCount % pageSize > 0)
        {
            TotalPages++;
        }
        
        PageIndex = pageIndex;
        PageSize = pageSize;
        
        AddRange(source);
    }
}