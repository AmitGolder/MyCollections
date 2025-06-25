using Microsoft.EntityFrameworkCore;
using MyCollections.Models;

namespace MyCollections.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Brand> Brands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Brands with sample data
            modelBuilder.Entity<Brand>().HasData(
                new Brand { Id = 1, Name = "PORSCHE" },
                new Brand { Id = 2, Name = "McLAREN" },
                new Brand { Id = 3, Name = "HONDA" },
                new Brand { Id = 4, Name = "TYOTA"},
                new Brand { Id = 5, Name = "NISSAN"},
                new Brand { Id = 6, Name= "MAZDA"}
            );

            // Seed Cars with BrandId reference
            modelBuilder.Entity<Car>().HasData(
                new Car { Id = 1, Name = "McLAREN F1", BrandId = 2, Year = 2025, ProductNo = "HTB11", ImagePath = "/images/mclaren f1.jpg" },
                new Car { Id = 2, Name = "HONDA S800 RACING", BrandId = 3, Year = 2025, ProductNo = "HRY58", ImagePath = "/images/honda s800 racing.jpg" },
                new Car { Id = 3, Name = "94 TYOTA SUPRA", BrandId = 4, Year = 2025, ProductNo = "HTF27", ImagePath = "/images/94 toyota supra.jpg" },
                new Car { Id = 4, Name = "95 MAZDA RX-7", BrandId = 6, Year = 2025, ProductNo = "HTD97", ImagePath = "/images/95 mazda rx-7.jpg" },
                new Car { Id = 5, Name = "96 PORSCHE CARRERA", BrandId = 1, Year = 2025, ProductNo = "HTF01", ImagePath = "/images/96 porsche carrera.jpg" },
                new Car { Id = 6, Name = "91 MAZDA MX-5 MIATA", BrandId = 6, Year = 2025, ProductNo = "HTD80", ImagePath = "/images/91 mazda mx-5 miata.jpg" },
                new Car { Id = 7, Name = "NISSAN SKYLINE GT-R", BrandId = 5, Year = 2025, ProductNo = "HYY72", ImagePath = "/images/nissan skyline gt-r.jpg" }
            );
        }
    }



}
