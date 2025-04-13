using MultiShop.Catalog.Dtos.ProductDetailDtos; // Ürün Detay Data Transfer Object (DTO) sınıflarını içeren ad alanını ekler.

namespace MultiShop.Catalog.Services.ProductDetailDetailServices // 'MultiShop.Catalog.Services.ProductDetailDetailServices' ad alanı (namespace) tanımını başlatır.
{
    // Ürün detay işlemleri için metot imzalarını tanımlayan bir arayüz (interface).
    public interface IProductDetailService
    {
        // Tüm ürün detaylarını asenkron olarak getirecek metotun imzası. Geriye Task<List<ResultProductDetailDto>> döner.
        Task<List<ResultProductDetailDto>> GettAllProductDetailAsync();
        // Yeni bir ürün detayı oluşturacak asenkron metotun imzası. Parametre olarak CreateProductDetailDto alır.
        Task CreateProductDetailAsync(CreateProductDetailDto createProductDetailDto);
        // Bir ürün detayını güncelleyecek asenkron metotun imzası. Parametre olarak UpdateProductDetailDto alır.
        Task UpdateProductDetailAsync(UpdateProductDetailDto updateProductDetailDto);
        // Belirtilen ID'ye sahip ürün detayını silecek asenkron metotun imzası. Parametre olarak string ID alır.
        Task DeleteProductDetailAsync(string id);
        // Belirtilen ID'ye sahip ürün detayını getirecek asenkron metotun imzası. Parametre olarak string ID alır ve Geriye Task<GetByIdProductDetailDto> döner.
        Task<GetByIdProductDetailDto> GetByIdProductDetailAsync(string id);
        // Belirtilen Ürün ID'sine (ProductId) sahip ürün detayını getirecek asenkron metotun imzası. Parametre olarak string ID alır ve Geriye Task<GetByIdProductDetailDto> döner.
        Task<GetByIdProductDetailDto> GetByProductIdProductDetailAsync(string id);
    }
} // 'MultiShop.Catalog.Services.ProductDetailDetailServices' ad alanını sonlandırır.