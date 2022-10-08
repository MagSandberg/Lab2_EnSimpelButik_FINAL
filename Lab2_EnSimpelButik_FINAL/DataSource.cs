namespace Lab2_EnSimpelButik_FINAL;

public class DataSource
{
    public List<Customer> Customer { get; } = new();
    public List<Product> Stock { get; } = new();

    public DataSource()
    {
        Stock.AddRange(new[]
        {
            new Product { Id = 1, Name = "T-shirt", Price = 299.99, Qty = 1 },
            new Product { Id = 2, Name = "Jeans", Price = 1429.12, Qty = 1 },
            new Product { Id = 3, Name = "Keps", Price = 123.45, Qty = 1 },
        });

        Customer.AddRange(new[]
        {
            new Customer("Knatte", "123") { Id = 1, IsActive = false, Cart = { } },
            new Customer("Fnatte", "321") { Id = 2, IsActive = false, Cart = { } },
            new Customer("Tjatte", "213") { Id = 3, IsActive = false, Cart = { } },
        });
    }
}