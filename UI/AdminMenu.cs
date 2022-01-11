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
                            //ViewAllCustomerOrdersDON(existing);
                        break;
                        case "2":
                            //ViewAllCustomerOrdersDNO(existing);
                        break;
                        case "3":
                            //ViewAllCustomerOrdersPLH(existing);
                        break;
                        case "4":
                            //ViewAllCustomerOrdersPHL(existing);
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
}