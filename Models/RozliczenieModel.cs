namespace pitWeb.Models
{
    public class RozliczenieModel
    {
        public int Id { get; set; }
        public int RokPodatkowy { get; set; }
        public string Imie { get; set; } = string.Empty;
        public string Nazwisko { get; set; } = string.Empty;
        public string Pesel { get; set; } = string.Empty;
        public decimal PodatekNalezy { get; set; }
        public DateTime DataZapisu { get; set; }

        public string ImieNazwisko => $"{Imie} {Nazwisko}";
    }
}
