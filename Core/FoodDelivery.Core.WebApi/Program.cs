using Microsoft.AspNetCore;
using FoodDelivery.Core.WebApi.Register;


await WebHost.CreateDefaultBuilder(args)
        .UseContentRoot(AppContext.BaseDirectory)                     
        .UseStartup<Startup>()        
        .Build()                
        .RunAsync();
