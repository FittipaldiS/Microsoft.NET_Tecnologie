using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertInfoPc.Models
{
    public partial class PcInfo : ObservableObject
    {
        public PcInfo() { }

        public PcInfo(string name) 
        {

        }

        [ObservableProperty] private string _namePc;
        [ObservableProperty] private string _manufacturer;
        [ObservableProperty] private string _model;
        [ObservableProperty] private decimal _totalPysicalMemory;
        [ObservableProperty] private string _numberOfProcessor;
        [ObservableProperty] private string _nameGpu;
        [ObservableProperty] private decimal _ram;
        [ObservableProperty] private string _memoryType;
        [ObservableProperty] private string _speedMhz;
        [ObservableProperty] private string _name;
        [ObservableProperty] private string _coreNumbers;
        [ObservableProperty] private string _logicalProcessorNumber;
        [ObservableProperty] private string _maxClockSpeed;
    }
}
