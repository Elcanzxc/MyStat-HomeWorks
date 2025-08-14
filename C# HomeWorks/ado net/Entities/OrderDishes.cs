using ado_net.Interfaces;

namespace ado_net.Entities;

    class OrderDishes : IPrintable
{
      public required int OrderId { get; set; }

      public required int DishId { get; set; }

      public required int Quantity { get; set; }

    public override string ToString() => $"""
        ========= Order-Dish Info =========
        OrderId : {OrderId}
        DishId  : {DishId}
        Quantity: {Quantity}
        ===================================
        """;
}
