using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Digipolis.Iod_abs.Taken.Domain.Model;
using Digipolis.Iod_abs.Taken.Manager.Interface;
using Digipolis.Iod_abs.Taken.Service;
using Digipolis.Iod_abs.Taken.Service.Interface;
using Digipolis.Iod_abs.Taken.Service.Model;
using Microsoft.AspNetCore.Mvc;
using Narato.Common.Models;

namespace Digipolis.Iod_abs.Taken.Manager.Manager
{
    public class TaskManager : ITaskManager
    {
        private readonly IWorkflowService _workflowService;

        public TaskManager(IWorkflowService workflowService)
        {
            _workflowService = workflowService;
        }

        public PagedCollectionResponse<IEnumerable<Task>> GetTasksForDOssierIdAndRole(Guid dossierId, string role, int? page, int? size)
        {
            var tasks = new List<Task>();

            WorkflowInstanceOut processInstance = null;
            List<WorkflowTaskVariable> variables = null;

            if (dossierId != Guid.Empty)
            {
                processInstance = _workflowService.GetWorkflorInstanceByDossierId(dossierId).Result;
                
                
            }
            var workflowTasks =_workflowService.GetTasksForDossierIdAndRole(processInstance != null ? processInstance.Id : (int?)null, role);

           foreach (var workflowTask in workflowTasks.Result)
            {
                variables = _workflowService.GetTaskVariables(workflowTask.Id).Result;

                //Create the task
                var task = new Task
                {
                    Id = workflowTask.Id,
                    Name = workflowTask.Name,
                    ProcessInstanceId = workflowTask.ProcessInstanceId
                };
                //Add to the list
                tasks.Add(task);

                //Get the formfields
                var form = _workflowService.GetFormFieldsForTaskId((workflowTask.Id)).Result;

                task.Form = new Form(){FormKey = form.FormKey};

                if (form != null)
                {
                    //First loop over the formfields and create them in the task & find the value in the variables list of the worklow task
                    foreach (var workFlowFormField in form.FormProperties)
                    {
                        //Create the formfield
                        var formField = new TaskVariable()
                        {
                            Id = workFlowFormField.Id,
                            Name = workFlowFormField.Name,
                            Type = workFlowFormField.Type,
                            Readable = workFlowFormField.Readable,
                            Required = workFlowFormField.Required,
                            Writable = workFlowFormField.Writable,
                            EnumValues = workFlowFormField.EnumValues
                        };

                        //Find the variable in the task that holds the value
                        var valueVariable = variables.FirstOrDefault(x => x.Name == workFlowFormField.Id);
                        if (valueVariable != null)
                            formField.Value = valueVariable.Value;

                        task.Form.FormFields.Add(formField);
                    }
                }

                //Next find the variables that are not formfields and add these to the taskvariable collection
                foreach (var workflowVariable in variables.Where(x => task.Form == null || task.Form.FormFields.All(y => x.Name != y.Id)))
                {
                    //Create the formfield
                    var taskVariable = new TaskVariable()
                    {
                        Id = workflowVariable.Name,
                        Name = workflowVariable.Name,
                        Type = workflowVariable.Type,
                        Value = workflowVariable.Value
                    };

                    task.Variables.Add(taskVariable);
                }
            }
            var pageCollection = new PagedCollectionResponse<IEnumerable<Task>>();
            pageCollection.Data = tasks;

            return pageCollection;
        }

        public List<TaskVariable> CompleteTask(int id, List<TaskVariable> taskVariables)
        {
            var workflowTask = new WorkflowTask();
            workflowTask.Id = id;
            workflowTask.Variables = taskVariables.Select(x => new WorkflowTaskVariable() {Name = x.Name, Type = x.Type, Value = x.Value}).ToList();
            _workflowService.CompleteTask(workflowTask);
            return taskVariables;
        }

        public List<TaskVariable> UpdateTaskVariables(int id, List<TaskVariable> taskVariables)
        {
            foreach (var taskVariable in taskVariables)
            {
                _workflowService.UpdateTaskVariable(id, new WorkflowTaskVariable() { Name = taskVariable.Name, Type = taskVariable.Type, Value = taskVariable.Value });
            }
            
            return taskVariables;
        }
    }
}
