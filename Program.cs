using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using WebApi.Controllers;
using System.Text.Json.Serialization;

using WebApi.Helpers;
using WebApi.Repositories;


var builder = WebApplication.CreateBuilder(args);
// add services to DI container
{
    var services = builder.Services;
    var env = builder.Environment;
 
    services.AddDbContext<DataContext>();
    services.AddSwaggerGen();
    services.AddCors();
    services.AddControllers().AddJsonOptions(x => 
    {
        // serialize enums as strings in api responses (e.g. Role)
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    services.AddSwaggerGen();
    builder.Services.AddEndpointsApiExplorer();

    // configure strongly typed settings object
    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
    

    // configure DI for application services
builder.Services.AddScoped<IDeviceRepo, DeviceRepo>();
builder.Services.AddScoped<TestAsyncActionFilter>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
}

builder.Services.AddControllers().AddNewtonsoftJson(s => {
    s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// migrate any database changes on startup (includes initial db creation)
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();    
    dataContext.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
// generated swagger json and swagger ui middleware
    app.UseSwagger();
    app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", ".NET Sign-up and Verification API"));
}

app.UseHttpsRedirection();


// configure HTTP request pipeline
{
     // global cors policy
    app.UseCors(x => x
        .SetIsOriginAllowed(origin => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());

    // global error handler
    app.UseMiddleware<ErrorHandlerMiddleware>();

    // custom jwt auth middleware
    // app.UseMiddleware<JwtMiddleware>();

    app.MapControllers();
}

app.Run("http://localhost:4000");
