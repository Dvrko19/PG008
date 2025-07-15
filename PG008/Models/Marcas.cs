using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PG008.Models
{
    public class Marcas
    {
        public int IDMarca { get; set; }
        public string Descripcion { get; set; }
        public byte[] Imagen {  get; set; }
        public bool Estatus {  get; set; }

        public string ImagenBase64 { get; set; }

        //AAAAAAAA
    }
}