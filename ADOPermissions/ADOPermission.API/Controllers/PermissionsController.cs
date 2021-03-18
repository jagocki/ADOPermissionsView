using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADOPermission.API.Models;
using ADOPermission.API.Services;
using App.Metrics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace ADOPermission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        PactoTrace.IGenericEventSink eventSink;

        public UsersService UsersService { get; }

        private readonly IMetrics metrics;

        public PermissionsController(UsersService usersService, PactoTrace.IGenericEventSink eventSink, IMetrics metrics)
        {
            this.UsersService = usersService;
            this.eventSink = eventSink;
            this.metrics = metrics;
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
            return PactoTrace.Unit.Scope<IEnumerable<User>>( () =>
               {
                   metrics.Measure.Counter.Increment(MetricsRegistry.GetAllUsersPermissionsCounter);
                   return UsersService.GetAllUsers();
               });
            //https://docs.microsoft.com/pl-pl/dotnet/api/system.threadstaticattribute?view=netcore-3.1
            //https://docs.microsoft.com/pl-pl/dotnet/api/system.threading.threadlocal-1?view=netcore-3.1
        }

        [HttpGet("{id}")]
        public IEnumerable<User> GetUser(string id)
        {
            return PactoTrace.Unit.Scope<IEnumerable<User>>(() =>
            {
                metrics.Measure.Counter.Increment(MetricsRegistry.GetSingleUserPermissionsCounter);

                return UsersService.GetUser(id);
            });
        }

    }
}
