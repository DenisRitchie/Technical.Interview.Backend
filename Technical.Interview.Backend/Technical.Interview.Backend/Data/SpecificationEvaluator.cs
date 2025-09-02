namespace Technical.Interview.Backend.Data;

using Ardalis.Specification.EntityFrameworkCore;

public class AppSpecificationEvaluator : SpecificationEvaluator
{
    public static AppSpecificationEvaluator Instance { get; } = new AppSpecificationEvaluator();

    public AppSpecificationEvaluator() : base()
    {
        Evaluators.Add(QueryTagEvaluator.Instance);
    }
}
