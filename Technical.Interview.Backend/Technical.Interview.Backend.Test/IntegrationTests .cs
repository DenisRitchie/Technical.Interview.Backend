namespace Technical.Interview.Backend.Test;

using System.Net.Http.Json;

using Microsoft.AspNetCore.Mvc.Testing;

using Technical.Interview.Backend.Responses;

using Xunit;

public class IntegrationTest : IClassFixture<InterviewWebApplicationFactory<Program>>
{
    private readonly HttpClient Client;
    private readonly InterviewWebApplicationFactory<Program> Factory;

    public IntegrationTest()
    {
        this.Factory = new InterviewWebApplicationFactory<Program>();
        this.Client = Factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [Theory]
    [InlineData("/api/MarcasAutos")]
    [InlineData("/api/MarcasAutos?Id=0840e14f-81b5-45bd-afd9-913373573326")]
    [InlineData("/api/MarcasAutos?Name=Nissan")]
    [InlineData("/api/MarcasAutos?OriginCountry=Japan")]
    [InlineData("/api/MarcasAutos?PageSize=5")]
    public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string Url)
    {
        // Act
        var Response = await Client.GetAsync(Url);

        //// Assert
        Response.EnsureSuccessStatusCode(); // Status Code 200-299
        var Json = await Response.Content.ReadFromJsonAsync<Dictionary<string, object>>();

        Assert.NotNull(Json);
    }
}
