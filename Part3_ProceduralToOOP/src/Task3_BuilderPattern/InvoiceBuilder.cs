namespace Task3_BuilderPattern;

// Task 3.2: one fluent builder for the whole invoice.
public sealed class InvoiceBuilder
{
    private int? _invoiceId;
    private string? _customerName;
    private string? _customerEmail;
    private string? _customerPhone;
    private Address? _billingAddress;
    private Address? _shippingAddress;
    private DateTime? _orderDate;
    private string? _paymentMethod;
    private string? _currency;
    private decimal? _subTotal;
    private decimal _discountAmount;
    private decimal _taxAmount;

    public InvoiceBuilder SetInvoiceId(int invoiceId)
    {
        _invoiceId = invoiceId;
        return this;
    }

    public InvoiceBuilder SetCustomerName(string customerName)
    {
        _customerName = customerName;
        return this;
    }

    public InvoiceBuilder SetCustomerEmail(string customerEmail)
    {
        _customerEmail = customerEmail;
        return this;
    }

    public InvoiceBuilder SetCustomerPhone(string? customerPhone)
    {
        _customerPhone = customerPhone;
        return this;
    }

    public InvoiceBuilder SetBillingAddress(Address billingAddress)
    {
        _billingAddress = billingAddress;
        return this;
    }

    public InvoiceBuilder SetShippingAddress(Address shippingAddress)
    {
        _shippingAddress = shippingAddress;
        return this;
    }

    public InvoiceBuilder SetOrderDate(DateTime orderDate)
    {
        _orderDate = orderDate;
        return this;
    }

    public InvoiceBuilder SetPaymentMethod(string paymentMethod)
    {
        _paymentMethod = paymentMethod;
        return this;
    }

    public InvoiceBuilder SetCurrency(string currency)
    {
        _currency = currency;
        return this;
    }

    public InvoiceBuilder SetSubTotal(decimal subTotal)
    {
        _subTotal = subTotal;
        return this;
    }

    public InvoiceBuilder SetDiscountAmount(decimal discountAmount)
    {
        _discountAmount = discountAmount;
        return this;
    }

    public InvoiceBuilder SetTaxAmount(decimal taxAmount)
    {
        _taxAmount = taxAmount;
        return this;
    }

    public Invoice Build()
    {
        Validate();

        if (_discountAmount < 0)
            throw new InvalidOperationException("Discount amount cannot be negative.");

        if (_taxAmount < 0)
            throw new InvalidOperationException("Tax amount cannot be negative.");

        if (_discountAmount > _subTotal!.Value)
            throw new InvalidOperationException("Discount amount cannot be greater than subtotal.");

        return new Invoice(
            _invoiceId!.Value,
            _customerName!,
            _customerEmail!,
            _customerPhone,
            _billingAddress!,
            _shippingAddress!,
            _orderDate!.Value,
            _paymentMethod!,
            _currency!,
            _subTotal!.Value,
            _discountAmount,
            _taxAmount);
    }

    private void Validate()
    {
        if (!_invoiceId.HasValue || _invoiceId <= 0)
            throw new InvalidOperationException("InvoiceId is mandatory and must be positive.");

        if (string.IsNullOrWhiteSpace(_customerName))
            throw new InvalidOperationException("CustomerName is mandatory.");

        if (string.IsNullOrWhiteSpace(_customerEmail))
            throw new InvalidOperationException("CustomerEmail is mandatory.");

        if (_billingAddress is null)
            throw new InvalidOperationException("BillingAddress is mandatory.");

        if (_shippingAddress is null)
            throw new InvalidOperationException("ShippingAddress is mandatory.");

        if (!_orderDate.HasValue)
            throw new InvalidOperationException("OrderDate is mandatory.");

        if (string.IsNullOrWhiteSpace(_paymentMethod))
            throw new InvalidOperationException("PaymentMethod is mandatory.");

        if (string.IsNullOrWhiteSpace(_currency))
            throw new InvalidOperationException("Currency is mandatory.");

        if (!_subTotal.HasValue || _subTotal < 0)
            throw new InvalidOperationException("SubTotal is mandatory and cannot be negative.");
    }
}
