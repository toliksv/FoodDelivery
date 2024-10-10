using FoodDelivery.Core.Contracts.Application.Events.Orders;
using FoodDelivery.Core.Contracts.Domain.Aggregates.Orders;
using FoodDelivery.Core.Contracts.Ports.Orders.Out;
using FoodDelivery.PostgreSql.WebApi.Application.Web.Orders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.PostgreSql.WebApi.Controllers
{
    /// <summary>
    /// Контроллер для работы с событиями по заказам
    /// </summary>
    [Route("api/postgre-sql-adapter/orders/events")]
    [ApiController]
    public class OrderPostgreSqlAdapterController : ControllerBase
    {   
        private readonly IOrdersEventsSourceRepository _orderEventsSourceRepository;
        private readonly ILogger<OrderPostgreSqlAdapterController> _logger;

        public OrderPostgreSqlAdapterController(IOrdersEventsSourceRepository orderEventsSourceRepository, ILogger<OrderPostgreSqlAdapterController> logger)
        {
            _orderEventsSourceRepository = orderEventsSourceRepository ?? throw new ArgumentNullException(nameof(orderEventsSourceRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Запись события создания заказа.
        /// </summary>
        /// <param name="createOrderEvent"><see cref="CreateOrderEvent" /></param>
        /// <param name="cancellationToken">токен отменыю</param>
        /// <returns>результат.</returns>
        [HttpPost("write/create")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]        
        public Task<IActionResult> WriteCreateOrderEvent([FromBody]CreateOrderEvent createOrderEvent, CancellationToken cancellationToken)
         => WrapWriteOrderEvent(createOrderEvent, cancellationToken);

        [HttpPut("write/set-status")]
        public Task<IActionResult> WriteSetOrderStatusEvent([FromBody]SetOrderStatusEvent setOrderStatusEvent, CancellationToken cancellationToken)
         => WrapWriteOrderEvent(setOrderStatusEvent, cancellationToken);   

        [HttpPost("write/add-order-item")]
        public Task<IActionResult> WriteAddOrderItemEvent([FromBody]AddOrderItemEvent addOrderItemEvent, CancellationToken cancellationToken)
         => WrapWriteOrderEvent(addOrderItemEvent, cancellationToken);

        [HttpDelete("write/remove-order-item")]
        public Task<IActionResult> WriteRemoveOrderItemEvent([FromBody]RemoveOrderItemEvent addOrderItemEvent, CancellationToken cancellationToken)
         => WrapWriteOrderEvent(addOrderItemEvent, cancellationToken);

        [HttpPut("write/change-order-item-quantity")]
        public Task<IActionResult> WriteChangeOrderItemQuantityEvent([FromBody]ChangeOrderItemQuantityEvent addOrderItemEvent, CancellationToken cancellationToken)
         => WrapWriteOrderEvent(addOrderItemEvent, cancellationToken); 

        [HttpGet("query/by-order-id/{orderId}")]
        public Task<IActionResult> GetOrderEventsByOrderId([FromRoute]int orderId, CancellationToken cancellationToken)
            => WrapGetOrderEvents(rep=> rep.GetOrderEvents(orderId, cancellationToken));

        [HttpGet("query/by-client-id/{clientId}")]
        public Task<IActionResult> GetOrderEventsByClientId([FromRoute]int clientId, CancellationToken cancellationToken)
            => WrapGetOrderEvents(rep => rep.GetClientEvents(clientId, cancellationToken));    

        private async Task<IActionResult> WrapGetOrderEvents(Func<IOrdersEventsSourceRepository, Task<List<OrderEventBase>>> queryFunc)
        {
            try
            {
                var eventsList = await queryFunc(_orderEventsSourceRepository);
                if (eventsList is null || eventsList.Count == 0)
                {
                    return NoContent();
                }

                var response = new OrderQueryResponse();                    
                foreach (var orderEvent in eventsList)
                {
                    switch (orderEvent)
                    {
                        case CreateOrderEvent createOrderEvent:
                            response.CreateEvents.Add(createOrderEvent);
                            break;
                        case SetOrderStatusEvent setOrderStatusEvent:
                            response.SetStatusEvents.Add(setOrderStatusEvent);
                            break;
                        case AddOrderItemEvent addOrderItemEvent:
                            response.AddOrderItemEvents.Add(addOrderItemEvent);
                            break;
                        case RemoveOrderItemEvent removeOrderItemEvent:
                            response.RemoveOrderItemEvents.Add(removeOrderItemEvent);
                            break;
                        case ChangeOrderItemQuantityEvent changeOrderItemQuantityEvent:
                            response.ChangeOrderItemQuantityEvents.Add(changeOrderItemQuantityEvent);
                            break;
                        default:
                            throw new InvalidOperationException($"Неизвестный тип события {orderEvent.GetType().Name} ");                
                    }
                }


                return Ok(response);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка при запросе событий заказа.");
                return StatusCode(500);
            }
        }
    

        private async Task<IActionResult> WrapWriteOrderEvent(OrderEventBase orderEvent, CancellationToken cancellationToken)
        {
            if (orderEvent is null)
            {
                throw new ArgumentNullException(nameof(orderEvent));
            }

            try
            {
                await _orderEventsSourceRepository.SaveEvent(orderEvent, cancellationToken);
                return Ok();
            }
            catch (System.Exception ex)
            {                
                _logger.LogError(ex, "Произошла ошибка при записи события {OrderEvent}", orderEvent.GetType().Name);
                return StatusCode(500);
            }

        }       

    }
}
