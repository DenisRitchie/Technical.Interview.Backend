namespace Technical.Interview.Backend.Responses;

public record PagedResponse<T>
{
    public required Pagination Pagination { get; init; }

    public required List<T> Data { get; init; }
}