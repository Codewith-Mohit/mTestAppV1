namespace mTestAppV1.Entities
{
    public class Order
    {
        public required Guid OrderId { get; set; }

        public required Guid CustomerId { get; set; }

        public decimal OrderAmount { get; set; }

        public DateTime OrderDate { get; set; }

    }
}
