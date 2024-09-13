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
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using UçakDemo.BusinessLogic;
using Segment = UçakDemo.Models.Sefer.Segment;


namespace UçakDemo.Controllers;

public class FlightController : Controller
{
    private readonly SeferService _seferService;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    private readonly UcusFiyatService _ucusFiyatService;

    public FlightController(SeferService seferService, IHttpClientFactory httpClientFactory,
        IConfiguration configuration, UcusFiyatService ucusFiyatService)

    {
        _seferService = seferService;
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
        _ucusFiyatService = ucusFiyatService;
    }

    public async Task<IActionResult> Index(int YetiskinSayi, string KalkisAdi, string VarisAdi, DateTime Tarih,
        string UcusTuru)
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
                HttpContext.Session.SetObject("SeferListesi1", seferResponse);
                var yurticiSeferListesi = new YurticiSeferListesi
                {
                    Segmentler = seferResponse.Segmentler,
                    Secenekler = seferResponse.Secenekler
                };
                return View("YurticiSeferListesi", yurticiSeferListesi);
            }
            else if (UcusTuru == "Yurtdışı")
            {
                var InternationalseferRequest = new InternationalSeferRequest()
                {
                    YetiskinSayi = (byte)YetiskinSayi,
                    KalkisAdi = KalkisAdi,
                    VarisAdi = VarisAdi,
                    Tarih = Tarih,
                    UcusTuru = UcusTuru,
                };
                var seferResponseYurtDisi = await _seferService.GetInternationalSeferler(InternationalseferRequest);
                HttpContext.Session.SetObject("SeferListesi", seferResponseYurtDisi);
                var yurtdisiSeferListesi = new YurtdisiSeferListesi
                {
                    InternationalSegmentler = seferResponseYurtDisi.InternationalSegmentler,
                    InternationalSecenekler = seferResponseYurtDisi.InternationalSecenekler
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


    [HttpPost("Flight/ValidatePrice/{ucusID}/{ToplamFiyatE}")]
    public async Task<IActionResult> ValidatePrice([FromRoute] int ucusID, [FromRoute] decimal ToplamFiyatE)
    {
        var selectedSefer = HttpContext.Session.GetObject<YurtdisiSeferListesi>("SeferListesi");

        var selectedSegments = selectedSefer.InternationalSegmentler.Where(s => s.UcusID == ucusID).ToList();

        var SecenekID = selectedSegments.FirstOrDefault().SecenekID;
        var selectedSeceneks = selectedSefer.InternationalSecenekler.Where(s => s.ID == SecenekID).ToList();
        decimal toplam = 0;
       
            var flightServiceClient = new FlightServiceClient(_httpClientFactory, _configuration);
            var soapResponse = await flightServiceClient.FiyatCek(new()
            {
                FirmaNo = selectedSeceneks.FirstOrDefault().FirmaNo,
                YetiskinSayi = 1,
                CocukSayi = 0,
                BebekSayi = 0,
                OgrenciSayi = 0,
                YasliSayi = 0,
                AskerSayi = 0,
                GencSayi = 0,
                Segments = selectedSegments.Select(s=>new Models.UcusFiyat.Segment
                {
                    Kalkis = s.KalkisKod,
                    Varis = s.VarisKod,
                    KalkisTarih = s.KalkisTarih,
                    VarisTarih = s.VarisTarih,
                    UcusNo = s.UcusID,
                    FirmaKod = s.HavaYoluKod,
                    Sinif = s.Sinif,
                    DonusMu = s.DonusMu,
                    SeferKod = s.SeferKod,
                    
                }).ToList(),

            });
            toplam += ParseUcusFiyatResponse(soapResponse).ToplamBiletFiyati;
       

        if (toplam != ToplamFiyatE)
        {
            HttpContext.Session.SetObject("DogrulananFiyat", toplam);
            return View("Index");
        }
        return RedirectToAction("PassengerInfo", "Flight");

    }

    private UcusFiyatResponse ParseUcusFiyatResponse(string responseXml)
    {
        try
        {
            var serializer = new XmlSerializer(typeof(UcusFiyatResponse));
            using (var reader = new StringReader(responseXml))
            {
                return (UcusFiyatResponse)serializer.Deserialize(reader);
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    
    //[HttpPost]
    // public async Task<IActionResult> GetYasKurallar(int tasiyiciFirmaNo){
    //     var requestXml = $"<TasiyiciFirmaYolcuYasKurallar><TasiyiciFirmaNo>{tasiyiciFirmaNo}</TasiyiciFirmaNo></TasiyiciFirmaYolcuYasKurallar>";
    //     var client = _httpClientFactory.CreateClient("FlightServiceClient");
    //     var content = new StringContent(requestXml, Encoding.UTF8, "text/xml");var response = await client.PostAsync("/wstest/service.asmx", content);
    //     var responseContent = await response.Content.ReadAsStringAsync();
    //     if (response.IsSuccessStatusCode){
    //         try
    //         {
    //             var yasKurallarResponse = DeserializeXml<TasiyiciFirmaYolcuYasKurallar>(responseContent);
    //             return Json(new { isValid = true, data = yasKurallarResponse });
    //         }
    //         catch (Exception ex)
    //         {
    //             return Json(new { isValid = false, errorMessage = ex.Message });
    //         }
    //         
    //     }else{
    //         var statusCode = (int)response.StatusCode;
    //         var reasonPhrase = response.ReasonPhrase;
    //         return Json(new { isValid = false, statusCode, reasonPhrase });
    //     }}
}