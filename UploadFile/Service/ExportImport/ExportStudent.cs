using OfficeOpenXml;
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
        private void AddTitleColunm(ref ExcelWorksheet worksheet, List<string> listTitleCol)
        {
            var maxcol = listTitleCol.Count();


        }

        public async Task CalExport(List<StudentExportExcel> data)
        {
            try
            {
                //start export excel
                var stream = new MemoryStream();

                var xlPackage = new ExcelPackage(stream);
                using (xlPackage)
                {
                    //Define a worksheet
                    var worksheet = xlPackage.Workbook.Worksheets.Add("Student");

                    //Style
                    var customStyle = xlPackage.Workbook.Styles.CreateNamedStyle("CustomStyle");
                    customStyle.Style.Font.UnderLine = true;
                    customStyle.Style.Font.Color.SetColor(Color.Red);

                    //First row
                    var startRow = 5;
                    var row = startRow;

                    worksheet.Cells["A1"].Value = "File Export Student";

                    //Get Title Colunm
                    var listTitleCol = new List<string>();
                    GetTitleColExcel(ref listTitleCol);

                    //Add Title Colunm
                    AddTitleColunm(ref worksheet, listTitleCol);

                }

            }
            catch(Exception ex)
            {

            }
        }
    }
}
