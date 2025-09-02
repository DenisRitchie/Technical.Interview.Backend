namespace Technical.Interview.Backend.Entities;

/// <summary>
///     Represents a car brand.
/// </summary>
public class MarcasAuto
{
    /// <summary>
    ///     Represent the unique identifier 
    /// </summary>
    public Guid Id { get; private set; } = Guid.Empty;

    /// <summary>
    ///     Represents the name of the brand
    /// </summary>
    public string Nombre { get; private set; } = string.Empty;

    /// <summary>
    ///     Repsents the country of origin
    /// </summary>
    public string PaisOrigen { get; private set; } = string.Empty;

    /// <summary>
    ///     Represents the website of the brand
    /// </summary>
    public string SitioWeb { get; private set; } = string.Empty;

    /// <summary>
    ///     Create a new instance of <see cref="MarcasAuto"/>
    /// </summary>
    /// <param name="Name">
    ///     Name of the brand
    /// </param>
    /// <param name="OriginCountry">
    ///     Origin country of the brand
    /// </param>
    /// <param name="Website">
    ///     Website of the brand
    /// </param>
    /// <returns>
    ///     Returns a new instance of <see cref="MarcasAuto"/>
    /// </returns>
    public static MarcasAuto Create(string Name, string OriginCountry, string Website)
        => new()
        {
            Id = Guid.NewGuid(),
            Nombre = Name,
            PaisOrigen = OriginCountry,
            SitioWeb = Website
        };
}
