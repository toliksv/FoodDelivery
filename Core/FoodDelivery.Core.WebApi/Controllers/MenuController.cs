using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FoodDelivery.Core.Contracts.Ports.Menu.In;
using FoodDelivery.Core.Contracts.Application.Queries.Menu;


namespace FoodDelivery.Core.WebApi.Controllers
{
    [Route("api/core/menu")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuQueriesHandler _menuQueriesHandler;
        private readonly ILogger<MenuController> _logger;

        public MenuController(IMenuQueriesHandler menuQueriesHandler, ILogger<MenuController> logger)
        {
            _menuQueriesHandler = menuQueriesHandler ?? throw new ArgumentNullException(nameof(menuQueriesHandler));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetMenu(CancellationToken cancellationToken)
        {
            try
            {
                var response = await _menuQueriesHandler.Handle(new GetAllMenuItemsQuery(), cancellationToken);
                if (response is null || response.Menu is null || response.Menu.Count == 0)
                {
                    return NoContent();         
                }
    
                return Ok(response);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка при запросе меню");
                return StatusCode(500);                
            }
        }
    }
}
