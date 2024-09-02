using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;
using System.Data;
using System.Xml.Linq;
using UçakDemo.Models; 
using ucakdemo.Services; 

namespace UçakDemo.Services
{
    public class HavaNoktaService
    {
        private readonly FlightServiceClient _client;

        public HavaNoktaService(FlightServiceClient client)
        {
            _client = client;
        }
        public async Task<List<HavaNokta>> GetHavaNoktalari()
        {
            string xmlResponse = await _client.GetHavaNoktaXml();
            return ParseHavaNoktalar(xmlResponse);
        }

        private List<HavaNokta> ParseHavaNoktalar(string xml)
        {
            var element = XElement.Parse(xml);
            var havaNoktalar = new List<HavaNokta>();

            foreach (var havaNoktaElement in element.Descendants("HavaNokta"))
            {
                var havaNokta = new HavaNokta
                {
                    UlkeKod = (string)havaNoktaElement.Element("UlkeKod"),
                    UlkeAd = (string)havaNoktaElement.Element("UlkeAd"),
                    SehirKod = (string)havaNoktaElement.Element("SehirKod"),
                    SehirAd = (string)havaNoktaElement.Element("SehirAd"),
                    HavaAlanKod = (string)havaNoktaElement.Element("HavaAlanKod"),
                    HavaAlanAd = (string)havaNoktaElement.Element("HavaAlanAd"),
                    
                };
                havaNoktalar.Add(havaNokta);
            }
            return havaNoktalar;
        }
    }
}


