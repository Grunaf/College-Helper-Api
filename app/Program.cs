using Microsoft.EntityFrameworkCore;
using app;
using app.Interfaces;
using app.Repository;
using app.Services;
using app.Interfaces.Shedule;
using app.Interfaces.StudentAbsense;

IConfigurationRoot configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});


builder.Services.AddDbContext<ApplicationContext>();

builder.Services.AddScoped<IStudentAbsenceService, StudentAbsenceService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ISheduleService, SheduleService>();
builder.Services.AddScoped<IHomeworkService, HomeworkService>();
builder.Services.AddScoped<IStudentGroupSubjectService, StudentGroupSubjectService>();
builder.Services.AddScoped<IStudentGroupService, StudentGroupService>();

builder.Services.AddScoped<IStudentAbsenceRepository, StudentAbsenceRepository>();
builder.Services.AddScoped<IStudentGroupRepository, StudentGroupRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
builder.Services.AddScoped<ISheduleRepository, SheduleRepository>();
builder.Services.AddScoped<IHomeworkRepository, HomeworkRepository>();
builder.Services.AddScoped<IHomeworkFilesRepository, HomeworkFilesRepository>();
builder.Services.AddScoped<IStudentGroupSubjectRepository, StudentGroupSubjectRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

ApplicationContext db;

using var scope = app.Services.CreateScope();

db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
db.Database.Migrate();
var connString = db.connString;

app.MapControllers();
app.Run();
