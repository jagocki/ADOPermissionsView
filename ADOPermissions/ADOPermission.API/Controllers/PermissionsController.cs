using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADOPermission.API.Models;
using ADOPermission.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PactoTrace;

namespace ADOPermission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        IGenericEventSink eventSink;

        public UsersService UsersService { get; }

        public PermissionsController(UsersService usersService, IGenericEventSink eventSink)
        {
            this.UsersService = usersService;
            this.eventSink = eventSink;
        }

        [HttpGet]
        public IEnumerable<User> AllUsers()
        {
            //IEnumerable<User> result = null;
            //Unit.Scope(() =>
            //{
            //    result = UsersService.GetUsers();
            //});
            //return result;
            return Unit.Scope<IEnumerable<User>>( () =>
               {
                   return UsersService.GetAllUsers();
               });
            //https://docs.microsoft.com/pl-pl/dotnet/api/system.threadstaticattribute?view=netcore-3.1
            //https://docs.microsoft.com/pl-pl/dotnet/api/system.threading.threadlocal-1?view=netcore-3.1
        }

        [HttpGet("{id}")]
        public IEnumerable<User> GetUser(string id)
        {
            return Unit.Scope<IEnumerable<User>>(() =>
            {
                return UsersService.GetUser(id);
            });
        }

    }
}
