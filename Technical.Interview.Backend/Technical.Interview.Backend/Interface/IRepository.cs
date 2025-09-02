namespace Technical.Interview.Backend.Interface;


using Ardalis.Specification;

using Technical.Interview.Backend.Common;
using Technical.Interview.Backend.Responses;

public interface IRepository<T> where T : class
{
    ValueTask<T> AddAsync(T entity, CancellationToken cancellationToken = default);

    IAsyncEnumerable<T> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

    ValueTask UpdateAsync(T entity, CancellationToken cancellationToken = default);

    ValueTask DeleteAsync(T entity, CancellationToken cancellationToken = default);

    ValueTask DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

    ValueTask DeleteRangeAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);

    ValueTask<T?> FindAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull;

    ValueTask<T?> FirstOrDefaultAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);

    ValueTask<TResult?> FirstOrDefaultAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default);

    ValueTask<T?> SingleOrDefaultAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);

    ValueTask<TResult?> SingleOrDefaultAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default);

    ValueTask<List<T>> ListAsync(CancellationToken cancellationToken = default);

    ValueTask<List<T>> ListAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);

    ValueTask<List<TResult>> ListAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default);

    ValueTask<int> CountAsync(CancellationToken cancellationToken = default);

    ValueTask<int> CountAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);

    ValueTask<bool> AnyAsync(CancellationToken cancellationToken = default);

    ValueTask<bool> AnyAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);

    ValueTask<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}


public interface IReadRepository<T> where T : class
{
    ValueTask<T?> FindAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull;

    ValueTask<T?> FirstOrDefaultAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);

    ValueTask<TResult?> FirstOrDefaultAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default);

    ValueTask<T?> SingleOrDefaultAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);

    ValueTask<TResult?> SingleOrDefaultAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default);

    ValueTask<List<T>> ListAsync(CancellationToken cancellationToken = default);

    ValueTask<List<T>> ListAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);

    ValueTask<List<TResult>> ListAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default);

    ValueTask<int> CountAsync(CancellationToken cancellationToken = default);

    ValueTask<int> CountAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);

    ValueTask<bool> AnyAsync(CancellationToken cancellationToken = default);

    ValueTask<bool> AnyAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);

    ValueTask<TResult?> ProjectToFirstOrDefaultAsync<TResult>(ISpecification<T> specification, CancellationToken cancellationToken);

    ValueTask<List<TResult>> ProjectToListAsync<TResult>(ISpecification<T> specification, CancellationToken cancellationToken);

    ValueTask<PagedResponse<TResult>> ProjectToListAsync<TResult>(ISpecification<T> specification, BaseFilter filter, CancellationToken cancellationToken);
}