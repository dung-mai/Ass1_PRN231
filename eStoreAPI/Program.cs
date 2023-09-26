using BusinessObject.Models;
using Bussiness.Mapping;
using Repositories.IRepository;
using Repositories.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddTransient<IProductRepository, ProductRepository>()
    .AddDbContext<MyDBContext>(opt => builder.Configuration.GetConnectionString("ass1"));
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>()
    .AddDbContext<MyDBContext>(opt => builder.Configuration.GetConnectionString("ass1"));
builder.Services.AddTransient<IOrderDetailRepository, OrderDetailRepository>()
    .AddDbContext<MyDBContext>(opt => builder.Configuration.GetConnectionString("ass1"));
builder.Services.AddTransient<IOrderRepository, OrderRepository>()
    .AddDbContext<MyDBContext>(opt => builder.Configuration.GetConnectionString("ass1"));
builder.Services.AddTransient<IMemberRepository, MemberRepository>()
    .AddDbContext<MyDBContext>(opt => builder.Configuration.GetConnectionString("ass1"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
