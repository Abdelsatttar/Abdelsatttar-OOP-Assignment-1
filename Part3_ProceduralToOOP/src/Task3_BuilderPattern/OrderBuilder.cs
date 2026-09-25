namespace Task3_BuilderPattern;

// Task 3.3: reusable builder responsible only for order/payment information.
public sealed class OrderBuilder
{
    private DateTime? _orderDate;
    private string? _paymentMethod;
    private string? _currency;
    private decimal? _subTotal;
    private decimal _discountAmount;
    private decimal _taxAmount;

    public OrderBuilder SetOrderDate(DateTime orderDate)
    {
        _orderDate = orderDate;
        return this;
    }

    public OrderBuilder SetPaymentMethod(string paymentMethod)
    {
        _paymentMethod = paymentMethod;
        return this;
    }

    public OrderBuilder SetCurrency(string currency)
    {
        _currency = currency;
        return this;
    }

    public OrderBuilder SetSubTotal(decimal subTotal)
    {
        _subTotal = subTotal;
        return this;
    }

    public OrderBuilder SetDiscountAmount(decimal discountAmount)
    {
        _discountAmount = discountAmount;
        return this;
    }

    public OrderBuilder SetTaxAmount(decimal taxAmount)
    {
        _taxAmount = taxAmount;
        return this;
    }

    public OrderInfo Build()
    {
        if (!_orderDate.HasValue)
            throw new InvalidOperationException("OrderDate is required.");

        if (string.IsNullOrWhiteSpace(_paymentMethod))
            throw new InvalidOperationException("PaymentMethod is required.");

        if (string.IsNullOrWhiteSpace(_currency))
            throw new InvalidOperationException("Currency is required.");

        if (!_subTotal.HasValue || _subTotal < 0)
            throw new InvalidOperationException("SubTotal is required and cannot be negative.");

        if (_discountAmount < 0)
            throw new InvalidOperationException("Discount amount cannot be negative.");

        if (_taxAmount < 0)
            throw new InvalidOperationException("Tax amount cannot be negative.");

        if (_discountAmount > _subTotal.Value)
            throw new InvalidOperationException("Discount amount cannot be greater than subtotal.");

        return new OrderInfo(
            _orderDate.Value,
            _paymentMethod,
            _currency,
            _subTotal.Value,
            _discountAmount,
            _taxAmount);
    }
}
