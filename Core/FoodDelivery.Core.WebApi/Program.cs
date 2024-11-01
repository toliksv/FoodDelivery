using Microsoft.AspNetCore;
using FoodDelivery.Core.WebApi.Register;
using Confluent.Kafka;


await WebHost.CreateDefaultBuilder(args)
        .UseContentRoot(AppContext.BaseDirectory)                     
        .UseStartup<Startup>()        
        .Build()                
        .RunAsync();