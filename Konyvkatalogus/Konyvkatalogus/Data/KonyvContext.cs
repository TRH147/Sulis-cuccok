using Konyvkatalogus.Model;
using Microsoft.EntityFrameworkCore;

namespace Konyvkatalogus.Data
{
    public class KonyvContext : DbContext
    {
        public KonyvContext(DbContextOptions<KonyvContext> options) : base(options)
        {

        }

        public DbSet<Konyv> konyvek { get; set; }
    }
}
