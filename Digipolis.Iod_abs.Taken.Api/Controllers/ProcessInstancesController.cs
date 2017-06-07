using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Digipolis.Iod_abs.Taken.Domain.QueryModel;
using Digipolis.Iod_abs.Taken.Manager.Interface;
using Microsoft.AspNetCore.Mvc;
using Narato.Common;
using Narato.Common.Factory;

namespace Digipolis.Iod_abs.Taken.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProcessInstancesController : Controller
    {
        private readonly IResponseFactory _responseFactory;
        private readonly IProcessInstanceManager _processInstanceManager;
        public ProcessInstancesController(IResponseFactory responseFactory, IProcessInstanceManager processInstanceManager)
        {
            _responseFactory = responseFactory;
            _processInstanceManager = processInstanceManager;
        }

        [HttpPost]
        public IActionResult GetTasksForProcessAndRole([FromBody]ProcessInstanceQueryModel processInstanceQueryModel)
        {
            return _responseFactory.CreatePostResponse(() => _processInstanceManager.CreateNewProcessInstance(processInstanceQueryModel.ProcessDefinitionId, processInstanceQueryModel.DossierId), this.GetRequestUri());
        }
    }
}
