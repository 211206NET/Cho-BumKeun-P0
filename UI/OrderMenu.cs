// using CustomExceptions;
// namespace UI;

// public class OrderMenu : IMenu
// {
//     private IBL _bl;

//     public OrderMenu(IBL bl)
//     {
//         _bl = bl;
//     }

//     public void Start()
//     {
//         while (!exit)
//         {
//             Console.WriteLine("[[[[[[[[[[[[[[[[[ Order Menu ]]]]]]]]]]]]]]]]]");
//             Console.WriteLine("[1] View your order by date (old to new)");
//             Console.WriteLine("[2] View your order by date (new to old)");
//             Console.WriteLine("[3] View your order by price (low to high)");
//             Console.WriteLine("[4] View your order by price (high to low)");
//             Console.WriteLine("[5] View storefront order by date (old to new)");
//             Console.WriteLine("[6] View storefront order by date (new to old)");
//             Console.WriteLine("[7] View storefront order by price (low to high)");
//             Console.WriteLine("[8] View storefront order by price (high to low)");
//             Console.WriteLine("[x] Back to User Menu");

//             switch (Console.ReadLine())
//             {
//                 case "1":
//                     ViewAllCustomerOrders();
//                 break;
//                 case "2":
//                     //ViewAllProducts();
//                 break;
//                 case "3":
//                     //PlaceOrder(existing.Id);
//                 break;
//                 case "4":
//                     //ViewAllOrders(existing);
//                 break;
//                 case "5":
//                 break;
//                 case "6":
//                 break;
//                 case "x":
//                     exit = true;
//                 break;
//                 default:
//                     Console.WriteLine("Invalid input");
//                 break;
//             }
//         }
//     }

//     private void ViewAllCustomerOrders(Customer user)
//     {
//         List<Order> allOrders = _bl.GetAllOrders(user.Id);
//         if(allOrders.Count == 0)
//         {
//             Console.WriteLine("You have not made an order");
//         }
//         else
//         {
//             Console.WriteLine("Here are your order details");
//             Console.WriteLine("==================================");
//             foreach(Order ord in allOrders)
//             {
//                 Console.WriteLine(ord.ToString());
//             }
//         }
//     }
// }