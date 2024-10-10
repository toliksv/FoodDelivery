using System;

namespace FoodDelivery.Core.Contracts.Ports.Common.In.Calendar;

/// <summary>
/// Провайдер даты и времени.
/// </summary>
public interface IDateTimeProvider
{
    /// <summary>
    /// Текущая дата и время.
    /// </summary>
    DateTime Now { get; }
}
