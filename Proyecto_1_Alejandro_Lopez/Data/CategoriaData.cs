using Proyecto_1_Alejandro_Lopez.Models.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Proyecto_1_Alejandro_Lopez.Data
{
    public class CategoriaData
    {
       
        private static CategoriaData instance;

        public CategoriaData() { 
        
        }
        public static CategoriaData getInstance() {

            if (instance==null) { 
            instance = new CategoriaData();
            }
           return instance;
        }


        public List<Categoria> GetCategorias()
        {
            List<Categoria> Categorias = new List<Categoria>();

            string cadena = ConfigurationManager
                .ConnectionStrings["ConexionBD"]
                .ConnectionString;

            using (SqlConnection conexion = new SqlConnection(cadena))
            {
                string query = "SELECT  id_categoria, descripcion FROM dbo.vw_Categorias";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    conexion.Open();

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Categoria Categoria = new Categoria
                            {
                                id_categoria = Convert.ToInt32(reader["id_categoria"]),
                                descripcion = reader["descripcion"].ToString()
                            };

                            Categorias.Add(Categoria);
                        }
                    }
                }
            }

            return Categorias;
        }
    }
}