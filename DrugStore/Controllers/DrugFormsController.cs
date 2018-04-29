using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DrugStore.Model;
using DrugStore.Model.Models;
using DrugStore.DAL;
using System.Web.Security;
using PagedList;
using DrugStore.ViewModels;
using DrugStore.Helpers;

namespace DrugStore.Controllers
{
    [CustomAuthorize]
    public class DrugFormsController : BaseController
    {
        // GET: DrugCategories
        [CustomAuthorize(Roles = CustomRoles.AdminOrUser)]
        public ActionResult Index(string search, int? pageNumber, string sort)
        {
            search = search == null ? "" : search.ToLower();
            sort = sort == null ? "" : sort;
            if (checkSessions())
            {
                var drugForms = GetAllDrugForms();
                ViewBag.SortByName = string.IsNullOrEmpty(sort) ? "descending name" : "";
                if (!string.IsNullOrEmpty(search))
                {
                    drugForms = drugForms
                        .Where(x => x.Name.ToLower().Contains(search) || search == null)
                        .ToList().ToPagedList(pageNumber ?? 1, 20);
                }

                switch (sort)
                {
                    case "descending name":
                        drugForms = drugForms.OrderByDescending(x => x.Name);
                        break;
                    default:
                        drugForms = drugForms.OrderBy(x => x.Name);
                        break;
                }
                return View(drugForms.ToPagedList(pageNumber ?? 1, 20));
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // GET: DrugCategories/Details/5
        [CustomAuthorize(Roles = CustomRoles.Admin)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DrugFormVM VM = GetDrugFormVM_ById(id.Value);
            if (VM == null)
            {
                return HttpNotFound();
            }
            return View(VM);
        }

        // GET: DrugCategories/Create
        [CustomAuthorize(Roles = CustomRoles.Admin)]
        public ActionResult Create()
        {
            var drugFormVM = new DrugFormVM();
            return View(drugFormVM);
        }

        // POST: DrugCategories/Create
        [HttpPost, ValidateAntiForgeryToken, CustomAuthorize(Roles = CustomRoles.Admin)]
        public ActionResult Create(DrugFormVM VM)
        {
            if (ModelState.IsValid)
            {
                CreateDrugForm(VM);
                return RedirectToAction("Index");
            }
            return View(VM);
        }

        // GET: DrugCategories/Edit/5
        [CustomAuthorize(Roles = CustomRoles.Admin)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DrugFormVM drugFormVM = GetDrugFormVM_ById(id.Value);
            if (drugFormVM == null)
            {
                return HttpNotFound();
            }
            return View(drugFormVM);
        }

        // POST: DrugCategories/Edit/5
        [HttpPost, ValidateAntiForgeryToken, CustomAuthorize(Roles = CustomRoles.Admin)]
        public ActionResult Edit(DrugFormVM VM)
        {
            if (ModelState.IsValid)
            {
                UpdateDrugFormVM(VM);
                return RedirectToAction("Index");
            }
            return View(VM);
        }

        // GET: DrugCategories/Delete/5
        [CustomAuthorize(Roles = CustomRoles.Admin)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DrugFormVM VM = GetDrugFormVM_ById(id.Value);
            if (VM == null)
            {
                return HttpNotFound();
            }
            return View(VM);
        }

        // POST: DrugCategories/Delete/5
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken, CustomAuthorize(Roles = CustomRoles.Admin)]
        public ActionResult DeleteConfirmed(int id)
        {
            DeleteDrugForm(id);
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
