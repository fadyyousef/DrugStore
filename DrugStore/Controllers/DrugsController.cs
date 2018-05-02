using DrugStore.Helpers;
using DrugStore.ViewModels;
using PagedList;
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DrugStore.Controllers
{
    [Authorize]
    public class DrugsController : BaseController
    {
        // GET: Drugs
        [CustomAuthorize(Roles = CustomRoles.Admin_AdminLite)]
        public ActionResult Index(string option, string search, int? pageNumber, string sort)
        {
            option = option == null ? "" : option;
            search = search == null ? "" : search.ToLower();
            sort = sort == null ? "" : sort;
            if (checkSessions())
            {
                var drugs = GetAllDrugs();
                ViewBag.SortByName = string.IsNullOrEmpty(sort) ? "descending name" : "";
                ViewBag.SortByMarketName = sort == "MarketName" ? "descending MarketName" : "MarketName";
                var drugCats = Drugs_PagedList(drugs.ToList(), pageNumber);

                //validation
                if (string.IsNullOrEmpty(search) == false && string.IsNullOrEmpty(option) == true)
                {
                    ModelState.AddModelError("Options", "Please Select Option from the drop down");
                    return View(drugCats);
                }
                switch (option)
                {
                    case "Name":
                        drugs = drugs.Where(x => x.Name.ToLower().Contains(search) || search == null)
                            .ToList().ToPagedList(pageNumber ?? 1, 3);
                        break;
                    case "MarketName":
                        drugs = drugs.Where(x => x.MarketName.ToLower().Contains(search) || search == null)
                            .ToList().ToPagedList(pageNumber ?? 1, 3);
                        break;
                    default:
                        drugs = drugs.Where(x => x.Name.ToLower().StartsWith(search) || search == null)
                            .ToList().ToPagedList(pageNumber ?? 1, 3);
                        break;
                }
                switch (sort)
                {
                    case "descending name":
                        drugs = drugs.OrderByDescending(x => x.Name);
                        break;
                    case "descending MarketName":
                        drugs = drugs.OrderByDescending(x => x.MarketName);
                        break;
                    case "MarketName":
                        drugs = drugs.OrderBy(x => x.MarketName);
                        break;
                    default:
                        drugs = drugs.OrderBy(x => x.Name);
                        break;
                }

                drugCats = Drugs_PagedList(drugs.ToList(), pageNumber);
                return View(drugCats);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // GET: Drugs/Details/5
        [CustomAuthorize(Roles = CustomRoles.Admin_AdminLite_User)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var VM = GetDrugVM_ById(id.Value);
            if (VM == null)
            {
                return HttpNotFound();
            }
            SetLists(VM);
            return View(VM);
        }

        // GET: Drugs/Create
        [CustomAuthorize(Roles = CustomRoles.Admin_AdminLite)]
        public ActionResult Create()
        {
            var drugVM = new DrugVM();
            drugVM.ProductionDate = DateTime.Now;
            drugVM.ExpiryDate = DateTime.Now;
            SetLists(drugVM);
            return View(drugVM);
        }

        // POST: Drugs/Create
        [HttpPost, ValidateAntiForgeryToken, CustomAuthorize(Roles = CustomRoles.Admin_AdminLite)]
        public ActionResult Create(DrugVM drugVM)
        {
            if (ModelState.IsValid)
            {
                AddDrug(drugVM);
                return RedirectToAction("Index", "Drugs");
            }
            drugVM.ProductionDate = DateTime.Now;
            drugVM.ExpiryDate = DateTime.Now;
            SetLists(drugVM);
            return View(drugVM);
        }

        // GET: Drugs/Edit/5
        [CustomAuthorize(Roles = CustomRoles.Admin_AdminLite)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var drug = GetDrug(id.Value);
            if (drug == null)
            {
                return HttpNotFound();
            }
            var VM = GetDrugVM_ById(id.Value);
            SetLists(VM);
            return View(VM);
        }

        // POST: Drugs/Edit/5
        [HttpPost, ValidateAntiForgeryToken, CustomAuthorize(Roles = CustomRoles.Admin_AdminLite)]
        public ActionResult Edit(DrugVM drugVM)
        {
            if (ModelState.IsValid)
            {
                var drug = GetDrug(drugVM.ID);
                if (drug == null)
                {
                    return HttpNotFound();
                }
                UpdateDrug(drugVM);
                return RedirectToAction("Index", "Drugs");
            }
            SetLists(drugVM);
            return View(drugVM);
        }

        // GET: Drugs/Delete/5
        [CustomAuthorize(Roles = CustomRoles.Admin_AdminLite)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var drug = GetDrug(id.Value);
            if (drug == null)
            {
                return HttpNotFound();
            }
            var drugVM = CreateDrugVM(drug);
            SetLists(drugVM);
            return View(drugVM);
        }

        // POST: Drugs/Delete/5
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken, CustomAuthorize(Roles = CustomRoles.Admin_AdminLite)]
        public ActionResult DeleteConfirmed(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var drug = GetDrug(id);
            if (drug == null)
            {
                return HttpNotFound();
            }
            DeleteDrug(id);
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
