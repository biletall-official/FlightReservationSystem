namespace UçakDemo.Services;

public class InternationalSeferRequest
{
    public int FirmaNo { get; set; } = 1100;
    public string KalkisAdi { get; set; }
    public string VarisAdi { get; set; }
    public DateTime Tarih { get; set; }
    public DateTime DonusTarih { get; set; }
    public byte SeyahatTipi { get; set; }
    public byte IslemTipi { get; set; }
    public byte YetiskinSayi { get; set; }
    public byte CocukSayi { get; set; }
    public byte BebekSayi { get; set; }
    public string Ip { get; set; }
    public string UcusTuru { get; set; } = "Yurtdışı";

}