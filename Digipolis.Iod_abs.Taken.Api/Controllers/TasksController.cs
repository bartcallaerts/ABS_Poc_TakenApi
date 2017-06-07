using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Digipolis.Iod_abs.Taken.Domain.Dto;
using Digipolis.Iod_abs.Taken.Domain.Model;
using Digipolis.Iod_abs.Taken.Domain.QueryModel;
using Digipolis.Iod_abs.Taken.Manager.Interface;
using Microsoft.AspNetCore.Mvc;
using Narato.Common;
using Narato.Common.Factory;

namespace Digipolis.Iod_abs.Taken.Api.Controllers
{
    [Route("api/[controller]")]
    public class TasksController : Controller
    {
        private readonly IResponseFactory _responseFactory;
        private readonly ITaskManager _taskManager;
        public TasksController(IResponseFactory responseFactory, ITaskManager taskManager)
        {
            _responseFactory = responseFactory;
            _taskManager = taskManager;
        }

        [HttpGet]
        public IActionResult GetTasksForProcessAndRole(TaskQueryModel taskQueryModel, [FromQuery] int? page, [FromQuery(Name = "page-size")] int? pageSize)
        {
            return _responseFactory.CreateGetResponseForCollection(() => _taskManager.GetTasksForDOssierIdAndRole(taskQueryModel.DossierId, taskQueryModel.Role, page, pageSize), this.GetRequestUri());
        }

        [HttpPut("{id}")]
        public IActionResult CompleteTask(int id, [FromBody] List<TaskVariableDto> taskVariableDto)
        {
            return _responseFactory.CreatePostResponse(() => _taskManager.CompleteTask(id,taskVariableDto.Select(x => new TaskVariable(){Name = x.Name, Value = x.Value}).ToList()), this.GetRequestUri());
        }

        [HttpPut("{id}/variables")]
        public IActionResult UpdateTaskVariables(int id, [FromBody] List<TaskVariableDto> taskVariableDto)
        {
            return _responseFactory.CreatePostResponse(() => _taskManager.UpdateTaskVariables(id, taskVariableDto.Select(x => new TaskVariable() { Name = x.Name, Value = x.Value }).ToList()), this.GetRequestUri());
        }
    }
}
