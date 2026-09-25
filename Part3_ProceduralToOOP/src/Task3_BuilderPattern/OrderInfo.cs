namespace Task3_BuilderPattern;

public sealed class OrderInfo
{
    public DateTime OrderDate { get; }
    public string PaymentMethod { get; }
    public string Currency { get; }
    public decimal SubTotal { get; }
    public decimal DiscountAmount { get; }
    public decimal TaxAmount { get; }
    public decimal TotalAmount => SubTotal - DiscountAmount + TaxAmount;

    internal OrderInfo(
        DateTime orderDate,
        string paymentMethod,
        string currency,
        decimal subTotal,
        decimal discountAmount,
        decimal taxAmount)
    {
        OrderDate = orderDate;
        PaymentMethod = paymentMethod;
        Currency = currency;
        SubTotal = subTotal;
        DiscountAmount = discountAmount;
        TaxAmount = taxAmount;
    }
}
