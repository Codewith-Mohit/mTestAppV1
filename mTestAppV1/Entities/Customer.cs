namespace mTestAppV1.Entities
{
    public class Customer
    {
        public required Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

    }
}
