using Deposito.BLL.CryptoService;
using Deposito.BLL.Interfaces.Repository;
using Deposito.BLL.Interfaces.Services;
using Deposito.BLL.Services;
using Deposito.DAL.Context;
using Deposito.DAL.Repository;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

//services

builder.Services.AddTransient<DBContext, DBContext>();
builder.Services.AddTransient<IUsuarioService, UsuariosService>();
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();
app.Run();
