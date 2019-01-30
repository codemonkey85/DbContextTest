using DbContextTest.Extensions;
using DbContextTest.Migrations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace DbContextTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (TestContext context = new TestContext())
            {
                context.Database.CreateIfNotExists();
                context.TruncateTable<TestClass>();
                context.Tests.Add(new TestClass { Name = "Test!" });
                context.SaveChanges();
            }
        }
    }

    [Table("test_classes")]
    public class TestClass
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [Column("name")]
        public string Name { get; set; }
    }

    public class TestContext : DbContext
    {
        public TestContext() /*: base("name=test_classes")*/ =>
             Database.SetInitializer(new MigrateDatabaseToLatestVersion<TestContext, Configuration>());

        public DbSet<TestClass> Tests { get; set; }
    }
}