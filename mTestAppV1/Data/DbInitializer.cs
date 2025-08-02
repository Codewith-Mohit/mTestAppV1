using mTestAppV1.Entities;

namespace mTestAppV1.Data
{
    public static class DbInitializer
    {
        public static void Seed(TestDB context)
        {
            if (!context.Customers.Any()) // Only seed if empty
            {
                context.Customers.AddRange(new List<Customer>
                {
                    new Customer { Id = new Guid(), Name = "Admin", Email = "admin@example.com" },
                    new Customer { Id = new Guid(), Name = "Tyagi", Email = "test@example.com" },
                    new Customer { Id = new Guid(), Name = "HrDept.", Email = "hr@example.com" },
                    new Customer { Id = new Guid(), Name = "Info", Email = "info@example.com" }
                });

                context.SaveChanges();
            }
        }
    }
}
