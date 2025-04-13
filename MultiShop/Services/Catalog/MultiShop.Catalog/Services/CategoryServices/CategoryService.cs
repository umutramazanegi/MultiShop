using AutoMapper; // AutoMapper kütüphanesini (nesne eşleme için) içeri aktarır.
using MongoDB.Driver; // MongoDB .NET sürücüsünü içeri aktarır.
using MultiShop.Catalog.Dtos.CategoryDtos; // Kategori DTO (Data Transfer Object) sınıflarını içeri aktarır.
using MultiShop.Catalog.Entites; // Veritabanı varlık (entity) sınıflarını (Category gibi) içeri aktarır.
using MultiShop.Catalog.Settings; // Veritabanı ayarları sınıflarını (IDatabaseSettings) içeri aktarır.

namespace MultiShop.Catalog.Services.CategoryServices // 'MultiShop.Catalog.Services.CategoryServices' ad alanı (namespace) tanımını başlatır.
{
    // ICategoryService arayüzünü uygulayan CategoryService sınıfı.
    // ipmplement: Bir sınıfın (class), bir arayüzün (interface) belirlediği sözleşmeyi yerine getirmesi demektir.
    public class CategoryService : ICategoryService
    {
        // MongoDB'deki kategori koleksiyonuna erişim için özel, salt okunur (readonly) bir alan.
        private readonly IMongoCollection<Category> _categoryCollection;
        // Nesne eşleme işlemleri için özel, salt okunur bir AutoMapper alanı.
        private readonly IMapper _mapper;

        // CategoryService sınıfının yapıcı metodu (constructor). Bağımlılıkları (IMapper, IDatabaseSettings) enjekte eder.
        public CategoryService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            // Veritabanı ayarlarına göre yeni bir MongoDB istemcisi (client) oluşturur.
            var client = new MongoClient(_databaseSettings.ConnectionString);
            // İstemciyi kullanarak belirtilen veritabanına erişim sağlar.
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            // Veritabanından, ayarlarda belirtilen isimdeki Kategori koleksiyonunu alır ve _categoryCollection alanına atar.
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
            // Enjekte edilen IMapper örneğini _mapper alanına atar.
            _mapper = mapper;
        }

        // Yeni bir kategori oluşturan asenkron metot.
        public async Task CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            // CreateCategoryDto nesnesini Category varlık nesnesine dönüştürür (eşler).
            var value = _mapper.Map<Category>(createCategoryDto);
            // Eşlenen Category nesnesini MongoDB koleksiyonuna asenkron olarak ekler.
            await _categoryCollection.InsertOneAsync(value);
        }

        // Belirtilen ID'ye sahip kategoriyi silen asenkron metot.
        public async Task DeleteCategoryAsync(string id)
        {
            // Koleksiyonda CategoryId'si verilen id'ye eşit olan belgeyi asenkron olarak siler.
            await _categoryCollection.DeleteOneAsync(x => x.CategoryId == id);
        }

        // Belirtilen ID'ye sahip kategoriyi getiren asenkron metot.
        public async Task<GetByIdCategoryDto> GetByIdCategoryAsync(string id)
        {
            // Koleksiyonda CategoryId'si verilen id'ye eşit olan ilk belgeyi asenkron olarak bulur.
            var values = await _categoryCollection.Find<Category>(x => x.CategoryId == id).FirstOrDefaultAsync();
            // Bulunan Category varlık nesnesini GetByIdCategoryDto nesnesine dönüştürür (eşler) ve döndürür.
            return _mapper.Map<GetByIdCategoryDto>(values);
        }

        // Tüm kategorileri getiren asenkron metot.
        public async Task<List<ResultCategoryDto>> GettAllCategoryAsync()
        {
            // Koleksiyondaki tüm belgeleri (x => true filtresi ile) asenkron olarak bulur ve bir listeye dönüştürür.
            var values = await _categoryCollection.Find(x => true).ToListAsync();
            // Bulunan Category varlık nesneleri listesini ResultCategoryDto listesine dönüştürür (eşler) ve döndürür.
            return _mapper.Map<List<ResultCategoryDto>>(values);
        }

        // Bir kategoriyi güncelleyen asenkron metot.
        public async Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
        {
            // UpdateCategoryDto nesnesini Category varlık nesnesine dönüştürür (eşler).
            var values = _mapper.Map<Category>(updateCategoryDto);
            // Koleksiyonda CategoryId'si updateCategoryDto.CategoryID'ye eşit olan belgeyi bulur ve eşlenen 'values' nesnesiyle asenkron olarak değiştirir.
            await _categoryCollection.FindOneAndReplaceAsync(x => x.CategoryId == updateCategoryDto.CategoryID, values);
        }
    }
} // 'MultiShop.Catalog.Services.CategoryServices' ad alanını sonlandırır.