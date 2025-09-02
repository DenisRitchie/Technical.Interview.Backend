namespace Technical.Interview.Backend.Services;

using Ardalis.Specification;

using AutomaticInterface;

using Technical.Interview.Backend.Common;
using Technical.Interview.Backend.Data;
using Technical.Interview.Backend.Entities;
using Technical.Interview.Backend.Extensions;
using Technical.Interview.Backend.Responses;

[GenerateAutomaticInterface]
public class BrandService(InterviewContext Database, ISingleResultSpecification<MarcasAuto> SingleSpecification) : IBrandService
{
    public ValueTask<PagedResponse<BrandInfo>> FetchBrandsAsync(BrandFilter Filter)
    {
        SingleSpecification.Query
            .ById(Filter.Id)
            .ByName(Filter.Name)
            .ByOriginCountry(Filter.OriginCountry)
            .ByWebsite(Filter.Website)
            .ApplyOrdering();

        return Database.BrandReadRepository.ProjectToListAsync<BrandInfo>(SingleSpecification, Filter, default);
    }
}
