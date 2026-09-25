namespace Task3_BuilderPattern;

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("=== Task 3.2: Single InvoiceBuilder ===");

        var billingAddress = new AddressBuilder()
            .SetStreet("10 Nile Street")
            .SetCity("Cairo")
            .SetState("Cairo")
            .SetZipCode("11511")
            .SetCountry("Egypt")
            .Build();

        var shippingAddress = new AddressBuilder()
            .SetStreet("25 Tahrir Street")
            .SetCity("Giza")
            .SetState("Giza")
            .SetZipCode("12511")
            .SetCountry("Egypt")
            .Build();

        var invoice = new InvoiceBuilder()
            .SetInvoiceId(1001)
            .SetCustomerName("Ahmed Mohamed")
            .SetCustomerEmail("ahmed@example.com")
            .SetCustomerPhone("01000000000")
            .SetBillingAddress(billingAddress)
            .SetShippingAddress(shippingAddress)
            .SetOrderDate(new DateTime(2026, 9, 25))
            .SetPaymentMethod("Credit Card")
            .SetCurrency("EGP")
            .SetSubTotal(5000m)
            .SetDiscountAmount(500m)
            .SetTaxAmount(450m)
            .Build();

        PrintInvoice(invoice);

        Console.WriteLine();
        Console.WriteLine("=== Task 3.3: Composed Builders ===");

        var sharedAddressBuilder = new AddressBuilder();
        var billing = sharedAddressBuilder
            .SetStreet("10 Nile Street")
            .SetCity("Cairo")
            .SetState("Cairo")
            .SetZipCode("11511")
            .SetCountry("Egypt")
            .Build();

        var shipping = new AddressBuilder()
            .SetStreet("25 Tahrir Street")
            .SetCity("Giza")
            .SetState("Giza")
            .SetZipCode("12511")
            .SetCountry("Egypt")
            .Build();

        var orderInfo = new OrderBuilder()
            .SetOrderDate(new DateTime(2026, 9, 25))
            .SetPaymentMethod("Credit Card")
            .SetCurrency("EGP")
            .SetSubTotal(5000m)
            .SetDiscountAmount(500m)
            .SetTaxAmount(450m)
            .Build();

        var composedInvoice = new ComposedInvoiceBuilder()
            .SetInvoiceId(1002)
            .SetCustomerName("Sara Ali")
            .SetCustomerEmail("sara@example.com")
            .SetCustomerPhone("01111111111")
            .SetBillingAddress(billing)
            .SetShippingAddress(shipping)
            .SetOrderInfo(orderInfo)
            .Build();

        PrintInvoice(composedInvoice);

        Console.WriteLine();
        Console.WriteLine("=== Mandatory Validation Demo ===");

        try
        {
            new InvoiceBuilder()
                .SetCustomerName("Missing Id")
                .Build();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static void PrintInvoice(Invoice invoice)
    {
        Console.WriteLine($"Invoice: #{invoice.InvoiceId}");
        Console.WriteLine($"Customer: {invoice.CustomerName} | {invoice.CustomerEmail}");
        Console.WriteLine($"Billing: {invoice.BillingAddress}");
        Console.WriteLine($"Shipping: {invoice.ShippingAddress}");
        Console.WriteLine($"Payment: {invoice.PaymentMethod} | {invoice.Currency}");
        Console.WriteLine($"Total: {invoice.TotalAmount:N2} {invoice.Currency}");
    }

    private static void PrintInvoice(ComposedInvoice invoice)
    {
        Console.WriteLine($"Invoice: #{invoice.InvoiceId}");
        Console.WriteLine($"Customer: {invoice.CustomerName} | {invoice.CustomerEmail}");
        Console.WriteLine($"Billing: {invoice.BillingAddress}");
        Console.WriteLine($"Shipping: {invoice.ShippingAddress}");
        Console.WriteLine($"Payment: {invoice.OrderInfo.PaymentMethod} | {invoice.OrderInfo.Currency}");
        Console.WriteLine($"Total: {invoice.TotalAmount:N2} {invoice.OrderInfo.Currency}");
    }
}
