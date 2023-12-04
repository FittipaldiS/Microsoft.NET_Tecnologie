using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendEmailService.Models
{
    internal class ServiceSettings
    {
        public int IntervalSetting { get; set; }
        public string FromEmail { get; set; }
        public string ToEmail { get; set; }
        public string Password { get; set; }
        public string HostServer { get; set; }
        public string NameFrom { get; set; }
        public string NameTo { get; set; }
        public string BodyText { get; set; }
        public string Subject { get; set; }
    }
}
