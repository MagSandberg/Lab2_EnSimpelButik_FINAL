namespace Lab2_EnSimpelButik_FINAL;

public class Login
{
    //Field
    public string Name { get; set; }
    public string Password { get; set; }

    //Method
    public void LoginFields()
    {
        Console.Write("Fyll i namn: ");
        Name = Console.ReadLine();

        Console.Write("Fyll i lösenord: ");
        Password = Console.ReadLine();

        Console.Clear();
    }

    public Customer ReturnUserIfExists(DataSource db) //retunerar användaren om hen existerar annars null
    {

        foreach (var user in db.Customer)
        {
            if (CheckIfUserNameExists(user.Name))
            {
                if (CheckIfUserPasswordExists(user.Password))
                {
                    user.IsActive = true;
                    Bool.LoginMenu = false; //Stänger login
                    Bool.StoreMenu = true;  //Öppnar affären
                    return user;
                }
                else
                {
                    var tryAgain = true;
                    while (tryAgain)
                    {
                        Console.Clear();
                        Console.WriteLine("Fel lösenord! Vänligen försök igen.\n");
                        if (CheckIfUserPasswordExists(Console.ReadLine()))
                        {
                            Bool.LoginMenu = false;
                            Bool.StoreMenu = true;
                            tryAgain = false;
                        }
                        else
                        {
                            Console.WriteLine("Fel lösenord! tryck Q för att komma tillbaka till start.\n");
                            Console.WriteLine("Annars är det bara att trycka valfri knapp och försöka igen\n");
                            if (Console.ReadKey().Key == ConsoleKey.Q)
                            {
                                tryAgain = false;
                            }
                        }
                    }
                }

            }
        }
        Console.WriteLine("Användarnamnet kunde inte hittas. Vill du registrera dig?\n");
        Console.WriteLine("Tryck J för att registrera dig eller valfri tangent för att återgå till menyn.");
        var keyPress = Console.ReadKey();
        if (keyPress.Key == ConsoleKey.J)
        {
            Console.Clear();
            Console.WriteLine("* Registrera ny kund *\n");
            LoginFields(); //Name, Password
            var newCustomer = new Customer(Name, Password);
            db.Customer.Add(newCustomer); //Spara till lista
            return newCustomer;
        }
        else
        {
            Console.Clear();
            return null;
        }
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
        Console.WriteLine("Avsluta, är du säker?\nTryck J för att avsluta eller valfri tangent för att gå tillbaka\n");
        var verifyQuit = Console.ReadKey();
        if (verifyQuit.Key == ConsoleKey.J)
        {
            Console.Clear();
            Environment.Exit(0);
        }
    }
}