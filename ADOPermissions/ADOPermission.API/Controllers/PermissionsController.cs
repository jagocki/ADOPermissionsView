using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADOPermission.API.Models;
using ADOPermission.API.Services;
using App.Metrics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;

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
            return PactoTrace.Unit.Scope(() =>
            {
                MetricTags tags = new MetricTags(new[] { "TraceID", "Operation" }, new[] { System.Diagnostics.Activity.Current.TraceId.ToString(), "GetAllUser" });
                metrics.Measure.Counter.Increment(MetricsRegistry.GetGeneralPermissionsCounter,tags);
                return UsersService.GetAllUsers();
            });
            //return PactoTrace.Unit.Scope( () =>
            //   {
            //       metrics.Measure.Counter.Increment(MetricsRegistry.GetAllUsersPermissionsCounter);
            //       IEnumerable<User> users = UsersService.GetAllUsers();
            //       Log.Information("Users retrieved");
            //       return users;
            //   });
            //https://docs.microsoft.com/pl-pl/dotnet/api/system.threadstaticattribute?view=netcore-3.1
            //https://docs.microsoft.com/pl-pl/dotnet/api/system.threading.threadlocal-1?view=netcore-3.1
        }

        [HttpGet("{id}")]
        public IEnumerable<User> GetUser(string id)
        {
            return PactoTrace.Unit.Scope(() =>
            {
                MetricTags tags = new MetricTags(new[]{ "TraceID", "Operation" },new[] { System.Diagnostics.Activity.Current.TraceId.ToString(),"GetSingleUser" });
                
                metrics.Measure.Counter.Increment(MetricsRegistry.GetGeneralPermissionsCounter, tags );

                return UsersService.GetUser(id);
            });
        }

    }
}
