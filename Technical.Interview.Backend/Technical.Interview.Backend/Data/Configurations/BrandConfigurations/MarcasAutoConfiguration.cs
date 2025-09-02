namespace Technical.Interview.Backend.Data.Configurations.BrandConfigurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Technical.Interview.Backend.Entities;

public class MarcasAutoConfiguration : IEntityTypeConfiguration<MarcasAuto>
{
    public void Configure(EntityTypeBuilder<MarcasAuto> Builder)
    {
        Builder.Property(Prop => Prop.Id).ValueGeneratedOnAdd();
        Builder.Property(Prop => Prop.Nombre).HasMaxLength(100).IsRequired();
        Builder.Property(Prop => Prop.PaisOrigen).HasMaxLength(100).IsRequired();
        Builder.Property(Prop => Prop.SitioWeb).HasMaxLength(200).IsRequired();
    }
}
