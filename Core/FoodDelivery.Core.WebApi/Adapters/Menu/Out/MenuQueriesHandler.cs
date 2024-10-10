using System;
using FoodDelivery.Core.Contracts.Domain.Aggregates.Menu;
using FoodDelivery.Core.Contracts.Ports.Menu.Out;
using FoodDelivery.Core.WebApi.Ports.Menu.In;

namespace FoodDelivery.Core.WebApi.Adapters.Menu.Out;
    internal class MenuRepository : IMenuRepository
    {
        private readonly IMenuStorage _menuStorage;

        public MenuRepository(IMenuStorage menuStorage)
        {
            _menuStorage = menuStorage ?? throw new ArgumentNullException(nameof(menuStorage));
        }

        public Task<List<MenuItem>> GetAllMenuItems(CancellationToken cancellationToken)
            => _menuStorage.GetMenu(cancellationToken);       

    }

