using System;
using System.Reflection;
using FoodDelivery.PostgreSql.Application.Models;
using FoodDelivery.PostgreSql.Registrations;
using LinqToDB;
using LinqToDB.AspNet;
using LinqToDB.AspNet.Logging;



namespace FoodDelivery.PostgreSql.WebApi.Register;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
       Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();        
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options => {
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "FoodDelivery.Core.Contracts.xml"), true);            
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"), true);
        });       
        
        services.AddLinqToDBContext<FoodDeliveryDataConnection>((provider, options)        
            => options
                //will configure the AppDataConnection to use
                //sqite with the provided connection string
                //there are methods for each supported database
                .UsePostgreSQL(Configuration.GetConnectionString("Default"))
                .UseMappingSchema(FoodDeliveryDataConnection.ConfigureMappings())
                //default logging will log everything using the ILoggerFactory configured in the provider
                .UseDefaultLogging(provider));
        services.RegisterPostgreSqlAdapters();
        services.AddLogging(builder => builder.AddConsole());        
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {                      

        if (env.IsDevelopment())
        {
            // In Development, use the Developer Exception Page
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();  
        }
        else
        {
            // In Staging/Production, route exceptions to /error
            app.UseExceptionHandler("/error");
        }
        
        app.UseRouting();
        app.UseEndpoints(enp => enp.MapControllers());  
    }
}
