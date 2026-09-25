namespace ProceduralOrderOOP;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Object-Oriented Order System");
        Console.WriteLine("C# version of the original procedural C++ system.");

        OrderSystem system = new OrderSystem();
        system.SeedSampleData();
        system.RunDemoScenario();

        system.PrintCustomers();
        system.PrintProducts();
        system.PrintAllOrders();

        Console.WriteLine($"\nPaid sales total after demo: {system.CalculatePaidSalesTotal():F2}");

        ConsoleMenu menu = new ConsoleMenu(system);
        menu.Run();
    }
}
