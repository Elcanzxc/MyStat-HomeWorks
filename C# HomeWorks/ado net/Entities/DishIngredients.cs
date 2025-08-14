
using ado_net.Interfaces;

namespace ado_net.Entities;

class DishIngredients : IPrintable
{
    public required int DishId { get; set; }

    public required int IngredientId { get; set; }

    public override string ToString() => $"""
        ============ Dish-Ingredient Info ============
        DishId       : {DishId}
        IngredientId : {IngredientId}
        ==============================================
        """;
}
