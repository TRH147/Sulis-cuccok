namespace FilmRendezo
{
    public class Film
    {
        public string Cim { get; set; }
        public string Mufaj { get; set; }
        public string Rendezo { get; set; }
        public int MegjelenesEve { get; set; }
        public double Ertekeles { get; set; }

        public override string ToString()
        {
            return $"{Cim} : {Mufaj} - {Rendezo} - {MegjelenesEve} - {Ertekeles}";
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            string fajl = "filmek.txt";

            List<Film> filmek = File.ReadAllLines(fajl)
                .Select(sor =>
                {
                    var adatok = sor.Split(";");
                    return new Film
                    {
                        Cim = adatok[0],
                        Mufaj = adatok[1],
                        Rendezo = adatok[2],
                        MegjelenesEve = int.Parse(adatok[3]),
                        Ertekeles = double.Parse(adatok[4].Replace('.', ','))
                    };
                }).ToList();
            Console.WriteLine($"1. Filmadatbázis tartalma");
            foreach (var film in filmek)
            {
                Console.WriteLine(film);
            }

            Console.WriteLine($"2. Feladat:\nA filmadatbázisban található filmek száma: {filmek.Count}");

            var mufajok = filmek
                .GroupBy(f => f.Mufaj)
                .Select(g => new
                {
                    Mufaj = g.Key,
                    Darab = g.Count()
                });

            Console.WriteLine("3. Feladat");
            foreach (var mufaj in mufajok)
            {
                Console.WriteLine($"{mufaj.Mufaj} - {mufaj.Darab} film");
            }

            Console.WriteLine("4. Feladat");
            var legmagasabbPont = filmek.Max(f => f.Ertekeles);
            var legjobbraErtekeltFilm = filmek.Where(f => f.Ertekeles == legmagasabbPont);
            foreach (var film in legjobbraErtekeltFilm)
            {
                Console.WriteLine(film);
            }

            var megjelentIdoSzerintRendez = filmek.Where(f => f.MegjelenesEve > 2000);
            Console.WriteLine("5. Feladat: \n2000 után megjelent filmek:");
            foreach (var film in megjelentIdoSzerintRendez)
            {
                Console.WriteLine(film);
            }
        }
    }
}
