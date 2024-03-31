using System.IO;
using Utility.ExcelReader;
using OfficeOpenXml;

public static class ExcelCsvWriter
{
    private static void WriteHeaderAndSubHeaderOnExsitSheet(string filePath, RowData rowData)
    {
        FileInfo fileInfo = new FileInfo(filePath);
        using (ExcelPackage package = new ExcelPackage(fileInfo))
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets[rowData.MiddleCategory]; // 첫 번째 워크시트 가져오기

            for (int i = 1; i < rowData.Headers.Count; i++)
            {
                worksheet.Cells[1, i].Value = rowData.Headers[i];
                worksheet.Cells[2, i].Value = rowData.Types[i];
            }
            // 변경 사항 저장
            package.Save();
        }
    }
    private static void WriteHeaderAndSubHeaderOnNewSheet(string filePath, RowData rowData)
    {
        using (ExcelPackage package = new ExcelPackage())
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(rowData.MiddleCategory);

            for (int index = 0; index < rowData.Headers.Count; index++)
            {
                worksheet.Cells[1, index].Value = rowData.Headers[index];
                worksheet.Cells[2, index].Value = rowData.Types[index];
            }
            // Excel 파일 저장
            package.SaveAs(new FileInfo(filePath));
        }
    }

    public static void WriteValuesNewSheet(string filePath, RowData rowData, int rowIndex)
    {
        using (ExcelPackage package = new ExcelPackage())
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(rowData.MiddleCategory);

            for (int index = 0; index < rowData.Headers.Count; index++)
            {
                worksheet.Cells[rowIndex, index].Value = rowData.Values[index];
            }
            // Excel 파일 저장
            package.SaveAs(new FileInfo(filePath));
        }
    }
    public static void WriteValuesExsistSheet(string filePath, RowData rowData, int rowIndex)
    {
        FileInfo fileInfo = new FileInfo(filePath);
        using (ExcelPackage package = new ExcelPackage(fileInfo))
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets[rowData.MiddleCategory]; // 첫 번째 워크시트 가져오기
            
            for (int i = 1; i < rowData.Headers.Count; i++)
            {
                worksheet.Cells[rowIndex, i].Value = rowData.Values[i];
            }
            // 변경 사항 저장
            package.Save();
        }
    }
}
