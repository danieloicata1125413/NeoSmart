using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NeoSmart.Backend.Data;
using NeoSmart.Backend.Helper;
using NeoSmart.Backend.Interfaces;
using NeoSmart.Backend.Intertfaces;
using NeoSmart.Backend.Repositories;
using NeoSmart.Backend.Services;
using NeoSmart.Backend.UnitsOfWork;
using NeoSmart.ClassLibraries.Entities;
using NeoSmart.ClassLibraries.Helper;
using NeoSmart.ClassLibraries.Interfaces;
using NeoSmart.Data.Entities;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();

// Add services to the container.


builder.Services
    .AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "Api NeoSmart",
        Description = "Una API web ASP.NET Core para administrar elementos de neosmart.com, Grupo 2",
        TermsOfService = new Uri("https://neosmart.com"),
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "NeoSmart",
            Url = new Uri("https://neosmart.com")
        },
        License = new Microsoft.OpenApi.Models.OpenApiLicense
        {
            Name = "Licencia de Uso",
            Url = new Uri("https://neosmart.com")
        }
    });
    options.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "bearerAuth"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddIdentity<User, IdentityRole>(x =>
{
    x.Password.RequireDigit = false;
    x.Password.RequiredUniqueChars = 0;
    x.Password.RequireLowercase = false;
    x.Password.RequireNonAlphanumeric = false;
    x.Password.RequireUppercase = false;
})
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

builder.Services.AddDbContext<DataContext>(x =>
{
    x.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);
});

builder.Services.AddScoped(typeof(IGenericUnitOfWork<>), typeof(GenericUnitOfWork<>));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IApiService, ApiService>();
builder.Services.AddScoped<IUserHelper, UserHelper>();
builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();
builder.Services.AddTransient<IMailHelper, MailHelper>();
builder.Services.AddTransient<SeedDb>();



var app = builder.Build();
SeedData(app);

void SeedData(WebApplication app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
    using (var scope = scopedFactory!.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<SeedDb>();
        service!.SeedAsync().Wait();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// global cors policy
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin 
    .AllowCredentials());

app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();



app.Run();
