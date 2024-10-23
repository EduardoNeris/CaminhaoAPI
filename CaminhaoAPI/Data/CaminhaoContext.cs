using Microsoft.EntityFrameworkCore;
using CaminhaoAPI.Models;

namespace CaminhaoApi.Data
{
    public class CaminhaoContext : DbContext
    {
        public CaminhaoContext(DbContextOptions<CaminhaoContext> options) : base(options) { }

        public DbSet<Caminhao> Caminhoes { get; set; }
    }
}
