using GemBox.Spreadsheet.Charts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoPc.Utils.Helper
{
    public class StartUpHelper
    {
        private readonly ILogger<object> _logger;

        public StartUpHelper(ILogger<object> logger)
        {
            _logger = logger;
        }

        public StartUpHelper()
        {

        }

        public TimeSpan GetTimeSpan(int? hoursOfActivation = null, double? millisecond = null)
        {
            var scheduledTime = DateTime.Now;

            scheduledTime = millisecond == null ? DateTime.Today.AddHours((double)hoursOfActivation) : DateTime.Today.AddMilliseconds((double)millisecond);

            if (DateTime.Now >= scheduledTime)
            {
                scheduledTime = scheduledTime.AddDays(1);
            }

            _logger?.LogInformation($"The service will be started at : {scheduledTime}");

            var delayTime = scheduledTime - DateTime.Now;

            return delayTime;
        }
    }
}
