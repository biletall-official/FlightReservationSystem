using System.Data;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ucakdemo.BusinessLogic.FlightServiceClient;
using webapp.Models;

namespace webapp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly FlightServiceClient _client;
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        _client = new(); //class new lenir yoksa çalışmaz 
    }

    public async Task<IActionResult> Index()
    {
        string xmlRequest = "<HavaNoktaGetirKomut/>";
        var ds = await _client.stringtoDataset(xmlRequest);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            var a = row[0].ToString();
            Console.WriteLine(a);
        } 
        
        return View();
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