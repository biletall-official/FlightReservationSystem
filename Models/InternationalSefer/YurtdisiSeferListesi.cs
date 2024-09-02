using UçakDemo.Models.InternationalSefer;
using UçakDemo.Models.Sefer;
using UçakDemo.Services;
using Secenek = UçakDemo.Models.InternationalSefer.Secenek;
using Segment = UçakDemo.Models.InternationalSefer.Segment;

namespace UçakDemo.Services;

public class YurtdisiSeferListesi
{
    public List<Segment> InternationalSegmentler { get; set; }
    public List<Secenek> InternationalSecenekler { get; set; }
}