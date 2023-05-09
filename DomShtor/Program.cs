var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<DomShtor.BL.Auth.IAuth, DomShtor.BL.Auth.Auth>();
builder.Services.AddScoped<DomShtor.BL.Auth.ICurrentUser, DomShtor.BL.Auth.CurrentUser>();
builder.Services.AddScoped<DomShtor.BL.Auth.IDbSession, DomShtor.BL.Auth.DbSession>();
builder.Services.AddScoped<DomShtor.BL.General.IWebCoookie, DomShtor.BL.General.WebCookie>();
builder.Services.AddScoped<DomShtor.BL.Profile.IProfile, DomShtor.BL.Profile.Profile>();

builder.Services.AddSingleton<DomShtor.BL.Auth.IEncrypt, DomShtor.BL.Auth.Encrypt>();

builder.Services.AddSingleton<DomShtor.DAL.IAuthDAL, DomShtor.DAL.AuthDAL>();
builder.Services.AddSingleton<DomShtor.DAL.IDbSessionDAL, DomShtor.DAL.DbSessionDAL>();
builder.Services.AddSingleton<DomShtor.DAL.IUserTokenDAL, DomShtor.DAL.UserTokenDalDal>();
builder.Services.AddSingleton<DomShtor.DAL.IProfileDAL, DomShtor.DAL.ProfileDAL>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();



builder.Services.AddMvc();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();