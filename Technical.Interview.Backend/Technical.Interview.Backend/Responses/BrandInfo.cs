namespace Technical.Interview.Backend.Responses;

public class BrandInfo
{
    /// <summary>
    ///     Represent the unique identifier 
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    ///     Represents the name of the brand
    /// </summary>
    public required string Nombre { get; set; }

    /// <summary>
    ///     Repsents the country of origin
    /// </summary>
    public required string PaisOrigen { get; set; }

    /// <summary>
    ///     Fundation date
    /// </summary>
    public required DateTime Fundacion { get; set; }

    /// <summary>
    ///     Represents the website of the brand
    /// </summary>
    public required string SitioWeb { get; set; }
}