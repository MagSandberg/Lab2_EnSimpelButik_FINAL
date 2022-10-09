using Lab2_EnSimpelButik_FINAL;

var db = new DataSource(); //Instansierar produkt-"databas"
LoginMethod userLogin = new(); //Instansierar en användare
Customer? activeUser = null; //Instansierar en kund

//Tillåtna tangenter under LoginMenu
System.ConsoleKey[] keyLoginMenu = { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.Q };

//Tillåtna tangenttryckningar i StoreMenu
System.ConsoleKey[] keyStoreMenu = { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4, ConsoleKey.D5, ConsoleKey.D6, ConsoleKey.Q };

//Navigering
var keyPress = new ConsoleKeyInfo();
var keyPressShopMenu = new ConsoleKeyInfo();
var keyPressProd = new ConsoleKeyInfo();

while (true) //Programmet körs tills användaren avslutar
{
    LoginMenu(); //Login, registrering och verifierar användaren
    StoreMenu(); //Lägg till eller ta bort produkter, kundvagn, kassa
}

void LoginMenu()
{
    while (Bool.LoginMenu)
    {

            Console.WriteLine("1: Logga in | 2: Registrera ny kund | Q: Stäng program");
            keyPress = Console.ReadKey();
            var pressedKey = keyPress.KeyChar;

            Console.CursorLeft = 0;
            if (!keyLoginMenu.Contains(keyPress.Key))
            {
                LoginMethod.WrongKeyPress(pressedKey);
            }
            else
            {

                switch (keyPress.Key)
                {
                    case ConsoleKey.D1:
                        LoginMethod.LoginMenuActiveColor("Login");
                        userLogin.LoginFields(); //Login
                        activeUser = userLogin.ReturnUserIfExists();
                        break;
                    case ConsoleKey.D2:
                        userLogin.RegisterUser(); //Registrerar en användare
                        break;
                    case ConsoleKey.Q:
                        LoginMethod.VerifyQuit(); //Verifierar om användaren vill stänga programmet
                        break;
                }
            }
    }
}
void StoreMenu()
{
    while (Bool.StoreMenu)
    {
        while (Bool.WrongKey)
        {
            Console.WriteLine("1: Handla | 2: Kundvagn | 3: Till kassan | Q: Logga ut");
            keyPressShopMenu = Console.ReadKey();
            Console.CursorLeft = 0;
            var pressedKeyShop = keyPressShopMenu.KeyChar;

            if (!keyStoreMenu.Contains(keyPressShopMenu.Key))
            {
                StoreMethod.WrongKeyPress(pressedKeyShop);
            }
            else
            {
                Console.Clear();
                Bool.WrongKey = false;
            }
        }

        Bool.WrongKey = true;
        switch (keyPressShopMenu.Key)
        {
            case ConsoleKey.D1:
                while (Bool.WrongKeyProd)
                {
                    Console.Clear();
                    Console.WriteLine("1: Handla | 2: Kundvagn | 3: Till kassan | Q: Logga ut");
                    StoreMethod.ShopMenuActiveColor("Handla");
                    StoreMethod.ProductDisplay(db); //Visar produktutbudet
                    keyPressProd = Console.ReadKey();
                    if (!keyStoreMenu.Contains(keyPressProd.Key))
                    {
                        Console.Clear();
                        Console.Write("Fel inmatning: ");
                        ChangeTextColorProducts("Red");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nVänligen välj med tangenterna 1-6 eller Q.\n");
                        Console.ForegroundColor = ConsoleColor.Gray;
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
                        var keyPressed = int.Parse(keyPressProd.KeyChar.ToString());
                        if (keyPressed <= 3)
                        {
                            StoreMethod.AddToCart(keyPressed, activeUser!);
                        }
                        else if (keyPressed <= 6)
                        {
                            StoreMethod.RemoveFromCart(keyPressed, activeUser!);
                        }
                    }
                }
                break;

            case ConsoleKey.D2:
                while (Bool.WrongKey)
                {
                    Console.Clear();
                    Console.WriteLine("1: Handla | 2: Kundvagn | 3: Till kassan | Q: Logga ut");
                    StoreMethod.ShopMenuActiveColor("Kundvagn");

                    StoreMethod.PrintCart(activeUser!); //Skriver ut kundvagnen

                    keyPressShopMenu = Console.ReadKey();
                    if (!keyStoreMenu.Contains(keyPressShopMenu.Key))
                    {
                        Console.Clear();
                        Console.Write("Fel inmatning: ");
                        ChangeTextColorProducts("Red");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nVänligen välj med tangenterna 1-6 eller Q.\n");
                        Console.ForegroundColor = ConsoleColor.Gray;

                        StoreMethod.PrintCart(db.Customer.Find(p => p.IsActive = true)!); //Skriver ut kundvagnen

                        keyPressShopMenu = Console.ReadKey();
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
                    StoreMethod.ShopMenuActiveColor("Kassa");

                    Console.WriteLine("* Dina varor * \n");
                    StoreMethod.PrintCart(activeUser!); //Skriver ut kundvagnen
                    StoreMethod.SpecialDiscount(activeUser!); //Skriver ut totalpriset och rabatt om kunden uppnått kraven

                    keyPressShopMenu = Console.ReadKey();
                    if (!keyStoreMenu.Contains(keyPressShopMenu.Key))
                    {
                        Console.Clear();
                        Console.Write("Fel inmatning: ");
                        ChangeTextColorProducts("Red");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nVänligen välj med tangenterna 1-6 eller Q.\n");
                        Console.ForegroundColor = ConsoleColor.Gray;

                        keyPressShopMenu = Console.ReadKey();
                    }
                    else
                    {
                        Console.Clear();
                        Bool.WrongKeyProd = true;
                        Bool.WrongKey = false;
                    }
                }
                break;
            case ConsoleKey.Q:
                StoreMethod.VerifyLogout(db);
                break;
        }
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