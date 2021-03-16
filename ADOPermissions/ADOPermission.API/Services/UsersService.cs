using ADOPermission.API.Models;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.Collections.Generic;
using PactoTrace;
using System;

namespace ADOPermission.API.Services
{
    public class UsersService
    {
        private ILogger logger;
        private Unit unit;

        public UsersService( )
        {
            

        }

        public IEnumerable<User> GetAllUsers()
        {
            return Unit.Scope<List<User>>(() =>
            {

                return new List<User>() {
                    new User() { DistinguishedName = "dn:user1DN" },
                    new User() { DistinguishedName = "dn:user2DN" }
                 };
            });
        }

        internal IEnumerable<User> GetUser(string id)
        {
            return Unit.Scope<List<User>>(() =>
            {

                return new List<User>() { new User() { DistinguishedName = "dn:userDN" } };
            });
        }
    }
}