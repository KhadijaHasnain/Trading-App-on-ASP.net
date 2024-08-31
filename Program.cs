using AutoMapper;
using BIGQXWebsite.Data;
using BIGQXWebsite.LocalizationResources;
using BIGQXWebsite.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using XLocalizer;
using XLocalizer.Routing;
using XLocalizer.Xml;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

//builder.Services.AddSingleton(p => new MapperConfiguration(c =>
//{
//  c.AddProfile(new MapperProfile(p.GetService<IStringLocalizer>()));
//}).CreateMapper());

builder.Services.AddSingleton<IXResourceProvider, XmlResourceProvider>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IResourceContent, ResourceContentService>();

builder.Services
  .AddSingleton(HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin, UnicodeRanges.Latin1Supplement, UnicodeRanges.LatinExtendedA, UnicodeRanges.Arabic }));


builder.Services.Configure<RequestLocalizationOptions>(ops =>
{
  var cultures = new CultureInfo[] {
    new CultureInfo("en"),
    new CultureInfo("ar"),
    new CultureInfo("fa"),
    new CultureInfo("tr"),
    new CultureInfo("es"),
    new CultureInfo("pt"),
  };
  cultures[1].DateTimeFormat = new CultureInfo("en").DateTimeFormat;
  cultures[1].DateTimeFormat.Calendar = new GregorianCalendar();

  ops.SupportedCultures = cultures;
  ops.SupportedUICultures = cultures;
  ops.DefaultRequestCulture = new RequestCulture("en");
  ops.RequestCultureProviders.Insert(0, new RouteSegmentRequestCultureProvider(cultures));
});

builder.Services.AddControllersWithViews()
    // add culture route segment for controllers e.g. /en/Home/Index
    //.AddMvcOptions(ops => ops.Conventions.Insert(0, new RouteTemplateModelConventionMvc()))
    .AddXLocalizer<LocSource>(ops =>
    {
      ops.AutoAddKeys = true;
      ops.AutoTranslate = false;
      ops.TranslateFromCulture = "en";
      ops.UseExpressMemoryCache = true;
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseMigrationsEndPoint();
}
else
{
  app.UseExceptionHandler("/Home/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseRequestLocalization();

app.MapControllerRoute(
    name: "default",
    pattern: "{culture=en}/{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
