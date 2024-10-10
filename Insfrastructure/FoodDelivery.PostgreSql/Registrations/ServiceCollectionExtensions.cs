using System;
using FoodDelivery.Core.Contracts.Application.Events.Orders;
using FoodDelivery.Core.Contracts.Ports.Menu.Out;
using FoodDelivery.Core.Contracts.Ports.Orders.Out;
using FoodDelivery.PostgreSql.Adapters.Menu.Out;
using FoodDelivery.PostgreSql.Adapters.Orders.Out;
using FoodDelivery.PostgreSql.Application.Mapping;
using FoodDelivery.PostgreSql.Application.Mapping.Menu;
using FoodDelivery.PostgreSql.Application.Mapping.Orders;
using Microsoft.Extensions.DependencyInjection;


namespace FoodDelivery.PostgreSql.Registrations;

/// <summary>
/// Методы расширения для DI регистрации.
/// </summary>
public static class ServiceCollectionExtensions
{

    /// <summary>
    /// Регистрация адаптеров к 
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection RegisterPostgreSqlAdapters(this IServiceCollection services)
     => 
        services
            .RegisterMappers()
            .AddTransient<IMenuRepository, MenuRepository>()
            .AddTransient<IOrdersEventsSourceRepository, OrdersEventsSourceRepository>();

    /// <summary>
    /// Регистрация мапперов.
    /// </summary>
    /// <param name="services">контейнер.</param>
    /// <returns>контейнер с зарегистрированными сервисами.</returns>
    internal static IServiceCollection RegisterMappers(this IServiceCollection services)
     =>
        services
            .AddSingleton<IMenuItemMapper, MenuItemMapper>()
            .AddSingleton<IOrderEventMapper<CreateOrderEvent>, CreateOrderEventMapper>()
            .AddSingleton<IOrderEventMapper<SetOrderStatusEvent>, SetOrderStatusEventMapper>()
            .AddSingleton<IOrderEventMapper<AddOrderItemEvent>, AddOrderItemEventMapper>()
            .AddSingleton<IOrderEventMapper<RemoveOrderItemEvent>, RemoveOrderItemEventMapper>()
            .AddSingleton<IOrderEventMapper<ChangeOrderItemQuantityEvent>, ChangeOrderItemQuantityEventMapper>()
            .AddSingleton<IOrderEventMapperProvider, OrderEventMapperProvider>();
}
