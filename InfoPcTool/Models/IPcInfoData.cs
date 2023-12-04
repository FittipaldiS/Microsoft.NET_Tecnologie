using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoPcTool.Models
{
    public interface IPcInfoData
    {
        public string NamePc { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public decimal TotalPysicalMemory { get; set; }
        public string NumberOfProcessor { get; set; }

        public string NameGpu { get; set; }

        public decimal Ram { get; set; }
        public string MemoryType { get; set; }
        public string SpeedMhz { get; set; }

        public string NameProcessor { get; set; }
        public string CoreNumbers { get; set; }
        public string LogicalProcessorNumber { get; set; }
        public string MaxClockSpeed { get; set; }
        public double Temperatur { get; set; }
        public string CpuTemperatur { get; set; }
    }
}
