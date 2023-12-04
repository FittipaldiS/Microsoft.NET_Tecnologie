using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace SendEmailService.Models
{
    public class Gpu
    {
        public string Name { get; set; }

        public Gpu GetInfoVideoController()
        {
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");
            var gpuInfo = new Gpu();

            foreach (var systemInfo in searcher.Get())
            {

                gpuInfo.Name = systemInfo["Name"].ToString();
            }

            return gpuInfo;
        }
    }
}
