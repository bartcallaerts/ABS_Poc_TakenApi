using System;
using System.Collections.Generic;
using System.Text;
using Digipolis.Iod_abs.Taken.Domain.Model;
using Digipolis.Iod_abs.Taken.Manager.Interface;
using Digipolis.Iod_abs.Taken.Service.Interface;
using Digipolis.Iod_abs.Taken.Service.Model;

namespace Digipolis.Iod_abs.Taken.Manager.Manager
{
    public class ProcessInstanceManager : IProcessInstanceManager
    {
        private readonly IWorkflowService _workflowService;
        public ProcessInstanceManager(IWorkflowService workflowService)
        {
            _workflowService = workflowService;
        }

        public ProcessInstance CreateNewProcessInstance(string processDefinitionKey, Guid dossierId)
        {
            var workflowInstance = new WorkflowInstanceIn();
            workflowInstance.ProcessDefinitionKey = processDefinitionKey;
            workflowInstance.Variables.Add(new WorkflowInstanceVariable(){Name = "DossierId", Value = dossierId});

            var returnInstance = _workflowService.CreateProcessInstance(workflowInstance).Result;

            var processInstance = new ProcessInstance(){Id = returnInstance.Id, BusinessKey = returnInstance.BusinessKey, ProcessDefinitionKey = returnInstance.ProcessDefinitionKey, TenantKey = returnInstance.TenantKey};
            returnInstance.Variables.ForEach(x => processInstance.Variables.Add(new ProcessVariable(){Name = x.Name, Value = x.Value}));

            return processInstance;
        }
    }
}
