using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using DrugStore.Model;
using DrugStore.Model.Models;

namespace DrugStore.DAL.Repositories
{
    public class DrugCategoryRepository
    {
        private readonly DrugContext _context;

        public DrugCategoryRepository( )
        {
            if ( _context == null )
            {
                _context = new DrugContext( );
            }
        }

        public DrugCategoryRepository( DrugContext context )
        {
            if ( _context == null )
            {
                _context = context;
            }
        }

        public IEnumerable<DrugCategory> GetAllDrugCategories( )
        {
            return _context.DrugCategory.ToList( );
        }

        public DrugCategory GetDrugCategoryById( int Id )
        {
            return _context.DrugCategory.Find( Id );
        }

        public int AddDrugCategory( DrugCategory drugCategoryEntity )
        {
            int result = -1;
            if ( drugCategoryEntity != null )
            {
                _context.DrugCategory.Add( drugCategoryEntity );
                _context.SaveChanges( );
                result = drugCategoryEntity.CategoryID;
            }
            return result;
        }

        public int UpdateDrugCategory( DrugCategory drugCategoryEntity )
        {
            int result = -1;
            if ( drugCategoryEntity != null )
            {
                _context.Entry( drugCategoryEntity ).State = EntityState.Modified;
                _context.SaveChanges( );
                result = drugCategoryEntity.CategoryID;
            }
            return result;
        }

        public void DeleteDrugCategory( int Id )
        {
            DrugCategory drugCategoryEntity = _context.DrugCategory.Find( Id );
            _context.DrugCategory.Remove( drugCategoryEntity );
            _context.SaveChanges( );
        }

        #region Dispose
        private bool disposed = false;
        protected virtual void Dispose( bool disposing )
        {
            if ( !this.disposed )
            {
                if ( disposing )
                {
                    _context.Dispose( );
                }
            }
            this.disposed = true;
        }
        public void Dispose( )
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }
        #endregion
    }
}