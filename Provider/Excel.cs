using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Classes;
using OfficeOpenXml;

namespace Provider
{
    public class Excel
    {

        public static void CreateFile(string path, string name)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            var fileinfo = new FileInfo(path);

            if (fileinfo.Exists)
            {
                using (ExcelPackage p = new ExcelPackage(fileinfo))
                {
                    using (FileStream fs = new FileStream(name, FileMode.Create))
                    {
                        p.SaveAs(fs);
                    }

                }
            }
        }

        public static void Addrow(string path, int sheet, List<int> order, int node, int col)
        {
            var fileinfo = new FileInfo(path);

            if (fileinfo.Exists)
            {
                using (ExcelPackage p = new ExcelPackage(fileinfo))
                {
                    ExcelWorksheet ws = p.Workbook.Worksheets[sheet];

                    for (int i = 0; i < order.Count; i++)
                    {
                        //Insert Node-1 line
                        ws.InsertRow(order[i] + 1 + i * (node - 1), node - 1, order[i] + i * (node - 1));

                        //Copy 1st line to others line
                        for (int j = 0; j < (node - 1); j++)
                            ws.Cells[order[i] + i * (node - 1), 1, order[i] + i * (node - 1), col].Copy(ws.Cells[order[i] + i * (node - 1) + 1 + j, 1, order[i] + i * (node - 1) + 1 + j, col]);

                    }                    
                    p.Save();
                }
            }
        }

        public static void Addmultirow(string path, List<int> sheet, List<List<int>> order, int node, int col)
        {
            var fileinfo = new FileInfo(path);

            if (fileinfo.Exists)
            {
                using (ExcelPackage p = new ExcelPackage(fileinfo))
                {
                    ExcelWorksheet ws;
                    for (int k = 0; k < sheet.Count; k++)
                    {
                        ws = p.Workbook.Worksheets[sheet[k]];
                        for (int i = 0; i < order[k].Count; i++)
                        {
                            //Insert Node-1 line
                            ws.InsertRow(order[k][i] + 1 + i * (node - 1), node - 1, order[k][i] + i * (node - 1));

                            //Copy 1st line to others line
                            for (int j = 0; j < (node - 1); j++)
                                ws.Cells[order[k][i] + i * (node - 1), 1, order[k][i] + i * (node - 1), col].Copy(ws.Cells[order[k][i] + i * (node - 1) + 1 + j, 1, order[k][i] + i * (node - 1) + 1 + j, col]);

                        }
                    }
                    
                    p.Save();
                }
            }
        }

        public static void Fillby1list<T>(string name, int sheet, List<T> dt, int row, int col)
        {
            var fileinfo = new FileInfo(name);

            if (fileinfo.Exists)
            {
                using (ExcelPackage p = new ExcelPackage(fileinfo))
                {
                    ExcelWorksheet ws = p.Workbook.Worksheets[sheet];
                    ws.Cells[row, col].LoadFromCollection(dt);
                    //ws.Hidden = eWorkSheetHidden.VeryHidden;
                    p.Save();
                }
            }
        }

        public static void Fillbydatatable(string name, int sheet, DataTable dt, int row, int col)
        {
            var fileinfo = new FileInfo(name);

            if (fileinfo.Exists)
            {
                using (ExcelPackage p = new ExcelPackage(fileinfo))
                {
                    ExcelWorksheet ws = p.Workbook.Worksheets[sheet];
                    ws.Cells[row, col].LoadFromDataTable(dt, false);
                    //ws.Hidden = eWorkSheetHidden.VeryHidden;
                    p.Save();
                }
            }
        }

        public static void FillChecking<T>(string name, int sheet, List<T> list, int row1, int col1, DataTable dt, int row2, int col2)
        {
            var fileinfo = new FileInfo(name);

            if (fileinfo.Exists)
            {
                using (ExcelPackage p = new ExcelPackage(fileinfo))
                {
                    ExcelWorksheet ws = p.Workbook.Worksheets[sheet];
                    ws.Cells[row1, col1].LoadFromCollection(list);
                    ws.Cells[row2, col2].LoadFromDataTable(dt, false);
                    //ws.Hidden = eWorkSheetHidden.VeryHidden;
                    p.Save();
                }
            }
        }
    }
}
