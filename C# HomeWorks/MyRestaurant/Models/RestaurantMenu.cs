

using Azure.Core;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyRestaurant.Data;
using MyRestaurant.Entitites;
using MyRestaurant.Repositories;
using System;

namespace MyRestaurant.Models;

public static class RestaurantMenu
{

    public static void Run()
    {
        Console.WriteLine("Меню Ресторана");
        Console.WriteLine("1)Оформить заказ");
        Console.WriteLine("2)Админ меню\n");

        Console.Write("Какой действие выбрать? (1/2): ");

        int choice = Convert.ToInt32(Console.ReadLine());

        if (choice == 1)
        {
            TakeOrder();
        }
        else
        {
            AdminMenu();
        }

    }

    //dbContext.Customers.Add(customer);
    //dbContext.Waiters.Add(waiter);
    //dbContext.Menu.Add(menu);
    //dbContext.Orders.Add(order);
    //dbContext.OrderItems.Add(orderItem);
    //dbContext.SaveChanges();

    /*
    private static void TakeOrder()
    {
      //  using var DbContext = new RestaurantDbContext();

        using (var DbContext = new RestaurantDbContext())
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    Console.WriteLine("Создание\\Оформление клиента");
                    Console.Write("Имя клиента: ");
                    string customerName = Console.ReadLine();
                    Console.WriteLine();
                    Console.Write("Номер клиента (может быть пустым): ");
                    string? customerNumber = Console.ReadLine();

                    var customer = new Customer
                    {
                        FullName = customerName,
                    };

                    if (customerNumber != null)
                    {
                        customer.PhoneNumber = customerNumber;
                    }
                    DbContext.Customers.Add(customer);

                    // CustomerRepository.CreateCustomer(customer);
                    Console.WriteLine("Клиент создан\n");




                    Console.WriteLine("Кто из оффициантов будет оформлять заказ?");



                    var waiters = DbContext.Waiters
                        .Select(waiter => waiter)//waiter => new { waiter.Id ,waiter.FullName })   ПРОСТО ЗАПОМНИТЬ И В КОНЦЕ УДАЛИТЬ
                        .ToList();

                    foreach (var waiter in waiters)
                    {
                        Console.WriteLine($"Оффициант под номером {waiter.Id} : {waiter.FullName}");
                    }


                    Console.Write("Ваш выбор: ");
                    int waiterChoise = Convert.ToInt32(Console.ReadLine());


                    var order = new Order
                    {
                        Customer = customer,
                        Waiter = DbContext.Waiters.FirstOrDefault(waiter => waiter.Id == waiterChoise),
                        TotalPrice = 0,
                        OrderItems = new List<OrderItem>()
                    };




                    string waiterName = DbContext.Waiters
                                                 .Where(w => w.Id == waiterChoise)
                                                 .Select(w => w.FullName)
                                                 .FirstOrDefault();

                    Console.WriteLine($"Заказ оформляает: {waiterName}");

                    Console.Write("Нажмите любую кнопку чтобы продолжить");
                    Console.Read();
                    Console.Clear();

                    Console.WriteLine("Что будет заказывать клиент? (выбор по айди)");

                    MenuRepository.ShowAllMenu();


                    Console.WriteLine();
                    Console.Read();

                    var foods = new List<Menu>();


                    var orderItems = new List<OrderItem>();

                    while (true)
                    {
                        Console.Write("Блюдо под айди: ");
                        int menuChoice = Convert.ToInt32(Console.ReadLine());


                        Console.WriteLine("Вы выбрали:");
                        var food = DbContext.Menu
                            .Where(menu => menu.Id == menuChoice)
                            .Select(w => w)
                            .FirstOrDefault();

                        Console.WriteLine(food.ToString());




                        foods.Add(food);

                        Console.Write("Теперь количество: ");
                        int foodQuantity = Convert.ToInt32(Console.ReadLine());


                        var orderItem = new OrderItem
                        {
                            Menu = DbContext.Menu.FirstOrDefault(menu => menu.Id == menuChoice),
                            Order = order,
                            Quantity = foodQuantity,
                            PriceAtOrder = DbContext.Menu
                            .Where(menu => menu.Id == menuChoice)
                            .Select(menu => menu.Price)
                            .FirstOrDefault()
                        };


                        order.OrderItems.Add(orderItem);

                        // order.OrderItems.Add(orderItem);
                        //orderItems.Add(orderItem);

                        //DbContext.OrderItems.Add(orderItem);
                        //DbContext.SaveChanges();

                        Console.WriteLine("Блюдо записано");



                        Console.WriteLine("Это всё или есть еще блюдо? (y\n)");
                        string endornot = Console.ReadLine();
                        if (endornot == "y")
                        {
                            Console.Clear();
                            // всё отправить в базу данных
                            Console.WriteLine("Заказ оформлен: ");

                            Console.WriteLine(customer.ToString());
                            var waiter = DbContext.Waiters.FirstOrDefault(waiter => waiter.Id == waiterChoise);
                            Console.WriteLine(waiter.ToString());

                            foreach (var item in foods)
                            {
                                Console.WriteLine(item.ToString());
                                Console.WriteLine($"Количество: " +
                                    $"{orderItems
                                    .Where(orderItem => orderItem.Menu.Id == item.Id)
                                    .Select(orderItem => orderItem.Quantity)
                                    .FirstOrDefault()}");
                                Console.WriteLine();
                            }

                            order.TotalPrice = order.OrderItems.Sum(oi => oi.Quantity * oi.PriceAtOrder);
                            Console.WriteLine($"Общая сумма: {order.TotalPrice}");

                            DbContext.Orders.Add(order);
                            DbContext.SaveChanges();

                            foreach (var item in orderItems)
                            {
                                orderItem.OrderId = order.Id; // или: orderItem.Order = order;
                                DbContext.OrderItems.Add(orderItem);
                            }
                            DbContext.SaveChanges();

                        }
                        else if (endornot == "n")
                        {
                            Console.WriteLine("Запись еще одного блюдо");
                            continue;
                        }

                        Console.WriteLine("Как будет приосходить оплата? ");
                        Console.WriteLine("1)Картой ");
                        Console.WriteLine("2)Наличкой");

                        Console.WriteLine("Ваш выбор: ");
                        int paymentWay = Convert.ToInt32(Console.ReadLine());


                        if (paymentWay == 1)
                        {
                            Console.WriteLine("Оплат прошла наличкой, оффициант сам всё расчитает!");

                            Console.WriteLine("Клиент всё оплатил правильно? можно заканчивать заказ?");

                            string isUserPaymentEnd = Console.ReadLine();

                            if (isUserPaymentEnd == "y")
                            {
                                order.Status = "successfully completed";
                                break;
                            }
                            else if (isUserPaymentEnd == "n")
                            {
                                throw new Exception();
                            }
                        }
                        else if (paymentWay == 2)
                        {

                            Console.WriteLine("Оплат прошла картой");

                            Console.WriteLine("Клиент всё оплатил правильно? можно заканчивать заказ?");

                            string isUserPaymentEnd = Console.ReadLine();

                            if (isUserPaymentEnd == "y")
                            {
                                order.Status = "successfully completed";
                                break;
                            }
                            else if (isUserPaymentEnd == "n")
                            {
                                throw new Exception();
                            }
                        }



                    }

                    transaction.Commit();
                }
                catch(Exception)
                {
                    transaction.Rollback();
                }
            }

        }




    }

    */

