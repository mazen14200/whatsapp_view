using Bnan.Core;
using Microsoft.Extensions.FileProviders;
using Bnan.Inferastructure;
using NToastNotify;
using System.Globalization;
using Bnan.Inferastructure.MiddleWare;
using Bnan.Core.Interfaces;
using Bnan.Inferastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(opt =>
{
    opt.ModelBinderProviders.Insert(0, new CustomModelBinderProvider());
}).AddMvcLocalization();
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
builder.Services.AddRazorPages().AddNToastNotifyToastr(new ToastrOptions());
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.AddPersistenceServices();
builder.Services.AddSignalR();





var app = builder.Build();

app.UseNToastNotify();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
//seeding database
var data = System.IO.File.ReadAllText("BnanSaData.sql");
app.UseDatabaseInitializer(data);

//config for localization
var supportedCultures = new[] { "en-US", "ar-EG" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

//when user unauthorize redirect him to this page
app.UseStatusCodePagesWithRedirects("/identity/account/login");
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
/*app.UseMiddleware<CheckCultureOfUser>();*/

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<PressenceHub>("/pressenceHub");
});

app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Users}/{action=Login}/{id?}"
    );
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Identity}/{controller=Account}/{action=Login}/{id?}"
 );


app.Run();