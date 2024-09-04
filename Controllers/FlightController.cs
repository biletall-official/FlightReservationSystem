using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UçakDemo.Models;
using ucakdemo.Services;
using UçakDemo.Services;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UçakDemo.Controllers;

public class FlightController : Controller
{
    private readonly SeferService _seferService;

    public FlightController(SeferService seferService)
    {
        
        _seferService = seferService;
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

            return View("SeferListesi", seferResponse);
        }
        return View("Index", model);
    }

    //flight/getseferler
    [HttpPost]
    public async Task<IActionResult> GetSeferler( string FirmaAdı , string KalkisAdi ,string VarisAdi)
    {
      
        var seferRequest = new SeferRequest()
        {
            YetiskinSayi = 1,
            KalkisAdi = "IST",
            VarisAdi = "ASR",
            Tarih = DateTime.Parse("2024-09-20"),
        };
        var seferResponse = await _seferService.GetSeferListesi(seferRequest);
        return Ok(seferResponse);
    }
    
    //flight/getinternationalseferler
    public async Task<IActionResult> GetInternationalSeferler( string FirmaAdı , string KalkisAdi , string VarisAdi)
    {
        var ınternationalSeferRequest = new InternationalSeferRequest() 
        {
                YetiskinSayi = 1,
                KalkisAdi = "IST",
                VarisAdi = "LHR", 
                Tarih= DateTime.Parse("2024-09-20"), 
        };
        var ınternationalseferResponse = await _seferService.GetInternationalSeferler(ınternationalSeferRequest);
        return Ok(ınternationalseferResponse);

    }
    [HttpPost]
    public async Task<IActionResult> ValidatePrice(int ucusId, decimal mevcutFiyat)
    {
        try
        {
            var internationalSefer = await _seferService.GetInternationalSeferler(new InternationalSeferRequest());
            
            var secenek = internationalSefer.InternationalSecenekler.FirstOrDefault(s => s.ID == ucusId);
                
            if (secenek != null)
            {
                var isValid = secenek.ToplamFiyatE == mevcutFiyat;
                return Json(new { isValid });
            }

            return Json(new { isValid = false });
        }
        catch (Exception ex)
        {
            return Json(new { isValid = false, message = ex.Message });
        }
    }


}