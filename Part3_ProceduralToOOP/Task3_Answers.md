# Task 3 — Builder Pattern

## Task 3.1 — Questions

### 1. Why is a single 20-parameter constructor a problem in practice?

A constructor with around 20 parameters is difficult to read at the call site. When many values have the same type, the compiler can accept values in the wrong order without detecting the mistake. For example, two decimal values such as subtotal and tax can be swapped and the code will still compile. The same problem can happen with several string parameters such as city, state, country, or payment method.

It also becomes harder to maintain when another property is added. Every constructor call may need another argument, making existing code longer and increasing the chance of mistakes. Optional values are also awkward because callers have to remember placeholder values or create additional constructor overloads.

The Builder pattern solves this by giving each value a named method and by moving final validation into `Build()`.

### 2. Is this only a long-constructor problem?

No. The deeper issue is that the class contains several different concepts in one object. Customer information, billing/shipping addresses, and order/payment information have different responsibilities and validation rules.

A huge constructor is therefore a symptom of a larger cohesion problem. The object has too much unrelated construction data. Separating the address and order/payment concepts makes the model easier to understand, validate, reuse, and change.

## Task 3.2 — Single Builder Design

The `InvoiceBuilder` uses a fluent API. The caller can set properties with named methods such as `SetCustomerEmail()` and `SetTaxAmount()` instead of depending on parameter order.

Mandatory properties chosen for this solution:

- InvoiceId
- CustomerName
- CustomerEmail
- BillingAddress
- ShippingAddress
- OrderDate
- PaymentMethod
- Currency
- SubTotal

Optional properties:

- CustomerPhone
- DiscountAmount
- TaxAmount

`Build()` validates all mandatory properties and rejects invalid monetary values immediately. `TotalAmount` is calculated by the invoice rather than manually supplied, so the caller cannot create an inconsistent total.

## Task 3.3 — Composed Builders

The composed version separates responsibilities into `AddressBuilder` and `OrderBuilder`.

### Single Responsibility

`AddressBuilder` knows only how to construct and validate an address. `OrderBuilder` knows only about order/payment information. `ComposedInvoiceBuilder` combines the already-valid pieces with invoice/customer data.

### Independent Validation

Yes. `AddressBuilder.Build()` can guarantee that street, city, state, zip code, and country are present without the invoice builder knowing those address-specific rules.

`OrderBuilder.Build()` independently validates order date, payment method, currency, subtotal, discount, and tax rules.

### Reuse

The same `AddressBuilder` implementation is used for both billing and shipping. Without it, the street/city/state/zip/country construction and validation logic would have to be duplicated or pushed into a large parent builder.

### Readability at the Call Site

The single builder in Task 3.2 puts every concern into one long chain. The composed version lets the code build a billing address, a shipping address, and order information as meaningful separate objects, then combine them. This makes the code easier to scan and makes each validation rule easier to find.

## Design Summary

The final design uses the Builder pattern to make complex object creation readable and safe. Task 3.2 improves the construction process, while Task 3.3 improves the structure by composing smaller builders around natural domain boundaries.
