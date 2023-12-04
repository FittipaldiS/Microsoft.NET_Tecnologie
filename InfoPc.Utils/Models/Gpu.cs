using System.Management;

namespace InfoPc.Utils.Models
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
