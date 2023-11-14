using Ecommerce.EventBus.Azure.Extensions.Di;
using Ecommerce.Catalog.Infrastructure.Extensions.Di;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using Ecommerce.Catalog.Api.Security;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();

builder.Services.AddEventBus((provider, eventBus) =>
{
    eventBus.Subscribe<Ecommerce.EventBus.Events.CreateProductEvent, 
        Ecommerce.Catalog.Application.Notifications.Product.CreateProduct.CreateProductEventHandler>();
    eventBus.Subscribe<Ecommerce.EventBus.Events.DeleteProductEvent, 
        Ecommerce.Catalog.Application.Notifications.Product.DeleteProduct.DeleteProductEventHandler>();
    eventBus.Subscribe<Ecommerce.EventBus.Events.UpdateProductEvent, 
        Ecommerce.Catalog.Application.Notifications.Product.UpdateProduct.UpdateProductEventHandler>();
    eventBus.Subscribe<Ecommerce.EventBus.Events.CompanyCreatedEvent, 
        Ecommerce.Catalog.Application.Notifications.Company.CreateCompany.CompanyCreatedEventHandler>();
});

builder.Services.AddInfrastructure();

builder.Services.AddClaimProvider(provider =>
    {
        var contextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
        return new HttpContextClaimProvider(contextAccessor);
    });

builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    var openTypes = new[]
    {
        typeof(Ecommerce.Catalog.Application.Mediator.IAppRequestHandler<,>),
        typeof(Ecommerce.Catalog.Application.Mediator.IAppNotificationHandler<>),
    };

    foreach (var openType in openTypes)
    {
        builder
            .RegisterAssemblyTypes(typeof(Ecommerce.Catalog.Application.Commands.Product.CreateProduct.CreateProductHandler).Assembly)
            .AsClosedTypesOf(openType)
            .AsImplementedInterfaces();
    }
});

builder.Services.Configure<Ecommerce.EventBus.Azure.AzureServiceBusOptions>(
     builder.Configuration.GetSection(Ecommerce.EventBus.Azure.AzureServiceBusOptions.SECTION));

builder.Services.Configure<Ecommerce.Catalog.Infrastructure.Options.PostgresOptions>(
     builder.Configuration.GetSection(Ecommerce.Catalog.Infrastructure.Options.PostgresOptions.SECTION));

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
