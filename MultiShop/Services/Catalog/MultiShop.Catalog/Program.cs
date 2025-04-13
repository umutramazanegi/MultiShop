using Microsoft.AspNetCore.Authentication.JwtBearer; // JWT Bearer kimlik do�rulama bile�enlerini i�eri aktar�r.
using Microsoft.Extensions.Options; // Yap�land�rma desenleri i�in IOptions'� i�eri aktar�r.
//using MultiShop.Catalog.Services.AboutServices; //  About servisi aray�zlerini/s�n�flar�n� i�eri aktar�r 
//using MultiShop.Catalog.Services.BrandServices; //  Brand servisi aray�zlerini/s�n�flar�n� i�eri aktar�r 
using MultiShop.Catalog.Services.CategoryServices; // Kategori servisi aray�zlerini/s�n�flar�n� i�eri aktar�r.
//using MultiShop.Catalog.Services.ContactServices; // Contact servisi aray�zlerini/s�n�flar�n� i�eri aktar�r 
//using MultiShop.Catalog.Services.FeatureServices; //  Feature servisi aray�zlerini/s�n�flar�n� i�eri aktar�r 
//using MultiShop.Catalog.Services.FeatureSliderServices; //  FeatureSlider servisi aray�zlerini/s�n�flar�n� i�eri aktar�r
//using MultiShop.Catalog.Services.OfferDiscountServices; //  OfferDiscount servisi aray�zlerini/s�n�flar�n� i�eri aktar�r 
using MultiShop.Catalog.Services.ProductDetailDetailServices; // �r�n Detay servisi aray�zlerini/s�n�flar�n� i�eri aktar�r.
using MultiShop.Catalog.Services.ProductImageServices; // �r�n Resim servisi aray�zlerini/s�n�flar�n� i�eri aktar�r.
using MultiShop.Catalog.Services.ProductServices; // �r�n servisi aray�zlerini/s�n�flar�n� i�eri aktar�r.
//using MultiShop.Catalog.Services.SpecialOfferServices; //  SpecialOffer servisi aray�zlerini/s�n�flar�n� i�eri aktar�r 
//using MultiShop.Catalog.Services.StatisticServices; //  Statistic servisi aray�zlerini/s�n�flar�n� i�eri aktar�r 
using MultiShop.Catalog.Settings; // Veritaban� ayarlar� s�n�flar�n� i�eri aktar�r.
using System.Reflection; // Yans�ma (reflection) yeteneklerini i�eri aktar�r (AutoMapper i�in kullan�l�r).

var builder = WebApplication.CreateBuilder(args); // Varsay�lan ayarlarla yeni bir web uygulamas� olu�turucu (builder) ba�lat�r.

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // Ba��ml�l�k enjeksiyonuna (DI) kimlik do�rulama servislerini ekler, varsay�lan �ema olarak JWT Bearer'� belirtir.
    .AddJwtBearer(opt => // JWT Bearer kimlik do�rulama i�leyicisini (handler) yap�land�r�r.
    { // JWT Bearer yap�land�rma blo�unu ba�lat�r.
        opt.Authority = builder.Configuration["IdentityServerUrl"]; // Kimlik sa�lay�c�s�n�n (IdentityServer) adresini ayarlar.
        opt.Audience = "ResourceCatalog"; // JWT token'�nda beklenen 'audience' (hedef kitle) talebini ayarlar (bu API'nin kimli�i).
        opt.RequireHttpsMetadata = false; // Metadata endpoint'i i�in HTTPS gereklili�ini devre d��� b�rak�r (genellikle geli�tirme ortam� i�in).
    }); // JWT Bearer yap�land�rma blo�unu bitirir.

