using System.Text.Json;

public class StationRepository
{
    private readonly IWebHostEnvironment _env;
    private List<AffStation>? _cache;

    public StationRepository(IWebHostEnvironment env)
    {
        _env = env;
    }

    private async Task EnsureLoadedAsync()
    {
        if (_cache != null) return;

        string filePath = Path.Combine(_env.ContentRootPath, "App_Data", "affdata.json");
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"找不到 JSON 檔案：{filePath}");

        string json = await File.ReadAllTextAsync(filePath);

        _cache = JsonSerializer.Deserialize<List<AffStation>>(json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<AffStation>();
    }

    public async Task<(int total, List<AffStation> results)> SearchAsync(string? keyword)
    {
        await EnsureLoadedAsync();

        keyword = (keyword ?? string.Empty).Trim();

        // 沒輸入關鍵字：回傳全部（或你想限制前 N 筆也行）
        if (string.IsNullOrWhiteSpace(keyword))
            return (_cache!.Count, _cache!);

        var results = _cache!
            .Where(s => !string.IsNullOrWhiteSpace(s.observatoryname) &&
                        s.observatoryname.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return (_cache!.Count, results);
    }
}
