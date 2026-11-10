using Proyecto_1_Alejandro_Lopez.Data;
using Proyecto_1_Alejandro_Lopez.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_1_Alejandro_Lopez.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult InsertarUsuario(Usuario usuario)
        {
            bool result = UsuarioData.getInstance().InsertarUsuario(usuario);
            if (result)
            {
                return Json(new
                {
                    status = 200,
                    message = "Usuario insertado manera exitosa"
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    status = 404,
                    message = "Ha ocurrido un error al ingresar el usuario"
                }, JsonRequestBehavior.AllowGet);
            }

        }
        
        [HttpGet]
        public ActionResult ObtenerUsuariosAsignables()
        {
            List<Usuario> usuarios = UsuarioData.getInstance().obtenerUsuariosAsignables();
           
                return Json(new
                {
                    status = 200,
                    message = usuarios
                }, JsonRequestBehavior.AllowGet);
        }
    }
}