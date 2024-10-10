using System;
using FoodDelivery.Core.Contracts.Ports.Common.In.Calendar;

namespace FoodDelivery.Core.Adapters.Common.In.Calendar;

internal class DefaultDateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.Now;
}
