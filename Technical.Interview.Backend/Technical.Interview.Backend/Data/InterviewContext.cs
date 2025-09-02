namespace Technical.Interview.Backend.Data;

using System.Reflection;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

using Technical.Interview.Backend.Data.Seed;
using Technical.Interview.Backend.Entities;
using Technical.Interview.Backend.Interface;

public class InterviewContext(DbContextOptions<InterviewContext> Options, IMapper Mapper) : DbContext(Options)
{
    /// <summary>
    ///     Represent the DbSet 
    /// </summary>
    public DbSet<MarcasAuto> MarcasAutos => base.Set<MarcasAuto>();

    /// <summary>
    ///     Brand reposiroty for creation, update and delete operations
    /// </summary>
    public IRepository<MarcasAuto> BrandRepository => new Repository<MarcasAuto>(this, Mapper);

    /// <summary>
    ///     Brand Repository just for read operations
    /// </summary>
    public IReadRepository<MarcasAuto> BrandReadRepository => new ReadRepository<MarcasAuto>(this, Mapper);

    /// <summary>
    ///     <see langword="extension"/> method to apply configurations and seed data
    /// </summary>
    /// <param name="ModelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder ModelBuilder)
    {
        base.OnModelCreating(ModelBuilder);

        ModelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        ModelBuilder.SeedBrand();
    }
}
