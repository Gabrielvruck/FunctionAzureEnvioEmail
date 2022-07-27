using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Entidade
{
    [Table("SolicitacaoEmail")]
    public class SolicitacaoEmail
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("Titulo")]
        public string Titulo { get; set; }

        [Column("Mensagem")]
        public string Mensagem { get; set; }

        [Column("Destinatarios")]
        public string Destinatarios { get; set; }
        public bool Enviado { get; set; }
    }
}
