using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace MVCHomeWork02.Helper
{
    public static class ExportHelper
    {
        public static byte[] Exprot<T>(IEnumerable<T> dataList)
        {
            using (var p = new ExcelPackage())
            {
                p.Workbook.Worksheets.Add("Client");

                var ws = p.Workbook.Worksheets[1];
                ws.Name = "Client";
                ws.Cells.Style.Font.Size = 12;
                ws.Cells.Style.Font.Name = "新細明體";

                var dt = ConvertToDataTable(dataList);

                int colTotalCount = dt.Columns.Count;

                int rowIndex = 1;

                //Header
                for (int colIndex = 0; colIndex < colTotalCount; colIndex++)
                {
                    var cell = ws.Cells[rowIndex, colIndex + 1];

                    cell.Value = dt.Columns[colIndex];

                    var border = cell.Style.Border;
                    AddBorder(ref border);
                }

                //content
                for (int rowCount = 0; rowCount < dt.Rows.Count; rowCount++)
                {
                    var colIndex = 1;
                    rowIndex++;

                    for (int colCount = 0; colCount < colTotalCount; colCount++)
                    {
                        var cell = ws.Cells[rowIndex, colIndex];
                        cell.Value = dt.Rows[rowCount][colCount];

                        var border = cell.Style.Border;
                        AddBorder(ref border);

                        colIndex++;
                    }
                }

                AutoFitCol(ref ws);

                byte[] bin = p.GetAsByteArray();
                return bin;
            }
        }

        /// <summary>
        /// 把 List 轉換成 DataTable 
        /// </summary>
        /// <param name="dataList"></param>
        /// <returns></returns>
        private static DataTable ConvertToDataTable<T>(IEnumerable<T> dataList)
        {
            var type = typeof(T);
            var dt = new DataTable();

            var members = type.GetMembers()
                .Where(a => a.MemberType == MemberTypes.Property).ToList();

            //取得欄位 DisplayName
            members.ForEach(member =>
            {
                var colName = member.Name;
                var attr = member.CustomAttributes.FirstOrDefault();
                if (attr != null)
                {
                    if (attr.NamedArguments != null)
                    {
                        var nameArg = attr.NamedArguments.FirstOrDefault(a => a.MemberName == "Name");
                        colName = nameArg.TypedValue.Value == null ? string.Empty : nameArg.TypedValue.Value.ToString();
                    }
                }

                dt.Columns.Add(colName);
            });

            //取得欄位
            var memberNames = members.Select(a => a.Name).ToList();

            foreach (var item in dataList)
            {
                var dr = dt.NewRow();
                //把值塞到 DataRow
                for (int i = 0; i < memberNames.Count; i++)
                {
                    var memberName = memberNames[i];
                    dr[i] = item.GetType().InvokeMember(memberName, BindingFlags.GetProperty, null, item, null);
                }
                dt.Rows.Add(dr);
            }

            return dt;
        }

        /// <summary>
        /// 加上 Border
        /// </summary>
        /// <param name="border">Cell Border</param>
        private static void AddBorder(ref Border border)
        {
            border.Top.Style =
            border.Right.Style =
            border.Bottom.Style =
            border.Left.Style = ExcelBorderStyle.Thin;
        }

        /// <summary>
        /// 設定自動調整欄寬
        /// </summary>
        /// <param name="ws">ExcelWorksheet</param>
        private static void AutoFitCol(ref ExcelWorksheet ws)
        {
            int startCol = ws.Dimension.Start.Column;
            int endCol = ws.Dimension.End.Column;
            for (int i = startCol; i <= endCol; i++)
            {
                ws.Column(i).AutoFit();
            }
        }
    }
}