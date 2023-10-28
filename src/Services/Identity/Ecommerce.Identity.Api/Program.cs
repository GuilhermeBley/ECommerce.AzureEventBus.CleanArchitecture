using Ecommerce.Identity.Api.Options;
using Ecommerce.Identity.Api.Service;
using Ecommerce.Identity.Infrastructure.Extension.Di;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

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

    opt.RequireHttpsMetadata = false;
    opt.SaveToken = true;
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.Configure<JwtOptions>(
     builder.Configuration.GetSection(JwtOptions.SECTION));

builder.Services.Configure<Ecommerce.Identity.Infrastructure.Options.MySqlOptions>(
     builder.Configuration.GetSection(Ecommerce.Identity.Infrastructure.Options.MySqlOptions.SECTION));

builder.Services.AddInfrastructure(provider =>
{
    var contextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
    return new Ecommerce.Identity.Api.Security.HttpContextClaimProvider(contextAccessor);
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ITokenService, TokenService>();

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
