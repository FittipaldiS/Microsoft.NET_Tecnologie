using LibreHardwareMonitor.Hardware;

namespace InfoPc.Utils.Models
{
    public class CPUTemperatureHelperBase
    {
        private Computer computer;

        public CPUTemperatureHelperBase()
        {
            computer = new Computer();
            computer.IsCpuEnabled = true;
            computer.Open();
        }

        public string GetCPUTemperature()
        {
            string temperature = "N/A";

            foreach (var hardware in computer.Hardware)
            {
                if (hardware.HardwareType == HardwareType.Cpu)
                {
                    hardware.Update();
                    foreach (var sensor in hardware.Sensors)
                    {
                        if (sensor.Value != null )
                        {
                            temperature = $"{sensor.Value} °C";
                            break;
                        }
                    }
                    break;
                }
            }

            return temperature;
        }
    }
}