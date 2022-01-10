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
            Console.WriteLine("[3] Replenish inventory");
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
            Console.WriteLine("No stores found :/");
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
}