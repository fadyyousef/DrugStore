using DrugStore.Model;
using DrugStore.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DrugStore.DAL.Repositories
{
    public class DrugFormRepository
    {
        private readonly DrugContext _context;
        public DrugFormRepository()
        {
            if (_context == null)
            {
                _context = new DrugContext();
            }
        }
        public DrugFormRepository(DrugContext context)
        {
            if (_context == null)
            {
                _context = context;
            }
        }
        public IEnumerable<DrugForm> GetAllDrugForms()
        {
            return _context.DrugForm.ToList();
        }

        public DrugForm GetDrugFormById(int Id)
        {
            return _context.DrugForm.Find(Id);
        }

        public int AddDrugForm(DrugForm drugFormEntity)
        {
            int result = -1;
            if (drugFormEntity != null)
            {
                _context.DrugForm.Add(drugFormEntity);
                _context.SaveChanges();
                result = drugFormEntity.FormID;
            }
            return result;
        }

        public int UpdateDrugForm(DrugForm drugFormEntity)
        {
            int result = -1;
            if (drugFormEntity != null)
            {
                _context.Entry(drugFormEntity).State = EntityState.Modified;
                _context.SaveChanges();
                result = drugFormEntity.FormID;
            }
            return result;
        }

        public void DeleteDrugForm(int Id)
        {
            DrugForm drugFormEntity = _context.DrugForm.Find(Id);
            _context.DrugForm.Remove(drugFormEntity);
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