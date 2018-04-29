using DrugStore.DAL;
using DrugStore.Helpers;
using DrugStore.Model.Models;
using DrugStore.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace DrugStore.Controllers
{
    public class BaseController : Controller
    {
        public UnitOfWork _unitOfWork;
        public bool isAdmin = false;

        public BaseController()
        {
            if (_unitOfWork == null)
            {
                _unitOfWork = new UnitOfWork();
            }
        }

        #region Login
        public bool checkSessions()
        {
            var hasPassed = false;
            if (User.Identity.IsAuthenticated)
            {
                var _user = Membership.GetUser(HttpContext.User.Identity.Name, false);
                var userId = (int)_user.ProviderUserKey;
                var user = _unitOfWork.UserRepository.GetUserById(userId);
                Session["FirstName"] = user.FirstName;
                var userRole = user.UserRole.UserRoleName.ToLower();
                if (userRole == "admin" || userRole == "user")
                {
                    if (userRole == "admin")
                    {
                        Roles.AddUserToRole(user.UserName, "Admin");
                    }
                    if (userRole == "user")
                    {
                        Roles.AddUserToRole(user.UserName, "User");
                    }
                    hasPassed = true;
                }
                else
                {
                    hasPassed = false;
                }
            }
            else
            {
                Log_Out();
                hasPassed = false;
            }
            return hasPassed;
        }

        public void Log_Out()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
        }
        #endregion

        #region DrugCompany
        public IEnumerable<DrugCompany> GetAllDrugCompanies()
        {
            return _unitOfWork.DrugCompanyRepository.GetAllDrugCompanies();
        }

        public void CreateDrugCompany(DrugCompanyVM VM)
        {
            var drugCompany = new DrugCompany()
            {
                CompanyID = VM.CompanyID,
                Name = VM.Name,
                CreatedBy = VM.CreatedBy,
                CreatedDate = VM.CreatedDate,
                UpdatedBy = VM.UpdatedBy,
                UpdatedDate = VM.UpdatedDate
            };
            _unitOfWork.DrugCompanyRepository.AddDrugCompany(drugCompany);
        }

        public DrugCompanyVM GetDrugCompanyVM_ById(int id)
        {
            DrugCompany drugCompany = _unitOfWork.DrugCompanyRepository.GetDrugCompanyById(id);
            var VM = new DrugCompanyVM()
            {
                CompanyID = drugCompany.CompanyID,
                Name = drugCompany.Name,
                CreatedBy = drugCompany.CreatedBy,
                CreatedDate = drugCompany.CreatedDate,
                UpdatedBy = drugCompany.UpdatedBy,
                UpdatedDate = drugCompany.UpdatedDate
            };
            return VM;
        }

        public void UpdateDrugCompanyVM(DrugCompanyVM VM)
        {
            DrugCompany drugCompany = _unitOfWork.DrugCompanyRepository.GetDrugCompanyById(VM.CompanyID);
            if (drugCompany != null)
            {
                drugCompany.Name = VM.Name;
                drugCompany.CreatedBy = VM.CreatedBy;
                drugCompany.CreatedDate = VM.CreatedDate;
                drugCompany.UpdatedBy = VM.UpdatedBy;
                drugCompany.UpdatedDate = VM.UpdatedDate;
                _unitOfWork.DrugCompanyRepository.UpdateDrugCompany(drugCompany);
            }
        }

        public void DeleteDrugCompany(int id)
        {
            _unitOfWork.DrugCompanyRepository.DeleteDrugCompany(id);
        }
        #endregion

        #region DrugCategory
        public IEnumerable<DrugCategory> GetAllDrugCategories()
        {
            return _unitOfWork.DrugCategoryRepository.GetAllDrugCategories();
        }

        public void CreateDrugCategory(DrugCategoryVM VM)
        {
            var drugCat = new DrugCategory()
            {
                CategoryID = VM.CategoryID,
                Name = VM.Name,
                CreatedBy = VM.CreatedBy,
                CreatedDate = VM.CreatedDate,
                UpdatedBy = VM.UpdatedBy,
                UpdatedDate = VM.UpdatedDate
            };
            _unitOfWork.DrugCategoryRepository.AddDrugCategory(drugCat);
        }

        public DrugCategoryVM GetDrugCategoryVM_ById(int id)
        {
            DrugCategory drugCat = _unitOfWork.DrugCategoryRepository.GetDrugCategoryById(id);
            var VM = new DrugCategoryVM()
            {
                CategoryID = drugCat.CategoryID,
                Name = drugCat.Name,
                CreatedBy = drugCat.CreatedBy,
                CreatedDate = drugCat.CreatedDate,
                UpdatedBy = drugCat.UpdatedBy,
                UpdatedDate = drugCat.UpdatedDate
            };
            return VM;
        }

        public void UpdateDrugCategoryVM(DrugCategoryVM VM)
        {
            DrugCategory drugCategory = _unitOfWork.DrugCategoryRepository.GetDrugCategoryById(VM.CategoryID);
            if (drugCategory != null)
            {
                drugCategory.Name = VM.Name;
                drugCategory.CreatedBy = VM.CreatedBy;
                drugCategory.CreatedDate = VM.CreatedDate;
                drugCategory.UpdatedBy = VM.UpdatedBy;
                drugCategory.UpdatedDate = VM.UpdatedDate;
                _unitOfWork.DrugCategoryRepository.UpdateDrugCategory(drugCategory);
            }
        }

        public void DeleteDrugCategory(int id)
        {
            _unitOfWork.DrugCategoryRepository.DeleteDrugCategory(id);
        }
        #endregion

        #region DrugForm
        public IEnumerable<DrugForm> GetAllDrugForms()
        {
            return _unitOfWork.DrugFormRepository.GetAllDrugForms();
        }

        public void CreateDrugForm(DrugFormVM VM)
        {
            var drugForm = new DrugForm()
            {
                FormID = VM.FormID,
                Name = VM.Name,
                CreatedBy = VM.CreatedBy,
                CreatedDate = VM.CreatedDate,
                UpdatedBy = VM.UpdatedBy,
                UpdatedDate = VM.UpdatedDate
            };
            _unitOfWork.DrugFormRepository.AddDrugForm(drugForm);
        }

        public DrugFormVM GetDrugFormVM_ById(int id)
        {
            DrugForm drugForm = _unitOfWork.DrugFormRepository.GetDrugFormById(id);
            var VM = new DrugFormVM()
            {
                FormID = drugForm.FormID,
                Name = drugForm.Name,
                CreatedBy = drugForm.CreatedBy,
                CreatedDate = drugForm.CreatedDate,
                UpdatedBy = drugForm.UpdatedBy,
                UpdatedDate = drugForm.UpdatedDate
            };
            return VM;
        }

        public void UpdateDrugFormVM(DrugFormVM VM)
        {
            DrugForm drugForm = _unitOfWork.DrugFormRepository.GetDrugFormById(VM.FormID);
            if (drugForm != null)
            {
                drugForm.Name = VM.Name;
                drugForm.UpdatedBy = VM.UpdatedBy;
                drugForm.UpdatedDate = VM.UpdatedDate;
                drugForm.CreatedBy = VM.CreatedBy;
                drugForm.CreatedDate = VM.CreatedDate;
                _unitOfWork.DrugFormRepository.UpdateDrugForm(drugForm);
            }
        }

        public void DeleteDrugForm(int id)
        {
            _unitOfWork.DrugFormRepository.DeleteDrugForm(id);
        }
        #endregion

        #region Drug
        public IEnumerable<Drug> GetAllDrugs()
        {
            var drugs = _unitOfWork.DrugRepository.GetAllDrugs().Take(1000);
            return drugs != null ? drugs : new List<Drug>();
        }

        public IPagedList<IGrouping<string, Drug>> Drugs_PagedList(List<Drug> drugs, int? pageNumber)
        {
            return drugs.OrderBy(a => a.DrugCategory.Name)
                            .GroupBy(a => a.DrugCategory.Name)
                            .ToPagedList(pageNumber ?? 1, 3);
        }

        public DrugVM GetDrugVM_ById(int id)
        {
            Drug drug = id == 0 ? new Drug() : _unitOfWork.DrugRepository.GetDrugById(id);
            var VM = new DrugVM()
            {
                ID = drug.ID,
                CompanyID = drug.CompanyID,
                CategoryID = drug.CategoryID,
                FormID = drug.FormID,
                Name = drug.Name,
                MarketName = drug.MarketName,
                ActiveIngredients = drug.ActiveIngredients,
                DoseAmount = drug.DoseAmount,
                Price = drug.Price,
                AvailableAmount = drug.AvailableAmount,
                ProductionDate = drug.ProductionDate,
                ExpiryDate = drug.ExpiryDate,
                CreatedBy = drug.CreatedBy,
                CreatedDate = drug.CreatedDate,
                UpdatedBy = drug.UpdatedBy,
                UpdatedDate = drug.UpdatedDate
            };
            return VM;
        }

        public void AddDrug(DrugVM drugVM)
        {
            var _drug = new Drug();
            _drug.CompanyID = drugVM.CompanyID;
            _drug.CategoryID = drugVM.CategoryID;
            _drug.FormID = drugVM.FormID;
            _drug.Name = drugVM.Name;
            _drug.MarketName = drugVM.MarketName;
            _drug.ActiveIngredients = drugVM.ActiveIngredients;
            _drug.DoseAmount = drugVM.DoseAmount;
            _drug.Price = drugVM.Price;
            _drug.AvailableAmount = drugVM.AvailableAmount;
            _drug.ProductionDate = drugVM.ProductionDate;
            _drug.ExpiryDate = drugVM.ExpiryDate;
            _drug.UpdatedBy = HttpContext.User.Identity.Name;
            _drug.UpdatedDate = DateTime.Now;
            _drug.CreatedBy = HttpContext.User.Identity.Name;
            _drug.CreatedDate = DateTime.Now;
            _unitOfWork.DrugRepository.AddDrug(_drug);
        }

        public Drug GetDrug(int id)
        {
            return _unitOfWork.DrugRepository.GetDrugById(id);
        }

        public void UpdateDrug(DrugVM drugVM)
        {
            var drug = GetDrug(drugVM.ID);
            if (drug != null)
            {
                drug.CompanyID = drugVM.CompanyID;
                drug.CategoryID = drugVM.CategoryID;
                drug.FormID = drugVM.FormID;
                drug.Name = drugVM.Name;
                drug.MarketName = drugVM.MarketName;
                drug.ActiveIngredients = drugVM.ActiveIngredients;
                drug.DoseAmount = drugVM.DoseAmount;
                drug.Price = drugVM.Price;
                drug.AvailableAmount = drugVM.AvailableAmount;
                drug.ProductionDate = drugVM.ProductionDate;
                drug.ExpiryDate = drugVM.ExpiryDate;
                _unitOfWork.DrugRepository.UpdateDrug(drug);
            }
        }

        public DrugVM CreateDrugVM(Drug drug)
        {
            var drugVM = new DrugVM();
            drugVM.ID = drug.ID;
            drugVM.CompanyID = drug.CompanyID;
            drugVM.CategoryID = drug.CategoryID;
            drugVM.FormID = drug.FormID;
            drugVM.Name = drug.Name;
            drugVM.MarketName = drug.MarketName;
            drugVM.ActiveIngredients = drug.ActiveIngredients;
            drugVM.DoseAmount = drug.DoseAmount;
            drugVM.Price = drug.Price;
            drugVM.AvailableAmount = drug.AvailableAmount;
            drugVM.ProductionDate = drug.ProductionDate;
            drugVM.ExpiryDate = drug.ExpiryDate;
            drugVM.CreatedDate = drug.CreatedDate;
            drugVM.CreatedBy = drug.CreatedBy;
            drugVM.UpdatedDate = drug.UpdatedDate;
            drugVM.UpdatedBy = drug.UpdatedBy;
            return drugVM;
        }

        public void DeleteDrug(int id)
        {
            _unitOfWork.DrugRepository.DeleteDrug(id);
        }

        public DrugVM SetLists(DrugVM drugVM)
        {
            drugVM.DrugForms =
                new SelectList(_unitOfWork.DrugFormRepository.GetAllDrugForms(), "FormID", "Name", 0);

            drugVM.DrugCategories =
                new SelectList(_unitOfWork.DrugCategoryRepository.GetAllDrugCategories(), "CategoryID", "Name", 0);

            drugVM.DrugCompanies =
                new SelectList(_unitOfWork.DrugCompanyRepository.GetAllDrugCompanies(), "CompanyID", "Name", 0);

            return drugVM;
        }
        #endregion

        #region User
        public IEnumerable<User> GetAllUsers()
        {
            var users = _unitOfWork.UserRepository.GetAllUsers().Take(1000);
            return users != null ? users : new List<User>();
        }

        public void CreateUser(UserVM VM)
        {
            var User = new User()
            {
                ID = VM.ID,
                UserRoleID = VM.SelectedUserRoleID,
                FirstName = VM.FirstName,
                MiddleName = VM.MiddleName,
                Email = VM.Email,
                Phone = VM.Phone,
                UserName = VM.UserName,
                Password = VM.Password,
                IsActive = VM.IsActive,
                CreatedBy = VM.CreatedBy,
                CreatedDate = VM.CreatedDate,
                UpdatedBy = VM.UpdatedBy,
                UpdatedDate = VM.UpdatedDate
            };
            _unitOfWork.UserRepository.AddUser(User);
        }

        public UserVM GetUserVM_ById(int id)
        {
            User User = _unitOfWork.UserRepository.GetUserById(id);
            var VM = new UserVM()
            {
                ID = User.ID,
                SelectedUserRoleID = User.UserRoleID,
                FirstName = User.FirstName,
                MiddleName = User.MiddleName,
                Email = User.Email,
                Phone = User.Phone,
                UserName = User.UserName,
                Password = User.Password,
                IsActive = User.IsActive,
                CreatedBy = User.CreatedBy,
                CreatedDate = User.CreatedDate,
                UpdatedBy = User.UpdatedBy,
                UpdatedDate = User.UpdatedDate
            };
            return VM;
        }

        public void UpdateUserVM(UserVM VM)
        {
            User user = _unitOfWork.UserRepository.GetUserById(VM.ID);
            if (user != null)
            {
                user.FirstName = VM.FirstName;
                user.UserRoleID = VM.SelectedUserRoleID;
                user.FirstName = VM.FirstName;
                user.MiddleName = VM.MiddleName;
                user.Email = VM.Email;
                user.Phone = VM.Phone;
                user.UserName = VM.UserName;
                user.Password = VM.Password;
                user.IsActive = VM.IsActive;
                user.CreatedBy = VM.CreatedBy;
                user.CreatedDate = VM.CreatedDate;
                user.UpdatedBy = VM.UpdatedBy;
                user.UpdatedDate = VM.UpdatedDate;
                _unitOfWork.UserRepository.UpdateUser(user);
            }
        }

        public void DeleteUser(int id)
        {
            _unitOfWork.UserRepository.DeleteUser(id);
        }

        public UserVM CreateViewModel(User user)
        {
            var userRoles = _unitOfWork.UserRepository.GetAllRoles();
            if (user.ID != default(int))
            {
                ViewBag.UserRole = _unitOfWork.UserRepository.getUserRole(user.UserRoleID);
                var userVM = new UserVM()
                {
                    ID = user.ID,
                    UserName = user.UserName,
                    SelectedUserRoleID = user.UserRoleID,
                    UserRoles = new SelectList(userRoles, "UserRoleID", "UserRoleName", 1),
                    UserRole = user.UserRole.UserRoleName,
                    FirstName = user.FirstName,
                    MiddleName = user.MiddleName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Phone = user.Phone,
                    Password = PasswordHelper.Decrypt(user.Password),
                    ConfirmPassword = PasswordHelper.Decrypt(user.Password),
                    IsActive = user.IsActive,
                    CreatedBy = user.CreatedBy == null ? GetLoggenInUser() : user.CreatedBy,
                    CreatedDate = user.CreatedDate,
                    UpdatedBy = user.UpdatedBy,
                    UpdatedDate = user.UpdatedDate
                };
                return userVM;
            }
            else
            {
                var userVM = new UserVM()
                {
                    UserName = user.UserName,
                    SelectedUserRoleID = user.UserRoleID,
                    UserRoles = new SelectList(userRoles, "UserRoleID", "UserRoleName", 1),
                    UserRole = "User",
                    FirstName = user.FirstName,
                    MiddleName = user.MiddleName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Phone = user.Phone,
                    Password = null,
                    ConfirmPassword = null,
                    IsActive = user.IsActive,
                    CreatedBy = GetLoggenInUser(),
                    CreatedDate = user.CreatedDate,
                    UpdatedBy = GetLoggenInUser(),
                    UpdatedDate = user.UpdatedDate
                };
                return userVM;
            }
        }

        public void checkPageError(User user, int? id)
        {
            var duplicateUser = _unitOfWork.UserRepository.CheckDuplicateUser(user, id);
            if (!string.IsNullOrEmpty(duplicateUser))
            {
                var pageErrors = duplicateUser.Split(',')
                    .Where(a => a.ToString() != "")
                    .Distinct();
                foreach (var pageError in pageErrors.Distinct())
                {
                    if (pageError.Contains("Phone"))
                    {
                        ModelState.AddModelError("Phone", pageError);
                    }
                    if (pageError.Contains("Email"))
                    {
                        ModelState.AddModelError("Email", pageError);
                    }
                    if (pageError.Contains("UserName"))
                    {
                        ModelState.AddModelError("UserName", pageError);
                    }
                }
            }
        }

        public string GetLoggenInUser()
        {
            var _user = Membership.GetUser(HttpContext.User.Identity.Name, false);
            var userId = (int)_user.ProviderUserKey;
            var user = _unitOfWork.UserRepository.GetUserById(userId);
            return user.FullName;
        }

        #endregion

        #region Email

        public IEnumerable<EmailLog> GetAllEmails()
        {
            var emails = _unitOfWork.EmailLogRepository.GetAllEmails().Take(1000);
            return emails != null ? emails : new List<EmailLog>();
        }

        public IPagedList<IGrouping<string, EmailLog>> Emails_PagedList(List<EmailLog> emails, int? pageNumber)
        {
            return emails.OrderBy(a => a.EmailTo)
                .GroupBy(a => a.EmailTo)
                .ToPagedList(pageNumber ?? 1, 3);
        }

        #endregion
    }
}