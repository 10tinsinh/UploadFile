using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UploadFile.Model;
using UploadFile.Repositories;

namespace UploadFile.Service
{
    public class StudentSevice : IStudentSevice
    {
        private readonly IStudent _student;

        public StudentSevice(IStudent student)
        {
            _student = student;
        }
        public async Task Create(StudentNoIdModel student)
        {
            try
            {
                
            }
            catch(Exception ex)
            {

            }
        }

        public Task Delete(string code)
        {
            throw new NotImplementedException();
        }

        public Task<List<StudentNoIdModel>> GetData()
        {
            throw new NotImplementedException();
        }

        public Task<StudentNoIdModel> GetOne(string code)
        {
            throw new NotImplementedException();
        }

        public Task Update(string code, StudentNoIdModel data)
        {
            throw new NotImplementedException();
        }
    }
}
