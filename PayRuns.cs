using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Demo.PolymorphicFuncs
{
    public static class PayRuns
    {
        private static List<PayRun> _payruns = new List<PayRun>();

        [FunctionName(nameof(GetPayruns))]
        public static async Task<IActionResult> GetPayruns(
            [HttpTrigger(
                AuthorizationLevel.Function, "get", 
                Route = "client/{clientId:int}/payroll/{payrollId}/paygroup/{paygroupId}/payrun")
            ] HttpRequest req,
            int? clientId,
            int? payrollId,
            int? paygroupId,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            return await Task.FromResult(new OkObjectResult(_payruns));
        }

        [FunctionName(nameof(GetPayrun))]
        public static async Task<IActionResult> GetPayrun(
            [HttpTrigger(
                AuthorizationLevel.Function, "get", 
                Route = "client/{clientId:int}/payroll/{payrollId}/paygroup/{paygroupId}/payrun/{payrunId}")
            ] HttpRequest req,
            int? clientId,
            int? payrollId,
            int? paygroupId,
            int? payrunId,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var payrun = _payruns.FirstOrDefault(x => x.Id == payrunId);

            if (payrun == null)
            {
                return await Task.FromResult(new NotFoundResult());
            }
            
            return await Task.FromResult(new OkObjectResult(payrun));
        }

        [FunctionName(nameof(PostPayrun))]
        public static async Task<IActionResult> PostPayrun(
            [HttpTrigger(
                AuthorizationLevel.Function, "post", 
                Route = "client/{clientId:int}/payroll/{payrollId}/paygroup/{paygroupId}/payrun")
            ] HttpRequest req,
            int? clientId,
            int? payrollId,
            int? paygroupId,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic payrun = JsonConvert.DeserializeObject(requestBody);

            var deserializer = PayRunDeserializerFactory.GetDeserializer(payrun);
            PayRun response = deserializer.Deserialize(requestBody);
            response.Id = Math.Abs(Guid.NewGuid().GetHashCode());

            _payruns.Add(response);

            var location = $"/api/client/1/payroll/2/paygroup/3/payrun/{response.Id}";
            return await Task.FromResult(new CreatedResult(location, response));
        }
    }
}