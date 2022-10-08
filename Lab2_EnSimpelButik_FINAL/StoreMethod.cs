namespace Lab2_EnSimpelButik_FINAL;

public class StoreMethod
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
            customer.Cart.AddRange(new[] { db.Stock.FirstOrDefault(p => p.Id == prodId) });
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
            var addProd = String.Empty;

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
}