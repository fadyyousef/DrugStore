using DrugStore.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace DrugStore.Helpers
{
    public class CustomMemberShipUser : MembershipUser
    {
        private readonly User _userData;

        public User UserData
        {
            get { return _userData; }
        }

        /// <summary>
        /// Constructor of MemberShip derived class
        /// </summary>
        public CustomMemberShipUser(string providername, User userData) :
        base(providername,
             userData.Email,
             userData.ID,
             userData.Email,
             string.Empty,
             string.Empty,
             true,
             !userData.IsActive,
             userData.CreatedDate,
             DateTime.Now,
             DateTime.Now,
             DateTime.Now,
             DateTime.Now)
        {
            this._userData = userData;
        }
    }
}