using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UçakDemo.Models;
using ucakdemo.Services;
using UçakDemo.Services;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UçakDemo.Models.InternationalSefer;
using System.Net.Http.Json;
using UçakDemo.BusinessLogic; 
namespace UçakDemo.Controllers;

public class FlightController : Controller
{
    private readonly SeferService _seferService;
    private readonly IHttpClientFactory _httpClientFactory;

    public FlightController(SeferService seferService, IHttpClientFactory httpClientFactory)

    {
        _seferService = seferService;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> Index(int YetiskinSayi,string KalkisAdi, string VarisAdi,DateTime Tarih, string UcusTuru)
    {
        if (ModelState.IsValid)
        {
            if (UcusTuru == "Yurtiçi")
            {
                var seferRequest = new SeferRequest()
                {
                    YetiskinSayi = (byte)YetiskinSayi,
                    KalkisAdi = KalkisAdi,
                    VarisAdi = VarisAdi,
                    Tarih = Tarih,
                    UçuşTürü = UcusTuru,
                };
                var seferResponse = await _seferService.GetSeferListesi(seferRequest);
                
                var yurticiSeferListesi = new YurticiSeferListesi
                {
                    Segmentler = seferResponse.Segmentler,
                    Secenekler = seferResponse.Secenekler
                };
                return View("YurticiSeferListesi",yurticiSeferListesi);
            }
            else if (UcusTuru == "Yurtdışı")
            {
                var internationalSeferRequest = new InternationalSeferRequest()
                {
                    YetiskinSayi = (byte)YetiskinSayi,
                    KalkisAdi = KalkisAdi,
                    VarisAdi = VarisAdi,
                    Tarih = Tarih,
                    UcusTuru = UcusTuru,
                };
                var internationalSeferResponse = await _seferService.GetInternationalSeferler(internationalSeferRequest);
                if (internationalSeferResponse == null ||
                    internationalSeferResponse.InternationalSegmentler == null ||
                    !internationalSeferResponse.InternationalSegmentler.Any())
                {
                 
                    ModelState.AddModelError("", "Sefer bulunamadı.");
                    return View("Error");
                }
                
                var yurtdisiSeferListesi = new YurtdisiSeferListesi
                {
                    InternationalSegmentler = internationalSeferResponse.InternationalSegmentler,
                    InternationalSecenekler = internationalSeferResponse.InternationalSecenekler
                };
                HttpContext.Session.SetObject("SelectedSefer", internationalSeferResponse);
                return View("YurtdisiSeferListesi", yurtdisiSeferListesi);
            }
        }
        return View("Error");
    }
    public IActionResult Error()
    {
        var errorModel = new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        };
        return View(errorModel);
    }
    [HttpPost]
    public async Task<IActionResult> InternationalFlightSearch(InternationalSeferRequest request)
    {
        var internationalSeferResponse = await _seferService.GetInternationalSeferler(request);

        if (internationalSeferResponse == null || 
            internationalSeferResponse.InternationalSegmentler == null || 
            !internationalSeferResponse.InternationalSegmentler.Any())
        {
            ModelState.AddModelError("", "Sefer bulunamadı.");
            return View("Error");
        }

        return View("InternationalFlightList", internationalSeferResponse);
    }
    [HttpGet]
    public async Task<IActionResult> Search(FlightSearchModel model)
    {
        if (ModelState.IsValid)
        {
            var seferRequest = new SeferRequest()
            {
                KalkisAdi = model.KalkisAdi,
                VarisAdi = model.VarisAdi,
                Tarih = model.Tarih,
                YetiskinSayi = (byte)model.YetiskinSayi,
            };

            var seferResponse = await _seferService.GetSeferListesi(seferRequest);
            //HttpContext.Session.SetObject("SelectedSefer", seferResponse);

            return View("SeferListesi", seferResponse);
        }
        return View("Index", model);
    }

    [HttpPost]
    public async Task<IActionResult> ValidatePrice(int ucusID,decimal fiyat)
    {
        var selectedSefer = HttpContext.Session.GetObject<Segment>("SelectedSefer");
        if (selectedSefer == null)
        {
            return Json(new { isValid = false });
        }

        var ucusFiyatRequest = new UcusFiyatRequest
        {
            SeferID = ucusID,
            Fiyat = fiyat, 
            
        };

        var client = _httpClientFactory.CreateClient("FlightServiceClient");
        var response = await client.PostAsJsonAsync("/api/flight/price", ucusFiyatRequest);

        if (response.IsSuccessStatusCode)
        {
            var fiyatVeKurallar = await response.Content.ReadFromJsonAsync<FiyatVeKurallarResponse>();
            HttpContext.Session.SetObject("FiyatVeKurallar", fiyatVeKurallar);
            return Json(new { isValid = true });
        }
        else
        {
            return Json(new { isValid = false });
        }
    }

    [HttpPost]
    public IActionResult SelectFlight(int seferID)
    {
        HttpContext.Session.SetInt32("SeferID", seferID);
        if (seferID == null)
        {
            return RedirectToAction("ValidatePrice");
        }
        return View();
    }
 


}