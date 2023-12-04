using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace SendEmailService.Models
{
    public class Processor
    {
        public string Name { get; set; }
        public string CoreNumbers { get; set; }
        public string LogicalProcessorNumber { get; set; }
        public string MaxClockSpeed { get; set; }

        internal Processor GetInfoProcessor()
        {
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
            var processorInfo = new Processor();

            foreach (var systemInfo in searcher.Get())
            {

                processorInfo.Name = systemInfo["Name"].ToString();
                processorInfo.CoreNumbers = systemInfo["NumberOfCores"].ToString();
                processorInfo.LogicalProcessorNumber = systemInfo["NumberOfLogicalProcessors"].ToString();
                processorInfo.MaxClockSpeed = systemInfo["MaxClockSpeed"].ToString();
            }

            return processorInfo;
        }
    }

}
