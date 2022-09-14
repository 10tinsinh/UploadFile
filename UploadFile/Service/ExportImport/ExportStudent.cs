using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UploadFile.Model;

namespace UploadFile.Service.ExportImport
{
    public class ExportStudent
    {
        private void GetTitleColExcel(ref List<string> TitleCol)
        {
            var property = new StudentExportExcel();
            var data = property.GetType().GetProperties();
            foreach(var tmp in data)
            {
                TitleCol.Add(tmp.Name.ToString());
            }    
        }
        private void AddTitleColunm(ref ExcelWorksheet worksheet, List<string> listTitleCol, int maxColumn)
        {
            int row = 4;
            int maxCol = maxColumn;
            for(int i = 1; i<= maxCol; i++)
            {
                worksheet.Cells[row, i].Value = listTitleCol[i - 1];
            }    
        }

        private void AddDataColunm(ref ExcelWorksheet worksheet, List<StudentExportExcel> listData, int maxColumn, List<string> listTitleCol)
        {
            int row = 5;
            int maxCol = maxColumn;

            foreach(var tmp in listData)
            {
                var data = new StudentExportExcel(tmp);

                for(int i = 1; i<= maxCol; i++)
                {
                    var dataColumn = data.GetType().GetProperty(listTitleCol[i - 1]).GetValue(data);
                    worksheet.Cells[row, i].Value = dataColumn.ToString();
                    worksheet.Cells[row, i].Style.Font.SetFromFont(new Font("Arial", 14));
                    worksheet.Cells[row, i].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                    worksheet.Cells[row, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
                    worksheet.Cells[row, i].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                    worksheet.Cells[row, i].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                }
                row++;
            }    
        }

        public void CalExport(List<StudentExportExcel> data,ref MemoryStream stream)
        {
            try
            {
                var listData = data;
                //start export excel
                var xlPackage = new ExcelPackage(stream);
                using (xlPackage)
                {
                    //Define a worksheet
                    var worksheet = xlPackage.Workbook.Worksheets.Add("Student");

                    //Set Defaut All Column
                    worksheet.DefaultColWidth = 25;



                    //Style
                    using (var range = worksheet.Cells["A4:H4"])
                    {
                        // Set PatternType
                        range.Style.Fill.PatternType = ExcelFillStyle.DarkGray;
                        // Set Color for Background
                        range.Style.Fill.BackgroundColor.SetColor(Color.Green);
                        // Canh giữa cho các text
                        range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        // Set Font cho text  trong Range hiện tại
                        range.Style.Font.SetFromFont(new Font("Arial", 14));
                        // Set Border
                        range.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
                        range.Style.Border.Top.Style = ExcelBorderStyle.Thick;
                        range.Style.Border.Left.Style = ExcelBorderStyle.Thick;
                        range.Style.Border.Right.Style = ExcelBorderStyle.Thick;
                        // Set màu ch Border
                        range.Style.Border.Bottom.Color.SetColor(Color.Black);
                    }

                    //First row
                    var startRow = 5;
                    var row = startRow;

                    worksheet.Cells["A1"].Value = "File Export Student";
                    worksheet.Cells["A1"].Style.Font.SetFromFont(new Font("Arial", 20));
                    worksheet.Cells["A1"].Style.Font.SetFromFont(new Font("Arial", 20));

                    //Get Title Colunm
                    var listTitleCol = new List<string>();
                    GetTitleColExcel(ref listTitleCol);
                    int maxCol = listTitleCol.Count;

                    //Add Title Colunm
                    AddTitleColunm(ref worksheet, listTitleCol, maxCol);

                    //Add Data Colunm
                    AddDataColunm(ref worksheet, listData, maxCol, listTitleCol);


                    xlPackage.Workbook.Properties.Title = "Student List";
                    xlPackage.Workbook.Properties.Author = "Cuongmn";

                    xlPackage.Save();

                }
                stream.Position = 0;


            }
            catch(Exception ex)
            {
                
            }
        }
    }
}
