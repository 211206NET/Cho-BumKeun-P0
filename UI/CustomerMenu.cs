using CustomExceptions;
namespace UI;

public class CustomerMenu : IMenu
{
    private IBL _bl;

    public CustomerMenu(IBL bl)
    {
        _bl = bl;
    }

    public void Start()
    {
        bool exit = false;
        Console.WriteLine("Username:");
        string? input = Console.ReadLine();
        Console.WriteLine("Password:");
        string? pInput = Console.ReadLine();
        Customer existing = _bl.Login(new Customer {UserName = input, Password = pInput});
        if (existing.Id <= 0)
        {
            Console.WriteLine("User does not exist");
            return;
        }
        else
        {
            if (existing.Password == pInput)
            {
                goto accessMenu;
            }
            else
            {
                Console.WriteLine("Incorrect password");
                return;
            }
        }
        accessMenu:
        while (!exit)
        {
            Console.WriteLine("[[[[[[[[[[[[[[[[[ User Menu ]]]]]]]]]]]]]]]]]");
            Console.WriteLine("[1] View store locations");
            Console.WriteLine("[2] View all products");
            Console.WriteLine("[3] Place order");
            Console.WriteLine("[4] View your order history");
            Console.WriteLine("[5] View storefront order history");
            Console.WriteLine("[6] View your order history");
            Console.WriteLine("[x] Logout to Main Menu");

            switch (Console.ReadLine())
            {
                case "1":
                    ViewAllStores();
                break;
                case "2":
                    ViewAllProducts();
                break;
                case "3":
                    PlaceOrder(existing.Id);
                break;
                case "4":
                    ViewAllCustomerOrders(existing);
                break;
                case "5":
                    //ViewAllStorefrontOrders(existing, Store);
                break;
                case "6":
                    //MenuFactory.GetMenu("order").Start();
                break;
                case "x":
                    exit = true;
                break;
                default:
                    Console.WriteLine("Invalid input");
                break;
            }
        }
    }

    private void ViewAllStores()
    {
        List<Store> allStores = _bl.GetAllStores();
        if(allStores.Count == 0)
        {
            Console.WriteLine("No stores found");
        }
        else
        {
            Console.WriteLine("Here are all your stores!");
            foreach(Store sto in allStores)
            {
                Console.WriteLine(sto.ToString());
            }
        }
    }

    private void ViewAllProducts()
    {
        List<Product> allProducts = _bl.GetAllProducts();
        if(allProducts.Count == 0)
        {
            Console.WriteLine("No products found");
        }
        else
        {
            Console.WriteLine("Here are all the products!");
            foreach(Product prod in allProducts)
            {
                Console.WriteLine(prod.ToString());
            }
        }
    }

    private void PlaceOrder(int userId)
    {
        List<Store> allStores = _bl.GetAllStores();
        Console.WriteLine("Select a store to place orders for");
        Console.WriteLine("==================================");
        for(int i = 0; i < allStores.Count; i++)
        {
            Console.WriteLine($"[{i}] {allStores[i].ToString()}");
        }
        string? input = Console.ReadLine();
        int selection;
        bool parseSuccess = Int32.TryParse(input, out selection);
        if(parseSuccess && selection >= 0 && selection < allStores.Count)
        {
            List<Product> allProducts = _bl.GetAllProducts();
            Console.WriteLine("Select a product to purchase");
            Console.WriteLine("==================================");
            for(int i = 0; i < allProducts.Count; i++)
            {
                Console.WriteLine($"[{i}] {allProducts[i].ToString()}");
            }
            string? input2 = Console.ReadLine();
            int selection2;
            bool parseSuccess2 = Int32.TryParse(input2, out selection2);
            if(parseSuccess2 && selection2 >= 0 && selection2 < allProducts.Count)
            {
                createOrder:
                Console.WriteLine("Order quantity (max 10): ");
                int quantity;
                bool success = Int32.TryParse(Console.ReadLine(), out quantity);
                try
                {
                    _bl.AddOrder(allStores[selection].Id, allProducts[selection2].Id, allStores[selection].Name, allProducts[selection2].Title, quantity, allProducts[selection2].Price, userId);
                    _bl.UpdateInventory(allProducts[selection2].Id, allProducts[selection2].Inventory-quantity);
                    Console.WriteLine("Your order has been received!");
                }
                catch(InputInvalidException ex)
                {
                    Console.WriteLine(ex.Message);
                    goto createOrder;
                }
            }
        }
    }

    private void ViewAllCustomerOrders(Customer user)
    {
        List<Order> allOrders = _bl.GetAllOrders(user.Id);
        if(allOrders.Count == 0)
        {
            Console.WriteLine("You have not made an order");
        }
        else
        {
            Console.WriteLine("Here are your order details");
            Console.WriteLine("==================================");
            foreach(Order ord in allOrders)
            {
                Console.WriteLine(ord.ToString());
            }
        }
    }

    // private void ViewAllStorefrontOrders(Customer user, Store id)
    // {
    //     List<Store> allStores = _bl.GetAllStores();
    //     Console.WriteLine("Here are all your storefront orders");
    //     foreach(Store sto in allStores)
    //     {
    //         Console.WriteLine(sto.ToString());
    //         if(sto.Orders != null && sto.Orders.Count > 0)
    //         {
    //             Console.WriteLine("======Orders======");
    //             foreach(Order ord in sto.Orders)
    //             {
    //                 Console.WriteLine(ord.ToString());
    //             }
    //         }
    //         else
    //         {
    //             Console.WriteLine("No orders made");
    //         }
    //     }
    // }
}