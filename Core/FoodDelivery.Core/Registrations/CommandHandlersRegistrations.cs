using System;
using FoodDelivery.Core.Adapters.Common.In.Calendar;
using FoodDelivery.Core.Adapters.Menu.In;
using FoodDelivery.Core.Adapters.Orders.In;
using FoodDelivery.Core.Contracts.Ports.Common.In.Calendar;
using FoodDelivery.Core.Contracts.Ports.Common.In.OrderIdentificators;
using FoodDelivery.Core.Contracts.Ports.Menu.In;
using FoodDelivery.Core.Contracts.Ports.Orders.In;
using FoodDelivery.Core.Port.Common.In.OrderIdentificators;
using Microsoft.Extensions.DependencyInjection;

namespace FoodDelivery.Core.Registrations;

/// <summary>
/// Регистрации для обработчиков Core.
/// </summary>
public static class CoreServiceCollectionRegistrations
{
    /// <summary>
    /// Регистрирует адаптеры ядра.
    /// </summary>
    /// <param name="services">контейнер.</param>
    /// <returns>контейнер с зарегисрированными адаптерами.</returns>
    public static IServiceCollection RegisterCoreAdapters(this IServiceCollection services)
     => services     
            .AddTransient<IMenuQueriesHandler, MenuQueriesHandler>()
            .AddTransient<IOrdersQueriesHandler, OrdersQueriesHandler>()
            .AddTransient<IOrdersCommandHandler, OrdersCommandHandler>();

    /// <summary>
    /// Регистрирует дефолтный провайдер даты и времени.
    /// </summary>
    /// <param name="services">контейнеры</param>
    /// <returns>контейнер с зарегистрированным <see cref="DefaultDateTimeProvider"/>.</returns>
    public static IServiceCollection RegisterDefaultDateTimeProvider(this IServiceCollection services)
        => services.AddSingleton<IDateTimeProvider, DefaultDateTimeProvider>();        

    /// <summary>
    /// Регистрирует дефолтный генератор идентификатора заказа.
    /// </summary>
    /// <param name="services">контейнер.</param>
    /// <returns>контейнер с зарегистрированным <see cref="DefaultOrderIdentificatorGenerator"/>.</returns>
    public static IServiceCollection RegisterDefaultLOrderIdentifierGenerator(this IServiceCollection services)
       => services.AddSingleton<IOrderIdentificatorGenerator, DefaultOrderIdentificatorGenerator>();

}
