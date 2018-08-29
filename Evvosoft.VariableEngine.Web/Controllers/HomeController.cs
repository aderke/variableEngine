using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Evvosoft.ExtractVariables.Service.Interfaces;
using Evvosoft.ReplaceVariables.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Evvosoft.VariableEngine.Web.Models;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;

namespace Evvosoft.VariableEngine.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly int LongRunningProcessInSec = 1;

        private readonly string SessionIdToken = "session-id";

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<List<string>> ExtractVariables(ApiExpressionAddRequest request)
        {
            var userId = Guid.NewGuid().ToString();
            Response.Cookies.Append(SessionIdToken, userId);

            var extractActor = GetExtractActor(userId);
            var replaceActor = GetReplaceActor(userId);

            var variables = await extractActor.ExtractVariables(request.Expression);
            // save variables between actors
            await replaceActor.SaveVariables(variables);

            Thread.Sleep(TimeSpan.FromSeconds(LongRunningProcessInSec));

            return variables.Variables;
        }

        [HttpPost]
        public async Task<string> ReplaceVariables(ApiExpressionGetRequest request)
        {
            // add validation here for replace values if need
            var userId = Request.Cookies[SessionIdToken];

            if (string.IsNullOrEmpty(userId))
                Response.Cookies.Append(SessionIdToken, userId);

            var replaceActor = GetReplaceActor(userId);

            return await replaceActor.ReplaceVariables(request.Variables);
        }

        #region Private Methods

        private IExtractVariablesActor GetExtractActor(string userId)
        {
            return ActorProxy.Create<IExtractVariablesActor>(new ActorId(userId), 
                new Uri("fabric:/EvvosoftVariableEngine/ExtractVariablesActorService"));
        }

        private IReplaceVariablesActor GetReplaceActor(string userId)
        {
            return ActorProxy.Create<IReplaceVariablesActor>(new ActorId(userId), 
                new Uri("fabric:/EvvosoftVariableEngine/ReplaceVariablesActorService"));
        }

        #endregion
    }
}

