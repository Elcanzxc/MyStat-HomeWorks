using ado_net.Interfaces;

namespace ado_net.Entities;

    class OrderDishesDetailed : IPrintable
{
        public Orders? Order { get; set; }

        public Dishes? Dishes { get; set; }

        public required int Quantity { get; set; }

    public override string ToString() => $"""
        =============== Order With Dish ==================
        OrderId    : {Order.Id}
        CustomerId : {Order.CustomerId}
        OrderDate  : {Order.OrderDate:yyyy-MM-dd}
        DishId     : {Dishes.Id}
        DishName   : {Dishes.Name}
        DishPrice  : {Dishes.Price} AZN
        Quantity   : {Quantity}
        ==================================================
        """;


}

