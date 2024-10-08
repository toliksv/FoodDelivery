using FoodDelivery.Core.Contracts.Application.Events.Orders;
using FoodDelivery.Core.Contracts.Ports.Orders.Out;
using FoodDelivery.PostgreSql.Application.Mapping.Orders;
using FoodDelivery.PostgreSql.Application.Models;
using FoodDelivery.PostgreSql.Application.Models.Orders;
using LinqToDB;
using Microsoft.Extensions.Logging;

namespace FoodDelivery.PostgreSql.Adapters.Orders.Out;

internal class OrdersEventsSourceRepository : IOrdersEventsSourceRepository
{
    private readonly FoodDeliveryDataConnection _dbContext;    

    private readonly IOrderEventMapperProvider _orderEventMapperProvider;
    private readonly ILogger<OrdersEventsSourceRepository> _logger;

    public OrdersEventsSourceRepository(FoodDeliveryDataConnection dbContext, IOrderEventMapperProvider orderEventMapperProvider, ILogger<OrdersEventsSourceRepository> logger)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _orderEventMapperProvider = orderEventMapperProvider ?? throw new ArgumentNullException(nameof(orderEventMapperProvider));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<List<OrderEventBase>> GetClientEvents(int clientId, CancellationToken cancellationToken)
    {
        var entitiesList = await _dbContext.OrderEvents
                                    .Where(ent => ent.ClientId == clientId)
                                    .ToListAsync(cancellationToken)
                                    .ConfigureAwait(false);
        return GetEvents(entitiesList);
    }

    public async Task<List<OrderEventBase>> GetOrderEvents(int orderId, CancellationToken cancellationToken)
    {
        var entitiesList = await _dbContext.OrderEvents
                                    .Where(ent => ent.OrderId == orderId)
                                    .ToListAsync(cancellationToken)
                                    .ConfigureAwait(false); 
        return GetEvents(entitiesList);                            
    }

    public Task SaveEvent<TOrderEvent>(TOrderEvent orderEventBase, CancellationToken cancellationToken) where TOrderEvent : OrderEventBase
    {
        var mapper = _orderEventMapperProvider.ProvideMapper(orderEventBase);
        var entity = mapper.MapFromEvent(orderEventBase);
        return _dbContext
                .InsertAsync(entity, token:cancellationToken);                
    }

    private List<OrderEventBase> GetEvents(List<OrderEventEntity> orderEventEntities)
    {
        if (orderEventEntities is null)
        {
            throw new ArgumentNullException(nameof(orderEventEntities));
        }

        var eventsList = new List<OrderEventBase>();
        foreach (var orderEventEntity in orderEventEntities)
        {
           var mapper = _orderEventMapperProvider.ProvideMapper(orderEventEntity.OrderEventType);
           eventsList.Add(mapper.MapToEvent(orderEventEntity));     
        }

        return eventsList;
    }
}
