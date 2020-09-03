using ChartJs.Blazor.ChartJS.Common.Enums;
using System.Collections.Generic;

namespace Squash.Web.Client.ChartJS
{
    public static class TimeDisplayFormats
    {
        public static Dictionary<TimeMeasurement, string> Squash => new Dictionary<TimeMeasurement, string>
        {
            { TimeMeasurement.Millisecond, "DD.MM.YYYY" },
            { TimeMeasurement.Second, "HH:mm:ss" },
            { TimeMeasurement.Minute, "HH:mm" },
            { TimeMeasurement.Hour, "HH:00" },
            {TimeMeasurement.Day,"DD.MM.YYYY" },
            {TimeMeasurement.Year,"DD.MM.YYYY" }
        };
    }
}
