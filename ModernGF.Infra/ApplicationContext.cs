using Microsoft.EntityFrameworkCore;

namespace ModernGF.Infra
{
    public partial class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Customizations must go after base.OnModelCreating(builder)
            //builder.ApplyConfiguration(new ProductMap());
        }
    }
}
