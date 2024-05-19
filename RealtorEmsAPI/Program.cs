using Data.Data;
using Data.Entity;
using Data.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Prometheus;
using Services.ApiMiddleware;
using Services.Settings;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var sqlConnectionString = EncryptDecrypt.DecryptText(builder.Configuration.GetSection("ConnectionStrings:SqlConnection").Value);

builder.Services.AddDbContext<AppDB>(options => options.UseSqlServer(sqlConnectionString));
builder.Services.AddIdentity<Tbl_Users, IdentityRole>().AddEntityFrameworkStores<AppDB>().AddDefaultTokenProviders();

builder.Services.AddAuthentication(option =>
{
    //option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    //option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    //option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

    option.DefaultAuthenticateScheme = CustomMiddlewareOptions.DefaultScheme;
    option.DefaultChallengeScheme = CustomMiddlewareOptions.DefaultScheme;
    option.DefaultScheme = CustomMiddlewareOptions.DefaultScheme;
})
    .AddCustomMiddleware(option =>
    {
        option.IsHostOrigin = true;
    })
    .AddJwtBearer(option =>
    {
        option.SaveToken = true;
        option.RequireHttpsMetadata = false;
        option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = "User",
            ValidIssuer = "https://localhost:44317/",
            TryAllIssuerSigningKeys = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT:Secret").Value))
        };
    });

builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true)
    .AddApplicationPart(Assembly.GetEntryAssembly())
    .AddControllersAsServices();

builder.Services.AddHealthChecks()
    .AddCheck<ApiHealthCheck>("api")
    .ForwardToPrometheus();

RegisterIoC.RegisterMediator(builder.Services);
builder.Services.AddTransient<IUserTokenService, UserTokenService>();

builder.Services.AddEndpointsApiExplorer();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseForwardedHeaders();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowAll");

//TODO: Remove comment for create admin user and then after comment again
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    try
//    {
//        var context = services.GetRequiredService<AppDB>();
//        context.Database.EnsureCreated();

//        var userManager = services.GetRequiredService<UserManager<Tbl_Users>>();
//        DefaultUsers.SeedDefaultUser(userManager).GetAwaiter().GetResult();
//    }
//    catch (Exception ex)
//    {
//        var logger = services.GetRequiredService<ILogger<Program>>();
//        logger.LogError(ex, "An error occurred while seeding the database.");
//    }
//}

app.Run();