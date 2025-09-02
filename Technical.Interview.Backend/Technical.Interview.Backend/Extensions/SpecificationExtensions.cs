namespace Technical.Interview.Backend.Extensions;

using Ardalis.Specification;

using Technical.Interview.Backend.Common;
using Technical.Interview.Backend.Entities;

public static class SpecificationExtensions
{
    extension(ISpecificationBuilder<MarcasAuto> Builder)
    {
        // Let's assume we want to apply ordering for brands.
        // Conveniently, we can create add an extension method, and use it in any customer spec.
        public ISpecificationBuilder<MarcasAuto> ApplyOrdering(BaseFilter? Filter = null)
        {
            // If there is no filter apply default ordering;
            if (Filter is null) return Builder.OrderBy(Prop => Prop.Id);

            // We want the "asc" to be the default, that's why the condition is reverted.
            var IsAscending = !(Filter.OrderBy?.Equals("desc", StringComparison.OrdinalIgnoreCase) ?? false);

            return Filter.SortBy switch
            {
                nameof(MarcasAuto.Nombre) => IsAscending ? Builder.OrderBy(Prop => Prop.Nombre) : Builder.OrderByDescending(Prop => Prop.SitioWeb),
                _ => Builder.OrderBy(Prop => Prop.Id)
            };
        }

        public ISpecificationBuilder<MarcasAuto> ById(string? Id)
            => Id is not null ? Builder.Where(Prop => Prop.Id.Equals(Guid.Parse(Id))) : Builder;

        public ISpecificationBuilder<MarcasAuto> ByName(string? Name)
            => Name is not null ? Builder.Where(Prop => Prop.Nombre.Contains(Name)) : Builder;

        public ISpecificationBuilder<MarcasAuto> ByOriginCountry(string? OriginCountry)
            => OriginCountry is not null ? Builder.Where(Prop => Prop.PaisOrigen.Contains(OriginCountry)) : Builder;

        public ISpecificationBuilder<MarcasAuto> ByWebsite(string? Website)
            => Website is not null ? Builder.Where(Prop => Prop.SitioWeb.Contains(Website)) : Builder;
    }
}
