using DrugStore.DAL;
using DrugStore.Model.Models;
using DrugStore.ViewModels;
using System;
using System.Net;
using System.Net.Mail;

namespace DrugStore.Helpers
{
    public class EmailService
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();

        public string GetAdminEmail()
        {
            string[] emails = _unitOfWork.UserRepository.GetAdminEmails();
            return emails[0];
        }

        public AlertVM SendEmail(User user, string emailTo, string mailbody, string subject)
        {
            string msg = string.Empty;
            var alertVM = new AlertVM();

            //validate email, password cannot be reset within 24 hours
            bool emailPassed = SearchEmailLog(user.FullName, emailTo, mailbody, subject);

            if (emailPassed)
            {
                MailAddress from = new MailAddress(GetAdminEmail(), "DrugStore Pass Reset");
                MailAddress to = new MailAddress(emailTo);

                using (var mail = new MailMessage(from, to))
                {
                    mail.Subject = subject;
                    mail.Body = mailbody;
                    mail.IsBodyHtml = true;
                    mail.ReplyToList.Add(from);
                    mail.DeliveryNotificationOptions = DeliveryNotificationOptions.Delay |
                                                       DeliveryNotificationOptions.OnFailure |
                                                       DeliveryNotificationOptions.OnSuccess;
                    using (var client = new SmtpClient())
                    {
                        // We use gmail as our smtp client
                        client.Host = "smtp.gmail.com";
                        client.Port = 587;
                        client.EnableSsl = true;
                        client.UseDefaultCredentials = true;
                        client.Credentials = new NetworkCredential("fady0986@gmail.com", "Gianna20@");
                        client.Send(mail);
                        alertVM.Success = true;
                        alertVM.Message = "Successful...";

                        //save emaillog
                        AddEmailLog(user.FullName, emailTo, mailbody, subject);
                    }
                }
            }
            else
            {
                alertVM.Success = false;
                alertVM.Message = "Cannot reset password more than  once within 24 hours...";
            }
            return alertVM;
        }

        public int AddEmailLog(string fullName, string emailTo, string mailbody, string subject)
        {
            var emailLog = new EmailLog();
            emailLog.EmailTo = emailTo;
            emailLog.EmailSubject = subject;
            emailLog.EmailBody = mailbody;
            emailLog.EmailCC = emailTo;
            emailLog.EmailFrom = GetAdminEmail();
            emailLog.isHTML = true;
            emailLog.CreatedBy = fullName;
            emailLog.CreatedDate = DateTime.Now;
            emailLog.UpdatedBy = fullName;
            emailLog.UpdatedDate = DateTime.Now;

            var id = _unitOfWork.EmailLogRepository.AddEmailLog(emailLog);
            return id;
        }

        public bool SearchEmailLog(string fullName, string emailTo, string mailbody, string subject)
        {
            var emailLog = _unitOfWork.EmailLogRepository.SearchEmail(fullName, emailTo, mailbody, subject);
            if (emailLog != null)
            {
                var createdDate = emailLog.CreatedDate.AddDays(1);
                if (createdDate > DateTime.Now)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

    }
}