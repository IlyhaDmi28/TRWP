using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", async context =>
{
    context.Response.ContentType = "text/html";
    await context.Response.SendFileAsync("index.html");
});

app.MapPost("/", async context =>
{
    try
    {
        int x = Convert.ToInt32(context.Request.Headers["X-Value-x"]);
        int y = Convert.ToInt32(context.Request.Headers["X-Value-y"]);

        context.Response.Headers.Add("X-Value-z", (x + y).ToString());
        context.Response.StatusCode = 200;
    }
    catch(Exception) 
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync("Ошибка: некоректные данные!");
    }
});

app.Run();
