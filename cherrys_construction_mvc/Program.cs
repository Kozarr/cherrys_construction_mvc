using cherrys_construction_mvc.Data;
using cherrys_construction_mvc.DbInitializer;
using cherrys_construction_mvc.EfRepository;
using cherrys_construction_mvc.EfRepository.Interfaces;
using cherrys_construction_mvc.Helper;
using cherrys_construction_mvc.Interfaces;
using cherrys_construction_mvc.Mapper;
using cherrys_construction_mvc.Services;
using cherrys_construction_mvc.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Identity Configuration                                                                                           
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI();

// Initializer
builder.Services.AddScoped<IDbInitializer, DbInitializer>();


builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);


builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ITestimonyService, TestimonyService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<ICompanyCertificateService, CompanyCertificateService>();
builder.Services.AddScoped<ICompanyValueService, CompanyValueService>();
builder.Services.AddScoped<IServiceTypeService, ServiceTypeService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<ICompanyStoryService, CompanyStoryService>();
builder.Services.AddScoped<IHeroSliderImageService, HeroSliderImageService>();
builder.Services.AddScoped<IHeroSliderService, HeroSliderService>();
builder.Services.AddScoped<ICompanyQualityService, CompanyQualityService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IProjectTagService, ProjectTagService>();
builder.Services.AddScoped<ICompanyInfoService, CompanyInfoService>();
builder.Services.AddScoped<Mail>();
builder.Services.AddScoped<ICallToActionMessage, CallToActionMessageService>();
builder.Services.AddScoped<ICallToActionSetting, CallToActionSettingService>();
builder.Services.AddScoped<ICompanyCertificateSettingService, CompanyCertificateSettingService>();
builder.Services.AddScoped<ICompanyQualitiySettingService, CompanyQualitiySettingService>();
builder.Services.AddScoped<ILegalDocumentService, LegalDocumentService>();
builder.Services.AddScoped<IBlogPostService, BlogPostService>();
builder.Services.AddScoped<IBlogCategoryService, BlogCategoryService>();
builder.Services.AddTransient<IEmailSender, EmailSender>();

builder.Services.AddScoped(typeof(IEfRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));

builder.Services.AddAutoMapper(typeof(AppMappingProfile));

//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(100);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

SeedDatabase();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=User}/{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();


void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
    }
}