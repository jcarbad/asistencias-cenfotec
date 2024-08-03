using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ausencias.API
{
    public class Administrativo : Entity
    {
        public Administrativo() : base("conectionCtring")
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
        [Column("Estatus")]
        public string Estatus { get; set; }
        public override string TableName => "administrativos";

        public async Task<Administrativo> Create()
        {
            this.AdministrativoId = Guid.NewGuid().ToString();
            int newId = await this.InsertAsync();
            return this;
        }

        public static async Task<List<Administrativo>> Get(string where = null)
        {
            var estudiante = new Administrativo();
            return await estudiante.GetAllAsync<Administrativo>(where);
        }
    }
}
