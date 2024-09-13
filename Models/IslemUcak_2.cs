using UçakDemo.Models.InternationalSefer;

namespace UçakDemo.Models;

public class IslemUcak_2
{
    public int IslemTip { get; set; } = 1;// Rezervasyon için 1, Satış için 0
    public int FirmaNo { get; set; }
    public string TelefonNo { get; set; }
    public string CepTelefonNo { get; set; }
    public string Email { get; set; }
    public Segment Segment1 { get; set; }
    public Segment Segment2 { get; set; } 
    public YolcuModel Yolcu1 { get; set; }
}
   
