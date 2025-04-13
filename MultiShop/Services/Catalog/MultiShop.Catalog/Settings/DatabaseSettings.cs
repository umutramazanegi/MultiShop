namespace MultiShop.Catalog.Settings // 'MultiShop.Catalog.Settings' ad alanını (namespace) tanımlar.
{
    // IDatabaseSettings arayüzünü uygulayan (implemente eden) DatabaseSettings sınıfı.
    public class DatabaseSettings : IDatabaseSettings
    {
        // Kategori koleksiyon adını tutan özellik (property).
        public string CategoryCollectionName { get; set; }
        // Ürün koleksiyon adını tutan özellik.
        public string ProductCollectionName { get; set; }
        // Ürün Detay koleksiyon adını tutan özellik.
        public string ProductDetailCollectionName { get; set; }
        // Ürün Resmi koleksiyon adını tutan özellik.
        public string ProductImageCollectionName { get; set; }
        // Veritabanı bağlantı dizesini tutan özellik.
        public string ConnectionString { get; set; }
        // Veritabanı adını tutan özellik.
        public string DatabaseName { get; set; }
        // Öne Çıkan Slider koleksiyon adı özelliği.
        //public string FeatureSliderCollectionName { get; set; }
        // Özel Teklif koleksiyon adı özelliği.
        //public string SpecialOfferCollectionName { get; set; }
        // Özellik koleksiyon adı özelliği.
        //public string FeatureCollectionName { get; set; }
        // İndirim Teklifi koleksiyon adı özelliği.
        //public string OfferDiscountCollectionName { get; set; }
        // Marka koleksiyon adı özelliği.
        //public string BrandCollectionName { get; set; }
        // Hakkında koleksiyon adı özelliği.
        //public string AboutCollectionName { get; set; }
        // İletişim koleksiyon adı özelliği.
        //public string ContactCollectionName { get; set; }
    }
} 