namespace Task3_BuilderPattern;

// Task 3.3: composes the smaller builders without duplicating their rules.
public sealed class ComposedInvoiceBuilder
{
    private int? _invoiceId;
    private string? _customerName;
    private string? _customerEmail;
    private string? _customerPhone;
    private Address? _billingAddress;
    private Address? _shippingAddress;
    private OrderInfo? _orderInfo;

    public ComposedInvoiceBuilder SetInvoiceId(int invoiceId)
    {
        _invoiceId = invoiceId;
        return this;
    }

    public ComposedInvoiceBuilder SetCustomerName(string customerName)
    {
        _customerName = customerName;
        return this;
    }

    public ComposedInvoiceBuilder SetCustomerEmail(string customerEmail)
    {
        _customerEmail = customerEmail;
        return this;
    }

    public ComposedInvoiceBuilder SetCustomerPhone(string? customerPhone)
    {
        _customerPhone = customerPhone;
        return this;
    }

    public ComposedInvoiceBuilder SetBillingAddress(Address address)
    {
        _billingAddress = address;
        return this;
    }

    public ComposedInvoiceBuilder SetShippingAddress(Address address)
    {
        _shippingAddress = address;
        return this;
    }

    public ComposedInvoiceBuilder SetOrderInfo(OrderInfo orderInfo)
    {
        _orderInfo = orderInfo;
        return this;
    }

    public ComposedInvoice Build()
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

        if (_orderInfo is null)
            throw new InvalidOperationException("Order information is mandatory.");

        return new ComposedInvoice(
            _invoiceId.Value,
            _customerName,
            _customerEmail,
            _customerPhone,
            _billingAddress,
            _shippingAddress,
            _orderInfo);
    }
}
