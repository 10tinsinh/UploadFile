using Microsoft.AspNetCore.Http;
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
        private readonly ImportStudent _import;

        public StudentService(IStudent student)
        {
            _student = student;
            _export = new ExportStudent();
            _import = new ImportStudent();
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
                string ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                #region "Save file to project"
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "File", excelname);
                using (var fileStream = new FileStream(fullPath, FileMode.CreateNew, FileAccess.ReadWrite))
                {
                    stream.CopyTo(fileStream); // fileStream is not populated
                }
                #endregion "Save file to project"

                #region "Get link file"
                string link = fullPath;
                var listLink = new List<string>();
                listLink.AddRange(link.Split("\\"));
                int count = listLink.Count();
                string linkFile = "";
                for(int i = 0; i<count;i++)
                {
                    if(i == 0)
                    {
                        linkFile += listLink[i];
                    }
                    else
                    {
                        linkFile += "/" + listLink[i];
                    }
                    
                }
                #endregion "Get link file"
                return new ResponseExcel(excelname, stream, ContentType, linkFile);
                
            }
            catch
            {
                return new ResponseExcel("", new MemoryStream(), "", "");
            }
        }

        public async Task<string> ImportExcel(IFormFile file)
        {
            try
            {
                string kq = "";
                
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "File", file.FileName);

                if(file != null)
                {
                    

                    #region "Read file"
                    var list = new List<StudentExportExcel>();
                    kq = _import.CalImport(file, ref list);
                    if(kq != "")
                    {
                        return kq;
                    }
                    else
                    {
                        if(list != null && list.Count > 0)
                        {
                            foreach(var temp in list)
                            {
                                var dataTemp = new StudentNoIdModel(temp);
                                await Create(dataTemp);
                            }

                            #region "Save File to server"
                            using (var fileStream = new FileStream(fullPath, FileMode.CreateNew, FileAccess.ReadWrite))
                            {
                                await file.CopyToAsync(fileStream); // fileStream is not populated
                            }
                            #endregion "Save File to server"
                        }
                        else
                        {
                            return "Import False!";
                        }    
                        return kq;
                    }
                    #endregion "Read file"

                }
                return "File invalid!";
            }
            catch
            {
                return "Import False!";
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
