using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WildlifeAPI_Prod.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<WildlifeAPI_ProdContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WildlifeAPI_ProdContext") ?? throw new InvalidOperationException("Connection string 'WildlifeAPI_ProdContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options => {
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy => {
                          policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                      });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
