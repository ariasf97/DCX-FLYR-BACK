using WebAPI.Config;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin", builder =>
    {
        builder
            .AllowAnyOrigin() // Permite solicitudes desde cualquier origen
            .AllowAnyMethod() // Permite solicitudes de cualquier método (GET, POST, etc.)
            .AllowAnyHeader(); // Permite cualquier encabezado en la solicitud
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddConfiguration(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("AllowAnyOrigin");

app.MapControllers();

app.Run();
