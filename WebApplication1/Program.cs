using Application.Extensions;
using Application.Mappings;
using Infrastructure.Dapper;
using Infrastructure.Extensions;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

Dapper.SqlMapper.AddTypeHandler(new GuidTypeHandler());
builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapperModule();
builder.Services
    .AddRepositories()
    .AddApplicationServices();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
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
