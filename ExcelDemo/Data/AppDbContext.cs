using Microsoft.EntityFrameworkCore;
using ExcelDemo.Models;

namespace ExcelDemo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ExcelDocument> ExcelDocuments { get; set; }
    }
}