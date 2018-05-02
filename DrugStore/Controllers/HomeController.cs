using DrugStore.Helpers;
using DrugStore.Model.Models;
using DrugStore.ViewModels;
using System;
using System.Net;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web.Security;

namespace DrugStore.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            if (checkSessions())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        #region Register
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Register(RegisterVM vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var encrypted_password = PasswordHelper.Encrypt(vm.Password);
                    User user = new User()
                    {
                        UserRoleID = _unitOfWork.UserRepository.getUserRoleID("User"),
                        FirstName = PasswordHelper.FirstCharToUpper(vm.FirstName),
                        MiddleName = PasswordHelper.FirstCharToUpper(vm.MiddleName),
                        LastName = PasswordHelper.FirstCharToUpper(vm.LastName),
                        Email = vm.Email,
                        Phone = vm.Phone,
                        IsActive = true,
                        UserName = vm.UserName,
                        Password = encrypted_password
                    };
                    checkPageError(user, null);
                    if (ModelState.IsValid == false)
                    {
                        return View(vm); //error
                    }
                    else
                    {
                        _unitOfWork.UserRepository.AddUser(user);
                        return RedirectToAction("Login", "Home"); //success
                    }
                }
                return View(vm); //error
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Login
        public ActionResult Login()
        {
            Log_Out();
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.UserName != null && vm.Password != null)
                {
                    var encrypted_password = PasswordHelper.Encrypt(vm.Password);
                    var user = _unitOfWork.UserRepository.GetUserByNameAndPassword(vm.UserName, encrypted_password);
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(user.UserName, true);
                        HttpContext.Cache.Insert("User", user, null, DateTime.Now.AddDays(1), Cache.NoSlidingExpiration);
                        var isValid = Membership.ValidateUser(user.Email, encrypted_password);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Login data is incorrect!");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Please fill in all Login data!");
                }
            }
            return View(vm);
        }

        public ActionResult Logout()
        {
            Log_Out();
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Forgot Password
        public ActionResult ForgotPassword()
        {
            return PartialView("ForgotPassword");
            //return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(LoginVM vm)
        {
            var result = Json(new { msg = "", success = true });
            if (ModelState.IsValid)
            {
                if (vm.UserName != null)
                {
                    var user = _unitOfWork.UserRepository.GetUserByUserName(vm.UserName);
                    if (user != null)
                    {
                        EmailService emailService = new EmailService();
                        string to = user.Email;
                        string subject = "Forgot Password";
                        string body = "Please find your password in this email...<br /><br />"
                            + PasswordHelper.Decrypt(user.Password) + "<br />";
                        try
                        {
                            AlertVM alertVM = emailService.SendEmail(user, to, body, subject);
                            if (alertVM.Success)
                            {
                                ModelState.AddModelError("", alertVM.Message);
                                result = Json(new { msg = alertVM.Message, success = true });
                            }
                            else
                            {
                                ModelState.AddModelError("", alertVM.Message);
                                result = Json(new { msg = alertVM.Message, success = false });
                            }
                        }
                        catch (Exception ex)
                        {
                            ModelState.AddModelError("", ex.Message);
                            result = Json(new { msg = ex.Message, success = false });
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "User Name cannot be found!");
                        result = Json(new { msg = "User Name cannot be found!", success = false });
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Please fill in User Name!");
                    result = Json(new { msg = "Please fill in User Name!", success = false });
                }
            }
            return result;
        }
        #endregion

        #region Profile
        [CustomAuthorize(Roles = CustomRoles.Admin_AdminLite_User)]
        public ActionResult EditProfile(int? id)
        {
            if (Request.IsAuthenticated == false)
            {
                return RedirectToAction("Login", "Home");
            }
            if (id != null && id != 0)
            {
                var userRoles = _unitOfWork.UserRepository.GetAllRoles();
                var user = _unitOfWork.UserRepository.GetUserById(id.Value);
                ViewBag.UserRole = _unitOfWork.UserRepository.getUserRole(user.UserRoleID);
                var userVM = new UserVM()
                {
                    ID = user.ID,
                    UserName = user.UserName,
                    SelectedUserRoleID = user.UserRoleID,
                    UserRoles = new SelectList(userRoles, "UserRoleID", "UserRoleName", 1),
                    FirstName = user.FirstName,
                    MiddleName = user.MiddleName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Phone = user.Phone,
                    Password = PasswordHelper.Decrypt(user.Password),
                    ConfirmPassword = PasswordHelper.Decrypt(user.Password),
                    IsActive = user.IsActive,
                    CreatedBy = user.CreatedBy,
                    CreatedDate = user.CreatedDate,
                    UpdatedBy = user.UpdatedBy,
                    UpdatedDate = user.UpdatedDate
                };
                return View(userVM);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost, ValidateAntiForgeryToken, CustomAuthorize(Roles = CustomRoles.Admin_AdminLite_User)]
        public ActionResult EditProfile(UserVM vm)
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
                        _unitOfWork.UserRepository.UpdateUser(user);
                        return RedirectToAction("Logout", "Home"); //success
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

        [HttpPost]
        [ActionName("SetUser")]
        public ActionResult setUser()
        {
            return Json("text", JsonRequestBehavior.AllowGet);
        }
    }
}