using TableLibFullIntegration.API.Utils;

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

    // builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddScoped<IConnectionConfiguration, ConnectionConfiguration>();
    builder.Services.AddScoped<IDatabaseConnection, MySQLDatabaseConnection>();
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
