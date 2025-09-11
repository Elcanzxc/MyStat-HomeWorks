
using MyRestaurant.Data;
using Azure;
using Microsoft.EntityFrameworkCore;
using MyRestaurant.Data.Configurations;
using MyRestaurant.Entitites;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Dapper;
using MyRestaurant.Models;


// Дороботать ресторан меню тайк ордер!

// Убрать уник у номер телефона ведь он может придти еще раз

class Program
{
    // Работа приложения запускается от лица работников этого ресторана
    // Представим что это монитор который использует либо оффициант либо управляющий который зайдет в админ панель

    // Если выбран оформить клиента то нужны данные customer ( оффициант который будет оформлять заказ сам должен себя выбрать ) 
    // Если админ панель то там можно создать оффицианта который будет оформлять клиентов

    // У меня было мало практики по EfCore , а в задании сказали чтобы использовался еще и Даппер, чтобы чисто удовлетворить требование задания только Класс CustomerRepository написан на Даппере
    // Весь остальной код на EfCore , чтобы я больше изучал возможность EfCore + методами Linq для него

    static void Main(string[] args)
    {
        //var dbContext = new RestaurantDbContext();
        try
        {
            while (true)
            {
                RestaurantMenu.Run();
            }

        }
        catch (Exception ex) 
        {
            Console.WriteLine(ex.Message);
        }

     

        //var customer = new Customer
        //{
        //    FullName = "Elcan Aliyev",
        //    PhoneNumber = "+994554540506"
        //};

        //var waiter = new Waiter
        //{
        //    FullName = "Vusal Sefikhanli",
        //    Salary = 300.00M
        //};


        //var menu = new Menu
        //{
        //    Name = "Merci",
        //    Price = 2.50M,
        //    Category = "Sup",
        //    Description = "Merciden hazirlanmis sup donernen yaxsi gedir"
        //};


        //var order = new Order
        //{
        //    Customer = customer,
        //    Waiter = waiter,
        //    TotalPrice = 0,
        //    OrderItems = new List<OrderItem>()
        //};

        //var orderItem = new OrderItem
        //{
        //    Menu = menu,
        //    Order = order,
        //    Quantity = 2,
        //    PriceAtOrder = menu.Price
        //};


        //order.OrderItems.Add(orderItem);
        //order.TotalPrice = order.OrderItems.Sum(oi => oi.Quantity * oi.PriceAtOrder);


        //dbContext.Customers.Add(customer);
        //dbContext.Waiters.Add(waiter);
        //dbContext.Menu.Add(menu);
        //dbContext.Orders.Add(order);
        //dbContext.OrderItems.Add(orderItem);
        //dbContext.SaveChanges();



    
    }
}