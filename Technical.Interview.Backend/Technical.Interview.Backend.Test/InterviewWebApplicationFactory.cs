namespace Technical.Interview.Backend.Test;

using System.Data.Common;
using System.Linq;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

using Technical.Interview.Backend.Data;

public class InterviewWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder Builder)
    {
        Builder.ConfigureServices(static Services =>
        {
            var DbContextDescriptor = Services.SingleOrDefault(static S => S.ServiceType == typeof(IDbContextOptionsConfiguration<InterviewContext>));
            if (DbContextDescriptor is not null)
                Services.Remove(DbContextDescriptor);

            var DbConnectionDescriptor = Services.SingleOrDefault(static S => S.ServiceType == typeof(DbConnection));
            if (DbConnectionDescriptor is not null)
                Services.Remove(DbConnectionDescriptor);

            // Create open SqliteConnection so EF won't automatically close it.
            Services.AddSingleton<DbConnection>(static Service =>
            {
                var Connection = new SqliteConnection("DataSource=:memory:");
                Connection.Open();
                return Connection;
            });

            Services.AddDbContext<InterviewContext>(static (Service, Options) =>
            {
                var Connection = Service.GetRequiredService<DbConnection>();
                Options.UseSqlite(Connection);
            });

        });

        Builder.UseEnvironment("Development");
    }
}