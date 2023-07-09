

namespace ProductStore.Context {
    public class ProductDbContext : DbContext {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categorys { get; set; }
    }
}
