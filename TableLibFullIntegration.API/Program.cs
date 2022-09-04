using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    // Swagger/OpenAPI - https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddCors(options => options.AddPolicy("AllowAll", builder =>
    {
        builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    }));

    {    // Setting Up Data Source        
        string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddTransient<DbConnection>(s => new MySqlConnection(connectionString));
    }
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseHttpsRedirection();
    app.UseCors("AllowAll");
    app.MapControllers();
    app.Run();
}
