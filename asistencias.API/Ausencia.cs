using System.ComponentModel.DataAnnotations.Schema;

namespace Ausencias.API
{
    public class Ausencia
    {
        [Column("estudianteId")]
        public string Id { get; set; }

        [Column("id")]
        public string Asignatura { get; set; }

        [Column("nombre")]
        public string Grupo { get; set; }
        [Column("creadoPor")]
        public string CreadoPor { get; set; }

        [Column("registros")]
        public List<Registro> Registros { get; set; }
        
    }


    public class Registro
    {
        [Column("estudianteId")]
        public string EstudianteId { get; set; }

        [Column("Estado_Id")]
        public string EstadoId { get; set; }

        [Column("Cantidad")]
        public byte Cantidad { get; set; }
    }
}
