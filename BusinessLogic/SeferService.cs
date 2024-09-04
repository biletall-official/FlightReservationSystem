using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;
using System.Data;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using System.Xml.Serialization;
using UçakDemo.Models;
using UçakDemo.Models.InternationalSefer;
using UçakDemo.Models.Sefer;
using ucakdemo.Services;
using Secenek = UçakDemo.Models.InternationalSefer.Secenek;
using Segment = UçakDemo.Models.InternationalSefer.Segment;

namespace UçakDemo.Services
{
    public class SeferService
    {
        private readonly FlightServiceClient _client;

        public SeferService(FlightServiceClient client)
        {
            _client = client;
        }

        public async Task<SeferResponse> GetSeferListesi(SeferRequest seferRequest)
        {
            string xmlResponse = await _client.SearchFlightsAsync(seferRequest);
            var sefer = ParseSeferler(xmlResponse);
            return sefer;
        }
        public async Task<InternationalSeferResponse> GetInternationalSeferler(InternationalSeferRequest ınternationalSeferRequest)
        {
            string xmlResponse = await _client.SearchInternationalFlightsAsync(ınternationalSeferRequest);
            var internationalsefer = ParseInternationalSeferler(xmlResponse);
            return internationalsefer;
        }

        private SeferResponse ParseSeferler(string xml)
        {
            var element = XElement.Parse(xml);
            var seferler = new List<SeferResponse>();

            var sefer = new SeferResponse()
            {
                Segmentler = new List<Models.Sefer.Segment>(),
                Secenekler = new List<Models.Sefer.Secenek>(),
            };

            foreach (var seferElement in element.Descendants("Secenekler"))
            {
                try
                {
                    var secenek = new Models.Sefer.Secenek
                    {

                        ID = int.Parse(seferElement.Element("ID")?.Value),
                        FirmaNo = int.Parse(seferElement.Element("FirmaNo")?.Value),
                        FiyatP = decimal.Parse(seferElement.Element("FiytP")?.Value),
                        FiyatE = decimal.Parse(seferElement.Element("FiyatE")?.Value),
                        FiyatB = decimal.Parse(seferElement.Element("FiyatB")?.Value),
                        ServisUcretP = decimal.Parse(seferElement.Element("ServisUcretP")?.Value),
                        ServisUcretE = decimal.Parse(seferElement.Element("ServisUcretE")?.Value),
                        ServisUcretB = decimal.Parse(seferElement.Element("ServisUcretB")?.Value),
                        ToplamFiyatP = decimal.Parse(seferElement.Element("ToplamFiyatP")?.Value),
                        ToplamFiyatE = decimal.Parse(seferElement.Element("ToplamFiyatE")?.Value),
                        BagajP = seferElement.Element("BagajP")?.Value,
                        BagajE = seferElement.Element("BagajE")?.Value,
                        Vakit = seferElement.Element("Vakit")?.Value,
                    };
                    sefer.Secenekler.Add(secenek);
                }
                catch (Exception e)
                {
                    
                }
            }
            foreach (var segmentElement in element.Descendants("Segmentler"))
            {
                try
                {
                    var segment = new Models.Sefer.Segment
                    {
                        ID = int.Parse(segmentElement.Element("ID")?.Value),
                        SecenekID = int.Parse(segmentElement.Element("SecenekID")?.Value),
                        HavaYolu = segmentElement.Element("Firma")?.Value,
                        SeferNo = segmentElement.Element("SeferNo")?.Value,
                        KalkisKod = segmentElement.Element("Kalkis")?.Value,
                        VarisKod = segmentElement.Element("Varis")?.Value,
                        KalkisSehir = segmentElement.Element("KalkisSehir")?.Value,
                        VarisSehir = segmentElement.Element("VarisSehir")?.Value,
                        KalkisHavaalan = segmentElement.Element("KalkisHavaalan")?.Value,
                        VarisHavaalan = segmentElement.Element("VarisHavaalan")?.Value,
                        KalkisTarih = Convert.ToDateTime(segmentElement.Element("KalkisTarih")?.Value),
                        VarisTarih = Convert.ToDateTime(segmentElement.Element("VarisTarih")?.Value),
                        Vakit = int.Parse(segmentElement.Element("Sure")?.Value),
                        Sinif = segmentElement.Element("SinifE").Value,
                        KalanKoltukSayi = int.Parse(segmentElement.Element("KoltukE")?.Value),
                        FiyatPaketAnahtari = segmentElement.Element("BagajE")?.Value,
                    };
                    sefer.Segmentler.Add(segment);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            foreach (var secenek in sefer.Secenekler)
            {
                var ilgiliSegmentler = sefer.Segmentler.Where(s => s.SecenekID == secenek.ID).ToList();
                if (ilgiliSegmentler.Count > 1)
                {
                    var aktarmaliSefer = string.Join(" -> ", ilgiliSegmentler.Select(s => $"{s.KalkisKod}-{s.VarisKod}"));
                }
            }
            return sefer;
        }
            private InternationalSeferResponse ParseInternationalSeferler(string xml)
            {
                var element = XElement.Parse(xml);
                var ınternationalseferler = new List<InternationalSeferResponse>();

                var Internationalsefer = new InternationalSeferResponse()
                {
                    InternationalSegmentler = new List<Segment>(),
                    InternationalSecenekler = new List<Secenek>(),
                };

                foreach (var seferElement in element.Descendants("Secenekler"))
                {
                    try
                    {
                        var Internationalsecenek = new Secenek
                        {

                            ID = int.Parse(seferElement.Element("ID")?.Value),
                            FirmaNo = int.Parse(seferElement.Element("FirmaNo")?.Value),
                            VFiyat = decimal.Parse(seferElement.Element("VFiyat")?.Value),
                            NFiyat = decimal.Parse(seferElement.Element("NFiyat")?.Value),
                            Vakit = seferElement.Element("Vakit")?.Value,
                            //ToplamFiyatE = decimal.Parse(seferElement.Element("ToplamFiyatE")?.Value),
                        };
                        Internationalsefer.InternationalSecenekler.Add(Internationalsecenek);
                    }
                    catch (Exception e)
                    {
                        
                    }
                }
                foreach (var segmentElement in element.Descendants("Segmentler"))
                {
                    try
                    {
                        var Internationalsegment = new Segment
                        {
                            ID = int.Parse(segmentElement.Element("ID")?.Value),
                            UcusID = int.Parse(segmentElement.Element("UcusID")?.Value ?? "0"),
                            SecenekID = int.Parse(segmentElement.Element("SecenekID")?.Value),
                            HavaYolu = segmentElement.Element("HavaYolu")?.Value,
                            HavaYoluKod = segmentElement.Element("HavaYoluKod")?.Value,
                            GercekTFAciklama = segmentElement.Element("GercekTFAciklama")?.Value,
                            FirmaSeferNo = segmentElement.Element("SeferNo")?.Value,
                            KalkisKod = segmentElement.Element("KalkisKod")?.Value,
                            VarisKod = segmentElement.Element("VarisKod")?.Value,
                            KalkisSehir = segmentElement.Element("KalkisSehir")?.Value,
                            VarisSehir = segmentElement.Element("VarisSehir")?.Value,
                            KalkisTarih = Convert.ToDateTime(segmentElement.Element("KalkisTarih")?.Value),
                            VarisTarih = Convert.ToDateTime(segmentElement.Element("VarisTarih")?.Value),
                            Vakit = segmentElement.Element("Vakit")?.Value,
                            Sinif = segmentElement.Element("Sinif").Value,
                            KalanKoltukSayi = int.Parse(segmentElement.Element("KalanKoltukSayi")?.Value),
                            SeferNo = int.Parse(segmentElement.Element("SeferNo")?.Value),
                            UcusSuresi = int.Parse(segmentElement.Element("UcusSuresi")?.Value),
                            SinifTip = segmentElement.Element("SinifTip")?.Value,
                            ToplamSeyahatSuresi = int.Parse(segmentElement.Element("ToplamSeyahatSuresi")?.Value),
                            //SeferKod = int.Parse(segmentElement.Element("SeferKod")?.Value),
                           //Bagaj = segmentElement.Element("Bagaj")?.Value,
                            
                            
                        };
                        Internationalsefer.InternationalSegmentler.Add(Internationalsegment);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }
                foreach (var secenek in Internationalsefer.InternationalSecenekler)
                {
                    var ilgiliSegmentler = Internationalsefer.InternationalSegmentler
                        .Where(s => s.SecenekID == secenek.ID)
                        .ToList();
                    if (ilgiliSegmentler.Count > 1)
                    {
                        var aktarmaliSefer = string.Join(" -> ", ilgiliSegmentler
                            .Select(s => $"{s.KalkisKod}-{s.VarisKod}"));
                    }
                }
                return Internationalsefer;
            }
    };
}
