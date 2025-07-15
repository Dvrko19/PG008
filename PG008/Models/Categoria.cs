using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PG008.Models
{
    public class Categoria
    {
        public int idCategoria { get; set; }
        public string Descripcion { get; set; }
        public bool Estatus { get; set; } //Activo o Inactivo

        public byte[] Imagen { get; set; }

        public string ImagenBase64 { get; set; }

    }
}