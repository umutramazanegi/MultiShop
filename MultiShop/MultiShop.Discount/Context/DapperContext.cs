using Microsoft.Data.SqlClient; // SQL Server veritabanı işlemleri için ADO.NET sağlayıcısını (SqlConnection vb.) içeri aktarır.
using Microsoft.EntityFrameworkCore; // Entity Framework Core temel sınıflarını (DbContext, DbContextOptionsBuilder, DbSet vb.) içeri aktarır.
using MultiShop.Discount.Entites; // Projedeki 'Coupon' gibi varlık (entity) sınıflarını içeri aktarır.
using System.Data; // Veritabanı bağlantıları için temel arayüzleri (IDbConnection gibi) içeri aktarır.

namespace MultiShop.Discount.Context // 'MultiShop.Discount.Context' ad alanı (namespace) tanımını başlatır.
{
    // Hem Entity Framework Core (DbContext'ten miras alarak) hem de Dapper (CreateConnection metodu ile) kullanımı için tasarlanmış bir context sınıfı.
    public class DapperContext : DbContext
    {
        // Uygulama yapılandırma ayarlarına (appsettings.json) erişim için özel, salt okunur bir alan.
        private readonly IConfiguration _configuration;
        // Veritabanı bağlantı dizesini tutmak için özel, salt okunur bir alan.
        private readonly string _connectionString;

        // DapperContext sınıfının yapıcı metodu (constructor). IConfiguration bağımlılığını enjekte eder.
        public DapperContext(IConfiguration configuration)
        {
            // Enjekte edilen IConfiguration örneğini _configuration alanına atar.
            _configuration = configuration;
            // Yapılandırmadan "DefaultConnection" isimli bağlantı dizesini alır ve _connectionString alanına atar.
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        // DbContext'in yapılandırılması için üzerine yazılan (override) metot. EF Core tarafından kullanılır.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // EF Core için kullanılacak SQL Server bağlantı dizesini doğrudan (hardcoded) ayarlar.
            // Dikkat: Bu, constructor'da yapılandırmadan alınan bağlantı dizesini kullanmaz.
            optionsBuilder.UseSqlServer("Server=UMUT\\SQLEXPRESS;initial Catalog=MultiShopDiscountDb;integrated Security=true");
        }

        // EF Core tarafından yönetilecek 'Coupon' varlıkları için bir DbSet tanımlar. Veritabanındaki 'Coupons' tablosunu temsil eder.
        public DbSet<Coupon> Coupons { get; set; }

        // Dapper kullanımı için yeni bir veritabanı bağlantısı (SqlConnection) oluşturan bir metot.
        // Constructor'da yapılandırmadan alınan _connectionString'i kullanır.
        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
} 