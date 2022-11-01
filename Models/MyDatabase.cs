using DemoWebTemplate.Models.Shop;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DemoWebTemplate.Models
{
    public class MyDatabase : IdentityDbContext<AppUser>
    {
        //options sẽ đc DI inject khi dịch vụ Myblogcontext được tạo ra.
        public MyDatabase(DbContextOptions<MyDatabase> options) : base(options)
        {

        }
        //luôn luôn nạp chồng OnConfiguring . nó sẽ hoạt động khi MyBlogContext được tạo ra.
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }

        //phương thức thường đucợ nạp chồng.
        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Recieve> Recieves { get; set; }
        public DbSet<Tracking> Trackings { get; set; }

        public DbSet<RecieveDetail> RecieveDetails { get; set; }


    }
}
