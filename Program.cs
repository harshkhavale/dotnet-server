using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SportsClubApi.Services;
using SportsClubApi.Models;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SportsClubContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IStateService, StateService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IUserMembershipPlanDetailService, UserMembershipPlanDetailService>();
builder.Services.AddScoped<ICorporateUserService, CorporateUserService>();
builder.Services.AddScoped<MessageService>();
builder.Services.AddScoped<IMembershipPlanAttributeService, MembershipPlanAttributeService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<UserCategoryService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],   // Should match the Issuer
        ValidAudience = builder.Configuration["Jwt:Audience"], // Should match the Audience
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});


builder.Services.AddCors(options =>
{
    var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();

    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins(allowedOrigins)
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdministratorRole", policy => policy.RequireRole("Administrator"));
    options.AddPolicy("RequireCorporateRole", policy => policy.RequireRole("Corporate"));
    options.AddPolicy("RequireSupplierRole", policy => policy.RequireRole("Supplier"));
    options.AddPolicy("RequireNormalRole", policy => policy.RequireRole("Normal"));
    options.AddPolicy("RequireUnauthorizedRole", policy => policy.RequireRole("Unauthorized"));
    options.AddPolicy("RequireAdminOrUnauthorized", policy =>
        policy.RequireRole("Administrator", "Unauthorized"));
    options.AddPolicy("All", policy =>
        policy.RequireRole("Administrator", "Unauthorized","Normal","Supplier","Corporate"));
});




var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseExceptionHandler(options =>
{
    options.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        var exception = exceptionHandlerPathFeature?.Error;

        var response = new { error = exception?.Message };
        await context.Response.WriteAsJsonAsync(response);
    });
});

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
