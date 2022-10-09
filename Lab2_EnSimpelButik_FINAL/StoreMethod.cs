namespace Lab2_EnSimpelButik_FINAL;

public class StoreMethod : DataSource
{
    public static void AddToCart(int prodId, Customer customer)
    {
        if (customer.Cart!.Any(e => e.Id == prodId))
        {
            foreach (var item in customer.Cart!.Where(item => item.Id.Equals(prodId)))
            {
                item.Qty++;
            }
        }
        else
        {
            var db = new DataSource();
            customer.Cart!.AddRange(new[] { db.Stock.FirstOrDefault(p => p.Id == prodId) }!);
        }
    }
    public static void RemoveFromCart(int prodId, Customer customer)
    {
        if (customer.Cart!.Any(e => e.Id == prodId - 3))
        {
            foreach (var item in customer.Cart!.Where(item => item.Id.Equals(prodId - 3)))
            {
                if (item.Qty > 0)
                {
                    item.Qty--;
                }
            }
        }
    }
    public static void PrintCart(Customer cust)
    {
        double totalSum = 0;
        foreach (var p in cust.Cart!)
        {
            Console.WriteLine($"Produkt: {p.Name} | Styckpris: {p.Price} SEK | Antal: {p.Qty} | Totalpris: {string.Format("{0:0.00}", p.Qty * p.Price)} SEK");
            totalSum += p.Qty * p.Price;
        }

        Console.WriteLine($"\nPris för hela kundvagnen: {string.Format("{0:0.00}", totalSum)} SEK");
    }
    public static void ProductDisplay(DataSource db)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Lägg till eller ta bort en produkt i kundvagnen med tangenterna 1-6\neller Q för att gå tillbaka.\n");

        Console.ForegroundColor = ConsoleColor.Gray;
        foreach (var p in db.Stock)
        {
            string addProd;

            if (p.Id == 1)
            {
                addProd = "(1) Lägg till | (4) Ta bort:";
            }
            else if (p.Id == 2)
            {
                addProd = "(2) Lägg till | (5) Ta bort:";
            }
            else
            {
                addProd = "(3) Lägg till | (6) Ta bort:";
            }
            Console.WriteLine($"{addProd} {p.Name} / Pris: {p.Price} SEK");
        }
    }

    public static void SpecialDiscount(Customer cust)
    {
        double totalDiscount = 0;
        var discountRank = string.Empty;
        var discountPercent = 0;
        var discount = false;

        var totalSum = cust.Cart!.Sum(p => p.Qty * p.Price);

        double newTotalSum;
        if (totalSum >= 300 && totalSum < 600)
        {
            totalDiscount = 0.05 * totalSum;
            newTotalSum = totalSum - totalDiscount;
            discountRank = "Brons";
            discountPercent = 5;
            discount = true;
        }
        else if (totalSum >= 600 && totalSum < 1300)
        {
            totalDiscount = 0.1 * totalSum;
            newTotalSum = totalSum - totalDiscount;
            discountRank = "Silver";
            discountPercent = 10;
            discount = true;
        }
        else if (totalSum >= 1300)
        {
            totalDiscount = 0.15 * totalSum;
            newTotalSum = totalSum - totalDiscount;
            discountRank = "Guld";
            discountPercent = 15;
            discount = true;
        }
        else
        {
            newTotalSum = totalSum;
        }

        if (discount)
        {
            Console.WriteLine($"\nSom {discountRank}kund får du {discountPercent}% rabatt!");
            Console.WriteLine($"\nDin rabatt: {Math.Round(totalDiscount)} SEK");
            Console.WriteLine($"Du betalar endast {string.Format("{0:0.00}", Math.Round(newTotalSum))} SEK");
        }
        else
        {
            Console.WriteLine($"Pris för din beställning: {string.Format("{0:0.00}", totalSum)} SEK");
        }
    }

    public static void VerifyLogout(DataSource db)
    {
        ShopMenuActiveColor("Logout");
        Console.WriteLine("Logga ut, är du säker?\nTryck J för att avsluta eller valfri tangent för att gå tillbaka\n");

        var verifyQuit = Console.ReadKey();
        if (verifyQuit.Key == ConsoleKey.J)
        {
            foreach (var cust in db.Customer.Where(cust => cust.IsActive)) //Ser till så att ingen användare är "aktiv" efter logout
            {
                cust.IsActive = false;
            }
            Bool.LoginMenu = true;
            Bool.StoreMenu = false;
        }
        Console.Clear();
    }
    public static void WrongKeyPress(char key)
    {
        Console.Clear();
        Console.WriteLine("1: Handla | 2: Kundvagn | 3: Till kassan | Q: Logga ut\n");
        Console.Write("Fel inmatning: ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write(key);
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("\nVänligen välj mellan 1, 2, 3 eller Q.\nTryck på valfri tangent för att gå vidare.");
        Console.ReadKey();
        Console.Clear();
    }

    public static void ShopMenuActiveColor(string color)
    {
        switch (color)
        {
            case "Handla":
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("   ------");
                Console.ForegroundColor = ConsoleColor.Gray;
                break;
            case "Kundvagn":
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("            -----------");
                Console.ForegroundColor = ConsoleColor.Gray;
                break;
            case "Kassa":
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("                          --------------");
                Console.ForegroundColor = ConsoleColor.Gray;
                break;
            case "Logout":
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("                                           -----------");
                Console.ForegroundColor = ConsoleColor.Gray;
                break;
            default:
                Console.ForegroundColor = ConsoleColor.Gray;
                break;
        }
    }
}