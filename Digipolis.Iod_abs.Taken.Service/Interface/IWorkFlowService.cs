using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Digipolis.Iod_abs.Taken.Service.Model;

namespace Digipolis.Iod_abs.Taken.Service.Interface
{
    public interface IWorkflowService
    {
        Task<List<WorkflowTask>> GetTasksForDossierIdAndRole(int? processInstanceId, string role);
        Task<WorkFlowForm> GetFormFieldsForTaskId(int taskId);
        Task<WorkflowInstanceOut> CreateProcessInstance(WorkflowInstanceIn workflowInstance);
        Task<WorkflowTask> CompleteTask(WorkflowTask workFlowTask);
        Task<WorkflowTaskVariable> UpdateTaskVariable(int id, WorkflowTaskVariable variable);
        Task<WorkflowInstanceOut> GetWorkflorInstanceByDossierId(Guid dossierId);
        Task<List<WorkflowInstanceVariable>> GetProcessVariables(int processInstanceId);
        Task<List<WorkflowTaskVariable>> GetTaskVariables(int taskId);
    }
}
