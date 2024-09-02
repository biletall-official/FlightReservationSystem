namespace UçakDemo.Services;

public class SeferRequest
{
    public int FirmaNo { get; set; } = 1000;
    public string KalkisAdi { get; set; }
    public string VarisAdi { get; set; }
    public DateTime Tarih { get; set; }
    public DateTime DonusTarih { get; set; }
    public byte SeyahatTipi { get; set; }
    public byte IslemTipi { get; set; }
    public byte YetiskinSayi { get; set; }
    public byte CocukSayi { get; set; }
    public byte BebekSayi { get; set; }
    public byte OgrenciSayi { get; set; }
    public byte YasliSayi { get; set; }
    public byte AskerSayi { get; set; }
    public byte GencSayi { get; set; }
    public string UçuşTürü { get; set; } = "Yurtiçi";
    public string Ip { get; set; }
    public string SeferNo { get; set; }
    public string Havayolu { get; set; }
    public DateTime KalkisZamani { get; set; }
    public DateTime VarisZamani { get; set; }
    public decimal Fiyat { get; set; }
}