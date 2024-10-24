using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using michaelsimple_app.Models;
using System.Text.Json;

namespace michaelsimple_app.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        List<Weather> weather = [];

        using HttpClient http = new HttpClient();

        using (var response = await http.GetAsync("https://michaelssimpledemo-api.azurewebsites.net/weatherforecast"))
        {
            string json = await response.Content.ReadAsStringAsync();
            weather = JsonSerializer.Deserialize<List<Weather>>(json, options);
        }

        return View(weather);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
