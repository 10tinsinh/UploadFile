using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UploadFile.Model
{
    public class StudentModel:StudentNoIdModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _Id { get; set; }

        public StudentModel() { }
        public StudentModel(StudentModel data)
        {
            this.SetData(data);
        }
        public StudentModel(StudentNoIdModel data)
        {
            this.SetData(data);
        }


    }
    public class StudentNoIdModel:IModelNotSearch
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public int Age { get; set; }
        public DateTime BirthDay { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Note { get; set; }

        public StudentNoIdModel() { }
        public StudentNoIdModel(StudentNoIdModel data)
        {
            this.SetData(data);
        }
        public StudentNoIdModel(StudentModel data)
        {
            this.SetData(data);
        }
        public void SetData(StudentNoIdModel data)
        {
            if(data != null)
            {
                this.Code = data.Code;
                this.Name = data.Name;
                this.Class = data.Class;
                this.Age = data.Age;
                this.BirthDay = data.BirthDay;
                this.Address = data.Address;
                this.PhoneNumber = data.PhoneNumber;
                this.Note = data.Note;
                base.SetDataIModelNotSearch(data);
            }    
        }
    }
    public class StudentExportExcel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public int Age { get; set; }
        public DateTime BirthDay { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Note { get; set; }

        public StudentExportExcel() { }

        public StudentExportExcel(StudentNoIdModel data)
        {
            this.Code = data.Code;
            this.Name = data.Name;
            this.Class = data.Class;
            this.Age = data.Age;
            this.BirthDay = data.BirthDay;
            this.Address = data.Address;
            this.PhoneNumber = data.PhoneNumber;
            this.Note = data.Note;

        }
    }

    
}
