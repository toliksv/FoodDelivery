using FoodDelivery.PostgreSql.WebApi.Register;
using Microsoft.AspNetCore;

await WebHost.CreateDefaultBuilder(args)
        .UseContentRoot(AppContext.BaseDirectory)                     
        .UseStartup<Startup>()        
        .Build()                
        .RunAsync();