using System;
using Microsoft.EntityFrameworkCore;

namespace PolicyApi.Models
{
    public class Policy
    {
        public long Id { get; set; }
        public PolicyStatus Status { get; set; }
        public Product Product { get; set; }
        public Party Party { get; set; }
    }

    public enum PolicyStatus
    {
        ISSUED,
        CANCELLED,
        ISSUED_REINSTATED,
        VOID,
        NOT_ISSUED,
        EXPIRED
    }

    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }

    public class Party
    {
        public long Id { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
    }

    public class PolicyContext : DbContext
    {
        public PolicyContext(DbContextOptions<PolicyContext> options) : base(options)
        {
        }

        public DbSet<Policy> Policies { get; set; }
    }

    public class PolicyDTO
    {
        public long Id { get; set; }
        public Product Product { get; set; }
        public Party Party { get; set; }

        public Policy ToPolicy()
        {
            return new Policy
            {
                Status = PolicyStatus.ISSUED,
                Party = Party,
                Product = Product
            };
        }
    }
}