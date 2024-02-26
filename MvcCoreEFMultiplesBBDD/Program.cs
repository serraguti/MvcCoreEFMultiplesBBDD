using Microsoft.EntityFrameworkCore;
using MvcCoreEFMultiplesBBDD.Data;
using MvcCoreEFMultiplesBBDD.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//SQL CONNECTION
//builder.Services.AddTransient<IRepositoryEmpleados, RepositoryEmpleadosSQLServer>();
//string connectionString =
//    builder.Configuration.GetConnectionString("SqlServerHospital");
//builder.Services.AddDbContext<HospitalContext>
//    (options => options.UseSqlServer(connectionString));
//ORACLE CONNECTION
//builder.Services.AddTransient<IRepositoryEmpleados
//    , RepositoryEmpleadosOracle>();
//string connectionString =
//    builder.Configuration.GetConnectionString("OracleHospital");
//builder.Services.AddDbContext<HospitalContext>
//    (options => options.UseOracle(connectionString
//    , options => options.UseOracleSQLCompatibility("11")));
//MYSQL CONNECTION
builder.Services.AddTransient<IRepositoryEmpleados, RepositoryEmpleadosMySql>();
string connectionString = 
    builder.Configuration.GetConnectionString("MySqlHospital");
//CON MYSQL ES CASI IGUAL, EN LUGAR DE UTILIZAR AddDbContext, SE UTILIZA 
//EL METODO AddDbContextPool
builder.Services.AddDbContextPool<HospitalContext>
    (options => options.UseMySql(connectionString
    , ServerVersion.AutoDetect(connectionString)));

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
