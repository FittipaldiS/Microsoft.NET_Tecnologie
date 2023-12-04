using InfoPc.Utils.Models;

namespace InfoPcTool.Models
{
    public class PcInfoData: IPcInfoData
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

        public PcInfoData()
        {
            var computer = new ComputerInfo();
            var gpu = new Gpu();
            var physicalMemory = new PhysicalMemory();
            var processor = new Processor();

            var infoComputer = computer.GetInfoComputerSystem();
            var infoGpu = gpu.GetInfoVideoController();
            var infoPhysicalMemory = physicalMemory.GetInfoPhysicalMemory();
            var infoProcessor = processor.GetInfoProcessor();

            NamePc = infoComputer.NamePc;
            Manufacturer = infoComputer.Manufacturer;
            Model = infoComputer.Model;
            TotalPysicalMemory = infoComputer.TotalPysicalMemory;
            NumberOfProcessor = infoComputer.NumberOfProcessor;
            NameGpu = infoGpu.Name;
            Ram = infoPhysicalMemory.Ram;
            MemoryType = infoPhysicalMemory.MemoryType;
            SpeedMhz = infoPhysicalMemory.SpeedMhz;
            NameProcessor = infoProcessor.Name;
            CoreNumbers = infoProcessor.CoreNumbers;
            LogicalProcessorNumber = infoProcessor.LogicalProcessorNumber;
            MaxClockSpeed = infoProcessor.MaxClockSpeed;
            Temperatur = infoProcessor.Temperatur;
        }
    }


    
}
