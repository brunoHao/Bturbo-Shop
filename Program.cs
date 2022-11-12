using DemoWebTemplate.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions();
var mailsetting = builder.Configuration.GetSection("MailSettings");
builder.Services.Configure<MailSettings>(mailsetting);
builder.Services.AddSingleton<IEmailSender, SendMailService>();

builder.Services.AddDbContext<MyDatabase>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("MyDatabase");
    options.UseSqlServer(connectionString);
});

//builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<MyDatabase>()
//    .AddDefaultTokenProviders();

//đăng ký các dịch vụ của IdentityUser
builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<MyDatabase>()
                .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Thiết lập về Password
    options.Password.RequireDigit = false; // Không bắt phải có số
    options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
    options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
    options.Password.RequireUppercase = false; // Không bắt buộc chữ in
    options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
    options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

    // Cấu hình Lockout - khóa user
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Khóa 5 phút
    options.Lockout.MaxFailedAccessAttempts = 5; // Thất bại 5 lầ thì khóa
    options.Lockout.AllowedForNewUsers = true;

    // Cấu hình về User.
    options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;  // Email là duy nhất

    // Cấu hình đăng nhập.
    options.SignIn.RequireConfirmedEmail = false;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
    options.SignIn.RequireConfirmedPhoneNumber = false;     // Xác thực số điện thoại
    options.SignIn.RequireConfirmedAccount = false;

});

builder.Services.AddAuthentication()
    .AddFacebook(f =>
    {
        IConfiguration fbAuthentication = builder.Configuration.GetSection("Authentication:Facebook");
        f.AppId = fbAuthentication["AppId"];
        f.AppSecret = fbAuthentication["AppSecret"];
        f.CallbackPath = "/login-facebook";
    })
    .AddGoogle(g =>
    {
                    // Đọc thông tin Authentication:Google từ appsettings.json
                    IConfigurationSection ggAuthentication = builder.Configuration.GetSection("Authentication:Google");
                    // Thiết lập ClientID và ClientSecret để truy cập API google.
                    g.ClientId = ggAuthentication["ClientId"];
                    g.ClientSecret = ggAuthentication["ClientSecret"];
                    g.CallbackPath = "/login-google";
    });

//builder.Services.AddAuthentication()
//                .AddGoogle(g =>
//                {
//                    // Đọc thông tin Authentication:Google từ appsettings.json
//                    IConfigurationSection ggAuthentication = builder.Configuration.GetSection("Authentication:Google");
//                    // Thiết lập ClientID và ClientSecret để truy cập API google.
//                    g.ClientId = ggAuthentication["ClientId"];
//                    g.ClientSecret = ggAuthentication["ClientSecret"];
//                    g.CallbackPath = "/dang-nhap-tu-google";

//                })
//                .AddFacebook(f =>
//                {
//                    // Đọc thông tin Authentication:Facebook từ appsettings.json
//                    IConfigurationSection fbAuthentication = builder.Configuration.GetSection("Authentication:Facebook");
//                    // Thiết lập ClientID và ClientSecret để truy cập API facebook.
//                    f.AppId = fbAuthentication["AppId"];
//                    f.AppSecret = fbAuthentication["AppSecret"];
//                    f.CallbackPath = "/dang-nhap-tu-facebook";
//                });

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}");

app.Run();

//Cloning into 'brunoHao.github.io.git'