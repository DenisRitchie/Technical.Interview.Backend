namespace Technical.Interview.Backend.Responses;

public class PagedResponse<T>(List<T> data, Pagination pagination)
{
    public Pagination Pagination { get; } = pagination;

    public List<T> Data { get; } = data;
}