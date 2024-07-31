using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Ausencias.API
{
    public class Estudiante : Entity
    {
        public Estudiante() : base("connectionStringName")
        {
        }

        

        [Column("estudianteId")]
        public string EstudianteId { get; set; }

        [Column("id")]
        public string Id { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("apellido1")]
        public string Apellido1 { get; set; }

        [Column("apellido2")]
        public string Apellido2 { get; set; }

        [Column("responsableId")]
        public string ResponsableId { get; set; }

        [Column("responsableNombreCompleto")]
        public string ResponsableNombreCompleto { get; set; }

        [Column("responsableTelefono")]
        public string ResponsableTelefono { get; set; }

        [Column("responsableEmail")]
        public string ResponsableEmail { get; set; }

        [Column("creadoPor")]
        public string CreadoPor { get; set; }

        
        public override string TableName => "estudiantes";

        public async Task<Estudiante> Create()
        {
            int newId = await this.InsertAsync();
            return this;
        }

        public static async Task<List<Estudiante>> Get()
        {
            var estudiante = new Estudiante();
            return await estudiante.GetAllAsync<Estudiante>();
            
        }
    }
}
