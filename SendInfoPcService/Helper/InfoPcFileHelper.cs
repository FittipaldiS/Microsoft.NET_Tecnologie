using GemBox.Spreadsheet;
using InfoPc.Utils.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SendEmailService.Helper
{
    public class InfoPcFileHelper
    {

        //TODO: SF Inserisci questa classe all interno di InfoPcUtils
        private readonly ILogger<Worker> _logger;

        public InfoPcFileHelper(ILogger<Worker> logger)
        {
            _logger = logger;

            SetGemboxLicense();
        }

        public string CreatExcelFileReportInfo()
        {
            var gpuInfo = new Gpu();
            var gpu = gpuInfo.GetInfoVideoController();

            var memoryInfo = new PhysicalMemory();
            var memory = memoryInfo.GetInfoPhysicalMemory();

            var processorInfo = new Processor();
            var processor = processorInfo.GetInfoProcessor();

            var computerInfo = new ComputerInfo();
            computerInfo.GetInfoComputerSystem();
            var computer = computerInfo.GetInfoComputerSystem();

            //Read a workbook in excel file folder
            var excelFilePathName = GetPathFileName();

            var workbook = InsertPcInfoInExcelFile(excelFilePathName, computer, gpu, memory, processor);

            var tempFolder = Path.GetTempPath();

            string reportFilePath = Path.Combine(tempFolder, "reportInfoPc.xlsx");
            var isCreated = SaveReport(workbook, reportFilePath);

            if (isCreated)
            {
                _logger.LogInformation("File was created");
            }
            else
            {
                _logger.LogError("Couldn't created");
            }

            return reportFilePath;

        }

        public ExcelFile InsertPcInfoInExcelFile(string excelFilePathName, ComputerInfo computerInfo, Gpu gpu, PhysicalMemory physicalMemory, Processor processor)
        {
            _logger.LogInformation("Try to insert info pc inside the excel file");

            var workbook = ExcelFile.Load(excelFilePathName);

            var worksheet = workbook.Worksheets[0];
            //A video so you can see how to use the System.Management package https://www.youtube.com/watch?v=nhLfrvbUujk

            //PC Info
            worksheet.Cells["B2"].Value = computerInfo.NamePc;
            worksheet.Cells["B3"].Value = computerInfo.Manufacturer;
            worksheet.Cells["B4"].Value = computerInfo.Model;
            worksheet.Cells["B5"].Value = computerInfo.TotalPysicalMemory;
            worksheet.Cells["B6"].Value = computerInfo.NumberOfProcessor;

            //GPU info
            worksheet.Cells["B11"].Value = gpu.Name;

            //Phys
            worksheet.Cells["B16"].Value = physicalMemory.Ram;
            worksheet.Cells["B17"].Value = physicalMemory.MemoryType;
            worksheet.Cells["B18"].Value = physicalMemory.SpeedMhz;

            //Processor
            worksheet.Cells["B23"].Value = processor.Name;
            worksheet.Cells["B24"].Value = processor.CoreNumbers;
            worksheet.Cells["B25"].Value = processor.LogicalProcessorNumber;
            worksheet.Cells["B26"].Value = processor.MaxClockSpeed;

            return workbook;
        }

        private string GetPathFileName()
        {
            return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Excel\ConfigInfo.xlsx");
        }

        private void SetGemboxLicense()
        {
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
        }

        public bool SaveReport(ExcelFile excelFile, string reportFilePath, SaveOptions saveOptions = null)
        {
            var output = false;

            // Create folder structure if not existing
            var reportDirectory = Path.GetDirectoryName(reportFilePath);
            Directory.CreateDirectory(reportDirectory);

            if (saveOptions == null)
            {
                excelFile.Save(reportFilePath);
                output = true;

            }
            else
            {
                var fileName = Path.GetFileNameWithoutExtension(reportFilePath);
                var extension = GetFileExtensionBySaveOption(saveOptions);
                fileName = $"{fileName}{extension}";

                excelFile.Save(Path.Combine(reportDirectory, fileName), saveOptions);
                output = true;

            }

            return output;
        }

        public string GetFileExtensionBySaveOption(SaveOptions saveOptions)
        {
            var output = ".xlsx";

            if (saveOptions is PdfSaveOptions)
            {
                output = ".pdf";
            }
            else if (saveOptions is XlsSaveOptions)
            {
                output = ".xls";
            }
            else if (saveOptions is HtmlSaveOptions)
            {
                output = ".html";
            }
            else if (saveOptions is CsvSaveOptions)
            {
                output = ".csv";
            }
            else if (saveOptions is OdsSaveOptions)
            {
                output = ".ods";
            }

            return output;
        }

    }
}
