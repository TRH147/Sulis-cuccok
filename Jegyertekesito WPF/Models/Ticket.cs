using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jegyertekesito.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        public string Nev { get; set; } = string.Empty;

        public decimal Ar { get; set; }

        public int Darabszam { get; set; }
    }
}
