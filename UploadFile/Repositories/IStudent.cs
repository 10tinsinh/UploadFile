using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UploadFile.Model;
using UploadFile;

namespace UploadFile.Repositories
{
    public interface IStudent
    {
        public Task<List<StudentModel>> GetData();
        public Task Create(StudentModel characterModel);
        public Task<StudentModel> GetOne(string sysCode);
        public Task RemoveAsync(string sysCode);
    }
}
