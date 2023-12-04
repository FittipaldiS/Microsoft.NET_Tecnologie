using System.Management;

namespace InfoPc.Utils.Models
{
    public class Processor
    {
        public string Name { get; set; }
        public string CoreNumbers { get; set; }
        public string LogicalProcessorNumber { get; set; }
        public string MaxClockSpeed { get; set; }
        public double Temperatur { get; set; }
        public string CpuTemperatur { get; set; }

        public Processor GetInfoProcessor()
        {
            var CpuTemperatureHelper = new CPUTemperatureHelperBase();
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
            var processorInfo = new Processor();

            foreach (var systemInfo in searcher.Get())
            {

                processorInfo.Name = systemInfo["Name"].ToString();
                processorInfo.CoreNumbers = systemInfo["NumberOfCores"].ToString();
                processorInfo.LogicalProcessorNumber = systemInfo["NumberOfLogicalProcessors"].ToString();
                processorInfo.MaxClockSpeed = systemInfo["MaxClockSpeed"].ToString();
                processorInfo.Temperatur = GetInfoTemperaturMotherboard();
                processorInfo.CpuTemperatur = CpuTemperatureHelper.GetCPUTemperature();
            }

            return processorInfo;
        }

        //TODO: SF Issues -> Get temperatur is possible only if you are Admin
        public double GetInfoTemperaturMotherboard()
        {
            var searcher = new ManagementObjectSearcher(@"root\WMI", "SELECT * FROM  MSAcpi_ThermalZoneTemperature ");
            var processorInfo = new Processor();

            foreach (var systemInfo in searcher.Get())
            {
                processorInfo.Temperatur = Convert.ToDouble(systemInfo["CurrentTemperature"].ToString());
                // Convert the value to celsius degrees
                processorInfo.Temperatur = (processorInfo.Temperatur - 2732) / 10.0;
            }

            double temperaturCpu = processorInfo.Temperatur;

            return temperaturCpu;
        }

        //public string GetCPUTemperature()
        //{
        //    string cpuTemperature = "N/A";

        //    ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PerfFormattedData_Counters_ThermalZoneInformation");

        //    var processorInfo = new Processor();

        //    foreach (ManagementObject systemInfo in searcher.Get())
        //    {
        //        if (systemInfo["Name"] != null && systemInfo["Name"].ToString().Contains("CPU"))
        //        {
        //            if (systemInfo["Temperature"] != null)
        //            {
        //                cpuTemperature = systemInfo["Temperature"].ToString() + "°C";

        //                processorInfo.CpuTemperatur = cpuTemperature;

        //                break;
        //            }
        //        }
        //    }

        //    return cpuTemperature;
        //}

    }
}
