using System;
using FoodDelivery.Core.Contracts.Ports.Menu.In;
using FoodDelivery.Core.Contracts.Ports.Menu.Out;
using FoodDelivery.Core.Contracts.Application.Queries.Menu;

namespace FoodDelivery.Core.Adapters.Menu.In;
/// <summary>
/// Обработчик получения меню.
/// </summary>
internal class MenuQueriesHandler : IMenuQueriesHandler
{
    private readonly IMenuRepository _menuRepository;

    public MenuQueriesHandler(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository ?? throw new ArgumentNullException(nameof(menuRepository));
    }

    /// <inheritdoc />    
    public async Task<GetAllMenuItemsResponse> Handle(GetAllMenuItemsQuery query, CancellationToken cancellationToken)
    {
        var menues = await _menuRepository.GetAllMenuItems(cancellationToken);
        return new GetAllMenuItemsResponse(menues);
    }
}
