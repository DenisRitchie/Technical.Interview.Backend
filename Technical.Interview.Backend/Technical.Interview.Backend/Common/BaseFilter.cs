namespace Technical.Interview.Backend.Common;

public record BaseFilter(int? Page = null, int? PageSize = null, string? SortBy = null, string? OrderBy = null);
