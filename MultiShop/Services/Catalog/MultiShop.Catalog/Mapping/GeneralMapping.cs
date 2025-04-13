using AutoMapper; // AutoMapper kütüphanesini (nesne eşleme için temel sınıf ve metotları) içeri aktarır.
using MongoDB.Driver.Core.Misc; // MongoDB sürücüsünün bazı yardımcı sınıflarını içeri aktarır.
//using MultiShop.Catalog.Dtos.AboutDtos; // About DTO sınıflarını içeri aktarır.
//using MultiShop.Catalog.Dtos.BrandDtos; // Brand DTO sınıflarını içeri aktarır.
using MultiShop.Catalog.Dtos.CategoryDtos; // Kategori DTO sınıflarını içeri aktarır.
//using MultiShop.Catalog.Dtos.ContactDtos; // Contact DTO sınıflarını içeri aktarır.
//using MultiShop.Catalog.Dtos.FeatureDtos; // Feature DTO sınıflarını içeri aktarır.
//using MultiShop.Catalog.Dtos.FeatureSliderDtos; // FeatureSlider DTO sınıflarını içeri aktarır.
//using MultiShop.Catalog.Dtos.OfferDiscountDtos; // OfferDiscount DTO sınıflarını içeri aktarır.
using MultiShop.Catalog.Dtos.ProductDetailDtos; // ProductDetail DTO sınıflarını içeri aktarır.
using MultiShop.Catalog.Dtos.ProductDtos; // Product DTO sınıflarını içeri aktarır.
using MultiShop.Catalog.Dtos.ProductImageDtos; // ProductImage DTO sınıflarını içeri aktarır.
//using MultiShop.Catalog.Dtos.SpecialOfferDtos; // SpecialOffer DTO sınıflarını içeri aktarır.
using MultiShop.Catalog.Entites; // Veritabanı varlık (entity) sınıflarını içeri aktarır.

