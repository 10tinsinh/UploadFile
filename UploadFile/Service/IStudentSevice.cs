using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UploadFile.Model;

namespace UploadFile.Service
{
    public interface IStudentSevice
    {
        public Task<List<StudentNoIdModel>> GetData();
        public Task Create(StudentNoIdModel student);
        public Task<StudentNoIdModel> GetOne(string code);
        public Task Delete(string code);
        public Task Update(string code, StudentNoIdModel data);

    }
}
