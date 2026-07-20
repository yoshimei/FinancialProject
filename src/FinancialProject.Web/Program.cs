using FinancialProject.Business.Options;
using FinancialProject.Business.Services;
using FinancialProject.Common.Interfaces.Repositories;
using FinancialProject.Common.Interfaces.Services;
using FinancialProject.Data.Infrastructure;
using FinancialProject.Data.Repositories;
using FinancialProject.Web.Filters;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// 展示層
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
});
builder.Services.AddScoped<SessionAuthorizeFilter>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// 業務層設定（驗證規則來自設定檔，不寫死於程式碼）
builder.Services.Configure<FavoriteProductValidationOptions>(
    builder.Configuration.GetSection(FavoriteProductValidationOptions.SectionName));

// 資料層
builder.Services.AddSingleton<ISqlConnectionFactory>(
    new SqlConnectionFactory(builder.Configuration.GetConnectionString("FinancialDb")!));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ILikeListRepository, LikeListRepository>();

// 業務層
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IFavoriteProductService, FavoriteProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseSession();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}")
    .WithStaticAssets();

app.Run();
