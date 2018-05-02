using DrugStore.Model;
using DrugStore.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DrugStore.DAL.Repositories
{
    public class UserRepository
    {
        private readonly DrugContext _context;
        public UserRepository()
        {
            if (_context == null)
            {
                _context = new DrugContext();
            }
        }
        public UserRepository(DrugContext context)
        {
            if (_context == null)
            {
                _context = context;
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.User.Include("UserRole").ToList();
        }

        public IEnumerable<UserRole> GetAllRoles()
        {
            return _context.UserRole.ToList();
        }

        public bool UserExists(string email, string password)
        {
            return _context.User.Any(a => a.Email == email && a.Password == password);
        }

        public User GetUserById(int userId)
        {
            return _context.User.Find(userId);
        }

        public User GetUserByUserName(string username)
        {
            return _context.User.Where(a => a.UserName == username).FirstOrDefault();
        }

        public User GetUserByNameAndPassword(string userName, string password)
        {
            User user = _context.User.Where(a => a.UserName == userName && a.Password == password).FirstOrDefault();
            return user;
        }

        public int AddUser(User userEntity)
        {
            int result = -1;

            if (userEntity != null)
            {
                _context.User.Add(userEntity);
                _context.SaveChanges();
                result = userEntity.ID;
            }
            return result;
        }

        public bool UpdateUser(User userEntity)
        {
            if (userEntity != null)
            {
                _context.Entry(userEntity).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public void DeleteUser(int Id)
        {
            User userEntity = _context.User.Find(Id);
            _context.User.Remove(userEntity);
            _context.SaveChanges();
        }

        public int getUserRoleID(string role)
        {
            List<UserRole> userRoles = _context.UserRole.ToList();
            var result = userRoles.Where(a => a.UserRoleName == role).FirstOrDefault().UserRoleID;
            return result;
        }

        public UserRole getUserRoleByID(int userRoleID)
        {
            var result = _context.UserRole.Where(a => a.UserRoleID == userRoleID).FirstOrDefault();
            return result;
        }

        public string getUserRole(int userRoleID)
        {
            var result = _context.UserRole.Where(a => a.UserRoleID == userRoleID).FirstOrDefault().UserRoleName;
            return result;
        }

        public bool RoleExists(string rolename)
        {
            var roles = _context.UserRole.ToList();
            bool roleExists = false;
            foreach (UserRole role in roles)
            {
                if (role.UserRoleName.ToLower() == rolename.ToLower())
                {
                    roleExists = true;
                }
            }
            return roleExists;
        }

        public string[] GetRoles()
        {
            return _context.UserRole.Select(a => a.UserRoleName).ToArray();
        }

        public string[] GetUserNamesInRole(string role)
        {
            var users = _context.User.Include("UserRole")
                .Where(a => a.UserRole.UserRoleName.ToLower() == role.ToLower())
                .Select(a => a.UserName)
                .ToArray();
            return users;
        }

        public List<User> GetUsersInRole(string role)
        {
            var users = _context.User.Include("UserRole")
                .Where(a => a.UserRole.UserRoleName.ToLower() == role.ToLower()).ToList();
            return users;
        }

        public string[] GetAdminEmails()
        {
            var users = _context.User.Include("UserRole").Where(a => a.UserRole.UserRoleName.ToLower() == "admin").ToList();
            return users.Select(a => a.Email).ToArray();

        }

        public string CheckDuplicateUser(User user, int? id)
        {
            var msgResult = "";
            var allUsers = _context.User.ToList();
            if (id != null && id != 0)
            {
                allUsers = allUsers.Where(a => a.ID != id.Value).ToList();
            }
            foreach (var oldUser in allUsers)
            {
                if (oldUser.UserName == user.UserName)
                {
                    msgResult += "UserName is already in use...,";
                }
                if (oldUser.Email == user.Email)
                {
                    msgResult += "Email is already in use...,";
                }
                if (oldUser.Phone == user.Phone)
                {
                    msgResult += "Phone is already in use...,";
                }
            }
            return msgResult;
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