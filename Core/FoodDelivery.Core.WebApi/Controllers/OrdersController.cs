using System.Net;
using FoodDelivery.Core.Contracts.Application.Queries.Orders;
using FoodDelivery.Core.Contracts.Domain.Aggregates.Orders;
using FoodDelivery.Core.Contracts.Application.Commands.Orders.Web;
using FoodDelivery.Core.Contracts.Ports.Orders.In;
using FoodDelivery.Core.WebApi.Application.Commands.Orders.Mappers;
using FoodDelivery.Core.WebApi.Application.Queries.Orders.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Core.WebApi.Controllers
{
    [Route("api/core/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersCommandHandler _ordersCommandHandler;
        private readonly IOrderCommandMapper _orderCommandMapper;
        private readonly IOrdersQueriesHandler _ordersQueriesHandler;
        private readonly IOrdersQueryMapper _ordersQueryMapper;


        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IOrdersCommandHandler ordersCommandHandler, IOrdersQueriesHandler ordersQueriesHandler, ILogger<OrdersController> logger, IOrdersQueryMapper ordersQueryMapper, IOrderCommandMapper orderCommandMapper)
        {
            _ordersCommandHandler = ordersCommandHandler ?? throw new ArgumentNullException(nameof(ordersCommandHandler));
            _ordersQueriesHandler = ordersQueriesHandler ?? throw new ArgumentNullException(nameof(ordersQueriesHandler));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _ordersQueryMapper = ordersQueryMapper ?? throw new ArgumentNullException(nameof(ordersQueryMapper));
            _orderCommandMapper = orderCommandMapper ?? throw new ArgumentNullException(nameof(orderCommandMapper));
        }

        [HttpPost("command/create")]
        public async Task<IActionResult> CreateOrder([FromBody]CreateOrderCommandRequest createOrderCommandRequest, CancellationToken cancellationToken)
        {
            if (createOrderCommandRequest is null)
            {
                return BadRequest();
            }

            try
            {
                var createCommand = _orderCommandMapper.Map(createOrderCommandRequest);
                await _ordersCommandHandler.Handle(createCommand, cancellationToken);
                return Ok();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка при создании заказа");
                return StatusCode(500);
            }
        }

        [HttpPut("command/set-status")]
        public async Task<IActionResult> SetOrderStatus([FromBody]SetOrderStatusCommandRequest setOrderStatusCommandRequest, CancellationToken cancellationToken)
        {
            if (setOrderStatusCommandRequest is null)
            {
                return BadRequest();
            }

            try
            {
                var createCommand = _orderCommandMapper.Map(setOrderStatusCommandRequest);
                await _ordersCommandHandler.Handle(createCommand, cancellationToken);
                return Ok();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка при установлении статуса заказу.");
                return StatusCode(500);
            }
        }

        
        [HttpPost("command/add-items")]
        public async Task<IActionResult> AddItemsToOrder([FromBody]AddItemsToOrderCommandRequest addItemsToOrderCommandRequest, CancellationToken cancellationToken)
        {
            if (addItemsToOrderCommandRequest is null)
            {
                return BadRequest();
            }

            try
            {
                var createCommand = _orderCommandMapper.Map(addItemsToOrderCommandRequest);
                await _ordersCommandHandler.Handle(createCommand, cancellationToken);
                return Ok();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка при добавлении блюда в заказ.");
                return StatusCode(500);
            }
        }

        [HttpDelete("command/remove-items")]
        public async Task<IActionResult> RemoveItemFromOrder([FromBody]RemoveItemsFromOrderCommandRequest removeItemsFromOrderCommandRequest, CancellationToken cancellationToken)
        {
            if (removeItemsFromOrderCommandRequest is null)
            {
                return BadRequest();
            }

            try
            {
                var createCommand = _orderCommandMapper.Map(removeItemsFromOrderCommandRequest);
                await _ordersCommandHandler.Handle(createCommand, cancellationToken);
                return Ok();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка при удалении блюда из заказа.");
                return StatusCode(500);
            }
        }

        [HttpPut("command/change-item-quantity")]
    public async Task<IActionResult> ChangeOrderItemQuantity([FromBody]ChangeOrderItemQuantityCommandRequest changeOrderItemQuantityCommandRequest, CancellationToken cancellationToken)
        {
            if (changeOrderItemQuantityCommandRequest is null)
            {
                return BadRequest();
            }

            try
            {
                var createCommand = _orderCommandMapper.Map(changeOrderItemQuantityCommandRequest);
                await _ordersCommandHandler.Handle(createCommand, cancellationToken);
                return Ok();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка при количества блюда в заказе.");
                return StatusCode(500);
            }
        }


        [HttpGet("query/by-order-id/{orderId}")]        
        public async Task<IActionResult> GetOrderByOrderId([FromRoute()]int orderId, CancellationToken cancellationToken)
        {
            try
            {
                var query = new GetOrderByIdQuery(orderId);
                var response = await _ordersQueriesHandler.Handle(query, cancellationToken);
                if (response?.Order is null)
                {
                    return NoContent();
                }

                return Ok(_ordersQueryMapper.Map(response));  
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении заказа по его идентификатору:{OrderId}", orderId);
                return StatusCode(500);                
            }          
        }

        [HttpGet("query/by-client-id/{clientId}")]
        public async Task<IActionResult> GetOrdersByClientId([FromRoute]int clientId, CancellationToken cancellationToken)
        {
            try
            {
                            var query = new GetClientOrdersQuery(clientId);
                            var response = await _ordersQueriesHandler.Handle(query, cancellationToken);
                            if (response?.Orders is null || response.Orders.Count == 0)
                            {
                                return NoContent();
                            }
                
                            return Ok(_ordersQueryMapper.Map(response));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении заказов клиента:{ClientId}", clientId);
                return StatusCode(500);
            }
        }
    }
}
