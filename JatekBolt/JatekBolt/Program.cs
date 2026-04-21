namespace JatekBolt
{
    public class Jatek
    {
        public string TermekNev { get; set; }
        public string Kategoria { get; set; }
        public string Gyarto { get; set; }
        public int Ar {  get; set; }
        public int Keszlet { get; set; }
        public override string ToString()
        {
            return $"{TermekNev} - {Kategoria} - {Gyarto} - {Ar} Ft - {Keszlet} db";
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            string fajl = "jatekok.txt";

            List<Jatek> jatekok = File.ReadAllLines(fajl)
                .Select(sor =>
                {
                    var adatok = sor.Split(";");

                    return new Jatek
                    {
                        TermekNev = adatok[0],
                        Kategoria = adatok[1],
                        Gyarto = adatok[2],
                        Ar = int.Parse(adatok[3]),
                        Keszlet = int.Parse(adatok[4])
                    };
                }).ToList();
            Console.WriteLine($"1. Játékbolt termékei:");
            foreach (var jatek in jatekok)
            {
                Console.WriteLine(jatek);
            }

            Console.WriteLine($"2. Feladat:\nÖsszes termék száma: {jatekok.Count} db");

            var kategoriak = jatekok
                .GroupBy(j => j.Kategoria)
                .Select(g => new
                {
                    Kategoria = g.Key,
                    Darab = g.Count()
                });

            Console.WriteLine("3. Feladat:\nTermékek kategória szerinti csoportosítása:");
            foreach (var kategoria in kategoriak)
            {
                Console.WriteLine($"{kategoria.Kategoria} - {kategoria.Darab} db");
            }
        }
    }
}
