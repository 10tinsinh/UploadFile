using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UploadFile.Model
{
    public class Response
    {
        public string Success { get; set; }
        public Object Data { get; set; }
        public string Message { get; set; }

        public Response(string success, Object data ,string message)
        {
            this.Success = success;
            this.Data = data;
            this.Message = message;
        }
        public Response (string success, string message)
        {
            this.Success = success;
            this.Message = message;
        }
        public Response(string success, List<Object> data)
        {
            this.Success = success;
            this.Data = data;
        }

    }
}