//builder.Services.AddScoped<IStatisticService, StatisticService>(); //  StatisticService'i 'scoped' ya�am s�resiyle DI'a kaydeder 
builder.Services.AddScoped<ICategoryService, CategoryService>(); // CategoryService'i 'scoped' (istek ba��na) ya�am s�resiyle DI'a kaydeder.
builder.Services.AddScoped<IProductService, ProductService>(); // ProductService'i 'scoped' ya�am s�resiyle DI'a kaydeder.
builder.Services.AddScoped<IProductDetailService, ProductDetailService>(); // ProductDetailService'i 'scoped' ya�am s�resiyle DI'a kaydeder.
builder.Services.AddScoped<IProductImageService, ProductImageService>(); // ProductImageService'i 'scoped' ya�am s�resiyle DI'a kaydeder.
//builder.Services.AddScoped<IFeatureSliderService, FeatureSliderService>(); //  FeatureSliderService'i 'scoped' ya�am s�resiyle DI'a kaydeder 
//builder.Services.AddScoped<ISpecialOfferService, SpecialOfferService>(); // SpecialOfferService'i 'scoped' ya�am s�resiyle DI'a kaydeder 
//builder.Services.AddScoped<IFeatureService, FeatureService>(); // FeatureService'i 'scoped' ya�am s�resiyle DI'a kaydeder 
//builder.Services.AddScoped<IOfferDiscountService, OfferDiscountService>(); //  OfferDiscountService'i 'scoped' ya�am s�resiyle DI'a kaydeder 
//builder.Services.AddScoped<IBrandService, BrandService>(); //  BrandService'i 'scoped' ya�am s�resiyle DI'a kaydeder 
//builder.Services.AddScoped<IAboutService, AboutService>(); // AboutService'i 'scoped' ya�am s�resiyle DI'a kaydeder 
//builder.Services.AddScoped<IContactService, ContactService>(); // ContactService'i 'scoped' ya�am s�resiyle DI'a kaydeder 


builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly()); // AutoMapper'� DI'a kaydeder ve mevcut assembly'deki (bu projedeki) mapping profillerini tarar.

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings")); // Yap�land�rmadaki (appsettings.json) "DatabaseSettings" b�l�m�n� DatabaseSettings s�n�f�na ba�lar.
builder.Services.AddScoped<IDatabaseSettings>(sp => // IDatabaseSettings'i 'scoped' ya�am s�resiyle bir fabrika fonksiyonu kullanarak DI'a kaydeder.
{ // Fabrika fonksiyonu blo�unu ba�lat�r.
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value; // Yap�land�r�lm�� DatabaseSettings �rne�ini DI konteynerinden al�r ve d�nd�r�r.
}); // Fabrika fonksiyonu blo�unu bitirir.

builder.Services.AddControllers(); // API controller'lar� i�in gerekli servisleri DI konteynerine ekler.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle // Swagger/OpenAPI yap�land�rmas� hakk�nda daha fazla bilgi i�in standart �ablon yorumu.
builder.Services.AddEndpointsApiExplorer(); // API endpoint'lerini ke�fetmek i�in gereken servisleri ekler (Swagger taraf�ndan kullan�l�r).
builder.Services.AddSwaggerGen(); // Swagger olu�turma servislerini DI konteynerine ekler.

var app = builder.Build(); // Yap�land�r�lm�� builder'dan WebApplication �rne�ini olu�turur.

// Configure the HTTP request pipeline. // HTTP istek i�lem hatt�n� (middleware pipeline) yap�land�rmak i�in standart �ablon yorumu.
if (app.Environment.IsDevelopment()) // Uygulaman�n geli�tirme ortam�nda �al���p �al��mad���n� kontrol eder.
{ // Geli�tirme ortam� yap�land�rma blo�unu ba�lat�r.
    app.UseSwagger(); // Swagger JSON endpoint'ini olu�turmak i�in Swagger middleware'ini ekler.
    app.UseSwaggerUI(); // Etkile�imli API dok�mantasyon sayfas�n� sunmak i�in Swagger UI middleware'ini ekler.
} // Geli�tirme ortam� yap�land�rma blo�unu bitirir.

app.UseHttpsRedirection(); // HTTP isteklerini HTTPS'e y�nlendiren middleware'i ekler.

app.UseAuthentication(); // Kimlik do�rulama middleware'ini i�lem hatt�na ekler (token'lar� do�rular).
app.UseAuthorization(); // Yetkilendirme middleware'ini i�lem hatt�na ekler (izinleri kontrol eder).

app.MapControllers(); // Controller eylemleri i�in endpoint'leri istek i�lem hatt�na ekler.

app.Run(); // Uygulamay� ba�lat�r ve HTTP isteklerini dinlemeye ba�lar.