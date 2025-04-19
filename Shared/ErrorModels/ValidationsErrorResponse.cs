using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ErrorModels
{
    public class ValidationsErrorResponse
    {
        public int StatuCode {  get; set; }=(int)HttpStatusCode.BadRequest;


        public string ErrorMessage { get; set; } = "Validation Failed";

        public IEnumerable<ValidationsError> Errors { get; set; }


        
    }
}
