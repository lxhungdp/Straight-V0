using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tools
{
    public class Excel
    {
        public static void Insert(string path, int[] table, int node)
        {
            var fileinfo = new FileInfo(path);
            if (fileinfo.Exists)
            {
                using (ExcelPackage p = new ExcelPackage(fileinfo))
                {
                    ExcelWorksheet ws = p.Workbook.Worksheets[0];

                    for (int i = 0; i < table.GetLength(0); i++)
                    {
                        ws.InsertRow(table[i] + 1 + i * node, node, table[i] + i * node);
                        for (int j = 0; j < node; j++)
                            ws.Cells[table[i] + i * node, 1, table[i] + i * node, 15].Copy(ws.Cells[table[i] + i * node + 1 + j, 1, table[i] + i * node + 1 + j, 15]);
                    }

                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "Excel | *.xlsx";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        if (!string.IsNullOrEmpty(sfd.FileName))
                        {
                            using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create))
                            {
                                p.SaveAs(fs);
                            }
                        }
                    }
                }
            }
        }

        public static void Fill(string path, int[] table, int node)
        {
            var fileinfo = new FileInfo(path);
            if (fileinfo.Exists)
            {
                using (ExcelPackage p = new ExcelPackage(fileinfo))
                {
                    ExcelWorksheet ws = p.Workbook.Worksheets[0];

                    for (int i = 0; i < table.GetLength(0); i++)
                    {
                        ws.InsertRow(table[i] + 1 + i * node, node, table[i] + i * node);
                        for (int j = 0; j < node; j++)
                            ws.Cells[table[i] + i * node, 1, table[i] + i * node, 15].Copy(ws.Cells[table[i] + i * node + 1 + j, 1, table[i] + i * node + 1 + j, 15]);
                    }

                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "Excel | *.xlsx";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        if (!string.IsNullOrEmpty(sfd.FileName))
                        {
                            using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create))
                            {
                                p.SaveAs(fs);
                            }
                        }
                    }
                }
            }
        }

        public static void Fillwithdata(string path, int[] tableorder, int node, int[,] filllocation, DataTable filldata)
        {
            //tableorder = location of 1st row of data in the table
            // node = number of node (number of row)
            //filllocation: 1st column = order of table (table 1, table 2); 2nd row is the location of column in that table needed to be filled
            //filldata each column is data to fill
            
            var fileinfo = new FileInfo(path);
            
            if (fileinfo.Exists)
            {
                using (ExcelPackage p = new ExcelPackage(fileinfo))
                {
                    ExcelWorksheet ws = p.Workbook.Worksheets[0];

                    for (int i = 0; i < tableorder.GetLength(0); i++)
                    {
                        //Insert Node-1 line
                        ws.InsertRow(tableorder[i] + 1 + i * (node-1), node-1, tableorder[i] + i * (node-1));

                        //Copy 1st line to others line
                        for (int j = 0; j < (node-1); j++)
                            ws.Cells[tableorder[i] + i * (node-1), 1, tableorder[i] + i * (node-1), 15].Copy(ws.Cells[tableorder[i] + i * (node-1) + 1 + j, 1, tableorder[i] + i * (node-1) + 1 + j, 15]);

                       
                    }

                    for (int k = 0; k < filllocation.GetLength(0); k++)
                    {

                        List<Dim> list = filldata.AsEnumerable().Select(x => new Dim
                        {
                            v1 = (double)(x[k]),                           
                        }).ToList();                        
                        ws.Cells[tableorder[filllocation[k, 0] - 1] + (filllocation[k, 0] - 1) * (node - 1), filllocation[k, 1]].LoadFromCollection(list);
                        //ws.Cells[tableorder[filllocation[k, 0] - 1] + (filllocation[k, 0] - 1) * (node - 1), filllocation[k, 1]].LoadFromDataTable(filldata,false);
                    }


                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "Excel | *.xlsx";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        if (!string.IsNullOrEmpty(sfd.FileName))
                        {
                            using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create))
                            {
                                p.SaveAs(fs);
                            }
                        }
                    }
                }
            }
        }


    }
}
