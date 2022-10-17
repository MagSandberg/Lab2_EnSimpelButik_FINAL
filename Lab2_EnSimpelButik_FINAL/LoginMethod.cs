namespace Lab2_EnSimpelButik_FINAL;

public class LoginMethod : DataSource //Ärver från DataSource för att uppdatera listan vid nya kunder.
{
    //Field
    public string? Name { get; set; }
    public string? Password { get; set; }

    //Method
    public void LoginFields()
    {
        do
        {
            Console.Write("Fyll i namn: ");
            Name = Console.ReadLine()!;

            Console.Write("Fyll i lösenord: ");
            Password = Console.ReadLine()!;

            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Password))
            {
                Name = null;
                Password = null;
                Console.WriteLine("\nOj, du glömde visst fylla i ett fält!");
                Console.WriteLine("Tryck på valfri tangent för att försöka igen.");
                Console.ReadKey();
            }

            Console.Clear();

        } while (string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(Password));
    }

    public void RegisterUser()
    {
        LoginMenuActiveColor("Register");

        while (true)
        {
            LoginFields(); //Namn och lösenord

            var exists = Customer.Any(cust => Name == cust.Name);
            if (exists)
            {
                Console.WriteLine("Användarnamnet är upptaget, försök igen.");
            }
            else
            {
                Customer.Add(new Customer(Name!, Password!)); //Sparar kund till lista
                break;
            }
        }
    }

    public Customer? ReturnUserIfExists() //Retunerar användaren om hen existerar annars null
    {
        var toMenu = false;
        var db = new DataSource();
        foreach (var user in db.Customer.Where(user => user.Name==Name))
        {
            if (user.CheckIfUserPasswordExists(Password))
            {
                user.IsActive = true;
                Bool.LoginMenu = false; //Stänger login
                Bool.StoreMenu = true; //Öppnar affären
                return user;
            }
            else
            {
                var tryAgain = true;
                while (tryAgain)
                {
                    Console.WriteLine("Fel lösenord!\nTryck J för att försöka igen eller valfri tangent för att återgå till menyn.");
                    var keyPress = Console.ReadKey();
                    if (keyPress.Key == ConsoleKey.J)
                    {
                        Console.Clear();
                        Console.WriteLine("* Försök igen *\n");
                        Console.Write("Fyll i lösenord: ");
                        Password = Console.ReadLine()!;
                        Console.Clear();
                        if (user.CheckIfUserPasswordExists(Password))
                        {
                            user.IsActive = true;
                            Bool.LoginMenu = false; //Stänger login
                            Bool.StoreMenu = true; //Öppnar affären
                        }
                    }
                    if (keyPress.Key != ConsoleKey.J)
                    {
                        tryAgain = false;
                        toMenu = true;
                        Console.Clear();
                    }
                }
            }
        }

        if (toMenu == false)
        {
            Console.WriteLine("Användarnamnet kunde inte hittas, du kanske vill registrera dig?\n");
            Console.WriteLine("Tryck på valfri tangent för återgå till menyn.");
            Console.ReadKey();
            Console.Clear();
        }

        return null;
    }

    public static void VerifyQuit()
    {
        LoginMenuActiveColor("Quit");

        Console.WriteLine("Avsluta, är du säker?\nTryck J för att avsluta eller valfri tangent för att gå tillbaka\n");
        var verifyQuit = Console.ReadKey();
        if (verifyQuit.Key == ConsoleKey.J)
        {
            Console.Clear();
            Environment.Exit(0);
        }
        Console.Clear();
    }

    public static void WrongKeyPress(char key)
    {
        Console.Clear();
        Console.Write("Fel inmatning: ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write(key);
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nVänligen välj mellan 1, 2 eller Q för att avsluta.\n");
        Console.ForegroundColor = ConsoleColor.Gray;
    }

    public static void LoginMenuActiveColor(string color)
    {
        switch (color)
        {
            case "Login":
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("   -------- ");
                Console.ForegroundColor = ConsoleColor.Gray;
                break;
            case "Register":
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("              --------------------- ");
                Console.ForegroundColor = ConsoleColor.Gray;
                break;
            case "Quit":
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("                                      -----------------");
                Console.ForegroundColor = ConsoleColor.Gray;
                break;
            default:
                Console.ForegroundColor = ConsoleColor.Gray;
                break;
        }
    }
}