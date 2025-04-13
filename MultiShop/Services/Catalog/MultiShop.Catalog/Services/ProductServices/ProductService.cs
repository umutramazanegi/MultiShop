using AutoMapper; // AutoMapper kütüphanesini (nesne eşleme için) içeri aktarır.
using MongoDB.Driver; // MongoDB .NET sürücüsünü içeri aktarır.
using MultiShop.Catalog.Dtos.CategoryDtos; // Kategori DTO (Data Transfer Object) sınıflarını içeri aktarır (Yorumlu kodda kullanılıyor).
using MultiShop.Catalog.Dtos.ProductDtos; // Ürün DTO sınıflarını içeri aktarır.
using MultiShop.Catalog.Entites; // Veritabanı varlık (entity) sınıflarını (Product, Category) içeri aktarır.
using MultiShop.Catalog.Settings; // Veritabanı ayarları sınıflarını (IDatabaseSettings) içeri aktarır.

namespace MultiShop.Catalog.Services.ProductServices // 'MultiShop.Catalog.Services.ProductServices' ad alanı (namespace) tanımını başlatır.
{
    // IProductService arayüzünü uygulayan ProductService sınıfı.
    public class ProductService : IProductService
    {
        // Nesne eşleme işlemleri için özel, salt okunur bir AutoMapper alanı.
        private readonly IMapper _mapper;
        // MongoDB'deki ürün koleksiyonuna erişim için özel, salt okunur bir alan.
        private readonly IMongoCollection<Product> _productCollection;
        // MongoDB'deki kategori koleksiyonuna erişim için özel, salt okunur bir alan (Yorumlu kodda kullanılıyor).
        private readonly IMongoCollection<Category> _categoryCollection;

        // ProductService sınıfının yapıcı metodu (constructor). Bağımlılıkları (IMapper, IDatabaseSettings) enjekte eder.
        public ProductService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            // Veritabanı ayarlarına göre yeni bir MongoDB istemcisi (client) oluşturur.
            var client = new MongoClient(_databaseSettings.ConnectionString);
            // İstemciyi kullanarak belirtilen veritabanına erişim sağlar.
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            // Veritabanından, ayarlarda belirtilen isimdeki Product koleksiyonunu alır ve _productCollection alanına atar.
            _productCollection = database.GetCollection<Product>(_databaseSettings.ProductCollectionName);
            // Veritabanından, ayarlarda belirtilen isimdeki Category koleksiyonunu alır ve _categoryCollection alanına atar.
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
            // Enjekte edilen IMapper örneğini _mapper alanına atar.
            _mapper = mapper;
        }

        // Yeni bir ürün oluşturan asenkron metot.
        public async Task CreateProductAsync(CreateProductDto createProductDto)
        {
            // CreateProductDto nesnesini Product varlık nesnesine dönüştürür (eşler).
            var values = _mapper.Map<Product>(createProductDto);
            // Eşlenen Product nesnesini MongoDB koleksiyonuna asenkron olarak ekler.
            await _productCollection.InsertOneAsync(values);
        }

        // Belirtilen ID'ye sahip ürünü silen asenkron metot.
        public async Task DeleteProductAsync(string id)
        {
            // Koleksiyonda ProductId'si verilen id'ye eşit olan belgeyi asenkron olarak siler.
            await _productCollection.DeleteOneAsync(x => x.ProductId == id);
        }

        // Belirtilen ID'ye sahip ürünü getiren asenkron metot.
        public async Task<GetByIdProductDto> GetByIdProductAsync(string id)
        {
            // Koleksiyonda ProductId'si verilen id'ye eşit olan ilk belgeyi asenkron olarak bulur.
            var values = await _productCollection.Find<Product>(x => x.ProductId == id).FirstOrDefaultAsync();
            // Bulunan Product varlık nesnesini GetByIdProductDto nesnesine dönüştürür (eşler) ve döndürür.
            return _mapper.Map<GetByIdProductDto>(values);
        }

        // Ürünleri kategorileriyle birlikte getiren metot (şu anda yorumlu).
        //public async Task<List<ResultProductsWithCategoryDto>> GetProductsWithCategoryAsync()
        //{
        //    // Tüm ürünleri koleksiyondan asenkron olarak alır.
        //    var values = await _productCollection.Find(x => true).ToListAsync();
        //
        //    // Alınan her ürün için döngü başlatır.
        //    foreach (var item in values)
        //    {
        //        // Ürünün CategoryId'sine göre ilgili kategoriyi kategori koleksiyonundan bulur ve ürünün Category özelliğine atar.
        //        item.Category = await _categoryCollection.Find<Category>(x => x.CategoryId == item.CategoryId).FirstAsync();
        //    }
        //
        //    // Kategori bilgisi eklenmiş ürün listesini ResultProductsWithCategoryDto listesine eşler ve döndürür.
        //    return _mapper.Map<List<ResultProductsWithCategoryDto>>(values);
        //}

        // Belirli bir kategoriye ait ürünleri kategorileriyle birlikte getiren metot (şu anda yorumlu).
        //public async Task<List<ResultProductsWithCategoryDto>> GetProductsWithCategoryByCatetegoryIdAsync(string CategoryId)
        //{
        //    // Verilen CategoryId'ye sahip ürünleri koleksiyondan asenkron olarak alır.
        //    var values = await _productCollection.Find(x => x.CategoryId == CategoryId).ToListAsync();
        //
        //    // Alınan her ürün için döngü başlatır.
        //    foreach (var item in values)
        //    {
        //        // Ürünün CategoryId'sine göre ilgili kategoriyi kategori koleksiyonundan bulur ve ürünün Category özelliğine atar.
        //        item.Category = await _categoryCollection.Find<Category>(x => x.CategoryId == item.CategoryId).FirstAsync();
        //    }
        //
        //    // Kategori bilgisi eklenmiş ürün listesini ResultProductsWithCategoryDto listesine eşler ve döndürür.
        //    return _mapper.Map<List<ResultProductsWithCategoryDto>>(values);
        //}

        // Tüm ürünleri getiren asenkron metot.
        public async Task<List<ResultProductDto>> GettAllProductAsync()
        {
            // Koleksiyondaki tüm belgeleri (x => true filtresi ile) asenkron olarak bulur ve bir listeye dönüştürür.
            var values = await _productCollection.Find(x => true).ToListAsync();
            // Bulunan Product varlık nesneleri listesini ResultProductDto listesine dönüştürür (eşler) ve döndürür.
            return _mapper.Map<List<ResultProductDto>>(values);
        }

        // Bir ürünü güncelleyen asenkron metot.
        public async Task UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            // UpdateProductDto nesnesini Product varlık nesnesine dönüştürür (eşler).
            var values = _mapper.Map<Product>(updateProductDto);
            // Koleksiyonda ProductId'si updateProductDto.ProductId'ye eşit olan belgeyi bulur ve eşlenen 'values' nesnesiyle asenkron olarak değiştirir.
            await _productCollection.FindOneAndReplaceAsync(x => x.ProductId == updateProductDto.ProductId, values);
        }
    }
} // 'MultiShop.Catalog.Services.ProductServices' ad alanını sonlandırır.