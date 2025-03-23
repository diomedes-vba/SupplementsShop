using Microsoft.EntityFrameworkCore;
using SupplementsShop.Domain.Interfaces;
using SupplementsShop.Domain.Models;

namespace SupplementsShop.Infrastructure.Extensions;

public static class AsyncIQueryableExtensions
{
    /* getOnlyTotalCount - A value is indicating whether you want to only load the total number of records.
    Set to true if you don't want to load data from database */
    public static async Task<IPagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source, int pageIndex, int pageSize,
        bool getOnlyTotalCount = false)
    {
        if (source == null)
        {
            return new PagedList<T>(new List<T>(), pageIndex, pageSize);
        }

        pageSize = pageSize < 1 ? 1 : pageSize;
        var count = await source.CountAsync();
        
        var data = new List<T>();

        if (!getOnlyTotalCount)
        {
            data.AddRange(await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync());
        }
        
        return new PagedList<T>(data, pageIndex, pageSize, count);
    }
}