namespace MultiShop.Catalog.Settings // 'MultiShop.Catalog.Settings' ad alanı (namespace) tanımını başlatır.
{
    // Veritabanı ayarları için gerekli özellikleri tanımlayan bir arayüz (interface).
    public interface IDatabaseSettings
    {
        // MongoDB'deki Kategori koleksiyonunun adını alır veya ayarlar.
        public string CategoryCollectionName { get; set; }
        // MongoDB'deki Ürün koleksiyonunun adını alır veya ayarlar.
        public string ProductCollectionName { get; set; }
        // MongoDB'deki Ürün Detay koleksiyonunun adını alır veya ayarlar.
        public string ProductDetailCollectionName { get; set; }
        // MongoDB'deki Ürün Resmi koleksiyonunun adını alır veya ayarlar.
        public string ProductImageCollectionName { get; set; }
        //public string FeatureSliderCollectionName { get; set; } // Öne Çıkan Slider koleksiyon adı özelliği 
        //public string SpecialOfferCollectionName { get; set; } // Özel Teklif koleksiyon adı özelliği .
        //public string FeatureCollectionName { get; set; } // Özellik koleksiyon adı özelliği .
        //public string OfferDiscountCollectionName { get; set; } // İndirim Teklifi koleksiyon adı özelliği.
        //public string BrandCollectionName { get; set; } // Marka koleksiyon adı özelliği .
        //public string AboutCollectionName { get; set; } // Hakkında koleksiyon adı özelliği .
        //public string ContactCollectionName { get; set; } // İletişim koleksiyon adı özelliği.
        // Veritabanına bağlanmak için kullanılan bağlantı dizesini (connection string) alır veya ayarlar.
        public string ConnectionString { get; set; }
        // Kullanılacak veritabanının adını alır veya ayarlar.
        public string DatabaseName { get; set; }
    }
} 