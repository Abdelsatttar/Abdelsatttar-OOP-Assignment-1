namespace ProceduralOrderOOP;

public class Product
{
    public int Id { get; }
    public string Name { get; }
    public decimal Price { get; }
    public int Stock { get; private set; }

    public Product(int id, string name, decimal price, int stock)
    {
        Id = id;
        Name = name;
        Price = price;
        Stock = stock;
    }

    public bool HasEnoughStock(int quantity)
    {
        return quantity > 0 && Stock >= quantity;
    }

    public void ReduceStock(int quantity)
    {
        if (!HasEnoughStock(quantity))
        {
            throw new InvalidOperationException($"Not enough stock for product #{Id}.");
        }

        Stock -= quantity;
    }
}
