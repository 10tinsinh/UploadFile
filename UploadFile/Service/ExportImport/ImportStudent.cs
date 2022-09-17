using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UploadFile.Model;

namespace UploadFile.Service.ExportImport
{
    public class ImportStudent
    {
        public static bool IsValidDate(string date)
        {
            DateTime tempObject;
            return DateTime.TryParseExact(date, "dd/mm/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tempObject);
        }
        public static void ReadCode(ExcelWorksheet worksheet, int row, ref int col,ref StudentExportExcel data, ref string kq)
        {
            try
            {
                var value = worksheet.Cells[row, col].Value.ToString().Trim();
                if(value != null)
                {
                    data.Code = value;
                    
                }
                else
                {
                    if (kq != "") kq += ", ";
                    kq += "Value column Code row " + row.ToString() + " invalid";
                }
                col++;
            }
            catch
            {
                if (kq != "") kq += ", ";
                kq += "Value column Code row " + row.ToString() + " invalid";
                col++;
            }
        }
        public static void ReadName(ExcelWorksheet worksheet, int row, ref int col, ref StudentExportExcel data, ref string kq)
        {
            try
            {
                var value = worksheet.Cells[row, col].Value.ToString().Trim();
                if (value != null)
                {
                    data.Name = value;
                }
                else
                {
                    if (kq != "") kq += ", ";
                    kq += "Value column Name row " + row.ToString() + " invalid";
                }
                col++;
            }
            catch
            {
                if (kq != "") kq += ", ";
                kq += "Value column Name row " + row.ToString() + " invalid";
                col++;
            }
        }
        public static void ReadClass(ExcelWorksheet worksheet, int row, ref int col, ref StudentExportExcel data, ref string kq)
        {
            try
            {
                var value = worksheet.Cells[row, col].Value.ToString().Trim();
                if (value != null)
                {
                    data.Class = value;
                }
                else
                {
                    if (kq != "") kq += ", ";
                    kq += "Value column Class row " + row.ToString() + " invalid";
                }
                col++;
            }
            catch
            {
                if (kq != "") kq += ", ";
                kq += "Value column Class row " + row.ToString() + " invalid";
                col++;
            }
        }
        public static void ReadAge(ExcelWorksheet worksheet, int row, ref int col, ref StudentExportExcel data, ref string kq)
        {
            try
            {
                var value = worksheet.Cells[row, col].Value.ToString().Trim();
                int age = int.Parse(value);
                if (age != 0)
                {
                    data.Age = age;
                }
                else
                {
                    if (kq != "") kq += ", ";
                    kq += "Value column Age row " + row.ToString() + " invalid";
                }
                col++;
            }
            catch
            {
                if (kq != "") kq += ", ";
                kq += "Value column Age row " + row.ToString() + " invalid";
                col++;
            }
        }
        public static void ReadBirthDay(ExcelWorksheet worksheet, int row, ref int col, ref StudentExportExcel data, ref string kq)
        {
            try
            {
                var value = worksheet.Cells[row, col].Value.ToString().Trim();
                if (value != "")
                {
                    DateTime date = DateTime.ParseExact(value, "dd/mm/yyyy", null);
                    if (IsValidDate(value))
                    {
                        data.BirthDay = date;
                    }    
                    else
                    {
                        if (kq != "") kq += ", ";
                        kq += "Value column BirthDay row " + row.ToString() + " invalid. BirthDay is 'dd/mm/yyyy' ";
                    }    
                }
                else
                {
                    if (kq != "") kq += ", ";
                    kq += "Value column BirthDay row " + row.ToString() + " invalid. BirthDay is 'dd/mm/yyyy' ";
                }
                col++;
            }
            catch
            {
                if (kq != "") kq += ", ";
                kq += "Value column BirthDay row " + row.ToString() + " invalid. BirthDay is 'dd/mm/yyyy' ";
                col++;
            }
        }
        public static void ReadAddress(ExcelWorksheet worksheet, int row, ref int col, ref StudentExportExcel data, ref string kq)
        {
            try
            {
                var value = worksheet.Cells[row, col].Value.ToString().Trim();
                if (value != null)
                {
                    data.Address = value;
                }
                else
                {
                    if (kq != "") kq += ", ";
                    kq += "Value column Address row " + row.ToString() + " invalid";
                }
                col++;
            }
            catch
            {
                if (kq != "") kq += ", ";
                kq += "Value column Address row " + row.ToString() + " invalid";
                col++;
            }
        }
        public static void ReadPhoneNumber(ExcelWorksheet worksheet, int row, ref int col, ref StudentExportExcel data, ref string kq)
        {
            try
            {
                var value = worksheet.Cells[row, col].Value.ToString().Trim();
                if (value != null)
                {
                    data.Address = value;
                }
                else
                {
                    if (kq != "") kq += ", ";
                    kq += "Value column PhoneNumber row " + row.ToString() + " invalid";
                }
                col++;
            }
            catch
            {
                if (kq != "") kq += ", ";
                kq += "Value column PhoneNumber row " + row.ToString() + " invalid";
                col++;
            }
        }
        public static void ReadNote(ExcelWorksheet worksheet, int row, ref int col, ref StudentExportExcel data, ref string kq)
        {
            try
            {
                var value = worksheet.Cells[row, col].Value.ToString().Trim();
                if (value != null)
                {
                    data.Note = value;
                }
                else
                {
                    if (kq != "") kq += ", ";
                    kq += "Value column Note row " + row.ToString() + " invalid";
                }
                col++;
            }
            catch
            {
                if (kq != "") kq += ", ";
                kq += "Value column Note row " + row.ToString() + " invalid";
                col++;
            }
        }
        public void Validate(ExcelWorksheet worksheet, ref StudentExportExcel data, int row, ref string kq)
        {
            int col = 1;
            ReadCode(worksheet, row, ref col, ref data, ref kq);
            ReadName(worksheet, row, ref col, ref data, ref kq);
            ReadClass(worksheet, row, ref col, ref data, ref kq);
            ReadAge(worksheet, row, ref col, ref data, ref kq);
            ReadBirthDay(worksheet, row, ref col, ref data, ref kq);
            ReadAddress(worksheet, row, ref col, ref data, ref kq);
            ReadPhoneNumber(worksheet, row, ref col, ref data, ref kq);
            ReadNote(worksheet, row, ref col, ref data, ref kq);
        }
        public string CalImport(IFormFile file, ref List<StudentExportExcel> list)
        {
            try
            {
                string kq = "";
                using (var stream = new MemoryStream())
                {
                    file.CopyToAsync(stream);

                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        var rowcount = worksheet.Dimension.Rows;
                        
                        for (int row = 2; row <= rowcount; row++)
                        {
                            var data = new StudentExportExcel();
                            Validate(worksheet, ref data, row, ref kq);
                            if (kq != "")
                            {
                                return kq;
                            }
                            list.Add(data);
                        }
                    }
                }

                return kq;
            }
            catch
            {
                return "False";
            }
            
        }
    }
}
