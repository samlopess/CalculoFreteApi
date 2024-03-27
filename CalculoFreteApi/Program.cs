using CalculoFreteApi.Business;
using CalculoFreteApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//estava dando erro com get entao fiz com post, por isso coloquei o frombody também
app.MapPost("/calculoValorFrete", ([FromBody] ProdutoModel produto, double distancia) =>
{
    Frete frete = new Frete();

    return frete.RetornaValorDoFrete(produto, distancia);
})
.WithName("GetValorFrete")
.WithOpenApi();

app.Run();



