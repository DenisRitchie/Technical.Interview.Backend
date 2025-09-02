namespace Technical.Interview.Backend.Responses;

using Newtonsoft.Json;

using Technical.Interview.Backend.Common;

public class Pagination
{
    public int TotalItems { get; }

    public int TotalPages { get; }

    public int PageSize { get; }

    public int Page { get; }

    public int StartItem { get; }

    public int EndItem { get; }

    public bool HasPrevious { get; }

    public bool HasNext { get; }

    [JsonIgnore]
    public int Take { get; }

    [JsonIgnore]
    public int Skip { get; }

    [JsonConstructor]
    public Pagination(int TotalItems, int TotalPages, int PageSize, int Page, int StartItem, int EndItem, bool HasPrevious, bool HasNext)
    {
        this.PaginationSettings = new PaginationSettings(10, 1);
        this.TotalItems = TotalItems;
        this.TotalPages = TotalPages;
        this.PageSize = PageSize;
        this.Page = Page;
        this.StartItem = StartItem;
        this.EndItem = EndItem;
        this.HasPrevious = HasPrevious;
        this.HasNext = HasNext;
    }

    public Pagination(int ItemsCount, BaseFilter BaseFilter)
        : this(new PaginationSettings(10, 100), ItemsCount, BaseFilter.PageSize, BaseFilter.Page)
    {
    }

    public Pagination(PaginationSettings PaginationSettings, int ItemsCount, BaseFilter BaseFilter)
        : this(PaginationSettings, ItemsCount, BaseFilter.PageSize, BaseFilter.Page)
    {
    }

    public Pagination(PaginationSettings PaginationSettings, int ItemsCount, int? pageSize, int? page)
    {
        this.PaginationSettings = PaginationSettings;

        // The order of actions is important
        TotalItems = GetHandledTotalItems(ItemsCount);
        PageSize = GetHandledPageSize(pageSize);
        TotalPages = GetHandledTotalPages();
        Page = GetHandledPage(page);

        HasNext = Page != TotalPages;
        HasPrevious = Page != 1;

        StartItem = TotalItems == 0 ? 0 : PageSize * (Page - 1) + 1;
        EndItem = PageSize * Page > TotalItems ? TotalItems : PageSize * Page;

        Take = PageSize;
        Skip = PageSize * (Page - 1);
    }

    private static int GetHandledTotalItems(int ItemsCount)
    {
        return ItemsCount < 0 ? 0 : ItemsCount;
    }

    private int GetHandledPageSize(int? PageSize)
    {
        if (PageSize.GetValueOrDefault() <= 0) return PaginationSettings.DefaultPageSize;

        if (PageSize > PaginationSettings.DefaultPageSizeLimit) return PaginationSettings.DefaultPageSizeLimit;

        return PageSize.GetValueOrDefault();
    }

    private int GetHandledTotalPages()
    {
        return TotalItems == 0 ? 1 : (int)Math.Ceiling((decimal)TotalItems / GetHandledPageSize(PageSize));
    }

    private int GetHandledPage(int? Page)
    {
        if (Page.GetValueOrDefault() <= 0) return PaginationSettings.DefaultPage;

        if (Page.GetValueOrDefault() > TotalPages) return TotalPages;

        return Page.GetValueOrDefault();
    }

    private readonly PaginationSettings PaginationSettings;
}