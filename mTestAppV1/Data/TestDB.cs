using Microsoft.EntityFrameworkCore;
using mTestAppV1.Entities;

namespace mTestAppV1.Data
{
    public class TestDB : DbContext
    {
        public TestDB(DbContextOptions<TestDB> options) : base(options)
        {

        }
        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Order> Orders { get; set; }
    }    
}