namespace MultiShop.Catalog.Mapping // 'MultiShop.Catalog.Mapping' ad alanı (namespace) tanımını başlatır.
{
    // AutoMapper için eşleme (mapping) kurallarını tanımlayan profil sınıfı. Profile sınıfından miras alır.
    public class GeneralMapping : Profile
    {
        // Sınıfın yapıcı metodu (constructor). Eşleme kuralları burada tanımlanır.
        public GeneralMapping()
        {
            // Category varlığı ile ResultCategoryDto arasında çift yönlü eşleme tanımlar.
            CreateMap<Category, ResultCategoryDto>().ReverseMap();
            // Category varlığı ile CreateCategoryDto arasında çift yönlü eşleme tanımlar.
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            // Category varlığı ile UpdateCategoryDto arasında çift yönlü eşleme tanımlar.
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();
            // Category varlığı ile GetByIdCategoryDto arasında çift yönlü eşleme tanımlar.
            CreateMap<Category, GetByIdCategoryDto>().ReverseMap();

            // Product varlığı ile ResultProductDto arasında çift yönlü eşleme tanımlar.
            CreateMap<Product, ResultProductDto>().ReverseMap();
            // Product varlığı ile CreateProductDto arasında çift yönlü eşleme tanımlar.
            CreateMap<Product, CreateProductDto>().ReverseMap();
            // Product varlığı ile UpdateProductDto arasında çift yönlü eşleme tanımlar.
            CreateMap<Product, UpdateProductDto>().ReverseMap();
            // Product varlığı ile GetByIdProductDto arasında çift yönlü eşleme tanımlar.
            CreateMap<Product, GetByIdProductDto>().ReverseMap();

            // ProductDetail varlığı ile ResultProductDetailDto arasında çift yönlü eşleme tanımlar.
            CreateMap<ProductDetail, ResultProductDetailDto>().ReverseMap();
            // ProductDetail varlığı ile CreateProductDetailDto arasında çift yönlü eşleme tanımlar.
            CreateMap<ProductDetail, CreateProductDetailDto>().ReverseMap();
            // ProductDetail varlığı ile UpdateProductDetailDto arasında çift yönlü eşleme tanımlar.
            CreateMap<ProductDetail, UpdateProductDetailDto>().ReverseMap();
            // ProductDetail varlığı ile GetByIdProductDetailDto arasında çift yönlü eşleme tanımlar.
            CreateMap<ProductDetail, GetByIdProductDetailDto>().ReverseMap();

            // ProductImage varlığı ile ResultProductImageDto arasında çift yönlü eşleme tanımlar.
            CreateMap<ProductImage, ResultProductImageDto>().ReverseMap();
            // ProductImage varlığı ile CreateProductImageDto arasında çift yönlü eşleme tanımlar.
            CreateMap<ProductImage, CreateProductImageDto>().ReverseMap();
            // ProductImage varlığı ile UpdateProductImageDto arasında çift yönlü eşleme tanımlar.
            CreateMap<ProductImage, UpdateProductImageDto>().ReverseMap();
            // ProductImage varlığı ile GetByIdProductImageDto arasında çift yönlü eşleme tanımlar.
            CreateMap<ProductImage, GetByIdProductImageDto>().ReverseMap();

            // Product ile ResultProductsWithCategoryDto arasında çift yönlü eşleme tanımı.
            //CreateMap<Product, ResultProductsWithCategoryDto>().ReverseMap();

            // FeatureSlider ile ilgili DTO'lar arasında çift yönlü eşleme tanımları.
            //CreateMap<FeatureSlider, ResultFeatureSliderDto>().ReverseMap();
            //CreateMap<FeatureSlider, CreateFeatureSliderDto>().ReverseMap();
            //CreateMap<FeatureSlider, UpdateFeatureSliderDto>().ReverseMap();
            //CreateMap<FeatureSlider, GetByIdFeatureSliderDto>().ReverseMap();

            // SpecialOffer ile ilgili DTO'lar arasında çift yönlü eşleme tanımları.
            //CreateMap<SpecialOffer, ResultSpecialOfferDto>().ReverseMap();
            //CreateMap<SpecialOffer, CreateSpecialOfferDto>().ReverseMap();
            //CreateMap<SpecialOffer, UpdateSpecialOfferDto>().ReverseMap();
            //CreateMap<SpecialOffer, GetByIdSpecialOfferDto>().ReverseMap();

            // Feature ile ilgili DTO'lar arasında çift yönlü eşleme tanımları.
            //CreateMap<Feature, ResultFeatureDto>().ReverseMap();
            //CreateMap<Feature, CreateFeatureDto>().ReverseMap();
            //CreateMap<Feature, UpdateFeatureDto>().ReverseMap();
            //CreateMap<Feature, GetByIdFeatureDto>().ReverseMap();

            // OfferDiscount ile ilgili DTO'lar arasında çift yönlü eşleme tanımları.
            //CreateMap<OfferDiscount, ResultOfferDiscountDto>().ReverseMap();
            //CreateMap<OfferDiscount, CreateOfferDiscountDto>().ReverseMap();
            //CreateMap<OfferDiscount, UpdateOfferDiscountDto>().ReverseMap();
            //CreateMap<OfferDiscount, GetByIdOfferDiscountDto>().ReverseMap();

            // Brand ile ilgili DTO'lar arasında çift yönlü eşleme tanımları.
            //CreateMap<Brand, ResultBrandDto>().ReverseMap();
            //CreateMap<Brand, CreateBrandDto>().ReverseMap();
            //CreateMap<Brand, UpdateBrandDto>().ReverseMap();
            //CreateMap<Brand, GetByIdBrandDto>().ReverseMap();

            // About ile ilgili DTO'lar arasında çift yönlü eşleme tanımları.
            //CreateMap<About, ResultAboutDto>().ReverseMap();
            //CreateMap<About, CreateAboutDto>().ReverseMap();
            //CreateMap<About, UpdateAboutDto>().ReverseMap();
            //CreateMap<About, GetByIdAboutDto>().ReverseMap();

            // Contact ile ilgili DTO'lar arasında çift yönlü eşleme tanımları.
            //CreateMap<Contact, ResultContactDto>().ReverseMap();
            //CreateMap<Contact, CreateContactDto>().ReverseMap();
            //CreateMap<Contact, UpdateContactDto>().ReverseMap();
            //CreateMap<Contact, GetByIdContactDto>().ReverseMap();
        }

    }
} 