    private static void TakeOrder()
    {
        using var DbContext = new RestaurantDbContext();

        using var transaction = DbContext.Database.BeginTransaction();
        try
        {



            Console.WriteLine("Создание\\Оформление клиента");
            Console.Write("Имя клиента: ");
            string customerName = Console.ReadLine();
            Console.Write("Номер клиента (может быть пустым): ");
            string? customerNumber = Console.ReadLine();
            var customer = new Customer
            {
                FullName = customerName,
                PhoneNumber = customerNumber
            };
            DbContext.Customers.Add(customer);
            Console.WriteLine("Клиент создан\n");







            Console.WriteLine("Кто из официантов будет оформлять заказ?");
            var waiters = DbContext.Waiters.ToList();
            foreach (var waiter in waiters)
            {
                Console.WriteLine($"Официант под номером {waiter.Id} : {waiter.FullName}");
            }
            Console.Write("Ваш выбор: ");
            int waiterChoice = Convert.ToInt32(Console.ReadLine());
            var selectedWaiter = DbContext.Waiters.FirstOrDefault(waiter => waiter.Id == waiterChoice);
            if (selectedWaiter == null)
            {
                throw new InvalidOperationException("Выбранный официант не найден.");
            }
            Console.WriteLine($"Заказ оформляет: {selectedWaiter.FullName}");










            Console.Write("Нажмите любую кнопку, чтобы продолжить...");
            Console.ReadLine();
            Console.Clear();



            var order = new Order
            {
                Customer = customer,
                Waiter = selectedWaiter, 
                TotalPrice = 0,
                Status = "in progress",
                OrderItems = new List<OrderItem>() 
            };




            Console.WriteLine("Что будет заказывать клиент? (выбор по айди)");
            MenuRepository.ShowAllMenu();



            while (true)
            {


                Console.Write("Блюдо под айди: ");

                int menuChoice = Convert.ToInt32(Console.ReadLine());
                var selectedMenu = DbContext.Menu.FirstOrDefault(m => m.Id == menuChoice);
                if (selectedMenu == null)
                {
                    Console.WriteLine("Блюдо с таким ID не найдено. Попробуйте еще раз.");
                    continue;
                }
                Console.WriteLine($"Вы выбрали: {selectedMenu.Name}");








                Console.Write("Теперь количество: ");
                int foodQuantity = Convert.ToInt32(Console.ReadLine());
                var orderItem = new OrderItem
                {
                    Order = order,
                    Menu = selectedMenu,
                    Quantity = foodQuantity,
                    PriceAtOrder = selectedMenu.Price

                };
                order.OrderItems.Add(orderItem);
                Console.WriteLine("Блюдо записано");






                Console.WriteLine("Это всё или есть ещё блюдо? (y/n)");
                string endOrNot = Console.ReadLine();
                if (endOrNot.ToLower() == "y")
                {
                    break;
                }

            }





            Console.Clear();
            Console.WriteLine("Заказ оформлен:");
            Console.WriteLine($"Клиент: {order.Customer.FullName}");
            Console.WriteLine($"Официант: {order.Waiter.FullName}");
            Console.WriteLine("Позиции в заказе:");
            foreach (var item in order.OrderItems)
            {
                Console.WriteLine($"- {item.Menu.Name}, количество: {item.Quantity}, цена: {item.PriceAtOrder}");
            }
            order.TotalPrice = order.OrderItems.Sum(oi => oi.Quantity * oi.PriceAtOrder);
            Console.WriteLine($"Общая сумма: {order.TotalPrice}");





            DbContext.Orders.Add(order);
            DbContext.SaveChanges();






            Console.WriteLine("Как будет происходить оплата?");
            Console.WriteLine("1) Картой");
            Console.WriteLine("2) Наличкой");
            Console.Write("Ваш выбор: ");
            int paymentWay = Convert.ToInt32(Console.ReadLine());

            order.Status = (paymentWay == 1) ? "paid by card" : "paid by cash";
            DbContext.SaveChanges();

            Console.WriteLine("Оплата прошла успешно! Заказ завершен.");

            transaction.Commit();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
            transaction.Rollback();
        }
    }

