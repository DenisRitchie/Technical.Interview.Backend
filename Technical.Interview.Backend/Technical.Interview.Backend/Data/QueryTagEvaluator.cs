namespace Technical.Interview.Backend.Data;

using Ardalis.Specification;

using Microsoft.EntityFrameworkCore;

public class QueryTagEvaluator : IEvaluator
{
    private QueryTagEvaluator() { }
    public static QueryTagEvaluator Instance { get; } = new QueryTagEvaluator();

    public bool IsCriteriaEvaluator { get; } = true;

    public IQueryable<T> GetQuery<T>(IQueryable<T> Query, ISpecification<T> Specification) where T : class
    {
        if (Specification.Items.TryGetValue("TagWith", out var value) && value is string tag)
        {
            Query = Query.TagWith(tag);
        }

        return Query;
    }
}