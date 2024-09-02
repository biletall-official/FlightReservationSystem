namespace UçakDemo.Models.Sefer;

    public class Secenek
    {
        public int ID { get; set; }
        public int FirmaNo { get; set; }
        public decimal FiyatP { get; set; }
        public decimal FiyatE { get; set; }
        public decimal FiyatB { get; set; }
        public decimal ServisUcretP { get; set; }
        public decimal ServisUcretE { get; set; }
        public decimal ServisUcretB { get; set; }
        public decimal ToplamFiyatP { get; set; }
        public decimal ToplamFiyatE { get; set; }
        public string BagajP { get; set; }
        public string BagajE { get; set; }
        public string Vakit { get; set; }
        public string UçuşTürü { get; set; } = "Yurtiçi";
    }
