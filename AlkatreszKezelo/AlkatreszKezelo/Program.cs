namespace AlkatreszKezelo
{

    public class Alkatresz
    {
        public string Nev { get; set; }
        public string Gyarto { get; set; }
        public string Kategoria { get; set; }
        public int Megjelenesi_ev { get; set; }
        public int Ar { get; set; }

        public override string ToString()
        {
            return $"{Nev} - {Gyarto} - {Kategoria} - {Megjelenesi_ev} - {Ar} Ft";
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            string fajl = "alkatreszek.txt";

            List<Alkatresz> alkatreszek = File.ReadAllLines(fajl)
                .Select(sor=>
                {
                    var adatok = sor.Split(";");

                    return new Alkatresz
                    {
                        Nev = adatok[0],
                        Gyarto = adatok[1],
                        Kategoria = adatok[2],
                        Megjelenesi_ev = int.Parse(adatok[3]),
                        Ar = int.Parse(adatok[4])
                    };
                }).ToList();
            Console.WriteLine($"1. Számítógép alkatrészek adatai:\n");
            foreach (var alkatresz in alkatreszek)
            {
                Console.WriteLine(alkatresz);
            }

            Console.WriteLine($"\n2. Számítógép alkatrészek száma: {alkatreszek.Count}");

            var kategoriak = alkatreszek
                .GroupBy(a => a.Kategoria)
                .Select(g => new
                {
                    Kategoria = g.Key,
                    Darab = g.Count()
                });

            Console.WriteLine("\n3. Feladat\n");
            foreach (var kategoria in kategoriak)
            {
                Console.WriteLine($"{kategoria.Kategoria} - {kategoria.Darab} db");
            }

            Console.WriteLine("\n4. Feladat: A legdrágább alkatrész(ek)\n");
            var legdragabb = alkatreszek.Max(a => a.Ar);
            var legdragabbAlkatresz = alkatreszek.Where(a => a.Ar == legdragabb);

            foreach (var alkatresz in legdragabbAlkatresz)
            {
                Console.WriteLine(alkatresz);
            }

            var megjelentIdoSzerintRendez = alkatreszek.Where(a => a.Megjelenesi_ev > 2020);
            Console.WriteLine("\n5. Feladat\n");
            foreach(var alkatresz in megjelentIdoSzerintRendez)
            {
                Console.WriteLine(alkatresz);
            }

        }
    }
}
