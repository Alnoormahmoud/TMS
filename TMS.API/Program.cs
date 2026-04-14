using TMS.Application;
using TMS.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

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

// DI for services and repositories
builder.Services.AddInfrastructure();   // Repositories DI
builder.Services.AddApplication();      // Services DI

// APIs general settings
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();