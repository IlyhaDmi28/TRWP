using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", async context =>
{
    context.Response.ContentType = "text/html";
    await context.Response.SendFileAsync("index.html");
});

app.MapPost("/a", async context =>
{
    try
    {
        int x = Convert.ToInt32(context.Request.Headers["X-Value-x"]);
        int y = Convert.ToInt32(context.Request.Headers["X-Value-y"]);

        context.Response.Headers.Add("X-Value-z", (x + y).ToString());

        Thread.Sleep(10000);
        context.Response.StatusCode = 200;
    }
    catch (Exception)
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync("Ошибка: некоректные данные!");
    }
});

app.MapPost("/b", async context =>
{
    try
    {
        Random rand = new Random();
        int n = Convert.ToInt32(context.Request.Headers["X-Rand-N"]);
        int size = rand.Next(5, 11);

        int[] randomNumbers = new int[size];

        for (int i = 0; i < size; i++)
        {
            randomNumbers[i] = rand.Next(-n, n + 1);
        }

        Thread.Sleep(1000);
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(randomNumbers));
    }
    catch (Exception)
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync("Ошибка: некоректные данные!");
    }
});


app.Run();
