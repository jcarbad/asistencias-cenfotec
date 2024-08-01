using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Ausencias.API
{
    public class Grupo : Entity
    {
        public Grupo() : base("connectionStringName")
        {
        }

        [Key]
        [Column("grupoid")]
        public string GrupoId { get; set; }

        [Column("nivel")]
        public int? Nivel { get; set; }

        [Column("grupo")]
        public int? GrupoNumero { get; set; }

        [Column("subgrupo")]
        public char? SubGrupo { get; set; }

        [Column("anno")]
        public int? Anno { get; set; }

        [Column("guia")]
        public string Guia { get; set; }

        [Column("institutoId")]
        public string InstitutoId { get; set; }
        [Column("Estatus")]
        public string Estatus { get; set; }
        public override string TableName => "grupos";

        public async Task<Grupo> Create()
        {
            this.GrupoId = Guid.NewGuid().ToString();
            int newId = await this.InsertAsync();
            return this;
        }

        public static async Task<List<Grupo>> Get()
        {
            var grupo = new Grupo();
            return await grupo.GetAllAsync<Grupo>();
        }
    }
}
