namespace Debugging
{
    internal class Program
    {
        private static bool isDiscountApplied = false;
        static void Main(string[] args)
        {
            int[] quantity = new int[] { 3, 4, 2, 5, 6, 4, 8, 2, 3, 9, 9, 3, 2, 6, 7, 3, 4, 5, 6, 7 };
            int[] price = new int[] { 300, 100, 300, 200, 400, 500, 600, 700, 200, 300, 500, 400, 700, 200, 500, 600, 600, 900, 400, 300 };
            int[] avg = new int[20];
            for (int i = 0; i < quantity.Length; i++)
            {
                avg[i] = price[i] / quantity[i];
                Console.WriteLine($"Average: {avg[i]}, Quantity: {quantity[i]}, Price: {price[i]}");
            }
            ProcessOrder(new Order());
            Order order = new Order
            {
                total = 200
            };
            ApplyDiscount(order);
            ProcessOrder(order);
            Console.ReadKey();
            OrderValidator validator = new OrderValidator();
            validator.Validate(order);
            Validator validate = new Validator();
            string input = "hello";
            validate.Validate(input);
        }
        public static void ApplyDiscount(Order order)
        {
            isDiscountApplied = true;
            order.total = order.total * .90;

        }
        public static void ProcessOrder(Order order)
        {
            Console.WriteLine($"Order Total: {order.total}");
            Console.WriteLine($"Order Discount Applied: {isDiscountApplied}");
        }
        public class Order()
        {
            public double total { get; set; }
        }
        public class Validator
        {
            public bool Validate(string input)
            {
                if (string.IsNullOrEmpty(input))
                {
                    return false;
                }
                return true;
            }
        }
        public class OrderValidator
        {
            public bool Validate(Order order)
            {
                return order.total>0;
            }
        }
    }
    
}
