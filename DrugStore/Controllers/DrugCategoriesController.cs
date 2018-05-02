using DrugStore.Helpers;
using DrugStore.ViewModels;
using PagedList;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DrugStore.Controllers
{
    [CustomAuthorize]
    public class DrugCategoriesController : BaseController
    {
        // GET: DrugCategories
        [CustomAuthorize(Roles = CustomRoles.Admin_AdminLite)]
        public ActionResult Index(string search, int? pageNumber, string sort)
        {
            search = search == null ? "" : search.ToLower();
            sort = sort == null ? "" : sort;
            if (checkSessions())
            {
                var drugCategories = _unitOfWork.DrugCategoryRepository.GetAllDrugCategories();
                ViewBag.SortByName = string.IsNullOrEmpty(sort) ? "descending name" : "";
                if (!string.IsNullOrEmpty(search))
                {
                    drugCategories = drugCategories
                        .Where(x => x.Name.ToLower().Contains(search) || search == null)
                        .ToList().ToPagedList(pageNumber ?? 1, 3);
                }

                switch (sort)
                {
                    case "descending name":
                        drugCategories = drugCategories.OrderByDescending(x => x.Name);
                        break;
                    default:
                        drugCategories = drugCategories.OrderBy(x => x.Name);
                        break;
                }
                return View(drugCategories.ToPagedList(pageNumber ?? 1, 3));
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // GET: DrugCategories/Details/5
        [CustomAuthorize(Roles = CustomRoles.Admin_AdminLite_User)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var VM = GetDrugCategoryVM_ById(id.Value);
            if (VM == null)
            {
                return HttpNotFound();
            }
            return View(VM);
        }

        // GET: DrugCategories/Create
        [CustomAuthorize(Roles = CustomRoles.Admin_AdminLite)]
        public ActionResult Create()
        {
            var drugCategoryVM = new DrugCategoryVM();
            return View(drugCategoryVM);
        }

        // POST: DrugCategories/Create
        [HttpPost, ValidateAntiForgeryToken, CustomAuthorize(Roles = CustomRoles.Admin_AdminLite)]
        public ActionResult Create(DrugCategoryVM drugCategoryVM)
        {
            if (ModelState.IsValid)
            {
                CreateDrugCategory(drugCategoryVM);
                return RedirectToAction("Index");
            }
            return View(drugCategoryVM);
        }

        // GET: DrugCategories/Edit/5
        [CustomAuthorize(Roles = CustomRoles.Admin_AdminLite)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DrugCategoryVM drugCategoryVM = GetDrugCategoryVM_ById(id.Value);
            if (drugCategoryVM == null)
            {
                return HttpNotFound();
            }
            return View(drugCategoryVM);
        }

        // POST: DrugCategories/Edit/5
        [HttpPost, ValidateAntiForgeryToken, CustomAuthorize(Roles = CustomRoles.Admin_AdminLite)]
        public ActionResult Edit(DrugCategoryVM drugCategoryVM)
        {
            if (ModelState.IsValid)
            {
                UpdateDrugCategoryVM(drugCategoryVM);
                return RedirectToAction("Index");
            }
            return View(drugCategoryVM);
        }

        // GET: DrugCategories/Delete/5
        [CustomAuthorize(Roles = CustomRoles.Admin_AdminLite)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DrugCategoryVM VM = GetDrugCategoryVM_ById(id.Value);
            if (VM == null)
            {
                return HttpNotFound();
            }
            return View(VM);
        }

        // POST: DrugCategories/Delete/5
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken, CustomAuthorize(Roles = CustomRoles.Admin_AdminLite)]
        public ActionResult DeleteConfirmed(int id)
        {
            DeleteDrugCategory(id);
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
