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
        foreach (var p in cust.Cart!)
        {
            Console.WriteLine($"Produkt: {p.Name} | Styckpris: {p.Price} SEK | Antal: {p.Qty} | Totalpris: {p.Qty * p.Price:0.00} SEK");
        }
        Product.TotalSum(cust);
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
            PrintCart(cust);
            Console.WriteLine($"\nSom {discountRank}kund får du {discountPercent}% rabatt!");
            Console.WriteLine($"\nDin rabatt: {Math.Round(totalDiscount)} SEK");
            Console.WriteLine($"Du betalar endast {string.Format("{0:0.00}", Math.Round(newTotalSum))} SEK");
        }
        else
        {
            PrintCart(cust);
            Console.WriteLine($"Pris för din beställning: {string.Format("{0:0.00}", totalSum)} SEK");
        }
    }
    public static void VerifyLogout(DataSource db)
    {
        ShopMenuActiveColor("Logout");
        Console.WriteLine("Är du säker?\nTryck J för att avsluta eller valfri tangent för att gå tillbaka\n");

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
    public static void PressAnyKey()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nTryck valfri tangent för att återgå till menyn.");
        Console.ForegroundColor = ConsoleColor.Gray;
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