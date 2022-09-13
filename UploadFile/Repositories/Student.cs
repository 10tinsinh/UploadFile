using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UploadFile.ConnectionDB;
using UploadFile.Model;

namespace UploadFile.Repositories
{
    public class Student : IStudent
    {
        private readonly IMongoCollection<StudentModel> _studentCollection;
        public Student(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _studentCollection = database.GetCollection<StudentModel>(mongoDBSettings.Value.CollectionName = "UpLoad_Student");
        }

        public async Task Create(StudentModel student) =>
            await _studentCollection.InsertOneAsync(student);

        public async Task<List<StudentModel>> GetData() =>
            await _studentCollection.Find(_ => true).ToListAsync();

        public async Task<StudentModel> GetOne(string _id) =>
            await _studentCollection.Find(e => e._Id == _id).FirstOrDefaultAsync();


        public async Task RemoveAsync(string _id) =>
           await _studentCollection.DeleteOneAsync(e => e._Id == _id);
    }
}
