using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UploadFile.Model
{
    public class IModelNotSearch
    {
        public int ModifyKindInput { get; set; }
        public string ModifyUser { get; set; }
        public DateTime ModifyDate { get; set; }
        public int CreateKindInput { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        public int SysStatus { get; set; }

        public void SetDataIModelNotSearch(IModelNotSearch data)
        {
            if(data != null)
            {
                this.ModifyKindInput = data.ModifyKindInput;
                this.ModifyUser = data.ModifyUser;
                this.ModifyDate = data.ModifyDate;
                this.CreateKindInput = data.CreateKindInput;
                this.CreateDate = data.CreateDate;
                this.SysStatus = data.SysStatus;
            }    
        }
    }
}
