# Critique of the Procedural Order System

## 1. Global Variables

The program stores customers, products, and orders using global variables and arrays.

This is a design problem because any function can directly access and modify the shared data. As the program becomes larger, it becomes difficult to track where the data is being changed. An unexpected modification could also affect other parts of the system.

## 2. No Classes or Objects

The program does not have classes representing important entities such as customers, products, orders, or order lines.

This makes the code harder to organize because the data and the operations related to that data are separated. For example, customer information is stored in several arrays while customer-related operations are implemented as separate functions.

## 3. Parallel Arrays

Customer information is stored in multiple arrays such as customer IDs, names, emails, cities, and VIP status. Products and orders are also represented using multiple related arrays.

This design depends on indexes remaining synchronized. If one array is changed incorrectly, the data from different entities could become mixed up. For example, a customer's name could accidentally be associated with another customer's email or VIP status.

## 4. Fixed-Size Arrays

The program uses fixed maximum sizes such as 50 customers, 50 products, and 100 orders.

This limits the system's ability to grow. Once one of these limits is reached, new data cannot be added even if the computer has enough memory. Changing these limits also requires modifying the source code.

## 5. Functions Depend on Shared State

Many functions directly read and modify the global arrays.

This creates strong dependency between the functions and the global state. A function cannot easily be reused or tested independently because it expects specific global data to exist and be in a valid state.

## 6. Order Lines Are Difficult to Represent

Order lines are stored using two-dimensional arrays for product indexes and quantities.

This makes an order line difficult to understand as a single concept because the product and its quantity are stored separately. It also requires the program to keep the indexes synchronized. A dedicated order-line object would make this relationship clearer.

## 7. Weak Encapsulation

The program does not protect its data from direct access because the main data structures are global.

This means there are no clear boundaries controlling how customer, product, or order data can be changed. Invalid states could be created if another part of the program modifies the data incorrectly.

## 8. High Coupling

The functions are highly dependent on the specific global data structures used by the program.

For example, changing how customers or products are stored would require changes in several functions. This makes the system harder to maintain and extend.

## 9. Difficult Maintenance

As more features are added, the procedural structure can make the program increasingly difficult to maintain.

New functionality may require adding more global variables or functions that interact with the existing global state. This can make the code more complex and increase the possibility of introducing bugs.

## 10. Limited Separation of Responsibilities

The program contains data management, business rules, and user interaction in the same procedural structure.

For example, functions are responsible for manipulating data while the menu directly interacts with those operations. Separating responsibilities into classes would make the system easier to understand, test, and modify.

## Conclusion

The current program works, but its procedural design makes the system harder to maintain, extend, and protect from invalid changes. The main issues are the use of global state, parallel arrays, fixed-size storage, lack of encapsulation, and the absence of objects representing the real entities in the system.

The next step should be to redesign the system using object-oriented principles while preserving the existing behavior of the original program.
