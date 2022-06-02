using Autofac;
using Autofac.Extensions.DependencyInjection;
using BusinessManagement.Api.Extensions;
using BusinessManagement.BusinessLayer.CrossCuttingConcerns.Logging;
using BusinessManagement.BusinessLayer.DependencyResolvers.Autofac;
using Microsoft.AspNetCore.HttpOverrides;
using NLog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacModule()));

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// ALTTAKİ KOD .NET 5'DE ÇALIŞIYORDU. KULLANACAĞIN ZAMAN .NET 6'YA GÖRE DÜZENLEMELİSİN!!!!!!!!!!!!!!!!
// Bunu şimdilik dosya yüklemeyi kullanmadığım için kapattım.
//// Bu satır sunucuya büyük bir dosya gönderirken alınan HTTP 413 hatasını çözüyor.
//builder.Services.ConfigureFileTransferOptions(builder.Configuration);

builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureJwt(builder.Configuration);

builder.Services.AddControllers();

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);

if (app.Environment.IsProduction())
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
