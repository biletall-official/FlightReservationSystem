using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;
using Microsoft.Extensions.Configuration;
using UçakDemo.Models;
using UçakDemo.Services;


namespace ucakdemo.Services
{
    public class FlightServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public FlightServiceClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient("FlightServiceClient");
            _configuration = configuration;
        }

        public async Task<string> GetHavaNoktaXml()
        {
            var kullaniciAdi = _configuration["BiletallService:KullaniciAdi"];
            var sifre = _configuration["BiletallService:Sifre"];

            var soapRequest =
                $@"<?xml version=""1.0"" encoding=""utf-8""?>
                <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                    <soap:Body>
                        <XmlIslet xmlns=""http://tempuri.org/"">
                            <xmlIslem>
                                <HavaNoktaGetirKomut xmlns="""" />
                            </xmlIslem>
                            <xmlYetki>
                                <Kullanici xmlns="""">
                                    <Adi>{kullaniciAdi}</Adi>
                                    <Sifre>{sifre}</Sifre>
                                </Kullanici>
                            </xmlYetki>
                        </XmlIslet>
                    </soap:Body>
                </soap:Envelope>";

            var content = new StringContent(soapRequest, System.Text.Encoding.UTF8, "text/xml");
            var response = await _httpClient.PostAsync("/wstest/service.asmx", content);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> SearchFlightsAsync(SeferRequest seferRequest)
        {
            var kullaniciAdi = _configuration["BiletallService:KullaniciAdi"];
            var sifre = _configuration["BiletallService:Sifre"];

            var soapRequest = $@"<?xml version=""1.0"" encoding=""utf-8""?>
                <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                    <soap:Body>
                        <XmlIslet xmlns=""http://tempuri.org/"">
                            <xmlIslem>
                                <Sefer xmlns="""">
                                <FirmaNo>1000</FirmaNo>
                                <KalkisAdi>{seferRequest.KalkisAdi}</KalkisAdi>
                                <VarisAdi>{seferRequest.VarisAdi}</VarisAdi>
                                <Tarih>{seferRequest.Tarih.ToString("yyyy-MM-dd")}</Tarih>
                                <SeyahatTipi>{seferRequest.SeyahatTipi}</SeyahatTipi>
                                <IslemTipi>{seferRequest.IslemTipi}</IslemTipi>
                                <YetiskinSayi>{seferRequest.YetiskinSayi}</YetiskinSayi>
                                <CocukSayi>{seferRequest.CocukSayi}</CocukSayi>
                                <BebekSayi>{seferRequest.BebekSayi}</BebekSayi>
                                <OgrenciSayi>{seferRequest.OgrenciSayi}</OgrenciSayi>
                                <YasliSayi>{seferRequest.YasliSayi}</YasliSayi>
                                <AskerSayi>{seferRequest.AskerSayi}</AskerSayi>
                                <GencSayi>{seferRequest.GencSayi}</GencSayi>
                                <Ip>127.0.0.1</Ip>
                            </Sefer>
                            </xmlIslem>
                            <xmlYetki>
                                <Kullanici xmlns="""">
                                    <Adi>{kullaniciAdi}</Adi>
                                    <Sifre>{sifre}</Sifre>
                                </Kullanici>
                            </xmlYetki>
                        </XmlIslet>
                    </soap:Body>
                </soap:Envelope>";

            var content = new StringContent(soapRequest, System.Text.Encoding.UTF8, "text/xml");
            var response = await _httpClient.PostAsync("/wstest/service.asmx", content);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> SearchInternationalFlightsAsync(InternationalSeferRequest ınternationalSeferRequest)
        {
            var kullaniciAdi = _configuration["BiletallService:KullaniciAdi"];
            var sifre = _configuration["BiletallService:Sifre"];

            var soapRequest = $@"<?xml version=""1.0"" encoding=""utf-8""?>
                <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                    <soap:Body>
                        <XmlIslet xmlns=""http://tempuri.org/"">
                            <xmlIslem>
                                <Sefer xmlns="""">
                                <FirmaNo>1100</FirmaNo>
                                <KalkisAdi>{ınternationalSeferRequest.KalkisAdi}</KalkisAdi>
                                <VarisAdi>{ınternationalSeferRequest.VarisAdi}</VarisAdi>
                                <Tarih>{ınternationalSeferRequest.Tarih:yyyy-MM-dd}</Tarih>
                                <SeyahatTipi>{ınternationalSeferRequest.SeyahatTipi}</SeyahatTipi>
                                <IslemTipi>{ınternationalSeferRequest.IslemTipi}</IslemTipi>
                                <YetiskinSayi>{ınternationalSeferRequest.YetiskinSayi}</YetiskinSayi>
                                <CocukSayi>{ınternationalSeferRequest.CocukSayi}</CocukSayi>
                                <BebekSayi>{ınternationalSeferRequest.BebekSayi}</BebekSayi>
                                <Ip>127.0.0.1</Ip>
                            </Sefer>
                            </xmlIslem>
                            <xmlYetki>
                                <Kullanici xmlns="""">
                                    <Adi>{kullaniciAdi}</Adi>
                                    <Sifre>{sifre}</Sifre>
                                </Kullanici>
                            </xmlYetki>
                        </XmlIslet>
                    </soap:Body>
                </soap:Envelope>";

            var content = new StringContent(soapRequest, System.Text.Encoding.UTF8, "text/xml");
            var response = await _httpClient.PostAsync("/wstest/service.asmx", content);
            var readAsStringAsync = await response.Content.ReadAsStringAsync();
            return readAsStringAsync;
        }

    }
}


        
