using InfoPc.Utils.Models;
using InfoPcTool.Models;
using MVVM.Helper;
using System.Runtime.CompilerServices;
using System;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Interop;

namespace InfoPcTool.ViewModels
{
    public partial class MainWindowViewModel : BaseViewModel
    {
        private static System.Timers.Timer CheckTemperature;

        public MainWindowViewModel(IPcInfoData data)
        {
            NamePc = data.NamePc;
            Manufacturer = data.Manufacturer;
            Model = data.Model;
            TotalPysicalMemory = data.TotalPysicalMemory;
            NumberOfProcessor = data.NumberOfProcessor;
            NameGpu = data.NameGpu;
            Ram = data.Ram;
            MemoryType = data.MemoryType;
            SpeedMhz = data.SpeedMhz;
            NameProcessor = data.NameProcessor;
            CoreNumbers = data.CoreNumbers;
            LogicalProcessorNumber = data.LogicalProcessorNumber;
            MaxClockSpeed = data.MaxClockSpeed;
            Temperature = data.Temperatur;
            CpuTemperatur = data.CpuTemperatur;

            ClosedCommand = new RelayCommand(action => ((Window)action).Close());
        }

        private void RefreshTimerInSeconds(double seconds)
        {
            CheckTemperature = new System.Timers.Timer();

            CheckTemperature.Interval = seconds; // every tot seconds  

            CheckTemperature.Elapsed += RefreshTemperatur;

            CheckTemperature.AutoReset = true;

            CheckTemperature.Enabled = true;
        }

        public void RefreshTemperatur(object source, System.Timers.ElapsedEventArgs e)
        {
            //TODO SF : Refresch temperature with infopcUtils

            var processorInfo = new Processor();
            var processor = processorInfo.GetInfoProcessor();

            CpuTemperatur = processor.CpuTemperatur;
        }

        #region PropertyFull
        public string NamePc
        {
            get => _namePc;
            set => SetProperty(ref _namePc, value);
        }
        private string _namePc;

        public string Manufacturer
        {
            get => _manufacturer;
            set => SetProperty(ref _manufacturer, value);
        }
        private string _manufacturer;

        public string Model
        {
            get => _model;
            set => SetProperty(ref _model, value);
        }
        private string _model;

        public decimal TotalPysicalMemory
        {
            get => _totalPysicalMemory;
            set => SetProperty(ref _totalPysicalMemory, value);
        }
        private decimal _totalPysicalMemory;

        public string NumberOfProcessor
        {
            get => _numberOfProcessor;
            set => SetProperty(ref _numberOfProcessor, value);
        }
        private string _numberOfProcessor;

        public string NameGpu
        {
            get => _nameGpu;
            set => SetProperty(ref _nameGpu, value);
        }
        private string _nameGpu;

        public decimal Ram
        {
            get => _ram;
            set => SetProperty(ref _ram, value);
        }
        private decimal _ram;

        public string MemoryType
        {
            get => _memoryType;
            set => SetProperty(ref _memoryType, value);
        }
        private string _memoryType;


        public string SpeedMhz
        {
            get => _speedMhz;
            set => SetProperty(ref _speedMhz, value);
        }
        private string _speedMhz;

        public string NameProcessor
        {
            get => _nameProcessor;
            set => SetProperty(ref _nameProcessor, value);
        }
        private string _nameProcessor;

        public string CoreNumbers
        {
            get => _coreNumbers;
            set => SetProperty(ref _coreNumbers, value);
        }
        private string _coreNumbers;


        public string LogicalProcessorNumber
        {
            get => _logicalProcessorNumber;
            set => SetProperty(ref _logicalProcessorNumber, value);
        }
        private string _logicalProcessorNumber;

        public string MaxClockSpeed
        {
            get => _maxClockSpeed;
            set => SetProperty(ref _maxClockSpeed, value);
        }
        private string _maxClockSpeed;

        public double Temperature
        {
            get => _temperature;
            set {
                    SetProperty(ref _temperature, value);

                    //RefreshTimerInSeconds(5000);
                }
        }
        private double _temperature;


        public string CpuTemperatur
        {
            get => _cpuTemperatur;
            set
            {
                SetProperty(ref _cpuTemperatur, value);

                RefreshTimerInSeconds(5000);
            }
        }
        private string _cpuTemperatur;



        #endregion

        #region RelayCommand

        public RelayCommand ClosedCommand { get; private set; }

        private RelayCommand CreatePdfFile;

        public ICommand CreatePdfFileCommand => CreatePdfFile ??= new RelayCommand(CreatePdfFileFromExcel);

        private void CreatePdfFileFromExcel(object commandParameter)
        {
            //var excelFileInExceFormat = extensFile.CreateFileInfoPc(computer, gpu, memory, processor, Path.Combine(Path.GetTempPath(), $"TestFile_{DateTime.Now.Minute}"));
        }

        #endregion

    }
}
