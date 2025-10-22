using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CCMS.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class CCMSDbContextFactory : IDesignTimeDbContextFactory<CCMSDbContext>
{
    public CCMSDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        CCMSEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<CCMSDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));
        
        return new CCMSDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../CCMS.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
