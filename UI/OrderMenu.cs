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
//                     ViewAllStores();
//                 break;
//                 case "2":
//                     ViewAllProducts();
//                 break;
//                 case "3":
//                     PlaceOrder(existing.Id);
//                 break;
//                 case "4":
//                     ViewAllOrders(existing);
//                 break;
//                 case "5":
//                 break;
//                 case "6":
//                     MenuFactory.GetMenu("order").Start();
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

//     private void PlaceOrder(int userId)
//     {
//         List<Store> allStores = _bl.GetAllStores();
//         Console.WriteLine("Select a store to place orders for");
//         Console.WriteLine("==================================");
//         for(int i = 0; i < allStores.Count; i++)
//         {
//             Console.WriteLine($"[{i}] {allStores[i].ToString()}");
//         }
//         string? input = Console.ReadLine();
//         int selection;
//         bool parseSuccess = Int32.TryParse(input, out selection);
//         if(parseSuccess && selection >= 0 && selection < allStores.Count)
//         {
//             List<Product> allProducts = _bl.GetAllProducts();
//             Console.WriteLine("Select a product to purchase");
//             Console.WriteLine("==================================");
//             for(int i = 0; i < allProducts.Count; i++)
//             {
//                 Console.WriteLine($"[{i}] {allProducts[i].ToString()}");
//             }
//             string? input2 = Console.ReadLine();
//             int selection2;
//             bool parseSuccess2 = Int32.TryParse(input2, out selection2);
//             if(parseSuccess2 && selection2 >= 0 && selection2 < allProducts.Count)
//             {
//                 createOrder:
//                 Console.WriteLine("Order quantity (max 10): ");
//                 int quantity;
//                 bool success = Int32.TryParse(Console.ReadLine(), out quantity);
//                 try
//                 {
//                     _bl.AddOrder(allStores[selection].Id, allProducts[selection2].Id, allStores[selection].Name, allProducts[selection2].Title, quantity, allProducts[selection2].Price, userId);
//                     _bl.UpdateInventory(allProducts[selection2].Id, allProducts[selection2].Inventory-quantity);
//                     Console.WriteLine("Your order has been received!");
//                 }
//                 catch(InputInvalidException ex)
//                 {
//                     Console.WriteLine(ex.Message);
//                     goto createOrder;
//                 }
//             }
//         }
//     }

//     private void ViewAllOrders(Customer user)
//     {
//         List<Order> allOrders = _bl.GetAllOrders(user.Id);
//         Console.WriteLine("Select a store to place orders for");
//         Console.WriteLine("==================================");
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