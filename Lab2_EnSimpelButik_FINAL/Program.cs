using Lab2_EnSimpelButik_FINAL;


var db = new DataSource(); //Instansierar produkt-"databas"
Login userLogin = new(); //Loggar in en användare
Customer? activeUser = null; //Instansierar en kund

//Tillåtna tangenttryckningar i programmet
System.ConsoleKey[] cK = { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4, ConsoleKey.D5, ConsoleKey.D6, ConsoleKey.Q };
var keyPress = new ConsoleKeyInfo(); //Switch-navigering
var keyPressMenuShop = new ConsoleKeyInfo();
var keyPressProd = new ConsoleKeyInfo();

while (true) //Programmet körs tills användaren avslutar
{
    LoginMenu(); //Verifierar användaren
    StoreMenu(); //Lägg till eller ta bort produkter, kundvagn, kassa
}

void LoginMenu()
{
    while (Bool.LoginMenu)
    {
        Console.WriteLine("1: Logga in | 2: Registrera ny kund | Q: Stäng program");
        keyPress = Console.ReadKey();
        Console.CursorLeft = 0;

        System.ConsoleKey[] cK = { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.Q };
        if (!cK.Contains(keyPress.Key))
        {
            Console.Clear();
            Console.Write("Fel inmatning: ");
            ChangeTextColorLogin("Red");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nVänligen välj mellan 1, 2 eller Q för att avsluta.\n");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        else
        {
            switch (keyPress.Key)
            {
                case ConsoleKey.D1:

                    ChangeTextColorLogin("Green.Login");
                    userLogin.LoginFields();
                    activeUser = userLogin.ReturnUserIfExists(db);
                    break;
                case ConsoleKey.D2:

                    ChangeTextColorLogin("Green.Register");
                    userLogin.LoginFields(); //Name, Password
                    db.Customer.Add(new Customer(userLogin.Name, userLogin.Password)); //Spara till lista
                    break;

                case ConsoleKey.Q:

                    ChangeTextColorLogin("Green.Quit");
                    Login.VerifyQuit(); //Verifierar om användaren vill stänga programmet
                    Console.Clear();
                    break;
            }
        }
    }
} //IF loginMenu = true
void StoreMenu()
{
    while (Bool.StoreMenu)
    {
        while (Bool.WrongKey)
        {
            Console.WriteLine("1: Handla | 2: Kundvagn | 3: Till kassan | Q: Logga ut");
            keyPressMenuShop = Console.ReadKey();
            Console.CursorLeft = 0;

            if (!cK.Contains(keyPressMenuShop.Key))
            {
                Console.Clear();
                Console.WriteLine("1: Handla | 2: Kundvagn | 3: Till kassan | Q: Logga ut\n");
                Console.Write("Fel inmatning: ");
                ChangeTextColorMenuShop("Red");
                Console.WriteLine("\nVänligen välj 1, 2, 3 eller Q för att logga ut.\n");
                keyPressMenuShop = Console.ReadKey();
            }
            else
            {
                Console.Clear();
                Bool.WrongKey = false;
            }
        }

        Bool.WrongKey = true;
        switch (keyPressMenuShop.Key)
        {
            case ConsoleKey.D1:
                while (Bool.WrongKeyProd)
                {
                    Console.Clear();
                    Console.WriteLine("1: Handla | 2: Kundvagn | 3: Till kassan | Q: Logga ut");
                    ChangeTextColorMenuShop("Green.Handla");

                    StoreMethod.ProductDisplay(); //Visar produktutbudet

                    keyPressProd = Console.ReadKey();

                    if (!cK.Contains(keyPressProd.Key))
                    {  
                        Console.Clear();
                        Console.Write("Fel inmatning: ");
                        ChangeTextColorProducts("Red");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nVänligen välj med tangenterna 1-6 eller Q.\n");
                        Console.ForegroundColor = ConsoleColor.Gray;

                        StoreMethod.ProductDisplay(); //Visar produktutbudet

                        keyPressProd = Console.ReadKey();
                    }
                    else
                    {
                        if (keyPressProd.Key == ConsoleKey.Q)
                        {
                            Bool.WrongKeyProd = false;
                            Console.Clear();
                            break;
                        }

                        int keyPressed = int.Parse(keyPressProd.KeyChar.ToString());
                        if (keyPressed <= 3)
                        {
                            StoreMethod.AddToCart(keyPressed, activeUser);
                        }
                        else if (keyPressed <= 6)
                        {
                            StoreMethod.RemoveFromCart(keyPressed, activeUser);
                        }
                    }
                }
                break;

            case ConsoleKey.D2:
                while (Bool.WrongKey)
                {
                    Console.Clear();
                    Console.WriteLine("1: Handla | 2: Kundvagn | 3: Till kassan | Q: Logga ut");
                    ChangeTextColorMenuShop("Green.Kundvagn");

                    StoreMethod.PrintCart(activeUser); //Skriver ut kundvagnen

                    keyPressMenuShop = Console.ReadKey();
                    if (!cK.Contains(keyPressMenuShop.Key))
                    {
                        Console.Clear();
                        Console.Write("Fel inmatning: ");
                        ChangeTextColorProducts("Red");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nVänligen välj med tangenterna 1-6 eller Q.\n");
                        Console.ForegroundColor = ConsoleColor.Gray;

                        StoreMethod.PrintCart(db.Customer.Find(p => p.IsActive = true)); //Skriver ut kundvagnen

                        keyPressMenuShop = Console.ReadKey();
                    }
                    else
                    {
                        Console.Clear();
                        Bool.WrongKeyProd = true;
                        Bool.WrongKey = false;
                    }
                }
                break;
            case ConsoleKey.D3:
                while (Bool.WrongKey)
                {
                    Console.Clear();
                    Console.WriteLine("1: Handla | 2: Kundvagn | 3: Till kassan | Q: Logga ut");
                    ChangeTextColorMenuShop("Green.Kassa");
                    Console.WriteLine("hej");
                    Console.ReadKey();
                }
                break;
            case ConsoleKey.Q:
                Console.WriteLine("1: Handla | 2: Kundvagn | 3: Till kassan | Q: Logga ut");
                ChangeTextColorMenuShop("Green.Quit");
                Console.WriteLine("Logga ut, är du säker?\nTryck J för att avsluta eller valfri tangent för att gå tillbaka\n");
                var verifyQuit = Console.ReadKey();
                if (verifyQuit.Key == ConsoleKey.J)
                {
                    foreach (var cust in db.Customer)
                    {
                        if (cust.IsActive)
                        {
                            cust.IsActive = false;
                        }
                    }
                    Bool.LoginMenu = true;
                    Bool.StoreMenu = false;
                }
                Console.Clear();
                break;
        }
    }
} //IF storeMenu = true

void ChangeTextColorLogin(string color)
{
    switch (color)
    {
        case "Red":
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(keyPress.KeyChar);
            Console.ForegroundColor = ConsoleColor.Gray;
            break;
        case "Green.Login":
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("   -------- ");
            Console.ForegroundColor = ConsoleColor.Gray;
            break;
        case "Green.Register":
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("              --------------------- ");
            Console.ForegroundColor = ConsoleColor.Gray;
            break;
        case "Green.Quit":
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("                                      -----------------");
            Console.ForegroundColor = ConsoleColor.Gray;
            break;
        default:
            Console.ForegroundColor = ConsoleColor.Gray;
            break;
    }
}
void ChangeTextColorMenuShop(string color)
{
    switch (color)
    {
        case "Red":
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(keyPressMenuShop.KeyChar);
            Console.ForegroundColor = ConsoleColor.Gray;
            break;
        case "Green.Handla":
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("   ------");
            Console.ForegroundColor = ConsoleColor.Gray;
            break;
        case "Green.Kundvagn":
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("            -----------");
            Console.ForegroundColor = ConsoleColor.Gray;
            break;
        case "Green.Kassa":
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("                          --------------");
            Console.ForegroundColor = ConsoleColor.Gray;
            break;
        case "Green.Quit":
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("                                           -----------");
            Console.ForegroundColor = ConsoleColor.Gray;
            break;
        default:
            Console.ForegroundColor = ConsoleColor.Gray;
            break;
    }
}
void ChangeTextColorProducts(string color)
{
    switch (color)
    {
        case "Red":
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(keyPressProd.KeyChar);
            Console.ForegroundColor = ConsoleColor.Gray;
            break;
        default:
            Console.ForegroundColor = ConsoleColor.Gray;
            break;
    }
}