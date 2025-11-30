using System;

class Program
{
    static void Main(string[] args)
    {
        // ---------------------------
        // ORDER 1 (USA Customer)
        // ---------------------------
        Address addr1 = new Address("123 Main St", "Dallas", "TX", "USA");
        Customer cust1 = new Customer("James Carter", addr1);
        Order order1 = new Order(cust1);

        order1.AddProduct(new Product("Laptop", "L123", 900, 1));
        order1.AddProduct(new Product("Mouse", "M555", 25, 2));

        // ---------------------------
        // ORDER 2 (International Customer)
        // ---------------------------
        Address addr2 = new Address("456 Avenue Matadi", "Kinshasa", "Kinshasa", "DRC");
        Customer cust2 = new Customer("Seraphin Kiongo", addr2);
        Order order2 = new Order(cust2);

        order2.AddProduct(new Product("Phone Case", "P001", 10, 3));
        order2.AddProduct(new Product("Charger", "C900", 15, 1));

        // ---------------------------
        // DISPLAY RESULTS
        // ---------------------------
        DisplayOrder(order1);
        Console.WriteLine();
        DisplayOrder(order2);
    }

    static void DisplayOrder(Order order)
    {
        Console.WriteLine(order.GetPackingLabel());
        Console.WriteLine(order.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order.GetTotalCost()}");
        Console.WriteLine("-----------------------------------");
    }
}
