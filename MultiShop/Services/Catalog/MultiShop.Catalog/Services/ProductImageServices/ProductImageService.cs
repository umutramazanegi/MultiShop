using AutoMapper; // AutoMapper kütüphanesini (nesne eşleme için) içeri aktarır.
using MongoDB.Driver; // MongoDB .NET sürücüsünü içeri aktarır.
using MultiShop.Catalog.Dtos.ProductImageDtos; // Ürün Resmi DTO (Data Transfer Object) sınıflarını içeri aktarır.
using MultiShop.Catalog.Entites; // Veritabanı varlık (entity) sınıflarını (ProductImage gibi) içeri aktarır.
using MultiShop.Catalog.Settings; // Veritabanı ayarları sınıflarını (IDatabaseSettings) içeri aktarır.

namespace MultiShop.Catalog.Services.ProductImageServices // 'MultiShop.Catalog.Services.ProductImageServices' ad alanı (namespace) tanımını başlatır.
{
    // IProductImageService arayüzünü uygulayan ProductImageService sınıfı.
    public class ProductImageService : IProductImageService
    {
        // MongoDB'deki ürün resmi koleksiyonuna erişim için özel, salt okunur bir alan.
        private readonly IMongoCollection<ProductImage> _ProductImageCollection;
        // Nesne eşleme işlemleri için özel, salt okunur bir AutoMapper alanı.
        private readonly IMapper _mapper;

        // ProductImageService sınıfının yapıcı metodu (constructor). Bağımlılıkları (IMapper, IDatabaseSettings) enjekte eder.
        public ProductImageService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            // Veritabanı ayarlarına göre yeni bir MongoDB istemcisi (client) oluşturur.
            var client = new MongoClient(_databaseSettings.ConnectionString);
            // İstemciyi kullanarak belirtilen veritabanına erişim sağlar.
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            // Veritabanından, ayarlarda belirtilen isimdeki ProductImage koleksiyonunu alır ve _ProductImageCollection alanına atar.
            _ProductImageCollection = database.GetCollection<ProductImage>(_databaseSettings.ProductImageCollectionName);
            // Enjekte edilen IMapper örneğini _mapper alanına atar.
            _mapper = mapper;
        }

        // Yeni bir ürün resmi oluşturan asenkron metot.
        public async Task CreateProductImageAsync(CreateProductImageDto createProductImageDto)
        {
            // CreateProductImageDto nesnesini ProductImage varlık nesnesine dönüştürür (eşler).
            var value = _mapper.Map<ProductImage>(createProductImageDto);
            // Eşlenen ProductImage nesnesini MongoDB koleksiyonuna asenkron olarak ekler.
            await _ProductImageCollection.InsertOneAsync(value);
        }

        // Belirtilen ID'ye sahip ürün resmini silen asenkron metot.
        public async Task DeleteProductImageAsync(string id)
        {
            // Koleksiyonda ProductImageID'si verilen id'ye eşit olan belgeyi asenkron olarak siler.
            await _ProductImageCollection.DeleteOneAsync(x => x.ProductImageID == id);
        }

        // Belirtilen ID'ye sahip ürün resmini getiren asenkron metot.
        public async Task<GetByIdProductImageDto> GetByIdProductImageAsync(string id)
        {
            // Koleksiyonda ProductImageID'si verilen id'ye eşit olan ilk belgeyi asenkron olarak bulur.
            var values = await _ProductImageCollection.Find<ProductImage>(x => x.ProductImageID == id).FirstOrDefaultAsync();
            // Bulunan ProductImage varlık nesnesini GetByIdProductImageDto nesnesine dönüştürür (eşler) ve döndürür.
            return _mapper.Map<GetByIdProductImageDto>(values);
        }

        // Belirtilen Ürün ID'sine (ProductId) sahip ürün resmini getiren asenkron metot.
        public async Task<GetByIdProductImageDto> GetByProductIdProductImageAsync(string id)
        {
            // Koleksiyonda ProductId'si verilen id'ye eşit olan ilk belgeyi asenkron olarak bulur.
            // Not: Eğer bir ürünün birden fazla resmi olması bekleniyorsa, FirstOrDefaultAsync yerine ToListAsync ve dönüş tipi List<> kullanılabilir.
            var values = await _ProductImageCollection.Find(x => x.ProductId == id).FirstOrDefaultAsync();
            // Bulunan ProductImage varlık nesnesini GetByIdProductImageDto nesnesine dönüştürür (eşler) ve döndürür.
            return _mapper.Map<GetByIdProductImageDto>(values);
        }

        // Tüm ürün resimlerini getiren asenkron metot.
        public async Task<List<ResultProductImageDto>> GettAllProductImageAsync()
        {
            // Koleksiyondaki tüm belgeleri (x => true filtresi ile) asenkron olarak bulur ve bir listeye dönüştürür.
            var values = await _ProductImageCollection.Find(x => true).ToListAsync();
            // Bulunan ProductImage varlık nesneleri listesini ResultProductImageDto listesine dönüştürür (eşler) ve döndürür.
            return _mapper.Map<List<ResultProductImageDto>>(values);
        }

        // Bir ürün resmini güncelleyen asenkron metot.
        public async Task UpdateProductImageAsync(UpdateProductImageDto updateProductImageDto)
        {
            // UpdateProductImageDto nesnesini ProductImage varlık nesnesine dönüştürür (eşler).
            var values = _mapper.Map<ProductImage>(updateProductImageDto);
            // Koleksiyonda ProductImageID'si updateProductImageDto.ProductImageID'ye eşit olan belgeyi bulur ve eşlenen 'values' nesnesiyle asenkron olarak değiştirir.
            await _ProductImageCollection.FindOneAndReplaceAsync(x => x.ProductImageID == updateProductImageDto.ProductImageID, values);
        }
    }
} // 'MultiShop.Catalog.Services.ProductImageServices' ad alanını sonlandırır.