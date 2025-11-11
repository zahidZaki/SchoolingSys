using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;
using Consul;
using Ocelot.Provider.Consul;
using System;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;
using Serilog;
using Newtonsoft.Json.Linq;
using SchoolingSysApiGateway.RegisterServices;
using SchoolingSysApiGateway.HttpCient;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager _configuration = builder.Configuration;

builder.Host.UseSerilog((ctx, lc) => lc
.WriteTo.Console()
.ReadFrom.Configuration(ctx.Configuration));

// Load Ocelot configuration from appsettings.json



if (builder.Environment.IsProduction())
{
    string baseFolder = "prod-SchoolGateway";

    string[] configPaths = {
            Path.Combine(baseFolder, "prod-SchoolGateway.json"),
            Path.Combine(baseFolder, "prod-ClassApi.json"),
            Path.Combine(baseFolder, "prod-TeacherApi.json"),
            Path.Combine(baseFolder, "prod-StudentApi.json")
        };

    const string mergedConfigPath = "ocelot.merged.json";
    try
    {
        var combinedRoutes = new JArray();
        var routeSet = new HashSet<string>();
        JObject? globalConfig = null;

        foreach (var path in configPaths)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"Missing config file: {path}");

            var config = JObject.Parse(File.ReadAllText(path));
            var routes = config["Routes"] as JArray;

            if (routes != null)
            {
                foreach (var route in routes)
                {
                    var routeKey = route.ToString(Formatting.None);
                    if (routeSet.Add(routeKey))
                        combinedRoutes.Add(route);
                }
            }

            if (path.Contains("prod-SchoolGateway.json"))
                globalConfig = config["GlobalConfiguration"] as JObject;
        }

        var mergedConfig = new JObject
        {
            ["Routes"] = combinedRoutes,
            ["GlobalConfiguration"] = globalConfig ?? new JObject()
        };

        File.WriteAllText(mergedConfigPath, mergedConfig.ToString(Formatting.Indented));
        builder.Configuration.AddJsonFile(mergedConfigPath, optional: false, reloadOnChange: true);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error merging Ocelot configuration: {ex.Message}");
        throw;
    }
}
else
{
    string baseFolder = "dev-SchoolGateway";

    string[] configPaths = {
            Path.Combine(baseFolder, "dev-SchoolGateway.json"),
            Path.Combine(baseFolder, "dev-ClassApi.json"),
            Path.Combine(baseFolder, "dev-TeacherApi.json"),
            Path.Combine(baseFolder, "dev-StudentApi.json")
        };

    const string mergedConfigPath = "ocelot.merged.json";
    try
    {
        var combinedRoutes = new JArray();
        var routeSet = new HashSet<string>();
        JObject? globalConfig = null;

        foreach (var path in configPaths)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"Missing config file: {path}");

            var config = JObject.Parse(File.ReadAllText(path));
            var routes = config["Routes"] as JArray;

            if (routes != null)
            {
                foreach (var route in routes)
                {
                    var routeKey = route.ToString(Formatting.None);
                    if (routeSet.Add(routeKey))
                        combinedRoutes.Add(route);
                }
            }

            if (path.Contains("dev-SchoolGateway.json"))
                globalConfig = config["GlobalConfiguration"] as JObject;
        }

        var mergedConfig = new JObject
        {
            ["Routes"] = combinedRoutes,
            ["GlobalConfiguration"] = globalConfig ?? new JObject()
        };

        File.WriteAllText(mergedConfigPath, mergedConfig.ToString(Formatting.Indented));
        builder.Configuration.AddJsonFile(mergedConfigPath, optional: false, reloadOnChange: true);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error merging Ocelot configuration: {ex.Message}");
        throw;
    }
    //    //builder.Configuration.AddJsonFile("dev-SchoolGateway.json", optional: false, reloadOnChange: true);
}


// Register Ocelot services
builder.Services.AddOcelot(builder.Configuration).AddConsul();
builder.Services.AddControllers();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer("Bearer", options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = _configuration["JWT:ValidAudience"],
        ValidIssuer = _configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"] ?? string.Empty)),
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero
    };
});

//builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpClient<HttpClientHelper>();
builder.Services.RegisterDataServices();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "SchoolingSys-Gateway", Version = "v1" });
    option.DocumentFilter<SwaggerExcludeOcelotEndpoints>();
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddCors(options => options.AddPolicy("SchoolingSysAPI",
    policy => policy
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetPreflightMaxAge(TimeSpan.FromMinutes(10))
    ));

// Initialize Firebase

var app = builder.Build();


app.UseCors("SchoolingSys");
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication(); // Make sure to call this before UseAuthorization
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    _ = app.MapControllers(); // Map your controllers
});

await app.UseOcelot();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
});

app.UseSerilogRequestLogging();
app.Run();


public class SwaggerExcludeOcelotEndpoints : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var pathsToRemove = new[] { "/configuration", "/outputcache", "/outputcache/region" };

        foreach (var path in pathsToRemove)
        {
            swaggerDoc.Paths.Remove(path);
        }
    }
}