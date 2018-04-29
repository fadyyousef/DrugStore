using DrugStore.Model;
using DrugStore.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DrugStore.DAL.Repositories
{
    public class DrugCompanyRepository
    {
        private readonly DrugContext _context;
        public DrugCompanyRepository()
        {
            if (_context == null)
            {
                _context = new DrugContext();
            }
        }
        public DrugCompanyRepository(DrugContext context)
        {
            if (_context == null)
            {
                _context = context;
            }
        }
        public IEnumerable<DrugCompany> GetAllDrugCompanies()
        {
            return _context.DrugCompany.ToList();
        }

        public DrugCompany GetDrugCompanyById(int Id)
        {
            return _context.DrugCompany.Find(Id);
        }

        public int AddDrugCompany(DrugCompany drugCompanyEntity)
        {
            int result = -1;
            if (drugCompanyEntity != null)
            {
                _context.DrugCompany.Add(drugCompanyEntity);
                _context.SaveChanges();
                result = drugCompanyEntity.CompanyID;
            }
            return result;
        }

        public int UpdateDrugCompany(DrugCompany drugCompanyEntity)
        {
            int result = -1;
            if (drugCompanyEntity != null)
            {
                _context.Entry(drugCompanyEntity).State = EntityState.Modified;
                _context.SaveChanges();
                result = drugCompanyEntity.CompanyID;
            }
            return result;
        }

        public void DeleteDrugCompany(int Id)
        {
            DrugCompany drugCompanyEntity = _context.DrugCompany.Find(Id);
            _context.DrugCompany.Remove(drugCompanyEntity);
            _context.SaveChanges();
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