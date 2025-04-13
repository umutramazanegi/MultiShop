using MultiShop.Catalog.Dtos.ProductImageDtos; // Ürün Resmi Data Transfer Object (DTO) sınıflarını içeren ad alanını ekler.

namespace MultiShop.Catalog.Services.ProductImageServices // 'MultiShop.Catalog.Services.ProductImageServices' ad alanı (namespace) tanımını başlatır.
{
    // Ürün resmi işlemleri için metot imzalarını tanımlayan bir arayüz (interface).
    public interface IProductImageService
    {
        // Tüm ürün resimlerini asenkron olarak getirecek metotun imzası. Geriye Task<List<ResultProductImageDto>> döner.
        Task<List<ResultProductImageDto>> GettAllProductImageAsync();
        // Yeni bir ürün resmi oluşturacak asenkron metotun imzası. Parametre olarak CreateProductImageDto alır.
        Task CreateProductImageAsync(CreateProductImageDto createProductImageDto);
        // Bir ürün resmini güncelleyecek asenkron metotun imzası. Parametre olarak UpdateProductImageDto alır.
        Task UpdateProductImageAsync(UpdateProductImageDto updateProductImageDto);
        // Belirtilen ID'ye sahip ürün resmini silecek asenkron metotun imzası. Parametre olarak string ID alır.
        Task DeleteProductImageAsync(string id);
        // Belirtilen ID'ye sahip ürün resmini getirecek asenkron metotun imzası. Parametre olarak string ID alır ve Geriye Task<GetByIdProductImageDto> döner.
        Task<GetByIdProductImageDto> GetByIdProductImageAsync(string id);
        // Belirtilen Ürün ID'sine (ProductId) sahip ürün resmini getirecek asenkron metotun imzası. Parametre olarak string ID alır ve Geriye Task<GetByIdProductImageDto> döner.
        Task<GetByIdProductImageDto> GetByProductIdProductImageAsync(string id); // Not: Ürünün birden fazla resmi varsa, dönüş tipi List<> olabilir.
    }
} // 'MultiShop.Catalog.Services.ProductImageServices' ad alanını sonlandırır.