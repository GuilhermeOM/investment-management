namespace InvestmentManagement.Client.API.Repositories.Helper;

using Microsoft.EntityFrameworkCore;

public class PagedList<T>
{
    private PagedList(List<T> items, int page, int pageSize, int totalCount)
    {
        this.Items = items;
        this.Page = page;
        this.PageSize = pageSize;
        this.TotalCount = totalCount;
    }

    public List<T> Items { get; }
    public int Page { get; }
    public int PageSize { get; }
    public int TotalCount { get; }
    public bool HasNextPage => this.Page * this.PageSize < this.TotalCount;
    public bool HasPreviousPage => this.PageSize > 1;

    public static async Task<PagedList<T>> CreateAsync(IQueryable<T> query, int page, int pageSize)
    {
        page = page < 1 ? 1 : page;
        pageSize = pageSize < 0 ? 0 : pageSize;

        var totalCount = await query.CountAsync();
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return new(items, page, pageSize, totalCount);
    }
}
