using DrugStore.Helpers;
using DrugStore.Model.Models;
using DrugStore.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DrugStore.Controllers
{
    [CustomAuthorize]
    public class UsersController : BaseController
    {
        // GET: Users
        [CustomAuthorize(Roles = CustomRoles.Admin)]
        public ActionResult Index()
        {
            if (checkSessions())
            {
                var users = GetAllUsers();
                var userVMList = new List<UserVM>();

                foreach (var user in users)
                {
                    var userVM = CreateViewModel(user);
                    userVMList.Add(userVM);
                }
                userVMList = userVMList.OrderBy(a => a.UserRole).ToList();
                return View(userVMList);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // GET: Users/Details/5
        [CustomAuthorize(Roles = CustomRoles.Admin)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = _unitOfWork.UserRepository.GetUserById(id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }
            var userVM = CreateViewModel(user);
            return View(userVM);
        }

        #region Create
        // GET: Users/Create
        [CustomAuthorize(Roles = CustomRoles.Admin)]
        public ActionResult Create()
        {
            var userRoles = _unitOfWork.UserRepository.GetAllRoles();
            var userVM = CreateViewModel(new User());
            return View(userVM);
        }

        // POST: Users/Create
        [HttpPost, ValidateAntiForgeryToken, CustomAuthorize(Roles = CustomRoles.Admin)]
        public ActionResult Create(UserVM vm)
        {
            if (ModelState.IsValid)
            {
                var user = new User();
                user.UserRoleID = vm.SelectedUserRoleID;
                user.UserName = vm.UserName;
                user.Password = PasswordHelper.Encrypt(vm.Password);
                user.Email = vm.Email;
                user.Phone = vm.Phone;
                user.FirstName = vm.FirstName;
                user.MiddleName = vm.MiddleName;
                user.LastName = vm.LastName;
                user.IsActive = vm.IsActive;
                _unitOfWork.UserRepository.AddUser(user);
                return RedirectToAction("index", "Users");
            }
            return View(vm);
        }
        #endregion

        #region Edit
        // GET: Users/Edit/5
        [CustomAuthorize(Roles = CustomRoles.Admin)]
        public ActionResult Edit(int? id)
        {
            if (Request.IsAuthenticated == false)
            {
                return RedirectToAction("Login", "Users");
            }
            if (id != null && id != 0)
            {
                var user = _unitOfWork.UserRepository.GetUserById(id.Value);
                if (user == null)
                {
                    return HttpNotFound();
                }
                var userVM = CreateViewModel(user);
                return View(userVM);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // POST: Users/Edit/5
        [HttpPost, ValidateAntiForgeryToken, CustomAuthorize(Roles = CustomRoles.Admin)]
        public ActionResult Edit(UserVM vm)
        {
            if (ModelState.IsValid)
            {
                var user = _unitOfWork.UserRepository.GetUserById(vm.ID);
                if (user != null)
                {
                    checkPageError(user, vm.ID);
                    if (ModelState.IsValid == false)
                    {
                        return View(vm); //error
                    }
                    else
                    {
                        user.UserRoleID = vm.SelectedUserRoleID;
                        user.UserName = vm.UserName;
                        user.Password = PasswordHelper.Encrypt(vm.Password);
                        user.Email = vm.Email;
                        user.Phone = vm.Phone;
                        user.FirstName = vm.FirstName;
                        user.MiddleName = vm.MiddleName;
                        user.LastName = vm.LastName;
                        user.IsActive = vm.IsActive;
                        user.CreatedBy = vm.CreatedBy;
                        _unitOfWork.UserRepository.UpdateUser(user);
                        return RedirectToAction("Index", "Users"); //success
                    }
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            return View(vm);
        }
        #endregion

        #region Delete
        // GET: Users/Delete/5
        [CustomAuthorize(Roles = CustomRoles.Admin)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = _unitOfWork.UserRepository.GetUserById(id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }
            var userVM = CreateViewModel(user);
            return View(userVM);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken, CustomAuthorize(Roles = CustomRoles.Admin)]
        public ActionResult DeleteConfirmed(int id)
        {
            if (id != 0)
            {
                var user = _unitOfWork.UserRepository.GetUserById(id);
                if (user != null)
                {
                    if (user.FullName == GetLoggenInUser()) // cannot delete loggen in user
                    {
                        ModelState.AddModelError("", "Can't delete user while logged in لا يمكن الغاء المستخدم الحالي");
                        var userVM = CreateViewModel(user);
                        return View(userVM);
                    }
                    _unitOfWork.UserRepository.DeleteUser(id);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View();
        }
        #endregion

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
