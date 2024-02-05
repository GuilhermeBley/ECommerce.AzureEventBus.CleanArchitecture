using Ecommerce.EventBus.Azure.Extensions.Di;
using Ecommerce.Catalog.Infrastructure.Extensions.Di;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using Ecommerce.Catalog.Api.Security;
using Ecommerce.EventBus.Abstractions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();

builder.Services.AddEventBus(
    subscriptionName: builder.Configuration[$"{Ecommerce.EventBus.Azure.AzureServiceBusOptions.SECTION}:catalog-subs"],
    (provider, eventBus) =>
    {
        eventBus.Subscribe<Ecommerce.EventBus.Events.CreateProductEvent,
            Ecommerce.Catalog.Application.Notifications.Product.CreateProduct.CreateProductEventHandler>();
        eventBus.Subscribe<Ecommerce.EventBus.Events.DisableProductEvent,
            Ecommerce.Catalog.Application.Notifications.Product.DeleteProduct.DeleteProductEventHandler>();
        eventBus.Subscribe<Ecommerce.EventBus.Events.UpdateProductEvent,
            Ecommerce.Catalog.Application.Notifications.Product.UpdateProduct.UpdateProductEventHandler>();
        eventBus.Subscribe<Ecommerce.EventBus.Events.CompanyCreatedEvent,
            Ecommerce.Catalog.Application.Notifications.Company.CreateCompany.CompanyCreatedEventHandler>();
        eventBus.Subscribe<Ecommerce.EventBus.Events.CreateUserEvent,
            Ecommerce.Catalog.Application.Notifications.User.CreateUser.CreateUserEventHandler>();
    });

builder.Services.AddInfrastructure();

builder.Services.AddClaimProvider(provider =>
    {
        var contextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
        return new HttpContextClaimProvider(contextAccessor);
    });

builder.Services.Configure<Ecommerce.EventBus.Azure.AzureServiceBusOptions>(
     builder.Configuration.GetSection(Ecommerce.EventBus.Azure.AzureServiceBusOptions.SECTION));

builder.Services.Configure<Ecommerce.Catalog.Infrastructure.Options.PostgresOptions>(
     builder.Configuration.GetSection(Ecommerce.Catalog.Infrastructure.Options.PostgresOptions.SECTION));

var app = builder.Build();

_ = app.Services.GetRequiredService<IEventBus>(); // subscribing in events

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
