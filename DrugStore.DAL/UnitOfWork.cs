using DrugStore.DAL.Repositories;
using DrugStore.Model;
using System;
using System.Web;

namespace DrugStore.DAL
{
    public class UnitOfWork
    {
        private DrugContext _drugContext;
        private DrugRepository _drugRepository;
        private DrugCategoryRepository _drugCategoryRepository;
        private DrugCompanyRepository _drugCompanyRepository;
        private DrugFormRepository _drugFormRepository;
        private UserRepository _userRepository;
        private EmailLogRepository _emailLogRepository;

        public UnitOfWork()
        {
            if (_drugContext == null)
            {
                _drugContext = new DrugContext();
            }
        }

        public UnitOfWork(DrugContext drugContext)
        {
            if (_drugContext == null)
            {
                _drugContext = drugContext;
            }
        }

        public DrugRepository DrugRepository
        {
            get
            {
                if (_drugRepository == null)
                {
                    _drugRepository = new DrugRepository(_drugContext);
                }
                return _drugRepository;
            }
        }
        public DrugCategoryRepository DrugCategoryRepository
        {
            get
            {
                if (_drugCategoryRepository == null)
                {
                    _drugCategoryRepository = new DrugCategoryRepository(_drugContext);
                }
                return _drugCategoryRepository;
            }
        }
        public DrugCompanyRepository DrugCompanyRepository
        {
            get
            {
                if (_drugCompanyRepository == null)
                {
                    _drugCompanyRepository = new DrugCompanyRepository(_drugContext);
                }
                return _drugCompanyRepository;
            }
        }
        public DrugFormRepository DrugFormRepository
        {
            get
            {
                if (_drugFormRepository == null)
                {
                    _drugFormRepository = new DrugFormRepository(_drugContext);
                }
                return _drugFormRepository;
            }
        }
        public UserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_drugContext);
                }
                return _userRepository;
            }
        }
        public EmailLogRepository EmailLogRepository
        {
            get
            {
                if (_emailLogRepository == null)
                {
                    _emailLogRepository = new EmailLogRepository(_drugContext);
                }
                return _emailLogRepository;
            }
        }

        public void Save()
        {
            _drugContext.SaveChanges();
        }

        public bool IsAuthenticated()
        {
            bool isAuthed = false;
            try
            {
                if (HttpContext.Current.User.Identity.Name != null)
                {
                    if (HttpContext.Current.User.Identity.Name.Length != 0)
                    {
                        isAuthed = true;
                    }
                }
            }
            catch { } // not authed
            return isAuthed;
        }

        #region Dispose
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _drugContext.Dispose();
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