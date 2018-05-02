using DrugStore.Helpers;
using DrugStore.Model.Models;
using DrugStore.ViewModels;
using PagedList;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DrugStore.Controllers
{
    [CustomAuthorize]
    public class UsersController : BaseController
    {
        // GET: Users
        [CustomAuthorize(Roles = CustomRoles.Admin_AdminLite)]
        public ActionResult Index(string option, string search, int? pageNumber, string sort)
        {
            option = option == null ? "" : option;
            search = search == null ? "" : search.ToLower();
            sort = sort == null ? "" : sort;
            if (checkSessions())
            {
                var users = GetAllUsers();
                ViewBag.SortByFullName = string.IsNullOrEmpty(sort) ? "descending FullName" : "";
                ViewBag.SortByEmail = sort == "Email" ? "descending Email" : "Email";
                ViewBag.SortByPhone = sort == "Phone" ? "descending Phone" : "Phone";
                var userGroups = Users_PagedList(users.ToList(), pageNumber);

                //validation
                if (string.IsNullOrEmpty(search) == false && string.IsNullOrEmpty(option) == true)
                {
                    ModelState.AddModelError("Options", "Please Select Option from the drop down");
                    return View(userGroups);
                }
                switch (option)
                {
                    case "FullName":
                        users = users.Where(x => x.FullName.ToLower().Contains(search) || search == null)
                            .ToList().ToPagedList(pageNumber ?? 1, 3);
                        break;
                    case "Email":
                        users = users.Where(x => x.Email.ToLower().Contains(search) || search == null)
                            .ToList().ToPagedList(pageNumber ?? 1, 3);
                        break;
                    case "Phone":
                        users = users.Where(x => x.Phone.ToLower().Contains(search) || search == null)
                            .ToList().ToPagedList(pageNumber ?? 1, 3);
                        break;
                    default:
                        users = users.Where(x => x.FullName.ToLower().StartsWith(search) || search == null)
                            .ToList().ToPagedList(pageNumber ?? 1, 3);
                        break;
                }
                switch (sort)
                {
                    case "FullName":
                        users = users.OrderBy(x => x.FullName);
                        break;
                    case "Email":
                        users = users.OrderBy(x => x.Email);
                        break;
                    case "Phone":
                        users = users.OrderBy(x => x.Phone);
                        break;
                    case "descending FullName":
                        users = users.OrderByDescending(x => x.FullName);
                        break;
                    case "descending Email":
                        users = users.OrderByDescending(x => x.Email);
                        break;
                    case "descending Phone":
                        users = users.OrderByDescending(x => x.Phone);
                        break;
                    default:
                        users = users.OrderBy(x => x.FullName);
                        break;
                }
                userGroups = Users_PagedList(users.ToList(), pageNumber);
                return View(userGroups);
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
