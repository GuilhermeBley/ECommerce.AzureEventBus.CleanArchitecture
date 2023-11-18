using Autofac;
using Autofac.Extensions.DependencyInjection;
using Ecommerce.EventBus.Azure.Extensions.Di;
using Ecommerce.Identity.Api.Options;
using Ecommerce.Identity.Api.Service;
using Ecommerce.Identity.Infrastructure.Extension.Di;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(opt =>
{
    var key = Encoding.ASCII.GetBytes(
        builder.Configuration.GetSection($"{JwtOptions.SECTION}:{nameof(JwtOptions.Secret)}")
        .Value);

    opt.SaveToken = true;
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});

builder.Services.Configure<Ecommerce.EventBus.Azure.AzureServiceBusOptions>(
     builder.Configuration.GetSection(Ecommerce.EventBus.Azure.AzureServiceBusOptions.SECTION));

builder.Services.Configure<JwtOptions>(
     builder.Configuration.GetSection(JwtOptions.SECTION));

builder.Services.Configure<Ecommerce.Identity.Infrastructure.Options.MySqlOptions>(
     builder.Configuration.GetSection(Ecommerce.Identity.Infrastructure.Options.MySqlOptions.SECTION));

builder.Services.AddInfrastructure(provider =>
{
    var contextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
    return new Ecommerce.Identity.Api.Security.HttpContextClaimProvider(contextAccessor);
});

builder.Services.AddEventBus((provider, eventBus) =>
{

});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ITokenService, TokenService>();

builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    var openTypes = new[]
    {
        typeof(Ecommerce.Identity.Application.Mediator.IAppRequestHandler<,>),
        typeof(Ecommerce.Identity.Application.Mediator.IAppNotificationHandler<>),
    };

    foreach (var openType in openTypes)
    {
        builder
            .RegisterAssemblyTypes(typeof(Ecommerce.Identity.Application.Commands.User.CreateUser.CreateUserHandler).Assembly)
            .AsClosedTypesOf(openType)
            .AsImplementedInterfaces();
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
