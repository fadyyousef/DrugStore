using DrugStore.Helpers;
using PagedList;
using System.Linq;
using System.Web.Mvc;

namespace DrugStore.Controllers
{
    [CustomAuthorize]
    public class EmailsController : BaseController
    {
        // GET: Users
        [CustomAuthorize(Roles = CustomRoles.Admin)]
        public ActionResult Index(string option, string search, int? pageNumber, string sort)
        {
            option = option == null ? "" : option;
            search = search == null ? "" : search.ToLower();
            sort = sort == null ? "" : sort;
            if (checkSessions())
            {
                var emails = GetAllEmails();
                ViewBag.SortByEmailTo = string.IsNullOrEmpty(sort) ? "descending EmailTo" : "";
                ViewBag.SortBySubject = sort == "Subject" ? "descending Subject" : "Subject";
                var emailGroups = Emails_PagedList(emails.ToList(), pageNumber);

                //validation
                if (string.IsNullOrEmpty(search) == false && string.IsNullOrEmpty(option) == true)
                {
                    ModelState.AddModelError("Options", "Please Select Option from the drop down");
                    return View(emailGroups);
                }
                switch (option)
                {
                    case "EmailTo":
                        emails = emails.Where(x => x.EmailTo.ToLower().Contains(search) || search == null)
                            .ToList().ToPagedList(pageNumber ?? 1, 3);
                        break;
                    case "Subject":
                        emails = emails.Where(x => x.EmailSubject.ToLower().Contains(search) || search == null)
                            .ToList().ToPagedList(pageNumber ?? 1, 3);
                        break;
                    default:
                        emails = emails.Where(x => x.EmailTo.ToLower().StartsWith(search) || search == null)
                            .ToList().ToPagedList(pageNumber ?? 1, 3);
                        break;
                }
                switch (sort)
                {
                    case "descending EmailTo":
                        emails = emails.OrderByDescending(x => x.EmailTo);
                        break;
                    case "descending Subject":
                        emails = emails.OrderByDescending(x => x.EmailSubject);
                        break;
                    case "Subject":
                        emails = emails.OrderBy(x => x.EmailSubject);
                        break;
                    default:
                        emails = emails.OrderBy(x => x.EmailTo);
                        break;
                }

                emailGroups = Emails_PagedList(emails.ToList(), pageNumber);
                return View(emailGroups);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult ViewEmailBody(string id)
        {
            var intID = int.Parse(id);
            var emailLog = _unitOfWork.EmailLogRepository.GetEmailById(intID);
            return PartialView("_PartialEmailBody", emailLog);
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
