using ECommerceApi.API.Configurations.CustomUserName;
using ECommerceApi.API.Statics;
using ECommerceApi.Application;
using ECommerceApi.Application.Validators;
using ECommerceApi.Infrastructure;
using ECommerceApi.Infrastructure.Filters;
using ECommerceApi.Infrastructure.Services.Storage.Local;
using ECommerceApi.Persistance;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;
using System.Security.Claims;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using ECommerceApi.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureService();
builder.Services.AddApplicationServices();

builder.Services.AddStorage<LocalStorage>();
// to do bunu da islede bilerik amma birincini isledirik. bu hisseni sonda sil
//builder.Services.AddStorage(ECommerceApi.Infrastructure.enums.StorageType.Azure);

// There is no need for CORS for requests made through Swagger.
// If our client application is deployed with the same port, host, and protocol information as the API, there is no need for CORS.
builder.Services.AddCors(opt => opt.AddDefaultPolicy(policy => policy.WithOrigins("http://localhost:7278", "https://localhost:7278").AllowAnyHeader().AllowAnyMethod().AllowCredentials()));

// add validation filter
builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>());

//Validator settings and disable default validations
builder.Services.AddControllers().AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>()).ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

SqlColumn sqlColumn = new SqlColumn();
sqlColumn.ColumnName = "UserName";
sqlColumn.DataType = System.Data.SqlDbType.NVarChar;
sqlColumn.PropertyName = "UserName";
sqlColumn.DataLength = 50;
sqlColumn.AllowNull = true;
ColumnOptions columnOpt = new ColumnOptions();
columnOpt.Store.Remove(StandardColumn.Properties);
columnOpt.Store.Add(StandardColumn.LogEvent);
columnOpt.AdditionalColumns = new Collection<SqlColumn> { sqlColumn };

Logger log = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt")
    .WriteTo.MSSqlServer(
    connectionString: builder.Configuration.GetConnectionString("MSSQL"),
     sinkOptions: new MSSqlServerSinkOptions
     {
         AutoCreateSqlTable = true,
         TableName = "logs",
     },
     appConfiguration: null,
     columnOptions: columnOpt
    )
    .Enrich.FromLogContext()
    .Enrich.With<CustomUserNameColumn>()
    .MinimumLevel.Information()
    .CreateLogger();
builder.Host.UseSerilog(log);
builder.Host.UseSerilog(log);
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;

});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer("Admin", opt =>
{
    opt.TokenValidationParameters = new()
    {
        ValidateAudience = true, // Yaratdigimiz tokeni kimlerin hansi originlerin saytlarin istifade edeceyini mueyyen etdiyimiz deyerdir.
        ValidateIssuer = true, // Yaradilacaq tokeni kimin paylayacaghini ifade etdiyimiz yerdir.
        ValidateLifetime = true, // Yaradilacaq tokenin vaxtini idare etmek
        ValidateIssuerSigningKey = true, // Yaranacaq token deyerinin app-e aid bir deyer oldugunu ifade eden sekury key datasinin tesdiqlenmesi. 
        ValidAudience = builder.Configuration["Token:Audience"],
        ValidIssuer = builder.Configuration["Token:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
        LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow.AddMinutes(240) : false,
        NameClaimType = ClaimTypes.Name // JWR uzerinden NameClaim-e qarsiliq olan deyeri USer.Identity.Name-den elde edirik

    };
});
builder.Services.AddHttpClient();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.ConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());
app.UseStaticFiles();
app.UseSerilogRequestLogging();
app.UseHttpLogging();
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.Use(async (context, next) =>
{
    var username = context.User?.Identity?.IsAuthenticated == true ? context.User.Identity.Name : null;
    LogContext.PushProperty("user_name", username);
    await next();
});
app.MapControllers();


app.Run();
