using System.Text.Json;

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
        Random rand = new Random();
        int n = Convert.ToInt32(context.Request.Headers["X-Rand-N"]);
        int size = rand.Next(5, 11);

        int[] randomNumbers = new int[size];

        for (int i = 0; i < size; i++)
        {
            randomNumbers[i] = rand.Next(-n, n + 1);
        }

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
