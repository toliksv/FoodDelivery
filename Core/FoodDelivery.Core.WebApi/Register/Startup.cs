using System;
using System.Reflection;
using Microsoft.Net.Http.Headers;

using System.Net;
using FoodDelivery.Core.Registrations;

namespace FoodDelivery.Core.WebApi.Register;

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
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"), true);
        });                 
        
        services.AddLogging(builder => builder.AddConsole());      

        services
            .RegisterDefaultDateTimeProvider()
            .RegisterDefaultLOrderIdentifierGenerator()
            .RegisterCoreAdapters()
            .RegisterPolly()
            .RegisterStorageClient(Configuration)
            .RegisterAdaptersAndMapper();                    
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {   
        app.UseRouting();
        app.UseEndpoints(enp => enp.MapControllers());                   

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
          
    }

}
