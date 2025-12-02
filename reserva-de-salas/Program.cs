using Microsoft.EntityFrameworkCore;
using reserva_de_salas.Data;
using reserva_de_salas.Interfaces;
using reserva_de_salas.Repositories;
using reserva_de_salas.Services;
using reserva_de_salas.Services.Strategy;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BancoContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ISalaRepository, SalaRepository>();

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ISalaService, SalaService>();

builder.Services.AddScoped<IReservaRepository, ReservaRepository>();

builder.Services.AddScoped<ValidadorDeReservaCapacidade>();
builder.Services.AddScoped<ValidadorDeReservaHorario>();

builder.Services.AddScoped<ReservasFacede>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles(); // <-- FALTAVA ISSO

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // <-- FALTAVA O PONTO E VÍRGULA

app.Run();