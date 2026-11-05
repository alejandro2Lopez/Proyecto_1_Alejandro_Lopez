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

        public static UsuarioData getInstance() {

            if (instance == null) { 
            
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
            catch {
                return false;
            }
        }
    }
}