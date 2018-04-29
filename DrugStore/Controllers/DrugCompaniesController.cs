using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;
using DrugStore.ViewModels;
using DrugStore.Helpers;

namespace DrugStore.Controllers
{
    [CustomAuthorize]
    public class DrugCompaniesController : BaseController
    {
        // GET: DrugCompanies
        [CustomAuthorize(Roles = CustomRoles.AdminOrUser)]
        public ActionResult Index(string search, int? pageNumber, string sort)
        {
            search = search == null ? "" : search.ToLower();
            sort = sort == null ? "" : sort;
            if (checkSessions())
            {
                var drugCompanies = GetAllDrugCompanies();
                ViewBag.SortByName = string.IsNullOrEmpty(sort) ? "descending name" : "";
                if (!string.IsNullOrEmpty(search))
                {
                    drugCompanies = drugCompanies
                        .Where(x => x.Name.ToLower().Contains(search) || search == null)
                        .ToList().ToPagedList(pageNumber ?? 1, 3);
                }
                switch (sort)
                {
                    case "descending name":
                        drugCompanies = drugCompanies.OrderByDescending(x => x.Name);
                        break;
                    default:
                        drugCompanies = drugCompanies.OrderBy(x => x.Name);
                        break;
                }
                return View(drugCompanies.ToPagedList(pageNumber ?? 1, 3));
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // GET: DrugCompanies/Details/5
        [CustomAuthorize(Roles = CustomRoles.Admin)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var VM = GetDrugCompanyVM_ById(id.Value);
            if (VM == null)
            {
                return HttpNotFound();
            }
            return View(VM);
        }

        // GET: DrugCompanies/Create
        [CustomAuthorize(Roles = CustomRoles.Admin)]
        public ActionResult Create()
        {
            var VM = new DrugCompanyVM();
            return View(VM);
        }

        // POST: DrugCompanies/Create
        [HttpPost, ValidateAntiForgeryToken, CustomAuthorize(Roles = CustomRoles.Admin)]
        public ActionResult Create(DrugCompanyVM VM)
        {
            if (ModelState.IsValid)
            {
                CreateDrugCompany(VM);
                return RedirectToAction("Index");
            }
            return View(VM);
        }

        // GET: DrugCompanies/Edit/5
        [CustomAuthorize(Roles = CustomRoles.Admin)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DrugCompanyVM drugCompanyVM = GetDrugCompanyVM_ById(id.Value);
            if (drugCompanyVM == null)
            {
                return HttpNotFound();
            }
            return View(drugCompanyVM);
        }

        // POST: DrugCompanies/Edit/5
        [HttpPost, ValidateAntiForgeryToken, CustomAuthorize(Roles = CustomRoles.Admin)]
        public ActionResult Edit(DrugCompanyVM drugCompanyVM)
        {
            if (ModelState.IsValid)
            {
                UpdateDrugCompanyVM(drugCompanyVM);
                return RedirectToAction("Index");
            }
            return View(drugCompanyVM);
        }

        // GET: DrugCompanies/Delete/5
        [CustomAuthorize(Roles = CustomRoles.Admin)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DrugCompanyVM VM = GetDrugCompanyVM_ById(id.Value);
            if (VM == null)
            {
                return HttpNotFound();
            }
            return View(VM);
        }

        // POST: DrugCompanies/Delete/5
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken, CustomAuthorize(Roles = CustomRoles.Admin)]
        public ActionResult DeleteConfirmed(int id)
        {
            DeleteDrugCompany(id);
            return RedirectToAction("Index");
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
