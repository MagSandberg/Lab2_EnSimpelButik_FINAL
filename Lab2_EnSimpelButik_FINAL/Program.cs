using Lab2_EnSimpelButik_FINAL;

var db = new DataSource(); //Instansierar produkt-"databas"
LoginMethod userLogin = new(); //Instansierar en användare
Customer? activeUser = null; //Instansierar en kund

//Tillåtna tangenter under LoginMenu
ConsoleKey[] keyLoginMenu = { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.Q };

//Navigering
ConsoleKeyInfo keyPress;
ConsoleKeyInfo keyPressShopMenu;
ConsoleKeyInfo keyPressProd;

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
            if (keyPress.Key == ConsoleKey.D1)
            {
                LoginMethod.LoginMenuActiveColor("Login");
                userLogin.LoginFields(); //Login
                activeUser = userLogin.ReturnUserIfExists();
            }
            else if (keyPress.Key == ConsoleKey.D2)
            {
                userLogin.RegisterUser(); //Registrerar en användare
            }
            else if (keyPress.Key == ConsoleKey.Q)
            {
                LoginMethod.VerifyQuit(); //Verifierar om användaren vill stänga programmet
            }
        }
    }
}
void StoreMenu()
{
    while (Bool.StoreMenu)
    {
        Console.WriteLine("1: Handla | 2: Kundvagn | 3: Till kassan | Q: Logga ut");
        keyPressShopMenu = Console.ReadKey();
        Console.CursorLeft = 0;

        switch (keyPressShopMenu.Key)
        {
            case ConsoleKey.D1:
                {
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine("1: Handla | 2: Kundvagn | 3: Till kassan | Q: Logga ut");
                        StoreMethod.ShopMenuActiveColor("Handla");
                        StoreMethod.ProductDisplay(db); //Visar produktutbudet
                        keyPressProd = Console.ReadKey();
                        if (keyPressProd.Key == ConsoleKey.Q)
                        {
                            Console.CursorLeft = 0;
                            Console.Clear();
                            break;
                        }

                        var keyPressed = keyPressProd.KeyChar.ToString();

                        switch (keyPressProd.Key)
                        {
                            case ConsoleKey.D1 or ConsoleKey.D2 or ConsoleKey.D3:
                                int keyPressedInt = int.Parse(keyPressed);
                                StoreMethod.AddToCart(keyPressedInt, activeUser!);
                                break;
                            case ConsoleKey.D4 or ConsoleKey.D5 or ConsoleKey.D6:
                                keyPressedInt = int.Parse(keyPressed);
                                StoreMethod.RemoveFromCart(keyPressedInt, activeUser!);
                                break;
                        }
                    }
                    break;
                }
            case ConsoleKey.D2:
                {
                    StoreMethod.ShopMenuActiveColor("Kundvagn");
                    StoreMethod.PrintCart(activeUser!); //Skriver ut kundvagnen
                    StoreMethod.PressAnyKey();
                    break;
                }
            case ConsoleKey.D3:
                {
                    StoreMethod.ShopMenuActiveColor("Kassa");
                    StoreMethod.SpecialDiscount(activeUser!); //Skriver ut totalpriset och rabatt om kunden uppnått kraven
                    StoreMethod.PressAnyKey();
                    break;
                }
            case ConsoleKey.Q:
                StoreMethod.VerifyLogout(db);
                break;
            default:
                Console.Write("Fel tangent: ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(keyPressShopMenu.KeyChar);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nVänligen välj med tangenterna 1, 2, 3 eller Q.\n");
                Console.ForegroundColor = ConsoleColor.Gray;
                StoreMethod.PressAnyKey();
                break;
        }
    }
}