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
    public class CatalogController : Controller
    {
        // GET: Catalog
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetRoleList()
        {

            List<Role> roleList = RoleData.getInstance().GetRoles();

            return Json(new
            {
                status = 200,
                list = roleList
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCategoriaList()
        {

            List<Categoria> categoriaList = CategoriaData.getInstance().GetCategorias();

            return Json(new
            {
                status = 200,
                list = categoriaList
            }, JsonRequestBehavior.AllowGet);
        }
    }
}