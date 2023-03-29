using Employee.Sample.Core.Middleware;
using Employee.Sample.Data.ResultModels;
using Employee.Sample.Repository.Repository;
using Employee.Sample.Services.Interfaces;
using Employee.Sample.Services.Svcs;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureLogging(logBuilder => logBuilder.AddFile(options =>
{
    options.LogDirectory            = "Logs";          //로그저장폴더
    options.FileName                = "log-";             
    options.FileSizeLimit           = null;          
    options.RetainedFileCountLimit  = null;
}));

// DB Connection
builder.Services.AddSingleton<EmployeeDbContext>();
// Add services to the container.
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployee, EmployeeService>();

builder.Services.AddLogging(logBuilder =>
{
    logBuilder.AddConfiguration(builder.Configuration.GetSection(key: "Logging"));
    logBuilder.AddConsole();
    logBuilder.AddDebug();
});

IConfigurationSection appSettingsSection = builder.Configuration.GetSection("AppSettings");
AppSettings appSettings = appSettingsSection.Get<AppSettings>();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Exception Handler
app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
