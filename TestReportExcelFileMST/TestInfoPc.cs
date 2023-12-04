using GemBox.Spreadsheet;
using InfoPc.Utils.ConvertExtensions;
using InfoPc.Utils.Helper;
using InfoPc.Utils.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestReportExcelFileMST
{
    [TestClass]
    public class TestInfoPc
    {
        [TestMethod]
        public void AddInfoPcInfo()
        {
            var variusHelper = new VariusHelper();
            variusHelper.SetGemboxLicense();

            var gpuInfo = new Gpu();
            var gpu = gpuInfo.GetInfoVideoController();
            Assert.IsNotNull(gpu);

            var memoryInfo = new PhysicalMemory();
            var memory = memoryInfo.GetInfoPhysicalMemory();
            Assert.IsNotNull(memory);

            var processorInfo = new Processor();
            var processor = processorInfo.GetInfoProcessor();
            Assert.IsNotNull(processor);

            var computerInfo = new ComputerInfo();
            computerInfo.GetInfoComputerSystem();
            var computer = computerInfo.GetInfoComputerSystem();
            Assert.IsNotNull(computerInfo);

            var extensFile = new ExcelFile();

            var excelFileInExceFormat = extensFile.CreateFileInfoPc(computer, gpu, memory, processor, Path.Combine(Path.GetTempPath(), $"TestFile_{DateTime.Now.Minute}"));

            File.Delete(excelFileInExceFormat);

            Assert.IsFalse(File.Exists(excelFileInExceFormat));
        }

        [TestMethod]
        public async Task GetTimeSpan_ShuldBeWaitOneMinuteBeforeitActives()
        {
            var isActive = false;

            var minutesSpan = DateTime.Now.AddMilliseconds(100).TimeOfDay.TotalMinutes;

            var stoppingToken = CancellationToken.None;

            while (stoppingToken.IsCancellationRequested == false)
            {
                var startupTime = new StartUpHelper();

                var delayTime = startupTime.GetTimeSpan(null, minutesSpan);

                await Task.Delay(delayTime, stoppingToken);

                if ((int)DateTime.Now.TimeOfDay.TotalMinutes == (int)minutesSpan)
                {
                    isActive = true;
                }

                Assert.IsTrue(isActive);
                break;
            }
        }
    }
}