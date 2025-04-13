using AutoMapper; // AutoMapper kütüphanesini (nesne eşleme için) içeri aktarır.
using MongoDB.Driver; // MongoDB .NET sürücüsünü içeri aktarır.
using MultiShop.Catalog.Dtos.ProductDetailDtos; // Ürün Detay DTO (Data Transfer Object) sınıflarını içeri aktarır.
using MultiShop.Catalog.Entites; // Veritabanı varlık (entity) sınıflarını (ProductDetail gibi) içeri aktarır.
using MultiShop.Catalog.Settings; // Veritabanı ayarları sınıflarını (IDatabaseSettings) içeri aktarır.

namespace MultiShop.Catalog.Services.ProductDetailDetailServices // 'MultiShop.Catalog.Services.ProductDetailDetailServices' ad alanı (namespace) tanımını başlatır.
{
    // IProductDetailService arayüzünü uygulayan ProductDetailService sınıfı.
    public class ProductDetailService : IProductDetailService
    {
        // Nesne eşleme işlemleri için özel, salt okunur bir AutoMapper alanı.
        private readonly IMapper _mapper;
        // MongoDB'deki ürün detay koleksiyonuna erişim için özel, salt okunur bir alan.
        private readonly IMongoCollection<ProductDetail> _ProductDetailCollection;

        // ProductDetailService sınıfının yapıcı metodu (constructor). Bağımlılıkları (IMapper, IDatabaseSettings) enjekte eder.
        public ProductDetailService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            // Veritabanı ayarlarına göre yeni bir MongoDB istemcisi (client) oluşturur.
            var client = new MongoClient(_databaseSettings.ConnectionString);
            // İstemciyi kullanarak belirtilen veritabanına erişim sağlar.
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            // Veritabanından, ayarlarda belirtilen isimdeki ProductDetail koleksiyonunu alır ve _ProductDetailCollection alanına atar.
            _ProductDetailCollection = database.GetCollection<ProductDetail>(_databaseSettings.ProductDetailCollectionName);
            // Enjekte edilen IMapper örneğini _mapper alanına atar.
            _mapper = mapper;
        }

        // Yeni bir ürün detayı oluşturan asenkron metot.
        public async Task CreateProductDetailAsync(CreateProductDetailDto createProductDetailDto)
        {
            // CreateProductDetailDto nesnesini ProductDetail varlık nesnesine dönüştürür (eşler).
            var values = _mapper.Map<ProductDetail>(createProductDetailDto);
            // Eşlenen ProductDetail nesnesini MongoDB koleksiyonuna asenkron olarak ekler.
            await _ProductDetailCollection.InsertOneAsync(values);
        }

        // Belirtilen ID'ye sahip ürün detayını silen asenkron metot.
        public async Task DeleteProductDetailAsync(string id)
        {
            // Koleksiyonda ProductDetailId'si verilen id'ye eşit olan belgeyi asenkron olarak siler.
            await _ProductDetailCollection.DeleteOneAsync(x => x.ProductDetailId == id);
        }

        // Belirtilen ID'ye sahip ürün detayını getiren asenkron metot.
        public async Task<GetByIdProductDetailDto> GetByIdProductDetailAsync(string id)
        {
            // Koleksiyonda ProductDetailId'si verilen id'ye eşit olan ilk belgeyi asenkron olarak bulur.
            var values = await _ProductDetailCollection.Find<ProductDetail>(x => x.ProductDetailId == id).FirstOrDefaultAsync();
            // Bulunan ProductDetail varlık nesnesini GetByIdProductDetailDto nesnesine dönüştürür (eşler) ve döndürür.
            return _mapper.Map<GetByIdProductDetailDto>(values);
        }

        // Belirtilen Ürün ID'sine (ProductId) sahip ürün detayını getiren asenkron metot.
        public async Task<GetByIdProductDetailDto> GetByProductIdProductDetailAsync(string id)
        {
            // Koleksiyonda ProductId'si verilen id'ye eşit olan ilk belgeyi asenkron olarak bulur.
            var values = await _ProductDetailCollection.Find<ProductDetail>(x => x.ProductId == id).FirstOrDefaultAsync();
            // Bulunan ProductDetail varlık nesnesini GetByIdProductDetailDto nesnesine dönüştürür (eşler) ve döndürür.
            return _mapper.Map<GetByIdProductDetailDto>(values);
        }

        // Tüm ürün detaylarını getiren asenkron metot.
        public async Task<List<ResultProductDetailDto>> GettAllProductDetailAsync()
        {
            // Koleksiyondaki tüm belgeleri (x => true filtresi ile) asenkron olarak bulur ve bir listeye dönüştürür.
            var values = await _ProductDetailCollection.Find(x => true).ToListAsync();
            // Bulunan ProductDetail varlık nesneleri listesini ResultProductDetailDto listesine dönüştürür (eşler) ve döndürür.
            return _mapper.Map<List<ResultProductDetailDto>>(values);
        }

        // Bir ürün detayını güncelleyen asenkron metot.
        public async Task UpdateProductDetailAsync(UpdateProductDetailDto updateProductDetailDto)
        {
            // UpdateProductDetailDto nesnesini ProductDetail varlık nesnesine dönüştürür (eşler).
            var values = _mapper.Map<ProductDetail>(updateProductDetailDto);
            // Koleksiyonda ProductDetailId'si updateProductDetailDto.ProductDetailId'ye eşit olan belgeyi bulur ve eşlenen 'values' nesnesiyle asenkron olarak değiştirir.
            await _ProductDetailCollection.FindOneAndReplaceAsync(x => x.ProductDetailId == updateProductDetailDto.ProductDetailId, values);
        }
    }
} // 'MultiShop.Catalog.Services.ProductDetailDetailServices' ad alanını sonlandırır.