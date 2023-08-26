using Ecommerce.EventBus.Azure.Extensions.Di;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEventBus();

builder.Services.Configure<Ecommerce.EventBus.Azure.AzureServiceBusOptions>(
     builder.Configuration.GetSection(Ecommerce.EventBus.Azure.AzureServiceBusOptions.SECTION));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
