using Lab_10.Data;
using Lab_10.Models.DbModels;
using Microsoft.EntityFrameworkCore;
// Noa Denise Ishac NET23
namespace Lab_10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var context = new NorthwindContext())
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Choose a number from the menu:");
                    Console.WriteLine("1. Show all customers in ascending order");
                    Console.WriteLine("2. Show all customers in descending order");
                    Console.WriteLine("3. Show customer information and orders");
                    Console.WriteLine("4. Add a new customer");
                    Console.WriteLine("5. Exit program");

                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            GetCustomers(context, true); //anropar funktionen för att visa kunder i stigande ordning
                            break;

                        case "2":
                            GetCustomers(context, false); //anropar funktionen för att visa kunder i fallande ordning
                            break;

                        case "3":
                            ShowCustomerDetails(context);//anropar funktion som visar kunddetaljer och deras ordrar
                            break;

                        case "4":
                            AddCustomer(context);//anropar funktion för att lägga till ny kund
                            break;

                        case "5":
                            return;

                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }

                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        static void GetCustomers(NorthwindContext context, bool ascending)
        {
            var customers = context.Customers
                .OrderBy(c => c.CompanyName)
                .ToList();

            if (!ascending)
                customers.Reverse();//vänder på ordningen i listan

            foreach (var customer in customers)
            {
                Console.WriteLine($"Customer ID: {customer.CustomerId}");
                Console.WriteLine($"Company Name: {customer.CompanyName}");
                Console.WriteLine($"Country: {customer.Country}");
                Console.WriteLine($"Region: {customer.Region}");
                Console.WriteLine($"Phone-number: {customer.Phone}");
                Console.WriteLine($"Amount of orders: {customer.Orders.Count}");
                Console.WriteLine();
            }
        }

        static void ShowCustomerDetails(NorthwindContext context)
        {
            Console.Write("Enter the customer ID of the chosen customer: ");
            string customerId = Console.ReadLine();

            var customer = context.Customers
                .Include(c => c.Orders)//inkluderar kundens ordrar
                .SingleOrDefault(c => c.CustomerId == customerId);

            if (customer != null)
            {
                Console.WriteLine($"Company Name: {customer.CompanyName}");
                Console.WriteLine($"Country: {customer.Country}");
                Console.WriteLine($"Region: {customer.Region}");
                Console.WriteLine($"Phone-number: {customer.Phone}");
                Console.WriteLine($"Amount of orders: {customer.Orders.Count}");

                foreach (var order in customer.Orders)
                {
                    Console.WriteLine($"Order ID: {order.OrderId}, Date: {order.OrderDate}");
                }
            }
            else
            {
                Console.WriteLine("Customer not found.");
            }
        }

        static void AddCustomer(NorthwindContext context)
        {
            Console.Write("Enter company name: ");
            string companyName = Console.ReadLine();

            Console.Write("Enter country: ");
            string country = Console.ReadLine();

            Console.Write("Enter region: ");
            string region = Console.ReadLine();

            Console.Write("Enter phone-number: ");
            string phone = Console.ReadLine();

            var newCustomer = new Customer
            {
                CustomerId = GenerateRandomID(5),
                CompanyName = companyName,
                Country = string.IsNullOrEmpty(country) ? null : country,
                Region = string.IsNullOrEmpty(region) ? null : region,
                Phone = string.IsNullOrEmpty(phone) ? null : phone
            };

            context.Customers.Add(newCustomer);
            context.SaveChanges();

            Console.WriteLine("\nCustomer added!");
        }

        private static string GenerateRandomID(int length)
        {
            //konstant sträng med alla tecken för att generera slumpmässigt ID
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)//ny sträng med 5 tecken
                .Select(s => s[random.Next(s.Length)]).ToArray());//slumpar ett index för varje tecken i strängen chars
        }
    }
  }