using GemBox.Spreadsheet;

namespace InfoPc.Utils.Helper
{
    public class VariusHelper
    {
        public double ByteToGb(ulong value)
        {
            return Math.Round(value / 1024d / 1024d / 1024d, 2);
        }

        public void SetGemboxLicense()
        {
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
        }
    }
}
