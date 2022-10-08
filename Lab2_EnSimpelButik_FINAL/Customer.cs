namespace Lab2_EnSimpelButik_FINAL;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    private bool _isActive;

    public bool IsActive
    {
        get { return _isActive; }
        set { _isActive = value; }
    }
    public string Password { get; private set; }

    private List<Product> _cart;
    public List<Product> Cart { get { return _cart; } }

    public Customer(string name, string password)
    {
        Name = name;
        Password = password;
        _cart = new List<Product>();
    }

    public override string ToString()
    {
        var customerCart = string.Empty;
        foreach (var p in Cart)
        {
            customerCart += $"{p.Name}, ".ToString();
            customerCart += $"{p.Qty} st / ".ToString();
        }
        return $"\nNamn: {Name}\nLösenord: {Password}\nKundvagn: {customerCart}";
    }
}