using System;
using System.Data;
using System.Numerics;
using System.Text.Json.Serialization;
using LinqToDB;
using LinqToDB.Mapping;

namespace FoodDelivery.PostgreSql.Application.Models.Orders;

[Table("t_order_events")]
public class OrderEventEntity
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    [Column("order_event_id")]
    [PrimaryKey, Identity]
    public long EventId { get; set;} 

    /// <summary>
    /// Ид. заказа.
    /// </summary>
    [Column("order_id")]
    public int OrderId { get; set;}

    /// <summary>
    /// Идентификатор 
    /// </summary>
    [Column("client_id")]
    public int ClientId { get; set;}

    /// <summary>
    /// Дата события.
    /// </summary>
    [Column("occurred_on")]
    public DateTime OccuredOn { get; set;}

    /// <summary>
    /// Тип события.
    /// </summary>
    [Column("order_event_type")]
    public OrderEventType OrderEventType { get; set;}

    /// <summary>
    /// Данные события.
    /// </summary>
    [Column("order_event_data", DataType = DataType.BinaryJson)]
    
    public OrderEventData OrderEventData { get; set;}
}
