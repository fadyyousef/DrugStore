using DrugStore.DAL;
using DrugStore.Model.Models;
using System;

namespace DrugStore.Helpers
{
    public class AuthenticationService : IDisposable
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();
        public bool UserExists(string email, string password)
        {
            return _unitOfWork.UserRepository.UserExists(email, password);
        }

        public User GetUser(string username)
        {
            return _unitOfWork.UserRepository.GetUserByUserName(username);
        }

        public User GetUser(int userId)
        {
            return _unitOfWork.UserRepository.GetUserById(userId);
        }

        public bool UpdateUser(User user)
        {
            return _unitOfWork.UserRepository.UpdateUser(user);
        }

        public bool RoleExists(string rolename)
        {
            return _unitOfWork.UserRepository.RoleExists(rolename);
        }

        public string[] GetUsersInRole(string rolename)
        {
            return _unitOfWork.UserRepository.GetUserNamesInRole(rolename);
        }

        public string[] GetRoles()
        {
            return _unitOfWork.UserRepository.GetRoles();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed == false)
            {
                if (disposing == true)
                {
                    //base.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}