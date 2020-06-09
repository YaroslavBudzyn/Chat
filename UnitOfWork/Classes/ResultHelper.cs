using ChatTest.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ChatTest.UnitOfWork.Classes
{
    public class ResultHelper : IResultHelper
    {
        public async Task<ObjectResult> Response(HttpStatusCode statusCode, object data = null)
        {
            var task = new Task<ObjectResult>(() => new ObjectResult(data)
            {
                StatusCode = Convert.ToInt32(statusCode)

            });

            task.Start();
            return await task;
        }
    }
}
