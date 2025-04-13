using MultiShop.Catalog.Dtos.ProductDtos; // Ürün Data Transfer Object (DTO) sınıflarını içeren ad alanını ekler.

namespace MultiShop.Catalog.Services.ProductServices // 'MultiShop.Catalog.Services.ProductServices' ad alanı (namespace) tanımını başlatır.
{
    // Ürün işlemleri için metot imzalarını tanımlayan bir arayüz (interface).
    public interface IProductService
    {
        // Tüm ürünleri asenkron olarak getirecek metotun imzası. Geriye Task<List<ResultProductDto>> döner.
        Task<List<ResultProductDto>> GettAllProductAsync();
        // Yeni bir ürün oluşturacak asenkron metotun imzası. Parametre olarak CreateProductDto alır.
        Task CreateProductAsync(CreateProductDto createProductDto);
        // Bir ürünü güncelleyecek asenkron metotun imzası. Parametre olarak UpdateProductDto alır.
        Task UpdateProductAsync(UpdateProductDto updateProductDto);
        // Belirtilen ID'ye sahip ürünü silecek asenkron metotun imzası. Parametre olarak string ID alır.
        Task DeleteProductAsync(string id);
        // Belirtilen ID'ye sahip ürünü getirecek asenkron metotun imzası. Parametre olarak string ID alır ve Geriye Task<GetByIdProductDto> döner.
        Task<GetByIdProductDto> GetByIdProductAsync(string id);
        // Ürünleri kategorileriyle birlikte getirecek metot imzası 
        //Task<List<ResultProductsWithCategoryDto>> GetProductsWithCategoryAsync();
        // Belirli bir kategoriye ait ürünleri kategorileriyle birlikte getirecek metot imzası
        //Task<List<ResultProductsWithCategoryDto>> GetProductsWithCategoryByCatetegoryIdAsync(string CategoryId);
    }
} 