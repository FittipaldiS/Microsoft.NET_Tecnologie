using GemBox.Spreadsheet;
using InfoPc.Utils.Helper;
using InfoPc.Utils.Models;
using Microsoft.VisualBasic;
using System.Reflection;
using System.Reflection.Metadata;
using System.Resources;

namespace InfoPc.Utils.ConvertExtensions
{
    public static class ExcelExtension
    {
        public static string CreateFileInfoPc(this ExcelFile workbook, ComputerInfo computerInfo, Gpu gpu, PhysicalMemory physicalMemory, Processor processor, string myTempFile)
        {
            var variusHelper = new VariusHelper();

            variusHelper.SetGemboxLicense();

            workbook = InitializeExcelFile();

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

            workbook.SaveReportFileInfoComputer(myTempFile, XlsxSaveOptions.XlsxDefault);

            return myTempFile;
        }

        public static bool SaveReportFileInfoComputer(this ExcelFile excelFile, string reportFilePath, SaveOptions saveOptions = null)
        {
            var output = false;

            try
            {
                // Create folder structure if not existing
                var reportDirectory = Path.GetDirectoryName(reportFilePath);
                Directory.CreateDirectory(reportDirectory);

                if (saveOptions == null)
                {
                    excelFile.Save(reportFilePath);
                }
                else
                {
                    var fileName = Path.GetFileNameWithoutExtension(reportFilePath);
                    var extension = GetFileExtensionBySaveOption(saveOptions);
                    fileName = $"{fileName}{extension}";
                    excelFile.Save(Path.Combine(reportDirectory, fileName), saveOptions);
                }

                output = true;
            }
            catch (Exception)
            {
                output = false;
            }

            return output;
        }
        private static string GetFileExtensionBySaveOption(SaveOptions saveOptions)
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

        private static ExcelFile InitializeExcelFile()
        {
            var variusHelper = new VariusHelper();
            variusHelper.SetGemboxLicense();

            using (var stream = new MemoryStream(GetExcelTemplate()))
            {
                return ExcelFile.Load(stream, LoadOptions.XlsxDefault);
            }
        }

        private static byte[] GetExcelTemplate()
        {
            byte[] output = null;

            output = ResourceExcel.ConfigInfo;

            return output;
        }

    }

}


