using FoodDelivery.Core.Contracts.Ports.Menu.Out;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.PostgreSql.WebApi.Controllers
{
    [Route("api/postgre-sql-adapter/menu")]
    [ApiController]
    public class MenuPostgreSqlAdapterController : ControllerBase
    {
        private readonly IMenuRepository _menuRepository;
        private readonly ILogger<MenuPostgreSqlAdapterController> _logger;

        public MenuPostgreSqlAdapterController(IMenuRepository menuRepository, ILogger<MenuPostgreSqlAdapterController> logger)
        {
            _menuRepository = menuRepository ?? throw new ArgumentNullException(nameof(menuRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("list-all")]
        public async Task<IActionResult> GetAllMenuItems(CancellationToken cancellationToken)
        {
            try
            {
                var result = await _menuRepository.GetAllMenuItems(cancellationToken);
                if (result == null)
                {
                    return NoContent();
                }                
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Ошибка выдачи меню");
                return StatusCode(500);                
            }
        }
    }
}
