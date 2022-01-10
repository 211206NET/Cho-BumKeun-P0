using CustomExceptions;
namespace UI;

public class MainMenu : IMenu
{
    private IBL _bl;

    public MainMenu(IBL bl)
    {
        _bl = bl;
    }

    public void Start() {
        bool exit = false;
        while(!exit)
        {
            Console.WriteLine("[[[[[[[[[[[[[[[[[ Welcome to Vapor ]]]]]]]]]]]]]]]]]");
            Console.WriteLine("[1] Sign up new account");
            Console.WriteLine("[2] Login as Customer");
            Console.WriteLine("[3] Login as Admin");
            Console.WriteLine("[x] Exit");
            string? input = Console.ReadLine();

            if(!string.IsNullOrWhiteSpace(input))
            {
                switch (input)
                {
                    case "1":
                        createAccount:
                        Console.WriteLine("Create a username:");
                        string? username = Console.ReadLine();
                        Console.WriteLine("Create a password:");
                        string? password = Console.ReadLine();
                        try
                        {
                            Customer newCustomer = new Customer
                            {
                                UserName = username,
                                Password = password 
                            };
                            _bl.AddCustomer(newCustomer);
                        }
                        catch (InputInvalidException ex)
                        {
                            Console.WriteLine(ex.Message);
                            goto createAccount;
                        }
                        catch (DuplicateRecordException ex)
                        {
                            Console.WriteLine(ex.Message);
                            goto createAccount;
                        }
                    break;
                    case "2":
                        MenuFactory.GetMenu("customer").Start();
                    break;
                    case "3":
                        MenuFactory.GetMenu("admin").Start();
                    break;
                    case "x":
                        exit = true;
                        Console.WriteLine("Goodbye!");
                    break;
                }
            }
            else
            {
                Console.WriteLine("Please enter valid input");
            }
        }
    }
}