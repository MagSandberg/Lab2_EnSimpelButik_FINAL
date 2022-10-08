namespace Lab2_EnSimpelButik_FINAL;

public class Login
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
                Console.WriteLine("\nOj, du glömde visst fylla i ett fält!");
                Console.WriteLine("Tryck på valfri tangent för att försöka igen.");
                Console.ReadKey();
            }
            Console.Clear();
        } while (string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(Password));
    }

    public Customer? ReturnUserIfExists(DataSource db) //Retunerar användaren om hen existerar annars null
    {
        foreach (var user in db.Customer)
        {
            if (CheckIfUserNameExists(user.Name))
            {
                if (CheckIfUserPasswordExists(user.Password))
                {
                    user.IsActive = true;
                    Bool.LoginMenu = false; //Stänger login
                    Bool.StoreMenu = true; //Öppnar affären
                    return user;
                }

                if (CheckIfUserPasswordExists(user.Password) == false)
                {
                    var tryAgain = true;
                    while (tryAgain)
                    {
                        Console.WriteLine("Fel lösenord!\nTryck J för att försöka igen eller Q för att återgå till menyn.");
                        var keyPress = Console.ReadKey();
                        if (keyPress.Key == ConsoleKey.J)
                        {
                            Console.Clear();
                            Console.WriteLine("* Försök igen *\n");
                            Console.Write("Fyll i lösenord: ");
                            Password = Console.ReadLine()!;
                            Console.Clear();
                            if (CheckIfUserPasswordExists(user.Password))
                            {
                                user.IsActive = true;
                                Bool.LoginMenu = false; //Stänger login
                                Bool.StoreMenu = true; //Öppnar affären
                                return user;
                            }
                        }
                        if (keyPress.Key == ConsoleKey.Q)
                        {
                            tryAgain = false;
                            Console.Clear();
                        }
                    }
                }
            }
        }
        Console.WriteLine("Användarnamnet kunde inte hittas. Vill du registrera dig?\n");
        Console.WriteLine("Tryck J för att registrera dig eller Q för att återgå till menyn.");

        var keyPres = Console.ReadKey();
        if (keyPres.Key == ConsoleKey.J)
        {
            Console.Clear();
            Console.WriteLine("* Registrera ny kund *\n");
            LoginFields(); //Name, Password
            var newCustomer = new Customer(Name!, Password!);
            db.Customer.Add(newCustomer); //Spara till lista
            return newCustomer;
        }

        if (keyPres.Key == ConsoleKey.Q)
        {
            Console.Clear();
        }

        return null;
    }

    private bool CheckIfUserPasswordExists(string userPassword)
    {
        return userPassword.Equals(Password);
    }

    public bool CheckIfUserNameExists(string name)
    {
        return name.Equals(Name);
    }

    public static void VerifyQuit()
    {
        Console.WriteLine(
            "Avsluta, är du säker?\nTryck J för att avsluta eller valfri tangent för att gå tillbaka\n");
        var verifyQuit = Console.ReadKey();
        if (verifyQuit.Key == ConsoleKey.J)
        {
            Console.Clear();
            Environment.Exit(0);
        }
    }
}