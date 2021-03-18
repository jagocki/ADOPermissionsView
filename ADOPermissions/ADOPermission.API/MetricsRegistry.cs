using App.Metrics;
using App.Metrics.Counter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADOPermission.API
{
    public class MetricsRegistry
    {
        public static CounterOptions GetAllUsersPermissionsCounter => new CounterOptions
        {
            Name = "AllPermissionGet",
            Context = "PermissionsApiContext",
            MeasurementUnit = Unit.Calls
        };

        public static CounterOptions GetSingleUserPermissionsCounter => new CounterOptions
        {
            Name = "SingleUserPermissionGet",
            Context = "PermissionsApiContext",
            MeasurementUnit = Unit.Calls
        };
    }
}
