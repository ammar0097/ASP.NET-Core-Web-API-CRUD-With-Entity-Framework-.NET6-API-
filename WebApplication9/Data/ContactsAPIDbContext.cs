using Microsoft.EntityFrameworkCore;
using WebApplication9.Models;

namespace WebApplication9.Data
{
    public class ContactsAPIDbContext : DbContext
    {
        public ContactsAPIDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
