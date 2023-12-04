using Org.BouncyCastle.Asn1.Crmf;
using SendEmailService.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace SendEmailService.Models
{
    public class PhysicalMemory:VariusHelper
    {
        public decimal Ram { get; set; }
        public string MemoryType { get; set; }
        public string SpeedMhz { get; set; }

        internal PhysicalMemory GetInfoPhysicalMemory()
        {
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory");
            var physicalMemoryInfo = new PhysicalMemory();

            foreach (var systemInfo in searcher.Get())
            {

                physicalMemoryInfo.Ram = (decimal)ByteToGb((ulong)systemInfo["Capacity"]);
                physicalMemoryInfo.MemoryType = systemInfo["MemoryType"].ToString();
                physicalMemoryInfo.SpeedMhz = systemInfo["Speed"].ToString();
            }

            return physicalMemoryInfo;
        }
    }
}
