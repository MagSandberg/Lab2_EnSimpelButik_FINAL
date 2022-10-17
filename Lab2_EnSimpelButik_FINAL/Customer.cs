namespace Lab2_EnSimpelButik_FINAL;

public class Customer
{
    public int Id { get; set; }
    public new string? Name { get; private set; }
    public new string? Password { get; private set; }
    public bool IsActive { get; set; }

    private readonly List<Product>? _cart;
    public List<Product>? Cart { get { return _cart; } }

    public Customer(string? name, string? password)
    {
        Name = name;
        Password = password;
        _cart = new List<Product>();
    }

    

    public bool CheckIfUserPasswordExists(string userPassword)
    {
        return userPassword.Equals(Password);
    }

    public bool CheckIfUserNameExists(string? name)
    {
        var lm = new LoginMethod();
        return name!.Equals(lm.Name);
    }

    public override string ToString()
    {
        var customerCart = string.Empty;
        if (Cart != null)
            foreach (var p in Cart)
            {
                customerCart += $"{p.Name}, ".ToString();
                customerCart += $"{p.Qty} st / ".ToString();
            }

        return $"\nNamn: {Name}\nLösenord: {Password}\nKundvagn: {customerCart}";
    }
}