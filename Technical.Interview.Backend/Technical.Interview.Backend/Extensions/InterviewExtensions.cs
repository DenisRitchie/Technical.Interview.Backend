namespace Technical.Interview.Backend.Extensions;

using Ardalis.Specification;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Technical.Interview.Backend.Data;
using Technical.Interview.Backend.Interface;
using Technical.Interview.Backend.Mapping;
using Technical.Interview.Backend.Services;

public static class InterviewExtensions
{
    extension(IServiceCollection Services)
    {
        public IServiceCollection AddExtensions(IConfiguration Configuration)
        {
            Services.AddOutputCache(Options =>
            {
                Options.AddBasePolicy(Builder => Builder.Expire(TimeSpan.FromSeconds(30)));
            });

            Services.AddDbContext<InterviewContext>(Options =>
            {
                Options.UseNpgsql(Configuration["ConnectionStrings:DefaultConnection"]);
            });

            Services.AddAutoMapper(Options =>
            {
                Options.AddProfile<MappingProfile>();
            });

            Services.AddScoped<IBrandService, BrandService>();
            Services.AddScoped(typeof(ISingleResultSpecification<>), typeof(SingleResultSpecification<>));
            Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            Services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));

            return Services;
        }
    }
}