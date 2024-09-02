using System.Xml;
using System.Data;
using System.Net.Http;
using System.Threading.Tasks;
using BusinessLogic.Service;

namespace ucakdemo.BusinessLogic.FlightServiceClient
{
    public class FlightServiceClient
    {
        public XmlDocument strtoXmlDocument(string str) // String'i XmlDocument'e dönüştüren yardımcı metot
        {
            XmlDocument xd = new XmlDocument();
            try
            {
                xd.LoadXml(str);
            }
            catch 
            {
                // Hataları yakalayabilirsiniz
            }
            return xd;
        }

        public async Task<DataSet> stringtoDataset(string xmlstring)
        {
            // İstek XML'i
            

            ServiceSoapClient serviceSoapClient = new(ServiceSoapClient.EndpointConfiguration.ServiceSoap);
            
            // Kullanıcı adı ve şifreyle XML oluşturma
            XmlDocument userCredentials = new XmlDocument();
            userCredentials.LoadXml($"<Kullanici><Adi>DevtestWs</Adi><Sifre>791e33</Sifre></Kullanici>");
            var response = await serviceSoapClient.XmlIsletAsync(strtoXmlDocument(xmlstring).DocumentElement,
                userCredentials.DocumentElement);
            
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(response.Body.XmlIsletResult.OuterXml);
            XmlNodeReader nr = new XmlNodeReader(doc.DocumentElement);
            DataSet ds = new DataSet();
            
            try
            {
                // XML verisini DataSet'e dönüştürme
                ds.ReadXml(nr);
            }
            catch 
            {
                // Hataları yakalayabilirsiniz
            }

            return ds;
        }
    }
}