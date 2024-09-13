namespace UçakDemo.Models.InternationalSefer;

public class Segment
{
    public int ID { get; set; } = 1000;
    public int SecenekID { get; set; }
    public int UcusID { get; set; }
    public string HavaYolu { get; set; }
    public string HavaYoluKod { get; set; }
    public string GercekTFAciklama { get; set; }
    public string FirmaSeferNo { get; set; }
    public string KalkisKod { get; set; }
    public string VarisKod { get; set; }
    public string KalkisSehir { get; set; }
    public string VarisSehir { get; set; }
    public DateTime KalkisTarih { get; set; }
    public DateTime VarisTarih { get; set; }
    public string Vakit { get; set; }
    public string Sinif { get; set; }
    public int SeferKod { get; set; }
    public int SeferNo { get; set; }
    public int KalanKoltukSayi { get; set; }

    public string UcusTuru { get; set; } = "Yurtdışı";
    public int UcusSuresi { get; set; }
    public byte YetiskinSayi { get; set; }
    public int ToplamSeyahatSuresi { get; set; }
    public int DonusMu { get; set; } = 1;
    public string SinifTip { get; set; }

  
}
