namespace Technical.Interview.Backend.Common;

public record BrandFilter(string? Id = null, string? Name = null, string? OriginCountry = null, string? Website = null) : BaseFilter;