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
    public int SeferNo { get; set; }
    public int KalanKoltukSayi { get; set; }

    public string UcusTuru { get; set; } = "Yurtdışı";
    public int UcusSuresi { get; set; }
    public int ToplamSeyahatSuresi { get; set; }
    public string SinifTip { get; set; }
   // public int SeferKod { get; set; }
    //public string Bagaj { get; set; }
}
//  <Segmentler>
//                       <FirmaID>1106</FirmaID>
//                       <Aktarma>false</Aktarma>
//                    

//                       <KalkisUlkeID>0</KalkisUlkeID>
//                       <KalkisUlke>Yunanistan</KalkisUlke>
//                       <KalkisHavaAlani>Eleftherios Venizelos Intl Arpt</KalkisHavaAlani>
//                       <VarisUlkeID>0</VarisUlkeID>
//                       <VarisUlke>Almanya</VarisUlke>
//                       <VarisHavaAlani>Munich Intl Arpt</VarisHavaAlani>
//                       <UcakTip>Airbus A320neo</UcakTip>
//                       <KoridorSayi>1</KoridorSayi>
//                       <KatSayi>1</KatSayi>
//                       <KoltukMesafe>76</KoltukMesafe>
//                       <KuralAnahtar>KmgqMFVqWDKAaD5RpAAAAA==,gws-eJxNTkEKwzAMe0zR3cmSrb2lJC0brN6gHayX/f8ZlRMKM9iWLSE7peTFBxnEp//o8OvGC/STAYVnLoShF4HjsEPERbx1fn7XDLNw3JDRyrbuqq5cS2RDDnNslAX2Wh/rRk6qsZiDHYZBLnCC6U6oY9leWiLVgzS51Rv45wE1yisJ</KuralAnahtar>
//                       <FiyatPaketTanimi/>
//                       <FiyatPaketAnahtari/>
