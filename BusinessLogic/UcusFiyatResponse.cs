namespace UçakDemo.BusinessLogic;

public class UcusFiyatResponse
{
        public int UcusID { get; set; }
        public int SecenekID { get; set; }
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

}
