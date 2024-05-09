using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using app;
using app.Interfaces;
using app.Repository;

IConfigurationRoot configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

string connString;
MySqlCommand mysqlCommand;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});


builder.Services.AddDbContext<ApplicationContext>();

builder.Services.AddScoped<IStudentGroupRepository, StudentGroupRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

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
connString = db.connString;/*
app.Map("users/", UserHandler.Handle);

using var myConnection = new MySqlConnection(connString);
myConnection.Open();

mysqlCommand = new MySqlCommand();
mysqlCommand.Connection = myConnection;

UserHandler.db = db;
UserHandler.mysqlCommand = mysqlCommand;*/
/*app.Run(async (context) =>
{
    var path = context.Request.Path;
    var response = context.Response;
    var request = context.Request;

    string expressionForId = @"\d+";

    try
    {


        //UserHandler.Handle(db, path, response, request, mysqlCommand);

        if (path == "/groups" && request.Method == "GET")
        {
            GetAllGroups(response);
        }
        else if (path == "/group" && request.Method == "POST")
        {
            AddGroup(request, response);
        }
        else if (path == "/group/" && request.Method == "PUT")
        {
            string? fieldToUpdate = request.Query["fieldToUpdate"];
            UpdateGroup(request, fieldToUpdate, response);
        }
        else if (Regex.IsMatch(path, expressionForId) && request.Method == "DELETE")
        {
            string? id = path.Value?.Split('/')[2];
            DeleteGroup(id, response);
        }
    }
    catch (MySqlException ex)
    {
        Console.WriteLine(ex.Message);
    }
});

async void UpdateGroup(HttpRequest request, string? fieldToUpdate,  HttpResponse response)
{
    StudentGroup? groupData = await request.ReadFromJsonAsync<StudentGroup>();
    if (string.IsNullOrEmpty(fieldToUpdate))
    {
        if (groupData != null)
        {
            var group = db.StudentGroups.FirstOrDefault(group => group.Id == groupData.Id);

            if (group != null)
            {
                switch (fieldToUpdate)
                {
                    case "CuratorId":
                        group.CuratorId = groupData.CuratorId;
                        return;
                    case "HeadBoyId":
                        group.HeadBoyId = groupData.HeadBoyId;
                        return;
                }
                db.SaveChanges();
            }
        }
    }
    else
    {
        response.StatusCode = 404;
        await response.WriteAsync("Field To Update not passed");
    }
}

async void DeleteGroup(string? id, HttpResponse response)
{
    if (id != null)
    {
        var group = db.StudentGroups.FirstOrDefault(group => group.Id == int.Parse(id));
        if (group != null)
        {
            db.StudentGroups.Remove(group);
            await response.WriteAsJsonAsync(group);
            await db.SaveChangesAsync();
        }
        //response.StatusCode = 204;
    }
    else
    {
        response.StatusCode = 400;
        await response.WriteAsync("Id = null");
    }
}
*//*async void DeleteGroup(string? id, HttpResponse response)
{
    if (id != null)
    {
        var user = db.Users.FirstOrDefault(user => user.Id == int.Parse(id));
        if (user != null)
        {
            db.Users.Remove(user);
            await response.WriteAsJsonAsync(user);
        }
        //response.StatusCode = 204;
    }
    else
    {
        response.StatusCode = 400;
        await response.WriteAsync("Id = null");
    }
}*//*

async void AddGroup(HttpRequest request, HttpResponse response)
{
    try
    {
        var group = await request.ReadFromJsonAsync<StudentGroup>();
        if (group != null)
        {
            db.StudentGroups.Add(group);
            response.StatusCode = 201;
        }
        else
        {
            throw new Exception("Некорректные данные");
        }
        await response.WriteAsJsonAsync(group);
    }
    catch (Exception e)
    {
        response.StatusCode = 400;
        await response.WriteAsync("Исключение" + e);
    }
}

async void GetAllGroups(HttpResponse response)
{
    mysqlCommand.CommandText = @"SELECT * FROM StudentGroups";
    using var mReader = mysqlCommand.ExecuteReader();
    List<Object> groupsList = [];
    while (mReader.Read())
    {
        var group = new {
            id = mReader.GetInt32("Id"),
            field = mReader.GetString("Field"),
            number = mReader.GetInt32("Number"),
            curatorId = mReader.IsDBNull("CuratorId") ? "Null" : mReader.GetInt32("CuratorId").ToString(),
            headboyId = mReader.IsDBNull("HeadBoyId") ? "Null" : mReader.GetInt32("HeadBoyId").ToString()
        };
        groupsList.Add(group);
    }
    await response.WriteAsJsonAsync(groupsList);
}*/

app.MapControllers();
app.Run();
