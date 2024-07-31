using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ausencias.API
{
    public class Administrativo : Entity
    {
        public Administrativo(string connectionStringName) : base("conectionCtring")
        {
        }

        [Column("administrativoId")]
        public string AdministrativoId { get; set; }

        [Column("id")]
        public string Id { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("apellido1")]
        public string Apellido1 { get; set; }

        [Column("apellido2")]
        public string Apellido2 { get; set; }

        [Column("email")]
        public string Email { get; set; }

        public override string TableName => "administrativos";
    }
}
