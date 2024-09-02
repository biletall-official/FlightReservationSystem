using System.Xml.Linq;
using UçakDemo.Models;
using UçakDemo.Models.Sefer;
using ucakdemo.Services;

namespace UçakDemo.Services;

public class SeferResponse
{
    public List<Segment> Segmentler { get; set; }
    public List<Secenek> Secenekler { get; set; }
}

