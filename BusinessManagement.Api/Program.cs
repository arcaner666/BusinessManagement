using Autofac;
using Autofac.Extensions.DependencyInjection;
using BusinessManagement.Api.Extensions;
using BusinessManagement.BusinessLayer.DependencyResolvers.Autofac;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

// Autofac Dependency Injection ayarları
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacModule()));

builder.Services.ConfigureCors(); 
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureJwt(builder.Configuration);

// Servisleri ekler bu sebeple servisler bu kodun üstünde ayarlanmalıdır.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// ALTTAKİ KOD .NET 5'DE ÇALIŞIYORDU. KULLANACAĞIN ZAMAN .NET 6'YA GÖRE DÜZENLEMELİSİN!!!!!!!!!!!!!!!!
// Bunu şimdilik dosya yüklemeyi kullanmadığım için kapattım.
//// Bu satır sunucuya büyük bir dosya gönderirken alınan HTTP 413 hatasını çözüyor.
//var fileTransferOptions = Configuration.GetSection("FileTransferOptions").Get<FileTransferOptions>();
//services.Configure<FormOptions>(o => {
//    // 1MB dosya yükleme limiti koydum. 1024 X 1024 = 1048576 b = 1024 kb = 1 mb
//    o.ValueLengthLimit = fileTransferOptions.UploadLimit;
//    o.MultipartBodyLengthLimit = fileTransferOptions.UploadLimit;
//    o.MemoryBufferThreshold = fileTransferOptions.UploadLimit;
//});

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();
else 
    app.UseHsts();

app.UseHttpsRedirection();

// ALTTAKİ KOD .NET 5'DE ÇALIŞIYORDU. KULLANACAĞIN ZAMAN .NET 6'YA GÖRE DÜZENLEMELİSİN!!!!!!!!!!!!!!!!
// Bunu şimdilik dosya yüklemeyi kullanmadığım için kapattım.
//app.UseStaticFiles();
//app.UseStaticFiles(new StaticFileOptions()
//{
//    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources/Images")),
//    RequestPath = new PathString("/Resources/Images")
//});

// ŞİMDİLİK CODEMAZE KİTABINA GÖRE ÜSTTEKİ YERİNE BUNU KULLANACAĞIM.
app.UseStaticFiles();

app.UseForwardedHeaders(new ForwardedHeadersOptions 
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
