using System.Data;
using ucakdemo.BusinessLogic.FlightServiceClient;

namespace webapp.Controllers
{
    public class FlightService
    {
        private readonly FlightServiceClient _client;

        public FlightService()
        {
            _client = new FlightServiceClient();
        }

        public DataSet GetHavaNoktalar(string kullaniciadi, string sifre)
        {
            return null;
        }
    }
}