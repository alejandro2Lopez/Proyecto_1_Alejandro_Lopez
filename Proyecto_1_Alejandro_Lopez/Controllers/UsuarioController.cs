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
        [HttpPost]
        public ActionResult validarUsuario(string email, string contrasenia)
        {
            Usuario usuario = UsuarioData.getInstance().ValidarUsuario(email, contrasenia);
            if (usuario!=null)
            {
                Session["UsuarioId"] = usuario.id_usuario;
                Session["NombreUsuario"] = usuario.nombre_usuario;
                Session["RolId"] = usuario.fk_id_rol;
                return Json(new
                {
                    status = 200,
                    message = "Bienvenido: " + usuario.nombre_usuario
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    status = 403,
                    message = "Acceso prohibido intente de nuevo."
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