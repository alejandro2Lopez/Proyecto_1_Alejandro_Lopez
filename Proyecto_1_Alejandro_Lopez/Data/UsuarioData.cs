using Proyecto_1_Alejandro_Lopez.Models.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Proyecto_1_Alejandro_Lopez.Data
{
    public class UsuarioData
    {
        private static UsuarioData instance;

        public static UsuarioData getInstance()
        {

            if (instance == null)
            {

                instance = new UsuarioData();

            }

            return instance;

        }

        public bool InsertarUsuario(Usuario usuario)
        {
            try
            {
                string cadenaConexion = ConfigurationManager
                    .ConnectionStrings["ConexionBD"]
                    .ConnectionString;
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                using (SqlCommand comando = new SqlCommand(
                    "dbo.pa_InsertarUsuario", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add("@nombre_usuario",
                        SqlDbType.NVarChar, 100).Value = usuario.nombre_usuario;

                    comando.Parameters.Add("@email_usuario",
                        SqlDbType.NVarChar, 100).Value = usuario.email_usuario;

                    comando.Parameters.Add("@contrasenia_usuario",
                        SqlDbType.NVarChar, 100).Value = usuario.contrasenia_usuario;

                    comando.Parameters.Add("@fk_id_rol",
                        SqlDbType.TinyInt).Value = usuario.fk_id_rol;

                    conexion.Open();

                    comando.ExecuteNonQuery();

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public Usuario ValidarUsuario(string email, string password)
        {
            string cadena = ConfigurationManager
                .ConnectionStrings["ConexionBD"]
                .ConnectionString;

            using (SqlConnection conexion = new SqlConnection(cadena))
            using (SqlCommand comando =
                new SqlCommand("dbo.pa_ValidarUsuario", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.Add("@email_usuario", SqlDbType.NVarChar, 100)
                    .Value = email;

                comando.Parameters.Add("@contrasenia_usuario", SqlDbType.NVarChar, 100)
                    .Value = password;

                conexion.Open();

                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Usuario
                        {
                            id_usuario = Convert.ToInt32(reader["id_usuario"]),
                            nombre_usuario = reader["nombre_usuario"].ToString(),
                            email_usuario = reader["email_usuario"].ToString(),
                            fk_id_rol = Convert.ToByte(reader["fk_id_rol"]),
                            estado = Convert.ToBoolean(reader["estado"])
                        };
                    }
                }
            }

            return null;
        }
        public List<Usuario> obtenerUsuariosAsignables()
        {
            string cadena = ConfigurationManager
                .ConnectionStrings["ConexionBD"]
                .ConnectionString;

            List<Usuario> listaUsuarios = new List<Usuario>();
            using (SqlConnection conexion = new SqlConnection(cadena))
            {
                string query = @"SELECT id_usuario,
                        nombre_usuario
                    
                 FROM dbo.vw_UsuariosAsignables
                 ORDER BY nombre_usuario";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    conexion.Open();

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listaUsuarios.Add(new Usuario
                            {
                                id_usuario = Convert.ToInt32(reader["id_usuario"]),
                                nombre_usuario = reader["nombre_usuario"].ToString()
                            
                               
                            });
                        }
                    }
                }

                return listaUsuarios;
            }
        }
    }

}