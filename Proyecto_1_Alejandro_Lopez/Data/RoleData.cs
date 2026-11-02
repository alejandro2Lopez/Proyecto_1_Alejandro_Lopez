using Proyecto_1_Alejandro_Lopez.Models.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Proyecto_1_Alejandro_Lopez.Data
{
    public class RoleData
    {
       
        private static RoleData instance;

        public RoleData() { 
        
        }
        public static RoleData getInstance() {

            if (instance==null) { 
            instance = new RoleData();
            }
           return instance;
        }


        public List<Role> GetRoles()
        {
            List<Role> Roles = new List<Role>();

            string cadena = ConfigurationManager
                .ConnectionStrings["ConexionBD"]
                .ConnectionString;

            using (SqlConnection conexion = new SqlConnection(cadena))
            {
                string query = "SELECT  id_rol, descripcion FROM dbo.vw_Roles";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    conexion.Open();

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Role Role = new Role
                            {
                                Id_rol = Convert.ToInt32(reader["id_rol"]),
                                Descripcion= reader["descripcion"].ToString()
                            };

                            Roles.Add(Role);
                        }
                    }
                }
            }

            return Roles;
        }
    }
}