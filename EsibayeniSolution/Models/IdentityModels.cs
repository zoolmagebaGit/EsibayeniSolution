using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EsibayeniSolution.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    { 
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
          // Database.SetInitializer<ApplicationDbContext>( new DropCreateDatabaseIfModelChanges<ApplicationDbContext>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

      
      
        public System.Data.Entity.DbSet<EsibayeniSolution.Models.Batch> Batches { get; set; }

        public System.Data.Entity.DbSet<EsibayeniSolution.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<EsibayeniSolution.Models.Supplier> Suppliers { get; set; }

        public System.Data.Entity.DbSet<EsibayeniSolution.Models.LivesStock> LivesStocks { get; set; }

        public System.Data.Entity.DbSet<EsibayeniSolution.Models.LivestockImage> LivestockImages { get; set; }

        public System.Data.Entity.DbSet<EsibayeniSolution.Models.MaintainanceProcess> MaintainanceProcesses { get; set; }

        public System.Data.Entity.DbSet<EsibayeniSolution.Models.MaintainanceStock> MaintainanceStocks { get; set; }

        public System.Data.Entity.DbSet<EsibayeniSolution.Models.ProductCategory> ProductCategories { get; set; }
        public System.Data.Entity.DbSet<EsibayeniSolution.Models.Maintainance> Maintainances { get; set; }
        public System.Data.Entity.DbSet<EsibayeniSolution.Models.LivestockMaintainanceStock> LivestockMaintainanceStocks { get; set; }

        public System.Data.Entity.DbSet<EsibayeniSolution.Models.LiveStockWeight> LiveStockWeights { get; set; }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order_Item> Order_Items { get; set; }
        public DbSet<Order_Address> Order_Addresses { get; set; }
        public DbSet<Order_Tracking> Order_Trackings { get; set; }
        public DbSet<Cart_Item> Cart_Items { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public System.Data.Entity.DbSet<EsibayeniSolution.Models.CategoryClasses.CategoryCost> CategoryCosts { get; set; }

        // public System.Data.Entity.DbSet<EsibayeniSolution.Models.LivestockImagesVM> LivestockImagesVMs { get; set; }
    }
}