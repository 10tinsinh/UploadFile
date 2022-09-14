using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UploadFile.Model;
using UploadFile.Repositories;
using UploadFile.Service.ExportImport;

namespace UploadFile.Service
{
    public class StudentService : IStudentService
    {
        private readonly IStudent _student;
        private readonly ExportStudent _export;

        public StudentService(IStudent student)
        {
            _student = student;
            _export = new ExportStudent();
        }
        public async Task<StudentNoIdModel> Create(StudentNoIdModel student)
        {
            try
            {
                var dataCheck = new StudentNoIdModel(student);
                var data = new StudentModel(dataCheck);

                await _student.Create(data);

                List<StudentModel> dataNew = await _student.GetData();
                var result = dataNew.Find(e => e.Code == data.Code);
                if (result == null)
                {
                    throw new Exception();
                }
                return result;
            }
            catch(Exception ex)
            {
                return new StudentNoIdModel();
            }
        }

        public async Task Delete(string code)
        {
            try
            {
                string codeCheck = code;
                List<StudentModel> data = await _student.GetData();
                var dataCheck = data.Find(e => e.Code == codeCheck);
                if (dataCheck == null)
                {
                    throw new Exception();
                }
                await _student.RemoveAsync(dataCheck._Id);
            }
            catch(Exception ex)
            {

            }
            
        }

        public async Task<ResponseExcel> ExportExcel(string code)
        {
            try
            {
                var result = new List<StudentExportExcel>();
                var dataAll = await _student.GetData();
                if(!String.IsNullOrEmpty(code))
                {
                    var dataCheck = dataAll.Find(e => e.Code == code);
                    if(dataCheck != null)
                    {
                        var dataTmp = new StudentNoIdModel(dataCheck);
                        result.Add(new StudentExportExcel(dataTmp));
                    }    
                }
                else
                {
                    var dataTmp = new List<StudentNoIdModel>(dataAll);
                    foreach(var tmp in dataTmp)
                    {
                        result.Add(new StudentExportExcel(tmp));
                    }    
                }
                var stream = new MemoryStream();
                 _export.CalExport(result, ref stream);

                string excelname = $"ExportFile-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
                string http = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";



                return new ResponseExcel(excelname, stream, http);
                
            }
            catch
            {
                return new ResponseExcel("", new MemoryStream(), "");
            }
        }

        public async Task<List<StudentNoIdModel>> GetData()
        {
            try
            {
                var dataCheck = await _student.GetData();
                if(dataCheck != null && dataCheck.Count >0)
                {
                    var data = new List<StudentNoIdModel>(dataCheck);
                    return data;
                } 
                else
                {
                    return new List<StudentNoIdModel>();
                }    
            }
            catch(Exception ex)
            {
                return new List<StudentNoIdModel>();
            }
        }

        public async Task<StudentNoIdModel> GetOne(string code)
        {
            try
            {
                string codeCheck = code;
                List<StudentModel> data = await _student.GetData();
                var dataCheck = data.Find(e => e.Code == codeCheck);
                if (dataCheck == null)
                {
                    throw new Exception();
                }
                var result = await _student.GetOne(dataCheck._Id);
                return new StudentNoIdModel(result);
            }
            catch (Exception ex)
            {
                return new StudentNoIdModel();
            }
        }

        public Task Update(string code, StudentNoIdModel data)
        {
            throw new NotImplementedException();
        }
    }
}
