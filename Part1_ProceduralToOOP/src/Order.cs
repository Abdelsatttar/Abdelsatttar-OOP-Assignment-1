namespace ProceduralOrderOOP;

public class Order
{
    private const int MaxLinesPerOrder = 20;
    private readonly List<OrderLine> _lines = new();

    public int Id { get; }
    public Customer Customer { get; }
    public string Date { get; }
    public bool IsPaid { get; private set; }
    public IReadOnlyList<OrderLine> Lines => _lines;

    public Order(int id, Customer customer, string date)
    {
        Id = id;
        Customer = customer;
        Date = date;
    }

    public bool AddLine(Product product, int quantity, out string errorMessage)
    {
        errorMessage = string.Empty;

        if (IsPaid)
        {
            errorMessage = "ERROR: cannot change a paid order.";
            return false;
        }

        if (_lines.Count >= MaxLinesPerOrder)
        {
            errorMessage = "ERROR: order has too many lines.";
            return false;
        }

        if (quantity <= 0)
        {
            errorMessage = "ERROR: quantity must be positive.";
            return false;
        }

        if (!product.HasEnoughStock(quantity))
        {
            errorMessage = $"ERROR: not enough stock for product #{product.Id}.";
            return false;
        }

        product.ReduceStock(quantity);
        _lines.Add(new OrderLine(product, quantity));
        return true;
    }

    public decimal CalculateTotal()
    {
        decimal total = 0m;

        foreach (OrderLine line in _lines)
        {
            total += line.CalculateTotal();
        }

        return Customer.ApplyDiscount(total);
    }

    public bool MarkAsPaid(out string errorMessage)
    {
        errorMessage = string.Empty;

        if (_lines.Count == 0)
        {
            errorMessage = "ERROR: cannot pay an empty order.";
            return false;
        }

        IsPaid = true;
        return true;
    }
}
