using Autofac;
using Autofac.Extensions.DependencyInjection;
using BusinessManagement.BusinessLayer.DependencyResolvers.Autofac;
using BusinessManagement.BusinessLayer.Utilities.Security.Encryption;
using BusinessManagement.BusinessLayer.Utilities.Security.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Autofac Dependency Injection ayarları
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacModule()));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Cross Origin Resource Sharing ayarları
builder.Services.AddCors();

// JWT ayarları
var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
    };
});

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
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseHttpsRedirection();

// ALTTAKİ KOD .NET 5'DE ÇALIŞIYORDU. KULLANACAĞIN ZAMAN .NET 6'YA GÖRE DÜZENLEMELİSİN!!!!!!!!!!!!!!!!
// Bunu şimdilik dosya yüklemeyi kullanmadığım için kapattım.
//app.UseStaticFiles();
//app.UseStaticFiles(new StaticFileOptions()
//{
//    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources/Images")),
//    RequestPath = new PathString("/Resources/Images")
//});

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
