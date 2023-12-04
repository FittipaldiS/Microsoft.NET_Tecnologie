﻿using InfoPc.Utils.Helper;
using System.Management;


namespace InfoPc.Utils.Models
{

    public class ComputerInfo : VariusHelper
    {
        public string NamePc { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public decimal TotalPysicalMemory { get; set; }
        public string NumberOfProcessor { get; set; }

        public ComputerInfo GetInfoComputerSystem()
        {
            //To found Computer System Hardware classes -> https://learn.microsoft.com/en-us/windows/win32/cimwin32prov/computer-system-hardware-classes
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem");
            var computerInfo = new ComputerInfo();

            foreach (var systemInfo in searcher.Get())
            {

                computerInfo.NamePc = systemInfo["Name"].ToString();
                computerInfo.Manufacturer = systemInfo["Manufacturer"].ToString();
                computerInfo.TotalPysicalMemory = (decimal)ByteToGb((ulong)systemInfo["TotalPhysicalMemory"]);
                computerInfo.Model = systemInfo["Model"].ToString();
                computerInfo.NumberOfProcessor = systemInfo["NumberOfProcessors"].ToString();
            }

            return computerInfo;
        }
    }


}
