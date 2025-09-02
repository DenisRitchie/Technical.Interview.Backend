namespace Technical.Interview.Backend.Data;

using System.Runtime.CompilerServices;

using Ardalis.Specification;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Microsoft.EntityFrameworkCore;

using Technical.Interview.Backend.Common;
using Technical.Interview.Backend.Interface;
using Technical.Interview.Backend.Responses;

public class Repository<T>(InterviewContext DbContext, IMapper mapper) : RepositoryBase<T>(DbContext, mapper), IRepository<T> where T : class;

public class ReadRepository<T>(InterviewContext DbContext, IMapper mapper) : RepositoryBase<T>(DbContext, mapper), IReadRepository<T> where T : class;

public abstract class RepositoryBase<TEntity> : IReadRepository<TEntity> where TEntity : class
{
    private readonly DbContext DbContext;
    private readonly IConfigurationProvider ConfigurationProvider;

    protected ISpecificationEvaluator Evaluator { get; }

    // We have a custom evaluator for QueryTag, therefore we're passing our custom specification evaluator
    protected RepositoryBase(DbContext DbContext, IMapper Mapper) : this(DbContext, AppSpecificationEvaluator.Instance, Mapper)
    {
    }

    protected RepositoryBase(DbContext DbContext, ISpecificationEvaluator SpecificationEvaluator, IMapper Mapper)
    {
        this.DbContext = DbContext;
        this.Evaluator = SpecificationEvaluator;
        this.ConfigurationProvider = Mapper.ConfigurationProvider;
    }

    public virtual async ValueTask<TEntity> AddAsync(TEntity Entity, CancellationToken CancellationToken = default)
    {
        DbContext.Set<TEntity>().Add(Entity);
        await SaveChangesAsync(CancellationToken);
        return Entity;
    }

    public virtual async IAsyncEnumerable<TEntity> AddRangeAsync(IEnumerable<TEntity> Entities, [EnumeratorCancellation] CancellationToken CancellationToken = default)
    {
        await DbContext.Set<TEntity>().AddRangeAsync(Entities, CancellationToken);
        await SaveChangesAsync(CancellationToken);

        if (CancellationToken.IsCancellationRequested)
            yield break;

        foreach (var Entity in Entities)
            yield return Entity;
    }

    public virtual async ValueTask UpdateAsync(TEntity Entity, CancellationToken CancellationToken = default)
    {
        DbContext.Set<TEntity>().Update(Entity);
        await SaveChangesAsync(CancellationToken);
    }

    public virtual async ValueTask DeleteAsync(TEntity Entity, CancellationToken CancellationToken = default)
    {
        DbContext.Set<TEntity>().Remove(Entity);
        await SaveChangesAsync(CancellationToken);
    }

    public virtual async ValueTask DeleteRangeAsync(IEnumerable<TEntity> Entities, CancellationToken CancellationToken = default)
    {
        DbContext.Set<TEntity>().RemoveRange(Entities);
        await SaveChangesAsync(CancellationToken);
    }

    public virtual async ValueTask DeleteRangeAsync(ISpecification<TEntity> Specification, CancellationToken CancellationToken = default)
    {
        var Wuery = ApplySpecification(Specification);
        DbContext.Set<TEntity>().RemoveRange(Wuery);
        await SaveChangesAsync(CancellationToken);
    }

    public virtual async ValueTask<int> SaveChangesAsync(CancellationToken CancellationToken = default)
    {
        return await DbContext.SaveChangesAsync(CancellationToken);
    }

    public virtual async ValueTask<TEntity?> FindAsync<TId>(TId Id, CancellationToken CancellationToken = default) where TId : notnull
    {
        return await DbContext.Set<TEntity>().FindAsync([Id], cancellationToken: CancellationToken);
    }

    public async ValueTask<TEntity?> FirstOrDefaultAsync(ISpecification<TEntity> Specification, CancellationToken CancellationToken = default)
    {
        return await ApplySpecification(Specification).FirstOrDefaultAsync(CancellationToken);
    }

    public async ValueTask<TResult?> FirstOrDefaultAsync<TResult>(ISpecification<TEntity, TResult> Specification, CancellationToken CancellationToken = default)
    {
        return await ApplySpecification(Specification).FirstOrDefaultAsync(CancellationToken);
    }

