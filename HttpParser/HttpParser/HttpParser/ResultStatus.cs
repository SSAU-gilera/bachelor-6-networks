using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpParser
{
    class ResultStatus
    {
        public string Result { get; set; }
        public string Status { get; set; }
        public int Length { get; set; }

        public ResultStatus(string result, string status,int length)
        {
            Result = result;
            Status = status;
            Length = length;
        }

        public ResultStatus()
        {
        
        }
    }
}
