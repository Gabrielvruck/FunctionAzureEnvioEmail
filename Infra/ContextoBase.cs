using Infra.Entidade;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra
{
    public class ContextoBase:DbContext
    {
        public ContextoBase(DbContextOptions<ContextoBase> options):base (options)
        {
            Database.EnsureCreated();
        }
        public DbSet<SolicitacaoEmail> SolicitacaoEmail { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetStringConectionConfig());
                base.OnConfiguring(optionsBuilder);
            }
        }

        private string GetStringConectionConfig()
        {
            string strcon = "Data Source=localhost;Initial Catalog=Fila_EmailAzure; Integrated Security = true; Encrypt = False; TrustServerCertificate = true";
            return strcon;
        }

    }
}