    public async ValueTask<TEntity?> SingleOrDefaultAsync(ISpecification<TEntity> Specification, CancellationToken CancellationToken = default)
    {
        return await ApplySpecification(Specification).SingleOrDefaultAsync(CancellationToken);
    }

    public async ValueTask<TResult?> SingleOrDefaultAsync<TResult>(ISpecification<TEntity, TResult> Specification, CancellationToken CancellationToken = default)
    {
        return await ApplySpecification(Specification).SingleOrDefaultAsync(CancellationToken);
    }

    public virtual async ValueTask<List<TEntity>> ListAsync(CancellationToken CancellationToken = default)
    {
        return await DbContext.Set<TEntity>().ToListAsync(CancellationToken);
    }

    public virtual async ValueTask<List<TEntity>> ListAsync(ISpecification<TEntity> Specification, CancellationToken CancellationToken = default)
    {
        var QueryResult = await ApplySpecification(Specification).ToListAsync(CancellationToken);
        return Specification.PostProcessingAction is null ? QueryResult : [.. Specification.PostProcessingAction(QueryResult)];
    }

    public virtual async ValueTask<List<TResult>> ListAsync<TResult>(ISpecification<TEntity, TResult> Specification, CancellationToken CancellationToken = default)
    {
        var QueryResult = await ApplySpecification(Specification).ToListAsync(CancellationToken);
        return Specification.PostProcessingAction is null ? QueryResult : [.. Specification.PostProcessingAction(QueryResult)];
    }

    public virtual async ValueTask<int> CountAsync(ISpecification<TEntity> Specification, CancellationToken CancellationToken = default)
    {
        return await ApplySpecification(Specification, true).CountAsync(CancellationToken);
    }

    public virtual async ValueTask<int> CountAsync(CancellationToken CancellationToken = default)
    {
        return await DbContext.Set<TEntity>().CountAsync(CancellationToken);
    }

    public virtual async ValueTask<bool> AnyAsync(ISpecification<TEntity> Specification, CancellationToken CancellationToken = default)
    {
        return await ApplySpecification(Specification, true).AnyAsync(CancellationToken);
    }

    public virtual async ValueTask<bool> AnyAsync(CancellationToken CancellationToken = default)
    {
        return await DbContext.Set<TEntity>().AnyAsync(CancellationToken);
    }

    public async ValueTask<TResult?> ProjectToFirstOrDefaultAsync<TResult>(ISpecification<TEntity> Specification, CancellationToken CancellationToken)
    {
        return await ApplySpecification(Specification).ProjectTo<TResult>(ConfigurationProvider).FirstOrDefaultAsync(CancellationToken);
    }

    public async ValueTask<List<TResult>> ProjectToListAsync<TResult>(ISpecification<TEntity> Specification, CancellationToken CancellationToken)
    {
        return await ApplySpecification(Specification).ProjectTo<TResult>(ConfigurationProvider).ToListAsync(CancellationToken);
    }

    public async ValueTask<PagedResponse<TResult>> ProjectToListAsync<TResult>(ISpecification<TEntity> Specification, BaseFilter Filter, CancellationToken CancellationToken)
    {
        var Count = await ApplySpecification(Specification).CountAsync(CancellationToken);
        var Pagination = new Pagination(Count, Filter);

        var Data = await ApplySpecification(Specification)
            .Skip(Pagination.Skip)
            .Take(Pagination.Take)
            .ProjectTo<TResult>(ConfigurationProvider)
            .ToListAsync(CancellationToken);

        return new PagedResponse<TResult> { Data = Data, Pagination = Pagination };
    }

    protected virtual IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> Specification, bool EvaluateCriteriaOnly = false)
    {
        return Evaluator.GetQuery(DbContext.Set<TEntity>(), Specification, EvaluateCriteriaOnly);
    }

    protected virtual IQueryable<TResult> ApplySpecification<TResult>(ISpecification<TEntity, TResult> specification)
    {
        return Evaluator.GetQuery(DbContext.Set<TEntity>(), specification);
    }
}