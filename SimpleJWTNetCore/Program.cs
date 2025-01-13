using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SimpleJWTNetCore.Application.Infrastracture.Account;
using SimpleJWTNetCore.Application.Repository.Account;
using SimpleJWTNetCore.Database.DBC;
using SimpleJWTNetCore.UI.Helper;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

#region Databases
builder.Services.AddDbContext<SimpleJWTNetCoreDbContext>(options => options.UseSqlServer(builder.Configuration["DefaultConnection"]));
#endregion

#region My Services
builder.Services.AddScoped<IAccount, AccountRepository>();
#endregion


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

#region JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    var jwtSettings = builder.Configuration.GetSection("JwtSettings");
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"])),
    };
});

builder.Services.AddAuthorization();
#endregion

# region Swagger Config
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "SimpleJWTNetCore API", Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});
#endregion

builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseMiddleware<JWTTokenMiddleWare>();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllers();

#region Swagger Run
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "SimpleJWTNetCore API v1");
    options.RoutePrefix = "swagger";
});
#endregion

app.Run();