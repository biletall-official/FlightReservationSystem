using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace UçakDemo.Models
{
    [XmlRoot("TasiyiciFirmaYolcuYasKurallar")]
    public class TasiyiciFirmaYolcuYasKurallar
    {
        [XmlElement("TasiyiciFirmaYolcuYasKural")]
        public List<TasiyiciFirmaYolcuYasKural> Kurallar { get; set; }
    }

    public class TasiyiciFirmaYolcuYasKural
    {
        public int TasiyiciFirmaNo { get; set; }
        public string TasiyiciFirma { get; set; }
        public int YolcuTip { get; set; }
        public string YolcuTipAciklama { get; set; }
        public int MinYas { get; set; }
        public int MaxYas { get; set; }
    }
}