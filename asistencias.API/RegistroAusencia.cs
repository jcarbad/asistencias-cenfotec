using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ausencias.API
{
    public class RegistroAusencia : Entity
    {
        public RegistroAusencia() : base("connectionStringName")
        {
        }

        [Key]
        [Column("registroAusenciaId")]
        public string RegistroAusenciaId { get; set; }

        [Column("estudianteId")]
        public string EstudianteId { get; set; }

        [Column("grupoId")]
        public string GrupoId { get; set; }

        [Column("asignaturaId")]
        public string AsignaturaId { get; set; }

        [Column("creadoPor")]
        public string CreadoPor { get; set; }

        [Column("creado")]
        public DateTime? Creado { get; set; }

        [Column("actualizadoPor")]
        public string ActualizadoPor { get; set; }

        [Column("actualizado")]
        public DateTime? Actualizado { get; set; }

        [Column("estadoId")]
        public string EstadoId { get; set; }

        [Column("cantidad")]
        public byte Cantidad { get; set; }

        public override string TableName => "registroAusencias";

        // Método para crear registros de ausencias
        public static async Task CreateAsync(Ausencia ausencias, string username)
        {

            foreach (var ausencia in ausencias.Registros)
            {
                var ausenciaActual = new RegistroAusencia();
                ausenciaActual.RegistroAusenciaId = Guid.NewGuid().ToString();
                ausenciaActual.EstudianteId = ausencia.EstudianteId;
                ausenciaActual.GrupoId = ausencias.Grupo;
                ausenciaActual.AsignaturaId = ausencias.Asignatura;
                ausenciaActual.CreadoPor = ausencias.CreadoPor;
                ausenciaActual.Creado = DateTime.Now;
                ausenciaActual.EstadoId = ausencia.EstadoId;
                ausenciaActual.Cantidad = ausencia.Cantidad;

                await ausenciaActual.InsertAsync();
            }
        }
    }
}
