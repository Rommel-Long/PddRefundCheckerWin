using ClosedXML.Excel;
using System.Collections.Generic;
using System.Linq;

public static class ExcelHelper
{
    public static List<string> ReadTrackingNumbers(string filePath)
    {
        var list = new List<string>();
        using var workbook = new XLWorkbook(filePath);
        var sheet = workbook.Worksheet(1);
        foreach (var row in sheet.RowsUsed().Skip(1))
        {
            var trackingNumber = row.Cell(2).GetString(); // 假设第2列是发货单号
            if (!string.IsNullOrWhiteSpace(trackingNumber))
                list.Add(trackingNumber);
        }
        return list;
    }

    public static void WriteStatus(string filePath, Dictionary<string, string> results)
    {
        using var workbook = new XLWorkbook(filePath);
        var sheet = workbook.Worksheet(1);
        var header = sheet.Row(1);
        int newColIndex = header.LastCellUsed().Address.ColumnNumber + 1;
        sheet.Cell(1, newColIndex).Value = "是否申请退货";

        foreach (var row in sheet.RowsUsed().Skip(1))
        {
            var tracking = row.Cell(2).GetString();
            if (results.TryGetValue(tracking, out var status))
            {
                row.Cell(newColIndex).Value = status;
            }
        }

        workbook.SaveAs("output.xlsx");
    }
}