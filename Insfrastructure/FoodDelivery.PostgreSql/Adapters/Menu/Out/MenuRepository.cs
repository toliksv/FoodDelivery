using FoodDelivery.Core.Contracts.Domain.Aggregates.Menu;
using FoodDelivery.Core.Contracts.Ports.Menu.Out;
using FoodDelivery.PostgreSql.Application.Mapping.Menu;
using LinqToDB;
using Microsoft.Extensions.Logging;
using FoodDelivery.PostgreSql.Application.Models;

namespace FoodDelivery.PostgreSql.Adapters.Menu.Out;

/// <inheritdoc cref="IMenuRepository"/> 
internal class MenuRepository : IMenuRepository
{
    private readonly FoodDeliveryDataConnection _dbContext;
    private readonly IMenuItemMapper _menuItemMapper;
    private ILogger<MenuRepository> _logger;

    public MenuRepository(FoodDeliveryDataConnection dbContext, IMenuItemMapper menuItemMapper, ILogger<MenuRepository> logger)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _menuItemMapper = menuItemMapper ?? throw new ArgumentNullException(nameof(menuItemMapper));
    }

    public async Task<List<MenuItem>> GetAllMenuItems(CancellationToken cancellationToken)
    {
        var menuItemEntities = await _dbContext.MenuItems
                                        .ToListAsync(cancellationToken)
                                        .ConfigureAwait(false);
        if (!menuItemEntities.Any())
        {
            _logger.LogWarning("Не найдено меню");
        }

        return menuItemEntities
                    .Select(_menuItemMapper.Map)
                    .ToList();
    }
}
