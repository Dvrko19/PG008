﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using PG008.Metodos;
using PG008.Models;

namespace PG008.Metodos
{
    public class MarcasMetodos
    {
        private static MarcasMetodos _instance = null;

        public MarcasMetodos()
        {
        }

        public static MarcasMetodos Instancia
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MarcasMetodos();
                }
                return _instance;
            }
        }
        public List<Marcas> Listar()
        {
            List<Marcas> oListaMarcas = new List<Marcas>();

            using (SqlConnection Ocnn = new SqlConnection(Conexion.Bd))
            {
                SqlCommand cmd = new SqlCommand("sp_consultaMarcas", Ocnn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                Ocnn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    oListaMarcas.Add(new Marcas()
                    {
                        IDMarca = Convert.ToInt32(dr["IdMarca"].ToString()),
                        Descripcion = dr["Descripcion"].ToString(),
                        Imagen = (byte[])dr["Imagen"],
                        Estatus = Convert.ToBoolean(dr["Estatus"].ToString()),
                        ImagenBase64 = Convert.ToBase64String((byte[])dr["Imagen"])

                    });
                    dr.Close();
                }
                return oListaMarcas;
            }
        }
        public bool Registrar(Marcas Marcas)
        {
            bool respuesta = true;
            using (SqlConnection Ocnn = new SqlConnection(Conexion.Bd))
            {
                try
                {
                    Ocnn.Open();
                    SqlCommand cmd = new SqlCommand("Sp_InsertarMarcas", Ocnn);
                    cmd.Parameters.AddWithValue("Descripcion", Marcas.Descripcion);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    if(!string.IsNullOrEmpty(Marcas.ImagenBase64))
                    {
                        byte[] imageBytes = Convert.FromBase64String(Marcas.ImagenBase64);
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
        public bool Modificar(Marcas Marcas)
        {
            bool respuesta = true;
            using (SqlConnection Ocnn = new SqlConnection(Conexion.Bd))
            {
                try
                {
                    Ocnn.Open();
                    SqlCommand cmd = new SqlCommand("Sp_ModificarMarcas", Ocnn);
                    cmd.Parameters.AddWithValue("IdMarca", Marcas.IDMarca);
                    cmd.Parameters.AddWithValue("Descripcion", Marcas.Descripcion);
                    cmd.Parameters.AddWithValue("Estatus", Marcas.Estatus);
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
                    string sBorrar = "UPDATE MARCAS SET ESTATUS = 'False' FROM MARCAS WHERE IDTipo = @A1";

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