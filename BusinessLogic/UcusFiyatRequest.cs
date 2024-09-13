

using UçakDemo.Models.UcusFiyat;
using UçakDemo.Services;

namespace UçakDemo.Models
{

    public class UcusFiyatRequest
    {
        
        public List<Segment> Segments { get; set; }

        public int FirmaNo { get; set; }
        public int YetiskinSayi { get; set; }
        public byte? CocukSayi { get; set; }
        public byte? BebekSayi { get; set; }
        public byte? OgrenciSayi { get; set; }
        public byte? YasliSayi { get; set; }
        public byte? AskerSayi { get; set; }
        public byte? GencSayi { get; set; }
        public byte? CIP { get; set; }
        public UcusFiyatRequest()
        {
            Segments = new List<Segment>();
        }
    }
    
}