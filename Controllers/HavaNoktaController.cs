using Microsoft.AspNetCore.Mvc;
using ucakdemo;
using ucakdemo.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UçakDemo.Services;

namespace UçakDemo.Controllers
{
    public class HavaNoktaController : Controller
    {
        private readonly HavaNoktaService _havaNoktaService;

        public HavaNoktaController(HavaNoktaService havaNoktaService)
        {
            _havaNoktaService = havaNoktaService;
        }

        public async Task<IActionResult> Index()
        {
            var havaNoktalari = await _havaNoktaService.GetHavaNoktalari();
            return View("GetHavaNoktalar",havaNoktalari);
        }
        public async Task<IActionResult> GetSuggestions([FromQuery] string query)
        {
            var havaNoktalar = await _havaNoktaService.GetHavaNoktalari();
            var suggestions = havaNoktalar
                .Where(hn => hn.HavaAlanAd.StartsWith(query, StringComparison.OrdinalIgnoreCase) ||
                             hn.SehirAd.StartsWith(query, StringComparison.OrdinalIgnoreCase) ||
                             hn.UlkeAd.StartsWith(query, StringComparison.OrdinalIgnoreCase))
                .Select(hn => $"{hn.HavaAlanAd} ({hn.SehirAd}, {hn.UlkeAd})")
                .Take(5)
                .ToList();

            return Json(suggestions);
        }
    }
}
