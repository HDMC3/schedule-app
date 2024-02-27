using Application.Wrappers;
using Microsoft.EntityFrameworkCore;

namespace ProductsApp.Data.Extensions;

public static class PagingExtension
{
    public static async Task<DataCollection<T>> GetPagedAsync<T>(this IQueryable<T> query, int page, int take)
    {
        // var offset = page - 1 > 0 ? (page - 1) * take : 0;

        // var result = await query
        //     .Skip(offset)
        //     .Take(take)
        //     .ToListAsync();

        // return result;

        var offset = page - 1 > 0 ? (page - 1) * take : 0;

        var result = new DataCollection<T>
        {
            Items = await query.Skip(offset).Take(take).ToListAsync(),
            Total = await query.CountAsync(),
            Page = page
        };

        if (result.Total > 0)
        {
            result.Pages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(result.Total) / take));
        }

        return result;
    }
}