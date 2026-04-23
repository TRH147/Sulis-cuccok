namespace Konyvkatalogus.Model
{
    public class Konyv
    {
        public int Id { get; set; }
        public string Cim { get; set; }
        public int KiadaIdeje { get; set; }
        public string Szerzo { get; set; }
        public string Kategoria { get; set; }
        public int Isbn { get; set; }

    }
}
