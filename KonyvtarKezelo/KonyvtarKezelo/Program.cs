namespace KonyvtarKezelo
{
    public class Konyv
    {
        public string Cim { get; set; }
        public string Szerzo { get; set; }
        public string Kategoria { get; set; }
        public int KiadasEve { get; set; }
        public int Oldalszam { get; set; }

        public override string ToString()
        {
            return $"{Cim} : {Szerzo} - ({Kategoria}) - {KiadasEve} - {Oldalszam}";
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            string fajl = "konyvtar.txt";

            List<Konyv> konyvek = File.ReadAllLines(fajl)
                .Select(sor =>
                {
                    var adatok = sor.Split(";");
                    return new Konyv
                    {
                        Cim = adatok[0],
                        Szerzo = adatok[1],
                        Kategoria = adatok[2],
                        KiadasEve = int.Parse(adatok[3]),
                        Oldalszam = int.Parse(adatok[4])
                    };
                }).ToList();

            Console.WriteLine("2. Feladat");
            foreach (var konyv in konyvek)
            {
                Console.WriteLine(konyv);
            }

            Console.WriteLine($"3. Feladat:\nA könyvek száma: {konyvek.Count}");

            var kategoriak = konyvek
                .GroupBy(k => k.Kategoria)
                .Select(g => new
                {
                    Kategoria = g.Key,
                    Darab = g.Count()
                });

            Console.WriteLine("4. Feladat");
            foreach (var kategoria in kategoriak)
            {
                Console.WriteLine($"{kategoria.Kategoria} - {kategoria.Darab} könyv");
            }

            Console.WriteLine("5. Feladat");
            var leghosszabbOldalszam = konyvek.Max(k => k.Oldalszam);
            var leghosszabbKonyv = konyvek.Where(k => k.Oldalszam == leghosszabbOldalszam);

            var leghosszabbKonyv2 = konyvek.OrderByDescending(k => k.Oldalszam).FirstOrDefault();

            foreach (var konyv in leghosszabbKonyv)
            {
                Console.WriteLine(konyv + " oldalszám");
            }

            var kiadottIdoSzerintRendez = konyvek.Where(k => k.KiadasEve > 1950);
            Console.WriteLine("6. Feladat");
            foreach(var konyv in kiadottIdoSzerintRendez)
            {
                Console.WriteLine(konyv);
            }
        }
    }
}
