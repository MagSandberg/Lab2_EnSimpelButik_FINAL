namespace Lab2_EnSimpelButik_FINAL;

public class StoreMethod //FIXA ARV
{
    public static void AddToCart(int prodId, Customer customer)
    {
        if (customer.Cart.Any(e => e.Id == prodId))
        {
            foreach (var item in customer.Cart.Where(item => item.Id.Equals(prodId)))
            {
                item.Qty++;
            }
        }
        else
        {
            var db = new DataSource();
            customer.Cart.AddRange(new[] { db.Stock.FirstOrDefault(p => p.Id == prodId) }!);
        }
    }
    public static void RemoveFromCart(int prodId, Customer customer)
    {
        if (customer.Cart.Any(e => e.Id == prodId - 3))
        {
            foreach (var item in customer.Cart.Where(item => item.Id.Equals(prodId - 3)))
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
        foreach (var p in cust.Cart)
        {
            Console.WriteLine($"Produkt: {p.Name} | Styckpris: {p.Price} | Antal: {p.Qty} | Totalpris: {string.Format("{0:0.00}", p.Qty * p.Price)}");
            totalSum += p.Qty * p.Price;
        }

        Console.WriteLine($"\nPris för hela kundvagnen: {string.Format("{0:0.00}", totalSum)}");
    }
    public static void ProductDisplay()
    {
        var db = new DataSource();
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
            Console.WriteLine($"{addProd} {p.Name} / Pris: {p.Price}");
        }
    }

    public static void SpecialDiscount(Customer cust)
    {
        double totalSum = 0;
        double totalDiscount = 0;
        var discountRank = string.Empty;
        var discountPercent = 0;
        bool discount = false;
        foreach (var p in cust.Cart)
        {
            totalSum += p.Qty * p.Price;
        }

        double newTotalSum;
        if (totalSum > 200 && totalSum < 500)
        {
            totalDiscount = 0.05 * totalSum;
            newTotalSum = totalSum - totalDiscount;
            discountRank = "Brons";
            discountPercent = 5;
            discount = true;
        }
        else if (totalSum > 500 && totalSum < 1000)
        {
            totalDiscount = 0.1 * totalSum;
            newTotalSum = totalSum - totalDiscount;
            discountRank = "Silver";
            discountPercent = 10;
            discount = true;
        }
        else if (totalSum > 1000)
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
            Console.WriteLine($"\nDin rabatt: {Math.Round(totalDiscount)}:-");
            Console.WriteLine($"Du betalar endast {string.Format("{0:0.00}", Math.Round(newTotalSum))}:-");
        }
        else
        {
            Console.WriteLine($"Pris för din beställning: {string.Format("{0:0.00}", totalSum)}:-");
        }
    }
}