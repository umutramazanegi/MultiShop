using Microsoft.AspNetCore.Authentication.JwtBearer; // JWT Bearer kimlik doðrulama bileþenlerini içeri aktarýr.
using Microsoft.Extensions.Options; // Yapýlandýrma desenleri için IOptions'ý içeri aktarýr.
//using MultiShop.Catalog.Services.AboutServices; //  About servisi arayüzlerini/sýnýflarýný içeri aktarýr 
//using MultiShop.Catalog.Services.BrandServices; //  Brand servisi arayüzlerini/sýnýflarýný içeri aktarýr 
using MultiShop.Catalog.Services.CategoryServices; // Kategori servisi arayüzlerini/sýnýflarýný içeri aktarýr.
//using MultiShop.Catalog.Services.ContactServices; // Contact servisi arayüzlerini/sýnýflarýný içeri aktarýr 
//using MultiShop.Catalog.Services.FeatureServices; //  Feature servisi arayüzlerini/sýnýflarýný içeri aktarýr 
//using MultiShop.Catalog.Services.FeatureSliderServices; //  FeatureSlider servisi arayüzlerini/sýnýflarýný içeri aktarýr
//using MultiShop.Catalog.Services.OfferDiscountServices; //  OfferDiscount servisi arayüzlerini/sýnýflarýný içeri aktarýr 
using MultiShop.Catalog.Services.ProductDetailDetailServices; // Ürün Detay servisi arayüzlerini/sýnýflarýný içeri aktarýr.
using MultiShop.Catalog.Services.ProductImageServices; // Ürün Resim servisi arayüzlerini/sýnýflarýný içeri aktarýr.
using MultiShop.Catalog.Services.ProductServices; // Ürün servisi arayüzlerini/sýnýflarýný içeri aktarýr.
//using MultiShop.Catalog.Services.SpecialOfferServices; //  SpecialOffer servisi arayüzlerini/sýnýflarýný içeri aktarýr 
//using MultiShop.Catalog.Services.StatisticServices; //  Statistic servisi arayüzlerini/sýnýflarýný içeri aktarýr 
using MultiShop.Catalog.Settings; // Veritabaný ayarlarý sýnýflarýný içeri aktarýr.
using System.Reflection; // Yansýma (reflection) yeteneklerini içeri aktarýr (AutoMapper için kullanýlýr).

var builder = WebApplication.CreateBuilder(args); // Varsayýlan ayarlarla yeni bir web uygulamasý oluþturucu (builder) baþlatýr.

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // Baðýmlýlýk enjeksiyonuna (DI) kimlik doðrulama servislerini ekler, varsayýlan þema olarak JWT Bearer'ý belirtir.
    .AddJwtBearer(opt => // JWT Bearer kimlik doðrulama iþleyicisini (handler) yapýlandýrýr.
    { // JWT Bearer yapýlandýrma bloðunu baþlatýr.
        opt.Authority = builder.Configuration["IdentityServerUrl"]; // Kimlik saðlayýcýsýnýn (IdentityServer) adresini ayarlar.
        opt.Audience = "ResourceCatalog"; // JWT token'ýnda beklenen 'audience' (hedef kitle) talebini ayarlar (bu API'nin kimliði).
        opt.RequireHttpsMetadata = false; // Metadata endpoint'i için HTTPS gerekliliðini devre dýþý býrakýr (genellikle geliþtirme ortamý için).
    }); // JWT Bearer yapýlandýrma bloðunu bitirir.

