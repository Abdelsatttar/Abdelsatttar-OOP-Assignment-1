namespace ProceduralOrderOOP;

public class OrderSystem
{
    private const int MaxCustomers = 50;
    private const int MaxProducts = 50;
    private const int MaxOrders = 100;

    private readonly List<Customer> _customers = new();
    private readonly List<Product> _products = new();
    private readonly List<Order> _orders = new();

    public bool AddCustomer(Customer customer, out string errorMessage)
    {
        errorMessage = string.Empty;

        if (_customers.Count >= MaxCustomers)
        {
            errorMessage = "ERROR: customer list is full.";
            return false;
        }

        if (_customers.Any(c => c.Id == customer.Id))
        {
            errorMessage = $"ERROR: customer id {customer.Id} already exists.";
            return false;
        }

        _customers.Add(customer);
        return true;
    }

    public bool AddProduct(Product product, out string errorMessage)
    {
        errorMessage = string.Empty;

        if (_products.Count >= MaxProducts)
        {
            errorMessage = "ERROR: product list is full.";
            return false;
        }

        if (_products.Any(p => p.Id == product.Id))
        {
            errorMessage = $"ERROR: product id {product.Id} already exists.";
            return false;
        }

        _products.Add(product);
        return true;
    }

    public Customer? FindCustomerById(int id)
    {
        return _customers.FirstOrDefault(c => c.Id == id);
    }

    public Product? FindProductById(int id)
    {
        return _products.FirstOrDefault(p => p.Id == id);
    }

    public Order? FindOrderById(int id)
    {
        return _orders.FirstOrDefault(o => o.Id == id);
    }

    public bool CreateOrder(int orderId, int customerId, string date, out string errorMessage)
    {
        errorMessage = string.Empty;

        if (_orders.Count >= MaxOrders)
        {
            errorMessage = "ERROR: order list is full.";
            return false;
        }

        if (_orders.Any(o => o.Id == orderId))
        {
            errorMessage = $"ERROR: order id {orderId} already exists.";
            return false;
        }

        Customer? customer = FindCustomerById(customerId);
        if (customer is null)
        {
            errorMessage = $"ERROR: customer id {customerId} not found.";
            return false;
        }

        _orders.Add(new Order(orderId, customer, date));
        return true;
    }

    public bool AddLineToOrder(int orderId, int productId, int quantity, out string errorMessage)
    {
        errorMessage = string.Empty;

        Order? order = FindOrderById(orderId);
        if (order is null)
        {
            errorMessage = $"ERROR: order id {orderId} not found.";
            return false;
        }

        Product? product = FindProductById(productId);
        if (product is null)
        {
            errorMessage = $"ERROR: product id {productId} not found.";
            return false;
        }

        return order.AddLine(product, quantity, out errorMessage);
    }

    public bool MarkOrderPaid(int orderId, out string errorMessage)
    {
        errorMessage = string.Empty;

        Order? order = FindOrderById(orderId);
        if (order is null)
        {
            errorMessage = $"ERROR: order id {orderId} not found.";
            return false;
        }

        return order.MarkAsPaid(out errorMessage);
    }

    public decimal CalculatePaidSalesTotal()
    {
        return _orders
            .Where(o => o.IsPaid)
            .Sum(o => o.CalculateTotal());
    }

    public void PrintCustomers()
    {
        Console.WriteLine($"\n=== CUSTOMERS ({_customers.Count}) ===");

        foreach (Customer customer in _customers)
        {
            Console.WriteLine($"#{customer.Id}  {customer.Name}  <{customer.Email}>  {customer.City}  vip={(customer.IsVip ? "yes" : "no")}");
        }
    }

    public void PrintProducts()
    {
        Console.WriteLine($"\n=== PRODUCTS ({_products.Count}) ===");

        foreach (Product product in _products)
        {
            Console.WriteLine($"#{product.Id}  {product.Name}  price={product.Price:F2}  stock={product.Stock}");
        }
    }

    public void PrintOrder(int orderId)
    {
        Order? order = FindOrderById(orderId);
        if (order is null)
        {
            Console.WriteLine($"ERROR: order id {orderId} not found.");
            return;
        }

        Console.WriteLine($"\n=== ORDER #{order.Id} ===");
        Console.WriteLine($"Date: {order.Date}");
        Console.WriteLine($"Customer: {order.Customer.Name} (#{order.Customer.Id})");
        Console.WriteLine($"Paid: {(order.IsPaid ? "yes" : "no")}");
        Console.WriteLine("Lines:");

        foreach (OrderLine line in order.Lines)
        {
            Console.WriteLine($"  - {line.Product.Name}  x{line.Quantity}  @{line.Product.Price:F2}  = {line.CalculateTotal():F2}");
        }

        Console.WriteLine($"TOTAL: {order.CalculateTotal():F2}");
    }

    public void PrintAllOrders()
    {
        Console.WriteLine($"\n=== ALL ORDERS ({_orders.Count}) ===");

        foreach (Order order in _orders)
        {
            PrintOrder(order.Id);
        }
    }

    public void SeedSampleData()
    {
        AddCustomer(new Customer(1, "Mona Ali", "mona@example.com", "Cairo", true), out _);
        AddCustomer(new Customer(2, "Omar Hassan", "omar@example.com", "Alexandria", false), out _);
        AddCustomer(new Customer(3, "Sara Nabil", "sara@example.com", "Giza", false), out _);

        AddProduct(new Product(101, "USB Cable", 50.0m, 100), out _);
        AddProduct(new Product(102, "Wireless Mouse", 250.0m, 40), out _);
        AddProduct(new Product(103, "Mechanical Keyboard", 1200.0m, 15), out _);
        AddProduct(new Product(104, "Laptop Stand", 400.0m, 25), out _);
    }

    public void RunDemoScenario()
    {
        CreateOrder(1001, 1, "2026-09-15", out _);
        AddLineToOrder(1001, 101, 2, out _);
        AddLineToOrder(1001, 102, 1, out _);
        MarkOrderPaid(1001, out _);

        CreateOrder(1002, 2, "2026-09-15", out _);
        AddLineToOrder(1002, 103, 1, out _);
        AddLineToOrder(1002, 104, 1, out _);

        CreateOrder(1003, 3, "2026-09-16", out _);
        AddLineToOrder(1003, 101, 5, out _);
        MarkOrderPaid(1003, out _);
    }
}
