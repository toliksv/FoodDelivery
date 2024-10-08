using System;
using FoodDelivery.PostgreSql.Application.Models.Menu;
using FoodDelivery.PostgreSql.Application.Models.Orders;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Mapping;
using Newtonsoft.Json;


namespace FoodDelivery.PostgreSql.Application.Models;

/// <summary>
/// Подключение к БД.
/// </summary>
public class FoodDeliveryDataConnection : DataConnection
{
    public FoodDeliveryDataConnection(DataOptions<FoodDeliveryDataConnection> options)
        :base(options.Options)
        {}

    public ITable<OrderEventEntity> OrderEvents => this.GetTable<OrderEventEntity>();

    public ITable<MenuItemEntity> MenuItems => this.GetTable<MenuItemEntity>();

    public static MappingSchema ConfigureMappings()
    {
        var schema = new MappingSchema();
        var builder = new FluentMappingBuilder(schema);
        builder.Entity<OrderEventEntity>()
            .Property(x=>x.OrderEventData)
             .HasDbType("jsonb")
             .HasConversionFunc(JsonConvert.SerializeObject, dt=> JsonConvert.DeserializeObject<OrderEventData>(dt));
        builder.Build();     
        return schema;
    }

}
