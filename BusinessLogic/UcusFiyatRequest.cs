

using UçakDemo.Services;

namespace UçakDemo.Models
{
    
    public class UcusFiyatRequest : SeferRequest
    
    {
        public byte YetiskinSayi { get; set; }
        public byte CocukSayi { get; set; }
        public byte BebekSayi { get; set; }
        public byte OgrenciSayi { get; set; }
        public byte YasliSayi { get; set; }
        public byte AskerSayi { get; set; }
        public byte GencSayi { get; set; }
        public int FirmaNo { get; set; } = 1100;
        public string Kalkis { get; set; }
        public string Varis { get; set; }
        public DateTime KalkisTarih { get; set; }
        public DateTime VarisTarih { get; set; }
        public int UcusNo { get; set; }
        public int FirmaKod { get; set; }
        public string Sinif { get; set; }
        public byte DonusMu { get; set; }
        public byte SeferKod { get; set; }

    }
}
