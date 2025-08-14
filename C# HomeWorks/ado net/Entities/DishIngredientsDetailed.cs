using ado_net.Interfaces;

namespace ado_net.Entities;

class DishIngredientsDetailed : IPrintable
{
   public Dishes? Dishes { get; set; }
   public Ingredients? Ingredients { get; set; }

   public override string ToString() => $"""
        === Order Detail ===
        DishId        : {Dishes.Id}
        DishName      : {Dishes.Name}
        DishPrice     : {Dishes.Price} AZN
        IngredientId  : {Ingredients.Id}
        IngredientName: {Ingredients.Name}
        ====================
        """;

}
