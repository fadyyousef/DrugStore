using DrugStore.Helpers;
using System.Net;
using System.Web.Mvc;

namespace DrugStore.Controllers
{
    [CustomAuthorize]
    public class UserRolesController : BaseController
    {
        // GET: Users
        [CustomAuthorize(Roles = CustomRoles.Admin_AdminLite)]
        public ActionResult Index()
        {
            if (checkSessions())
            {
                var userRoleVMs = GetAllUserRoleVMs();
                return View(userRoleVMs);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        // GET: Users/Details/5
        [CustomAuthorize(Roles = CustomRoles.Admin_AdminLite)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userRole = _unitOfWork.UserRepository.getUserRoleByID(id.Value);
            if (userRole == null)
            {
                return HttpNotFound();
            }
            var userRoleVM = CreateUserRoleViewModel(userRole);
            return View(userRoleVM);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
