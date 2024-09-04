namespace UçakDemo.Models.Sefer;

public class Segment
{
    public int ID { get; set; } = 1000;
    public int SecenekID { get; set; }
    public string HavaYolu { get; set; }
    public string SeferNo { get; set; }
    public string KalkisKod { get; set; }
    public string VarisKod { get; set; }
    public string KalkisSehir { get; set; }
    public string VarisSehir { get; set; }
    public string KalkisHavaalan { get; set; }
    public string VarisHavaalan { get; set; }
    public DateTime KalkisTarih { get; set; }
    public DateTime VarisTarih { get; set; }
    public int Vakit { get; set; }
    public string Sinif { get; set; }
    public int KalanKoltukSayi { get; set; }
    public string FiyatPaketAnahtari { get; set; }
    public string FiyatPaketTanimi { get; set; }
    public string UcusTuru { get; set; } = "Yurtiçi";
}