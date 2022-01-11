using CustomExceptions;
namespace UI;

public class AdminMenu : IMenu
{
    private IBL _bl;

    public AdminMenu(IBL bl)
    {
        _bl = bl;
    }

    public void Start()
    {
        bool exit = false;
        Console.WriteLine("Admin Username:");
        string? adminUsername = Console.ReadLine();
        while (adminUsername != "vaporadmin")
        {
            Console.WriteLine("Incorrect username");
            Console.WriteLine("Please re-enter username:");
            adminUsername = Console.ReadLine();
        }
        Console.WriteLine("Admin Password:");
        string? adminPassword = Console.ReadLine();
        if (adminPassword != "password")
        {
            Console.WriteLine("Incorrect password");
            exit = true;
        }
        while (!exit)
        {
            Console.WriteLine("[[[[[[[[[[[[[[[[[ Admin Menu ]]]]]]]]]]]]]]]]]");
            Console.WriteLine("[1] View store locations");
            Console.WriteLine("[2] View all products");
            Console.WriteLine("[3] View storefront order history");
            Console.WriteLine("[4] View storefront order history sorted");
            Console.WriteLine("[5] Replenish inventory");
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
                    List<Store> allStores = _bl.GetAllStores();
                    Console.WriteLine("Select a store to see orders for");
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
                        ViewAllStorefrontOrders(selection + 1);
                    }
                break;
                case "4":
                    Console.WriteLine("[[[[[[[[[[[[[[[[[ Order Menu ]]]]]]]]]]]]]]]]]");
                    Console.WriteLine("[1] View storefront order by date (old to new)");
                    Console.WriteLine("[2] View storefront order by date (new to old)");
                    Console.WriteLine("[3] View storefront order by price (low to high)");
                    Console.WriteLine("[4] View storefront order by price (high to low)");
                    Console.WriteLine("[x] Back to User Menu");
                    switch (Console.ReadLine())
                    {
                        case "1":
                        List<Store> allStores1 = _bl.GetAllStores();
                        Console.WriteLine("Select a store to see orders for");
                        Console.WriteLine("==================================");
                        for(int i = 0; i < allStores1.Count; i++)
                        {
                            Console.WriteLine($"[{i}] {allStores1[i].ToString()}");
                        }
                        string? input1 = Console.ReadLine();
                        int selection1;
                        bool parseSuccess1 = Int32.TryParse(input1, out selection1);
                        if(parseSuccess1 && selection1 >= 0 && selection1 < allStores1.Count)
                        {
                            ViewAllStorefrontOrdersDON(selection1 + 1);
                        }
                        break;
                        case "2":
                            List<Store> allStores2 = _bl.GetAllStores();
                            Console.WriteLine("Select a store to see orders for");
                            Console.WriteLine("==================================");
                            for(int i = 0; i < allStores2.Count; i++)
                            {
                                Console.WriteLine($"[{i}] {allStores2[i].ToString()}");
                            }
                            string? input2 = Console.ReadLine();
                            int selection2;
                            bool parseSuccess2 = Int32.TryParse(input2, out selection2);
                            if(parseSuccess2 && selection2 >= 0 && selection2 < allStores2.Count)
                            {
                                ViewAllStorefrontOrdersDNO(selection2 + 1);
                            }
                        break;
                        case "3":
                            List<Store> allStores3 = _bl.GetAllStores();
                            Console.WriteLine("Select a store to see orders for");
                            Console.WriteLine("==================================");
                            for(int i = 0; i < allStores3.Count; i++)
                            {
                                Console.WriteLine($"[{i}] {allStores3[i].ToString()}");
                            }
                            string? input3 = Console.ReadLine();
                            int selection3;
                            bool parseSuccess3 = Int32.TryParse(input3, out selection3);
                            if(parseSuccess3 && selection3 >= 0 && selection3 < allStores3.Count)
                            {
                                ViewAllStorefrontOrdersPLH(selection3 + 1);
                            }
                        break;
                        case "4":
                            List<Store> allStores4 = _bl.GetAllStores();
                            Console.WriteLine("Select a store to see orders for");
                            Console.WriteLine("==================================");
                            for(int i = 0; i < allStores4.Count; i++)
                            {
                                Console.WriteLine($"[{i}] {allStores4[i].ToString()}");
                            }
                            string? input4 = Console.ReadLine();
                            int selection4;
                            bool parseSuccess4 = Int32.TryParse(input4, out selection4);
                            if(parseSuccess4 && selection4 >= 0 && selection4 < allStores4.Count)
                            {
                                ViewAllStorefrontOrdersPHL(selection4 + 1);
                            }
                        break;
                    }
                break;
                case "5":
                    _bl.ReplenishInventory();
                    Console.WriteLine("Inventory has been replenished");
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

    private void ViewAllStorefrontOrders(int Id)
    {
        List<Order> allOrders = _bl.StoreOrders(Id);
        if(allOrders.Count == 0)
        {
            Console.WriteLine("No order has been made for this storefront");
        }
        else
        {
            Console.WriteLine("Here are the orders for the selected storefront");
            Console.WriteLine("==================================");
            foreach(Order ord in allOrders)
            {
                Console.WriteLine(ord.ToString());
            }
        }
    }

    private void ViewAllStorefrontOrdersDON(int Id)
    {
        List<Order> allOrders = _bl.GetAllOrdersStoreDateON(Id);
        if(allOrders.Count == 0)
        {
            Console.WriteLine("No order has been made for this storefront");
        }
        else
        {
            Console.WriteLine("Here are the orders for the selected storefront");
            Console.WriteLine("==================================");
            foreach(Order ord in allOrders)
            {
                Console.WriteLine(ord.ToString());
            }
        }
    }

    private void ViewAllStorefrontOrdersDNO(int Id)
    {
        List<Order> allOrders = _bl.GetAllOrdersStoreDateNO(Id);
        if(allOrders.Count == 0)
        {
            Console.WriteLine("No order has been made for this storefront");
        }
        else
        {
            Console.WriteLine("Here are the orders for the selected storefront");
            Console.WriteLine("==================================");
            foreach(Order ord in allOrders)
            {
                Console.WriteLine(ord.ToString());
            }
        }
    }

    private void ViewAllStorefrontOrdersPLH(int Id)
    {
        List<Order> allOrders = _bl.GetAllOrdersStorePriceLH(Id);
        if(allOrders.Count == 0)
        {
            Console.WriteLine("No order has been made for this storefront");
        }
        else
        {
            Console.WriteLine("Here are the orders for the selected storefront");
            Console.WriteLine("==================================");
            foreach(Order ord in allOrders)
            {
                Console.WriteLine(ord.ToString());
            }
        }
    }

    private void ViewAllStorefrontOrdersPHL(int Id)
    {
        List<Order> allOrders = _bl.GetAllOrdersStorePriceHL(Id);
        if(allOrders.Count == 0)
        {
            Console.WriteLine("No order has been made for this storefront");
        }
        else
        {
            Console.WriteLine("Here are the orders for the selected storefront");
            Console.WriteLine("==================================");
            foreach(Order ord in allOrders)
            {
                Console.WriteLine(ord.ToString());
            }
        }
    }
}