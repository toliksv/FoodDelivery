using LinqToDB.Mapping;

namespace FoodDelivery.PostgreSql.Application.Models.Menu;

/// <summary>
///     Меню ресторана.
/// </summary>
[Table("t_menu")]
public class MenuItemEntity
{
    /// <summary>
    ///     Идентификатор блюда.
    /// </summary>
    [Column("menu_item_id")]
    [PrimaryKey]
    [Identity]
    public int MenuItemId { get; set; }

    /// <summary>
    ///     Наименование блюда.
    /// </summary>
    [Column("menu_item_name")]
    public string Name { get; set; }

    /// <summary>
    ///     Описание блюда.
    /// </summary>
    [Column("description")]
    public string Description { get; set; }

    /// <summary>
    ///     Цена за штуку.
    /// </summary>
    [Column("price")]
    public decimal Price { get; set; }
}