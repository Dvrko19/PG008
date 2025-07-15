using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using PG008.Models;

namespace PG008.Metodos
{
    public class CategoriaMetodos
    {
        private static CategoriaMetodos _instance = null;
        public CategoriaMetodos()
        {
        }
        public static CategoriaMetodos Instancia
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CategoriaMetodos();
                }
                return _instance;
            }
        }
        public List<Categoria> Listar()
        {
            List<Categoria> oListaTipos = new List<Categoria>();

            using (SqlConnection Ocnn = new SqlConnection(Conexion.Bd))
            {
                SqlCommand cmd = new SqlCommand("sp_CnsultaCategoria", Ocnn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                Ocnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    oListaTipos.Add(new Categoria()
                    {
                        idCategoria = Convert.ToInt32(dr["IDTipo"].ToString()),
                        Descripcion = dr["Descripcion"].ToString(),
                        Estatus = Convert.ToBoolean(dr["Estatus"].ToString()),
                        ImagenBase64 = Convert.ToBase64String((byte[])dr["Imagen"])

                    });
                    dr.Close();
                }
                return oListaTipos;
            }
        }
        public bool Registrar(Categoria categoria)
        {
            bool respuesta = true;
            using (SqlConnection Ocnn = new SqlConnection(Conexion.Bd))
            {
                try
                {
                    Ocnn.Open();
                    SqlCommand cmd = new SqlCommand("sp_InsertarCategoria", Ocnn);
                    cmd.Parameters.AddWithValue("Descripcion", categoria.Descripcion);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    if (!string.IsNullOrEmpty(categoria.ImagenBase64))
                    {
                        byte[] imageBytes = Convert.FromBase64String(categoria.ImagenBase64);
                        cmd.Parameters.AddWithValue("Imagen", imageBytes);

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("Imagen", DBNull.Value);
                    }
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
                return respuesta;
            }

        }
        public bool Modificar(Categoria categoria)
        {
            bool respuesta = true;
            using (SqlConnection Ocnn = new SqlConnection(Conexion.Bd))
            {
                try
                {
                    Ocnn.Open();
                    SqlCommand cmd = new SqlCommand("sp_ModificaCategorias", Ocnn);
                    cmd.Parameters.AddWithValue("IDTipo", categoria.idCategoria);
                    cmd.Parameters.AddWithValue("Descripcion", categoria.Descripcion);
                    cmd.Parameters.AddWithValue("Estatus", categoria.Estatus);
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
        public bool Eliminar(int Id)
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