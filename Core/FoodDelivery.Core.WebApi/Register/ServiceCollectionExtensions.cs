using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Markup;
using Confluent.Kafka;
using FoodDelivery.Core.Contracts.Domain.Aggregates.Orders;
using FoodDelivery.Core.Contracts.Ports.Menu.In;
using FoodDelivery.Core.Contracts.Ports.Menu.Out;
using FoodDelivery.Core.Contracts.Ports.Orders.Out;
using FoodDelivery.Core.WebApi.Adapters.Menu.In;
using FoodDelivery.Core.WebApi.Adapters.Menu.Out;
using FoodDelivery.Core.WebApi.Adapters.Orders.In;
using FoodDelivery.Core.WebApi.Adapters.Orders.Out;
using FoodDelivery.Core.WebApi.Application.Commands.Orders.Mappers;
using FoodDelivery.Core.WebApi.Application.Queries.Orders.Mappers;
using FoodDelivery.Core.WebApi.Ports.Menu.In;
using FoodDelivery.Core.WebApi.Ports.Menu.Out;
using FoodDelivery.Core.WebApi.Ports.Orders.In;
using FoodDelivery.Core.WebApi.Ports.Orders.Out;
using FoodDelivery.Kafka.Options;
using FoodDelivery.Kafka.Register;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Polly;
using Polly.CircuitBreaker;
using RestEase;
using RestEase.HttpClientFactory;

namespace FoodDelivery.Core.WebApi.Register;

/// <summary>
/// Методы регистрации функционала
/// </summary>
internal static  class ServiceCollectionExtensions
{
    /// <summary>
    /// Реристрация стратегии Polly.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection RegisterPolly(this IServiceCollection services)
    {
        services.AddResiliencePipeline("main", builder => 
        {
            builder.AddCircuitBreaker(new CircuitBreakerStrategyOptions()
            {
              FailureRatio = 0.5,
              SamplingDuration = TimeSpan.FromSeconds(30),
              BreakDuration = TimeSpan.FromSeconds(30),                            
            });
        });

        return services;
    }

    public static IServiceCollection RegisterStorageClient(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration is null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }

        var jsonSetting = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver(),            
        };

        var storageHost = configuration.GetValue<string>("StorageHost");

        var restClient = new RestClient(storageHost)
        {
            JsonSerializerSettings = jsonSetting,
            ResponseDeserializer = new JsonResponseDeserializer()
            {
                
            },
            RequestBodySerializer = new JsonRequestBodySerializer(),
        };

        
        services.AddTransient(sp => restClient.For<IOrderStorageController>());
        services.AddTransient<IMenuStorageController>(sp=>restClient.For<IMenuStorageController>());
        return services;
    }

    public static IServiceCollection RegisterAdaptersAndMapper(this IServiceCollection services)
        =>
            services            
                .AddSingleton<IOrdersQueryMapper, OrdersQueryMapper>()
                .AddSingleton<IOrderCommandMapper, OrderCommandMapper>()       
                .AddTransient<IMenuStorage, MenuStorage>()                
                .AddTransient<IMenuRepository, MenuRepository>()
                .AddTransient<IOrderStorageControllerWrapper, OrderStorageControllerWrapper>()
                .AddTransient<IOrderStorage, OrderStorage>()
                .AddTransient<IOrdersEventsSourceRepository, OrdersEventsSourceRepository>();
}
