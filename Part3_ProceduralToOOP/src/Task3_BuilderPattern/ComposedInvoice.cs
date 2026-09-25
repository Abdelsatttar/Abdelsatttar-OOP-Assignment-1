namespace Task3_BuilderPattern;

public sealed class ComposedInvoice
{
    public int InvoiceId { get; }
    public string CustomerName { get; }
    public string CustomerEmail { get; }
    public string? CustomerPhone { get; }
    public Address BillingAddress { get; }
    public Address ShippingAddress { get; }
    public OrderInfo OrderInfo { get; }

    public decimal TotalAmount => OrderInfo.TotalAmount;

    internal ComposedInvoice(
        int invoiceId,
        string customerName,
        string customerEmail,
        string? customerPhone,
        Address billingAddress,
        Address shippingAddress,
        OrderInfo orderInfo)
    {
        InvoiceId = invoiceId;
        CustomerName = customerName;
        CustomerEmail = customerEmail;
        CustomerPhone = customerPhone;
        BillingAddress = billingAddress;
        ShippingAddress = shippingAddress;
        OrderInfo = orderInfo;
    }
}
