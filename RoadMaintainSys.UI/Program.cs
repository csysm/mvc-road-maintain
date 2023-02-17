using Microsoft.EntityFrameworkCore;
using RoadMaintainSys.BLL;
using RoadMaintainSys.DAL;
using RoadMaintainSys.DAL.MaintainSysContext;
using RoadMaintainSys.IBLL;
using RoadMaintainSys.IDAL;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<DbContext,MaintainSysDbContext>(options =>
                     options.UseSqlServer(builder.Configuration.GetConnectionString("MySql"))
                );

#region CustomIOC
builder.Services.AddScoped<ITableDAL, TableDAL>();
builder.Services.AddScoped<ITableBLL, TableBLL>();
builder.Services.AddScoped<ITableItemDAL, TableItemDAL>();
builder.Services.AddScoped<ITableItemBLL, TableItemBLL>();
builder.Services.AddScoped<IAdminDAL, AdminDAL>();
builder.Services.AddScoped<IAdminBLL, AdminBLL>();
builder.Services.AddScoped<IUserDAL, UserDAL>();
builder.Services.AddScoped<IUserBLL, UserBLL>();
builder.Services.AddScoped<INewsDAL, NewsDAL>();
builder.Services.AddScoped<INewsBLL, NewsBLL>();
#endregion


//在Controller中使用Session需要加上以下2项
builder.Services.AddSession(opts => opts.Cookie.IsEssential = true);
//允许Session存放在缓存(不加的话跳转后无法获取session)
builder.Services.AddDistributedMemoryCache().AddSession();


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

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=UserLogin}/{id?}");

app.Run();
