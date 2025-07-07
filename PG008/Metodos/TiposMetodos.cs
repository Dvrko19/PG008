using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using PG008.Metodos;
using PG008.Models;

namespace PG008.Metodos
{
    public class TiposMetodos
    {
        private static TiposMetodos _instance = null;

        public TiposMetodos() 
        { 
        }

        public static TiposMetodos Instancia
        {
            get 
            {
                if(_instance == null)
                {
                    _instance = new TiposMetodos();
                }
                return _instance;
            }
        }
        public List<Tipos> Listar()
        {
            List<Tipos> oListaTipos = new List<Tipos>();

            using (SqlConnection Ocnn = new SqlConnection(Conexion.Bd))
            {
                SqlCommand cmd = new SqlCommand("Sp_ListarTipos", Ocnn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                Ocnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read()) 
                {
                    
                }
            }
        }
    }
}