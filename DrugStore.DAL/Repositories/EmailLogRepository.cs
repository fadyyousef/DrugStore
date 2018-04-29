using DrugStore.Model;
using DrugStore.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DrugStore.DAL.Repositories
{
    public class EmailLogRepository
    {
        private readonly DrugContext _context;
        public EmailLogRepository()
        {
            if (_context == null)
            {
                _context = new DrugContext();
            }
        }
        public EmailLogRepository(DrugContext context)
        {
            if (_context == null)
            {
                _context = context;
            }
        }

        public int AddEmailLog(EmailLog entity)
        {
            int result = -1;
            if (entity != null)
            {
                _context.EmailLog.Add(entity);
                _context.SaveChanges();
                result = entity.EmailLogID;
            }
            return result;
        }

        public bool UpdateEmailLog(EmailLog EmailLogEntity)
        {
            if (EmailLogEntity != null)
            {
                _context.Entry(EmailLogEntity).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public void DeleteEmailLog(int Id)
        {
            EmailLog EmailLogEntity = _context.EmailLog.Find(Id);
            _context.EmailLog.Remove(EmailLogEntity);
            _context.SaveChanges();
        }

        public List<EmailLog> GetAllEmails()
        {
            return _context.EmailLog.ToList();
        }

        public EmailLog GetEmailById(int emailId)
        {
            return _context.EmailLog.Find(emailId);
        }

        public EmailLog SearchEmail(string fullName, string emailTo, string mailbody, string subject)
        {
            var emailLogs = _context.EmailLog.
                Where(a => (a.CreatedBy.Contains(fullName) || fullName.Contains(a.CreatedBy))
                && (a.EmailTo.Contains(emailTo) || emailTo.Contains(a.EmailTo))
                && (a.EmailBody.Contains(mailbody) || mailbody.Contains(a.EmailBody))
                && (a.EmailSubject.Contains(subject) || subject.Contains(a.EmailSubject)));

            var result = emailLogs != null
                ? emailLogs.OrderByDescending(a => a.CreatedDate).FirstOrDefault()
                : null;
            return result;
        }

        public EmailLog GetEmailByEmailTo(string emailTO)
        {
            var result = _context.EmailLog
                .Where(a => a.EmailTo == emailTO)
                .OrderBy(a => a.CreatedDate)
                .FirstOrDefault();
            return result;
        }

        public EmailLog GetEmailBySubject(string subject)
        {
            var result = _context.EmailLog
                .Where(a => a.EmailSubject == subject)
                .OrderBy(a => a.CreatedDate)
                .FirstOrDefault();
            return result;
        }

        public List<EmailLog> GetAllEmailsByEmailTo(string emailTO)
        {
            var result = _context.EmailLog
                .Where(a => a.EmailTo == emailTO)
                .OrderBy(a => a.CreatedDate)
                .ToList();
            return result;
        }

        public List<EmailLog> GetAllEmailsBySubject(string subject)
        {
            var result = _context.EmailLog
                .Where(a => a.EmailSubject == subject)
                .OrderBy(a => a.CreatedDate)
                .ToList();
            return result;
        }

        #region Dispose
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}