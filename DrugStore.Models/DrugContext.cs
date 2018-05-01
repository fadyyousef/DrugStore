using DrugStore.Model.Interfaces;
using DrugStore.Model.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace DrugStore.Model
{
    public class DrugContext : DbContext
    {
        public DrugContext() : base("DrugDB")
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Drug> Drug { get; set; }
        public DbSet<DrugCategory> DrugCategory { get; set; }
        public DbSet<DrugCompany> DrugCompany { get; set; }
        public DbSet<DrugForm> DrugForm { get; set; }
        public DbSet<EmailLog> EmailLog { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<UserRole>().ToTable("UserRole");
            modelBuilder.Entity<Drug>().ToTable("Drug");
            modelBuilder.Entity<DrugCategory>().ToTable("DrugCategory");
            modelBuilder.Entity<DrugCompany>().ToTable("DrugCompany");
            modelBuilder.Entity<DrugForm>().ToTable("DrugForm");
            modelBuilder.Entity<EmailLog>().ToTable("EmailLog");

            modelBuilder.Entity<User>()
                .HasRequired<UserRole>(x => x.UserRole)
                .WithMany(y => y.Users)
                .HasForeignKey<int>(x => x.UserRoleID);

            modelBuilder.Entity<Drug>()
                .HasRequired<DrugCategory>(s => s.DrugCategory)
                .WithMany(g => g.Drugs)
                .HasForeignKey<int>(s => s.CategoryID);

            modelBuilder.Entity<Drug>()
               .HasRequired<DrugCompany>(s => s.DrugCompany)
               .WithMany(g => g.Drugs)
               .HasForeignKey<int>(s => s.CompanyID);

            modelBuilder.Entity<Drug>()
               .HasRequired<DrugForm>(s => s.DrugForm)
               .WithMany(g => g.Drugs)
               .HasForeignKey<int>(s => s.FormID);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public override int SaveChanges()
        {
            var changeSet = ChangeTracker.Entries<IAuditBase>();
            DateTime currentDateTime = DateTime.Now;
            string currUser = "";
            if (IsAuthenticated())
            {
                var userId = (int)Membership.GetUser(HttpContext.Current.User.Identity.Name, false).ProviderUserKey;
                User user = User.Find(userId);
                currUser = user.FullName;
            }
            if (changeSet != null)
            {
                foreach (var entry in changeSet.Where(c => c.State == EntityState.Modified || c.State == EntityState.Added))
                {
                    entry.Entity.UpdatedDate = currentDateTime;
                    entry.Entity.UpdatedBy = entry.Entity.UpdatedBy == null ? currUser : entry.Entity.UpdatedBy;
                    if (entry.State == EntityState.Added)
                    {
                        entry.Entity.CreatedDate = currentDateTime;
                        entry.Entity.CreatedBy = entry.Entity.CreatedBy == null ? currUser : entry.Entity.CreatedBy;
                        break;
                    }
                }
            }
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
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
    }
}