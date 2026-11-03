using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_1_Alejandro_Lopez.Models.Entities
{
    public class Comentario
    {
        public int id_comentario { get; set; }
        public int id_ticket { get; set; }
        public int id_usuario { get; set; }
        public string texto { get; set; }
        public DateTime fecha { get; set; }
    }
}