//builder.Services.AddScoped<IStatisticService, StatisticService>(); //  StatisticService'i 'scoped' yaþam süresiyle DI'a kaydeder 
builder.Services.AddScoped<ICategoryService, CategoryService>(); // CategoryService'i 'scoped' (istek baþýna) yaþam süresiyle DI'a kaydeder.
builder.Services.AddScoped<IProductService, ProductService>(); // ProductService'i 'scoped' yaþam süresiyle DI'a kaydeder.
builder.Services.AddScoped<IProductDetailService, ProductDetailService>(); // ProductDetailService'i 'scoped' yaþam süresiyle DI'a kaydeder.
builder.Services.AddScoped<IProductImageService, ProductImageService>(); // ProductImageService'i 'scoped' yaþam süresiyle DI'a kaydeder.
//builder.Services.AddScoped<IFeatureSliderService, FeatureSliderService>(); //  FeatureSliderService'i 'scoped' yaþam süresiyle DI'a kaydeder 
//builder.Services.AddScoped<ISpecialOfferService, SpecialOfferService>(); // SpecialOfferService'i 'scoped' yaþam süresiyle DI'a kaydeder 
//builder.Services.AddScoped<IFeatureService, FeatureService>(); // FeatureService'i 'scoped' yaþam süresiyle DI'a kaydeder 
//builder.Services.AddScoped<IOfferDiscountService, OfferDiscountService>(); //  OfferDiscountService'i 'scoped' yaþam süresiyle DI'a kaydeder 
//builder.Services.AddScoped<IBrandService, BrandService>(); //  BrandService'i 'scoped' yaþam süresiyle DI'a kaydeder 
//builder.Services.AddScoped<IAboutService, AboutService>(); // AboutService'i 'scoped' yaþam süresiyle DI'a kaydeder 
//builder.Services.AddScoped<IContactService, ContactService>(); // ContactService'i 'scoped' yaþam süresiyle DI'a kaydeder 


builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly()); // AutoMapper'ý DI'a kaydeder ve mevcut assembly'deki (bu projedeki) mapping profillerini tarar.

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings")); // Yapýlandýrmadaki (appsettings.json) "DatabaseSettings" bölümünü DatabaseSettings sýnýfýna baðlar.
builder.Services.AddScoped<IDatabaseSettings>(sp => // IDatabaseSettings'i 'scoped' yaþam süresiyle bir fabrika fonksiyonu kullanarak DI'a kaydeder.
{ // Fabrika fonksiyonu bloðunu baþlatýr.
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value; // Yapýlandýrýlmýþ DatabaseSettings örneðini DI konteynerinden alýr ve döndürür.
}); // Fabrika fonksiyonu bloðunu bitirir.

builder.Services.AddControllers(); // API controller'larý için gerekli servisleri DI konteynerine ekler.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle // Swagger/OpenAPI yapýlandýrmasý hakkýnda daha fazla bilgi için standart þablon yorumu.
builder.Services.AddEndpointsApiExplorer(); // API endpoint'lerini keþfetmek için gereken servisleri ekler (Swagger tarafýndan kullanýlýr).
builder.Services.AddSwaggerGen(); // Swagger oluþturma servislerini DI konteynerine ekler.

var app = builder.Build(); // Yapýlandýrýlmýþ builder'dan WebApplication örneðini oluþturur.

// Configure the HTTP request pipeline. // HTTP istek iþlem hattýný (middleware pipeline) yapýlandýrmak için standart þablon yorumu.
if (app.Environment.IsDevelopment()) // Uygulamanýn geliþtirme ortamýnda çalýþýp çalýþmadýðýný kontrol eder.
{ // Geliþtirme ortamý yapýlandýrma bloðunu baþlatýr.
    app.UseSwagger(); // Swagger JSON endpoint'ini oluþturmak için Swagger middleware'ini ekler.
    app.UseSwaggerUI(); // Etkileþimli API dokümantasyon sayfasýný sunmak için Swagger UI middleware'ini ekler.
} // Geliþtirme ortamý yapýlandýrma bloðunu bitirir.

app.UseHttpsRedirection(); // HTTP isteklerini HTTPS'e yönlendiren middleware'i ekler.

app.UseAuthentication(); // Kimlik doðrulama middleware'ini iþlem hattýna ekler (token'larý doðrular).
app.UseAuthorization(); // Yetkilendirme middleware'ini iþlem hattýna ekler (izinleri kontrol eder).

app.MapControllers(); // Controller eylemleri için endpoint'leri istek iþlem hattýna ekler.

app.Run(); // Uygulamayý baþlatýr ve HTTP isteklerini dinlemeye baþlar.