    private static void AdminMenu()
    {
        Console.WriteLine("Выберите меню");

        Console.WriteLine("1) Customer");
        Console.WriteLine("2) Menu");
        Console.WriteLine("3) Waiter");
        Console.WriteLine("4) Order");
        Console.WriteLine("5) OrderItem");

        int inputChoice = Convert.ToInt32(Console.ReadLine());

        if (inputChoice == 1)
        {
            Console.WriteLine("Что хотите сделать с сущностью Customer");
            Console.WriteLine("1)Добавить клиента");
            Console.WriteLine("2)Показать всех кто делал заказы");
            Console.WriteLine("3)Показать клиента по айди");
            Console.WriteLine("4)Изменить заказ клиента");
            Console.WriteLine("5)Удалить по айди (удалится и его заказы)");

            int adminChoice = Convert.ToInt32(Console.ReadLine());

            if (adminChoice == 1)
            {
                Console.WriteLine("Создание клиента");
                Console.Write("Имя клиента: ");
                string customerName = Console.ReadLine();
                Console.Write("Номер клиента (может быть пустым): ");
                string? customerNumber = Console.ReadLine();
                var customer = new Customer
                {
                    FullName = customerName,
                    PhoneNumber = customerNumber
                };
                CustomerRepository.CreateCustomer(customer);
                Console.WriteLine("Клиент создан\n");
            }
            else if (adminChoice == 2)
            {
               CustomerRepository.ShowAllCustomers();
                
            }
            else if (adminChoice == 3)
            {
                Console.WriteLine("Введите айди");
                int customerId = Convert.ToInt32(Console.ReadLine());
                CustomerRepository.ShowCustomerById(customerId);
            }
            else if (adminChoice == 4)
            {
                Console.WriteLine("Введите айди клиента которого хотите изменить");
                int customerId = Convert.ToInt32(Console.ReadLine());
                CustomerRepository.ShowCustomerById(customerId);

                Console.WriteLine("Теперь его новые данные ");
                Console.Write("Имя клиента: ");
                string customerName = Console.ReadLine();
                Console.Write("Номер клиента (может быть пустым): ");
                string? customerNumber = Console.ReadLine();
                var customer = new Customer
                {
                    Id = customerId,
                    FullName = customerName,
                    PhoneNumber = customerNumber
                };

                CustomerRepository.UpdateCustomer(customer);
            }
            else if (adminChoice == 5)
            {

                Console.WriteLine("Введите айди клиента которого хотите удалить");
                int customerId = Convert.ToInt32(Console.ReadLine());
                CustomerRepository.DeleteCustomer(customerId);
                Console.WriteLine("Клиент: ");
                CustomerRepository.ShowCustomerById(customerId);
                Console.WriteLine("Удалён!");
            }
            else if (adminChoice == 6)
            {
                CustomerRepository.DeleteAllCustomers();
            }
           

        }
        else if(inputChoice == 2) 
        {
            Console.WriteLine("Что хотите сделать с сущностью Menu");
            Console.WriteLine("1)Добавить меню");
            Console.WriteLine("2)Показать всё меню");
            Console.WriteLine("3)Показать меню по айди");
            Console.WriteLine("4)Изменить меню");
            Console.WriteLine("5)Удалить меню по айди");
            Console.WriteLine("6)Удалить всё меню");

            int adminChoice = Convert.ToInt32(Console.ReadLine());

            if (adminChoice == 1)
            {
                Console.WriteLine("Введите имя блюда:");
                string foodName = Console.ReadLine();
                Console.WriteLine("Введите цену:");
                decimal price = Convert.ToDecimal(Console.ReadLine());
                Console.WriteLine("Введите категорию еды");
                string category = Console.ReadLine();
                Console.WriteLine("Введите описание еды");
                string description = Console.ReadLine();
                var menu = new Menu
                {
                    Name = foodName,
                    Price = price,
                    Category = category,
                    Description = description
                };
                MenuRepository.CreateMenu(menu);
            }
            else if (adminChoice == 2)
            {
                MenuRepository.ShowAllMenu();
            }
            else if (adminChoice == 3)
            {
                Console.WriteLine("Введите айди блюда");
                int foodId = Convert.ToInt32(Console.ReadLine());
                MenuRepository.ShowMenuById(foodId);
            }
            else if (adminChoice == 4)
            {
                Console.WriteLine("Введите айди блюда которое хотите изменить ");
                int foodId = Convert.ToInt32(Console.ReadLine());
                MenuRepository.ShowMenuById(foodId);

                Console.WriteLine("Новые данные ");
                Console.WriteLine("Введите имя блюда:");
                string foodName = Console.ReadLine();
                Console.WriteLine("Введите цену:");
                decimal price = Convert.ToDecimal(Console.ReadLine());
                Console.WriteLine("Введите категорию еды");
                string category = Console.ReadLine();
                Console.WriteLine("Введите описание еды");
                string description = Console.ReadLine();
                var menu = new Menu
                {
                    Id = foodId,
                    Name = foodName,
                    Price = price,
                    Category = category,
                    Description = description
                };
                MenuRepository.UpdateMenu(menu);

            }
            else if (adminChoice == 5)
            {
                Console.WriteLine("Введите айди блюда которое хотите удалить ");
                int foodId = Convert.ToInt32(Console.ReadLine());
                MenuRepository.ShowMenuById(foodId);
                MenuRepository.DeleteMenu(foodId);
            }
            else if (adminChoice == 6)
            {
                MenuRepository.DeleteAllMenu();
            }
        }
        else if (inputChoice == 3)
        {
            Console.WriteLine("Что хотите сделать с сущностью Waiter");
            Console.WriteLine("1)Добавить оффицианта");
            Console.WriteLine("2)Показать всех оффициантов");
            Console.WriteLine("3)Показать оффицианта по айди");
            Console.WriteLine("4)Изменить оффицианта");
            Console.WriteLine("5)Удалить по айди");
            Console.WriteLine("6)Удалить всех");

            int adminChoice = Convert.ToInt32(Console.ReadLine());

            if (adminChoice == 1)
            {
                Console.WriteLine("Создание оффицианта");
                Console.Write("Имя оффицианта: ");
                string waiterName = Console.ReadLine();
                Console.Write("Зарплата оффицианта, в долларах: ");
                decimal waiterPrice = Convert.ToDecimal(Console.ReadLine());
                var waiter = new Waiter
                {
                    FullName = waiterName,
                    Salary = waiterPrice
                };
                Console.WriteLine("оффициант создан\n");
                WaiterRepository.CreateWaiter(waiter);
            }
            else if (adminChoice == 2)
            {
                WaiterRepository.ShowAllWaiters();
            }
            else if (adminChoice == 3)
            {
                Console.WriteLine("Введите айди");
                int waiterId = Convert.ToInt32(Console.ReadLine());
                WaiterRepository.ShowWaiterById(waiterId);
            }
            else if (adminChoice == 4)
            {
                Console.WriteLine("Введите айди оффицианта которого хотите изменить");
                int waiterId = Convert.ToInt32(Console.ReadLine());
                WaiterRepository.ShowWaiterById(waiterId);

                Console.WriteLine("Теперь его новые данные ");
                Console.Write("Имя оффицианта: ");
                string waiterName = Console.ReadLine();
                Console.Write("Зарплата оффицианта, в долларах: ");
                decimal waiterPrice = Convert.ToDecimal(Console.ReadLine());
                var waiter = new Waiter
                {
                    Id = waiterId,
                    FullName = waiterName,
                    Salary = waiterPrice
                };
                WaiterRepository.UpdateWaiter(waiter);
            }
            else if (adminChoice == 5)
            {
                Console.WriteLine("Введите айди оффицианта которого хотите удалить");
                int waiterId = Convert.ToInt32(Console.ReadLine());
                WaiterRepository.DeleteWaiter(waiterId);
            }
            else if (adminChoice == 6)
            {
                WaiterRepository.DeleteAllWaiters();
            }

        }
        else if (inputChoice == 4)
        {
            Console.WriteLine("Что хотите сделать с сущностью Order");
            Console.WriteLine("1)Добавить заказ на существующего клиента");
            Console.WriteLine("2)Показать все заказы");
            Console.WriteLine("3)Показать заказ по айди");
            Console.WriteLine("4)Изменить заказ");
            Console.WriteLine("5)Удалить по айди");
            Console.WriteLine("6)Удалить всё");

            int adminChoice = Convert.ToInt32(Console.ReadLine());

            if (adminChoice == 1)
            {
                Console.WriteLine("Введите айди клиента чей заказ хотите добавить");
                int customerId  = Convert.ToInt32(Console.ReadLine());
                var customer = CustomerRepository.CustomerById(customerId);

                Console.WriteLine("Введите айди оффицианта который оформит этот заказ");
                int waiterId = Convert.ToInt32(Console.ReadLine());
                var waiter = WaiterRepository.WaiterById(waiterId);

                var order = new Order
                {
                    Customer = customer,
                    Waiter = waiter,
                    TotalPrice = 0,
                    OrderItems = new List<OrderItem>()
                };
                OrderRepository.CreateOrder(order);
            }
            else if (adminChoice == 2)
            {
                OrderRepository.ShowAllOrders();
            }
            else if (adminChoice == 3)
            {
                Console.WriteLine("Введите айди заказа");
                int orderId = Convert.ToInt32(Console.ReadLine());

                OrderRepository.ShowOrderById(orderId);

            }
            else if (adminChoice == 4)
            {
                Console.WriteLine("Введите айди заказа который хотите изменить");
                int orderId = Convert.ToInt32(Console.ReadLine());

                OrderRepository.ShowOrderById(orderId);

                Console.WriteLine("Теперь его новые данные ");

                Console.WriteLine("Введите айди клиента чей заказ хотите добавить");
                int customerId = Convert.ToInt32(Console.ReadLine());
                var customer = CustomerRepository.CustomerById(customerId);

                Console.WriteLine("Введите айди оффицианта который оформит этот заказ");
                int waiterId = Convert.ToInt32(Console.ReadLine());
                var waiter = WaiterRepository.WaiterById(waiterId);

                var order = new Order
                {
                    Id = orderId,
                    Customer = customer,
                    Waiter = waiter,
                    TotalPrice = 0,
                    OrderItems = new List<OrderItem>()
                };
                OrderRepository.UpdateOrder(order);
            }
            else if (adminChoice == 5)
            {
                Console.WriteLine("Введите айди заказа которого удалить");
                int orderId = Convert.ToInt32(Console.ReadLine());

                OrderRepository.DeleteOrder(orderId);
            }
            else if (adminChoice == 5)
            {
                OrderRepository.DeleteAllOrders();
            }
        }
        else if (inputChoice == 5)
        {
            Console.WriteLine("Что хотите сделать с сущностью OrderItem");
            Console.WriteLine("1)Добавить блюдо к заказу");
            Console.WriteLine("2)Показать все блюда заказа");
            Console.WriteLine("3)Показать блюдо заказа по айди");
            Console.WriteLine("4)Изменить блюдо конкретного заказа");
            Console.WriteLine("5)Удалить всё");

            int adminChoice = Convert.ToInt32(Console.ReadLine());

            if (adminChoice == 1)
            {
            }
            else if (adminChoice == 2)
            {
            }
            else if (adminChoice == 3)
            {
            }
            else if (adminChoice == 4)
            {
            }
            else if (adminChoice == 5)
            {
            }
        }
        order.TotalPrice = order.OrderItems.Sum(oi => oi.Quantity * oi.PriceAtOrder);
    }



}
