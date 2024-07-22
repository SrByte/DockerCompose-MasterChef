using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

// Configuração para a API URL
var apiUrl = Environment.GetEnvironmentVariable("API_URL") ?? "http://api:7107";

// Configuração dos serviços
builder.Services.AddControllers();

// Configuração do Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
    

// Configuração para clientes HTTP, se necessário
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri(apiUrl);
});

var app = builder.Build();

// Configuração do pipeline de requisições HTTP
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
