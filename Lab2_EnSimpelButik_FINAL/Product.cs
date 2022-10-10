namespace Lab2_EnSimpelButik_FINAL;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
    public int Qty { get; set; }

    public static void TotalSum(Customer cust)
    {
        var totalSum = cust.Cart!.Sum(p => p.Qty * p.Price);
        Console.WriteLine($"\nPris för hela kundvagnen: {totalSum:0.00} SEK");
    }
}