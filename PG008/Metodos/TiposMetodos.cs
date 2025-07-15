using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
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
                    oListaTipos.Add(new Tipos()
                    {
                        IdTipos = Convert.ToInt32(dr["IDTipo"].ToString()),
                        Descripcion = dr["Descripcion"].ToString(),
                        Estatus = Convert.ToBoolean(dr["Estatus"].ToString()),
                        ImagenBase64 = Convert.ToBase64String((byte[])dr["Imagen"])

                    });
                    dr.Close();
                }
                return oListaTipos;
            }
        }
        public bool Registrar(Tipos tipos)
        {
            bool respuesta = true;
            using (SqlConnection Ocnn = new SqlConnection(Conexion.Bd))
            {
                try{
                    Ocnn.Open();
                    SqlCommand cmd = new SqlCommand("sp_InsertaTipoVehiculo", Ocnn);
                    cmd.Parameters.AddWithValue("Descripcion", tipos.Descripcion);
                    cmd.Parameters.Add("Resultado",SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    if (!string.IsNullOrEmpty(tipos.ImagenBase64))
                    {
                        byte[] imageBytes = Convert.FromBase64String(tipos.ImagenBase64);
                        cmd.Parameters.AddWithValue("Imagen", imageBytes);

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("Imagen", DBNull.Value);
                    }
                }
                catch(Exception ex){
                    respuesta = false;
                }
                return respuesta;
            }
        }
        public bool Modificar(Tipos tipos) 
        {
            bool respuesta = true;
            using (SqlConnection Ocnn = new SqlConnection(Conexion.Bd))
            {
                try
                {
                    Ocnn.Open();
                    SqlCommand cmd = new SqlCommand("sp_ModificaTipoVehiculo", Ocnn);
                    cmd.Parameters.AddWithValue("IDTipo", tipos.IdTipos);
                    cmd.Parameters.AddWithValue("Descripcion", tipos.Descripcion);
                    cmd.Parameters.AddWithValue("Estatus", tipos.Estatus);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
                return respuesta;
            }

        }
        public bool Eliminar (int Id)
        {
            bool Respuesta = true;

            using (SqlConnection Ocnn = new SqlConnection(Conexion.Bd))
            {
                try
                {
                    Ocnn.Open();
                    string sBorrar = "UPDATE TIPOS SET ESTATUS = 'False' FROM TIPOS WHERE IDMarca = @A1";

                    SqlCommand cmd = new SqlCommand(sBorrar, Ocnn);
                    cmd.Parameters.AddWithValue("@A1", Id);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();

                    cmd.ExecuteNonQuery();

                    Respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception ex)
                {
                    Respuesta = false;
                }
                return Respuesta;
            }
        }
    }
}