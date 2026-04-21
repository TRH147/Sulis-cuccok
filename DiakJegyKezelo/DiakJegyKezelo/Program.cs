namespace DiakJegyKezelo
{
    public class Diak
    {
        public string Nev { get; set; }
        public string Tantargy { get; set; }
        public List<int> Jegyek { get; set; }
        public override string ToString()
        {
            return $"{Nev} : {Tantargy} - {string.Join(",", Jegyek)}";
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            string fajl = "jegyek.txt";

            List<Diak> diakok = File.ReadAllLines(fajl)
                .Select(sor =>
                {
                    var adatok = sor.Split(";");

                    return new Diak
                    {
                        Nev = adatok[0],
                        Tantargy = adatok[1],
                        Jegyek = adatok.Skip(2).Select(int.Parse).ToList()
                    };
                }).ToList();
            Console.WriteLine($"1. Diákok jegyei:");
            foreach (var diak in diakok)
            {
                Console.WriteLine(diak);
            }

            Console.WriteLine($"2. Feladat:\nAz osztály létszáma: {diakok.Count} fő");

            var tantargyak = diakok
                .GroupBy(d => d.Tantargy)
                .Select(g => new
                {
                    Tantargy = g.Key,
                    Darab = g.Count()
                });

            Console.WriteLine("3. Feladat:\nTantárgyak szerinti csoportosítás:");
            foreach (var tantargy in tantargyak)
            {
                Console.WriteLine($"{tantargy.Tantargy} - {tantargy.Darab} diák");
            }

            Console.WriteLine("4. Feladat:\nDiákok átlagai:");

            foreach (var diak in diakok)
            {
                double atlag = diak.Jegyek.Average();
                Console.WriteLine($"{diak.Nev} - {diak.Tantargy} - átlag: {atlag:F1}");
            }

            Console.WriteLine("5. Feladat: \nLegjobb tanuló(k):");

            var legjobbAtlag = diakok.Max(d => d.Jegyek.Average());
            var legjobbDiak = diakok.Where(d => d.Jegyek.Average() == legjobbAtlag);

            foreach (var diak in legjobbDiak)
            {                                
                Console.WriteLine($"{diak.Nev} - {diak.Tantargy} - átlag: {diak.Jegyek.Average():F1}");
            }
        }
    }
}
