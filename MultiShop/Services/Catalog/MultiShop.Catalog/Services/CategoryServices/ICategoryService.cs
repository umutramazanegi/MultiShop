using MultiShop.Catalog.Dtos.CategoryDtos; // Kategori Data Transfer Object (DTO) sınıflarını içeren ad alanını ekler.

namespace MultiShop.Catalog.Services.CategoryServices // 'MultiShop.Catalog.Services.CategoryServices' ad alanı (namespace) tanımını başlatır.
{
    // Kategori işlemleri için metot imzalarını tanımlayan bir arayüz (interface).
    public interface ICategoryService
    {
        // Tüm kategorileri asenkron olarak getirecek metotun imzası. Geriye Task<List<ResultCategoryDto>> döner.
        Task<List<ResultCategoryDto>> GettAllCategoryAsync();
        // Yeni bir kategori oluşturacak asenkron metotun imzası. Parametre olarak CreateCategoryDto alır.
        Task CreateCategoryAsync(CreateCategoryDto createCategoryDto);
        // Bir kategoriyi güncelleyecek asenkron metotun imzası. Parametre olarak UpdateCategoryDto alır.
        Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto);
        // Belirtilen ID'ye sahip kategoriyi silecek asenkron metotun imzası. Parametre olarak string ID alır.
        Task DeleteCategoryAsync(string id);
        // Belirtilen ID'ye sahip kategoriyi getirecek asenkron metotun imzası. Parametre olarak string ID alır ve Geriye Task<GetByIdCategoryDto> döner.
        Task<GetByIdCategoryDto> GetByIdCategoryAsync(string id);
    }
   // NOT:  asenkron: İşlem bitene kadar programın başka işler yapabilmesini sağlar.
} 