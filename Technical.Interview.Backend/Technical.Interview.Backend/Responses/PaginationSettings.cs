namespace Technical.Interview.Backend.Responses;

public class PaginationSettings(int PageSize, int PageSizeLimit)
{
    public int DefaultPage { get; } = 1;

    public int DefaultPageSize { get; } = PageSize;

    public int DefaultPageSizeLimit { get; } = PageSizeLimit;
}