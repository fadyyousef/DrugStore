using DrugStore.DAL;
using DrugStore.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ApplicationServices;
using System.Web.Mvc;
using System.Web.Security;

namespace DrugStore.Helpers
{
    public class CustomRoleProvider : RoleProvider
    {
        public override string ApplicationName
        {
            get { return "DrugStore"; }
            set { throw new NotImplementedException(); }
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var exists = false;
            using (var service = new AuthenticationService())
            {
                User user = service.GetUser(username);
                if (user.UserRole != null)
                {
                    exists = user.UserRole.UserRoleName.ToLower() == roleName.ToLower();
                }
                return exists;
            }
        }

        public override string[] GetRolesForUser(string username)
        {
            List<string> roles = new List<string>();
            using (var service = new AuthenticationService())
            {
                User user = service.GetUser(username);
                if (user.UserRole != null)
                {
                    roles.Add(user.UserRole.UserRoleName);
                }
                return roles.ToArray();
            }
        }

        public override bool RoleExists(string roleName)
        {
            using (var service = new AuthenticationService())
            {
                return service.RoleExists(roleName);
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            using (var service = new AuthenticationService())
            {
                return service.GetUsersInRole(roleName);
            }
        }

        public override string[] GetAllRoles()
        {
            using (var service = new AuthenticationService())
            {
                return service.GetRoles();
            }
        }

        ///
        ///
        /// all overrrided methods
        ///
        ///
        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {

        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {

        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

    }
}