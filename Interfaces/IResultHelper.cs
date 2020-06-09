using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatTest.Interfaces
{
    public interface IResultHelper
    {
        Task<ObjectResult> Response(System.Net.HttpStatusCode statusCode, object data = null);
    }
}
