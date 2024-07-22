using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

// Configura��o para a API URL
var apiUrl = Environment.GetEnvironmentVariable("API_URL") ?? "http://api:7107";

// Configura��o dos servi�os
builder.Services.AddControllers();

// Configura��o do Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
    

// Configura��o para clientes HTTP, se necess�rio
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri(apiUrl);
});

var app = builder.Build();

// Configura��o do pipeline de requisi��es HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection(); // Descomente se HTTPS estiver configurado

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
