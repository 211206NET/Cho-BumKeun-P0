using DL;

namespace UI;
public static class MenuFactory
{
    public static IMenu GetMenu(string menuString)
    {
        menuString = menuString.ToLower();
        string connectionString = File.ReadAllText("connectionString.txt");
        IRepo repo = new DBRepo(connectionString);
        IBL bl = new StoreBL(repo);
        switch (menuString)
        {
            case "main":
                return new MainMenu(bl);
            case "customer":
                return new CustomerMenu(bl);
            case "admin":
                return new AdminMenu(bl);
            //case "order":
                //return new OrderMenu(bl);
            default:
                return new MainMenu(bl);
        }
    }
}