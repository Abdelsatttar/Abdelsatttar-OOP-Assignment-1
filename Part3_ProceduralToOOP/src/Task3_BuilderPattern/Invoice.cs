namespace Task3_BuilderPattern;

public sealed class Invoice
{
    public int InvoiceId { get; }
    public string CustomerName { get; }
    public string CustomerEmail { get; }
    public string? CustomerPhone { get; }

    public Address BillingAddress { get; }
    public Address ShippingAddress { get; }

    public DateTime OrderDate { get; }
    public string PaymentMethod { get; }
    public string Currency { get; }
    public decimal SubTotal { get; }
    public decimal DiscountAmount { get; }
    public decimal TaxAmount { get; }
    public decimal TotalAmount { get; }

    internal Invoice(
        int invoiceId,
        string customerName,
        string customerEmail,
        string? customerPhone,
        Address billingAddress,
        Address shippingAddress,
        DateTime orderDate,
        string paymentMethod,
        string currency,
        decimal subTotal,
        decimal discountAmount,
        decimal taxAmount)
    {
        InvoiceId = invoiceId;
        CustomerName = customerName;
        CustomerEmail = customerEmail;
        CustomerPhone = customerPhone;
        BillingAddress = billingAddress;
        ShippingAddress = shippingAddress;
        OrderDate = orderDate;
        PaymentMethod = paymentMethod;
        Currency = currency;
        SubTotal = subTotal;
        DiscountAmount = discountAmount;
        TaxAmount = taxAmount;
        TotalAmount = SubTotal - DiscountAmount + TaxAmount;
    }
}
