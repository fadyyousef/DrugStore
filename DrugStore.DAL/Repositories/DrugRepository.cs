using DrugStore.Model;
using DrugStore.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DrugStore.DAL.Repositories
{
    public class DrugRepository
    {
        private readonly DrugContext _context;
        public DrugRepository()
        {
            if (_context == null)
            {
                _context = new DrugContext();
            }
        }
        public DrugRepository(DrugContext context)
        {
            if (_context == null)
            {
                _context = context;
            }
        }
        public List<Drug> GetAllDrugs()
        {
            return _context.Drug.Include("DrugCompany").Include("DrugCategory").ToList();
        }

        public Drug GetDrugById(int drugId)
        {
            return _context.Drug.Find(drugId);
        }

        public int AddDrug(Drug drugEntity)
        {
            int result = -1;

            if (drugEntity != null)
            {
                _context.Drug.Add(drugEntity);
                _context.SaveChanges();
                result = drugEntity.ID;
            }
            return result;
        }

        public int UpdateDrug(Drug drugEntity)
        {
            int result = -1;

            if (drugEntity != null)
            {
                _context.Entry(drugEntity).State = EntityState.Modified;
                _context.SaveChanges();
                result = drugEntity.ID;
            }
            return result;
        }

        public void DeleteDrug(int drugId)
        {
            Drug drugEntity = _context.Drug.Find(drugId);
            _context.Drug.Remove(drugEntity);
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