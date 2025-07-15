using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace PG008.Metodos
{
    public class Conexion
    {
        public static string Bd = ConfigurationManager.ConnectionStrings["DBMVCEntities"].ConnectionString;
    }
}