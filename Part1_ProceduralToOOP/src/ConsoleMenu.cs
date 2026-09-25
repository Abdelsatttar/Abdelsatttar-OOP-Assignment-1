namespace ProceduralOrderOOP;

public class ConsoleMenu
{
    private readonly OrderSystem _system;

    public ConsoleMenu(OrderSystem system)
    {
        _system = system;
    }

    public void Run()
    {
        int choice = -1;

        while (choice != 0)
        {
            PrintMenu();
            choice = ReadInt();

            switch (choice)
            {
                case 1:
                    _system.PrintCustomers();
                    break;
                case 2:
                    _system.PrintProducts();
                    break;
                case 3:
                    _system.PrintAllOrders();
                    break;
                case 4:
                    Console.Write("Order id: ");
                    _system.PrintOrder(ReadInt());
                    break;
                case 5:
                    CreateOrder();
                    break;
                case 6:
                    AddLine();
                    break;
                case 7:
                    MarkPaid();
                    break;
                case 8:
                    Console.WriteLine($"Paid sales total: {_system.CalculatePaidSalesTotal():F2}");
                    break;
                case 0:
                    Console.WriteLine("Bye.");
                    break;
                default:
                    Console.WriteLine("Unknown choice.");
                    break;
            }
        }
    }

    private void PrintMenu()
    {
        Console.WriteLine("\n---------- MENU ----------");
        Console.WriteLine("1) Print customers");
        Console.WriteLine("2) Print products");
        Console.WriteLine("3) Print all orders");
        Console.WriteLine("4) Print one order by id");
        Console.WriteLine("5) Create order");
        Console.WriteLine("6) Add line to order");
        Console.WriteLine("7) Mark order paid");
        Console.WriteLine("8) Show paid sales total");
        Console.WriteLine("0) Exit");
        Console.Write("Choice: ");
    }

    private void CreateOrder()
    {
        Console.Write("Order id: ");
        int orderId = ReadInt();

        Console.Write("Customer id: ");
        int customerId = ReadInt();

        Console.Write("Date (YYYY-MM-DD): ");
        string date = Console.ReadLine() ?? string.Empty;

        if (!_system.CreateOrder(orderId, customerId, date, out string errorMessage))
        {
            Console.WriteLine(errorMessage);
        }
    }

    private void AddLine()
    {
        Console.Write("Order id: ");
        int orderId = ReadInt();

        Console.Write("Product id: ");
        int productId = ReadInt();

        Console.Write("Quantity: ");
        int quantity = ReadInt();

        if (!_system.AddLineToOrder(orderId, productId, quantity, out string errorMessage))
        {
            Console.WriteLine(errorMessage);
        }
    }

    private void MarkPaid()
    {
        Console.Write("Order id: ");
        int orderId = ReadInt();

        if (!_system.MarkOrderPaid(orderId, out string errorMessage))
        {
            Console.WriteLine(errorMessage);
        }
    }

    private int ReadInt()
    {
        while (!int.TryParse(Console.ReadLine(), out int value))
        {
            Console.Write("Please enter a valid number: ");
        }

        return value;
    }
}
