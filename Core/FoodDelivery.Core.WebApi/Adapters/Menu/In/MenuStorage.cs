using System;
using FoodDelivery.Core.Contracts.Domain.Aggregates.Menu;
using FoodDelivery.Core.WebApi.Ports.Menu.In;
using FoodDelivery.Core.WebApi.Ports.Menu.Out;
using Polly;

namespace FoodDelivery.Core.WebApi.Adapters.Menu.In;

internal class MenuStorage : IMenuStorage
{
    private readonly IMenuStorageController _menuStorageController;
    private readonly ResiliencePipeline  _reilienePipeline;

    public MenuStorage(IMenuStorageController menuStorageController, [FromKeyedServices("main")]ResiliencePipeline reilienePipeline)
    {
        _menuStorageController = menuStorageController ?? throw new ArgumentNullException(nameof(menuStorageController));
        _reilienePipeline = reilienePipeline ?? throw new ArgumentNullException(nameof(reilienePipeline));
    }

    public async Task<List<MenuItem>> GetMenu(CancellationToken cancellationToken)
    {
        var menu = await _reilienePipeline.ExecuteAsync(async tkn => 
        {
            var menu = await _menuStorageController.GetAll(tkn);
            return menu;
        });
        return menu;
    }
}
