var builder = WebApplication.CreateBuilder(args);

// 讓 API 回傳 JSON 時保留 UTF-8
builder.Services.AddSingleton<StationRepository>();

var app = builder.Build();

// 允許讀取 wwwroot（index.html / js / css）
app.UseDefaultFiles(); // 沒指定路徑時會優先找 index.html
app.UseStaticFiles();

// API：/api/stations?q=xxx
app.MapGet("/api/stations", async (string? q, StationRepository repo) =>
{
    try
    {
        var (total, results) = await repo.SearchAsync(q);

        return Results.Ok(new
        {
            total,
            keyword = q ?? "",
            count = results.Count,
            results
        });
    }
    catch (FileNotFoundException ex)
    {
        return Results.Problem(ex.Message, statusCode: 500);
    }
});

app.Run();
