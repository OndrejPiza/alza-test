using AlzaTest.Products.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace AlzaTest.Products.Persistence.DBContexts
{
	public class ProductsDbContext : DbContext
	{
		public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options)
		{
		}

		public DbSet<Product> Products { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			var productBuilder = modelBuilder.Entity<Product>();

			productBuilder
				.ToTable("Products")
				.HasKey(p => p.Id);

			productBuilder
				.Property(p => p.Id)
				.IsRequired();

			productBuilder
				.Property(p => p.Name)
				.IsRequired();

			productBuilder
				.Property(p => p.ImgUri)
				.IsRequired();

			productBuilder
				.Property(p => p.Price)
				.IsRequired();
		}
	}
}
