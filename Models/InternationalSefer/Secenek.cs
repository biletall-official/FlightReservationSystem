namespace UçakDemo.Models.InternationalSefer;

public class Secenek
{
    public int ID { get; set; } = 1100;
    public int FirmaNo { get; set; }
    public decimal VFiyat { get; set; }
    public decimal NFiyat { get; set; }
    public decimal FiyatB { get; set; }
    public decimal ToplamFiyatE 
    {
        get { return VFiyat + NFiyat; }
    }
    public string BagajP { get; set; }
    public string BagajE { get; set; }
    public string Vakit { get; set; }
    public int YetiskinSayi { get; set; }
    public string UcusTuru { get; set; } = "Yurtdışı";


}