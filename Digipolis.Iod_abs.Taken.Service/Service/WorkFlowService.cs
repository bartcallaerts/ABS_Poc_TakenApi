using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Digipolis.Iod_abs.Taken.Common;
using Digipolis.Iod_abs.Taken.Service.Interface;
using Digipolis.Iod_abs.Taken.Service.Model;
using Microsoft.Extensions.Options;
using Narato.Common;
using Narato.Common.Exceptions;
using Narato.Common.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Digipolis.Iod_abs.Taken.Service.Service
{
    public class WorkFlowService : IWorkflowService
    {
        private readonly HttpClient _client;
        private readonly ApiConfiguration _apiConfiguration;

        public WorkFlowService(HttpClient httpClient, IOptions<ApiConfiguration> config)
        {
            _client = httpClient;
            _apiConfiguration = config.Value;
        }

        public async Task<List<WorkflowTask>> GetTasksForDossierIdAndRole(int? processInstanceId, string role)
        {
            string url = $"runtime/tasks?candidateGroup={role}";
            if (processInstanceId.HasValue)
                url += $"&processInstanceId={processInstanceId.Value}";

            var response = await _client.GetAsync(url);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseString = response.Content.ReadAsStringAsync().Result;
                var tasksResponse = JsonConvert.DeserializeObject<WorkflowResponse<WorkflowTask>>(responseString);
                return tasksResponse.Data;
            }
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                throw new ExceptionWithFeedback(new FeedbackItem()
                {
                    Description = $"The tasks could not be retrieved. Received a bad request from Workflow engine.",
                    Type = FeedbackType.Error
                });
            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                throw new ExceptionWithFeedback(new FeedbackItem()
                {
                    Description =
                        $"The tasks could not be retrieved. Received an internal server error from workflow engine.",
                    Type = FeedbackType.Error
                });
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                throw new EntityNotFoundException($"The endpoint was not found");

            return null;
        }

        public async Task<WorkFlowForm> GetFormFieldsForTaskId(int taskId)
        {
            var response = await _client.GetAsync($"form/form-data?taskId={taskId}");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseString = response.Content.ReadAsStringAsync().Result;
                var tasksResponse = JsonConvert.DeserializeObject<WorkFlowForm>(responseString);
                return tasksResponse;
            }
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                throw new ExceptionWithFeedback(new FeedbackItem()
                {
                    Description =
                        $"The task form fields could not be retrieved. Received a bad request from Workflow engine.",
                    Type = FeedbackType.Error
                });
            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                throw new ExceptionWithFeedback(new FeedbackItem()
                {
                    Description =
                        $"The tasks form fields could not be retrieved. Received an internal server error from workflow engine.",
                    Type = FeedbackType.Error
                });
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                throw new EntityNotFoundException($"The taskid {taskId} could not be found");

            return null;
        }

        public async Task<List<WorkflowInstanceVariable>> GetProcessVariables(int processInstanceId)
        {
            var response = await _client.GetAsync($"runtime/process-instances/{processInstanceId}/variables");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseString = response.Content.ReadAsStringAsync().Result;
                var tasksResponse = JsonConvert.DeserializeObject<List<WorkflowInstanceVariable>>(responseString);
                return tasksResponse;
            }
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                throw new ExceptionWithFeedback(new FeedbackItem()
                {
                    Description =
                        $"The variables could not be retrieved. Received a bad request from Workflow engine.",
                    Type = FeedbackType.Error
                });
            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                throw new ExceptionWithFeedback(new FeedbackItem()
                {
                    Description =
                        $"The variables could not be retrieved. Received an internal server error from workflow engine.",
                    Type = FeedbackType.Error
                });
            //if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            //    throw new EntityNotFoundException($"The process variables could not be found");

            return null;
        }

        public async Task<List<WorkflowTaskVariable>> GetTaskVariables(int taskId)
        {
            var response = await _client.GetAsync($"runtime/tasks/{taskId}/variables");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseString = response.Content.ReadAsStringAsync().Result;
                var tasksResponse = JsonConvert.DeserializeObject<List<WorkflowTaskVariable>>(responseString);
                return tasksResponse;
            }
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                throw new ExceptionWithFeedback(new FeedbackItem()
                {
                    Description =
                        $"The variables could not be retrieved. Received a bad request from Workflow engine.",
                    Type = FeedbackType.Error
                });
            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                throw new ExceptionWithFeedback(new FeedbackItem()
                {
                    Description =
                        $"The variables could not be retrieved. Received an internal server error from workflow engine.",
                    Type = FeedbackType.Error
                });
            //if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            //    throw new EntityNotFoundException($"The process variables could not be found");

            return null;
        }

        public async Task<WorkflowInstanceOut> CreateProcessInstance(WorkflowInstanceIn workflowInstance)
        {
            workflowInstance.TenantId = _apiConfiguration.WorkflowEngineConfig.TenantId;
            var serialized = JsonConvert.SerializeObject(workflowInstance, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            var stringContent = new StringContent(serialized);

            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _client.PostAsync($"runtime/process-instances", stringContent);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseString = response.Content.ReadAsStringAsync().Result;
                var instanceResponse = JsonConvert.DeserializeObject<WorkflowInstanceOut>(responseString);
                return instanceResponse;
            }
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                throw new ExceptionWithFeedback(new FeedbackItem()
                {
                    Description =
                        $"The processInstance could not be created. Received a bad request from Workflow engine.",
                    Type = FeedbackType.Error
                });
            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                throw new ExceptionWithFeedback(new FeedbackItem()
                {
                    Description =
                        $"The processInstance could not be created. Received an internal server error from workflow engine.",
                    Type = FeedbackType.Error
                });
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                throw new EntityNotFoundException($"The endpoint to create the processInstance could not be found");

            return null;
        }

        public async Task<WorkflowInstanceOut> GetWorkflorInstanceByDossierId(Guid dossierId)
        {
            var variable = new WorkflowQueryVariable() { Name = "DossierId", Value = dossierId.ToString(), Operation = "equals", Type = "string" };

            var variables = new List<WorkflowQueryVariable>() { variable };

            var body = new {Variables = variables};

            var serialized = JsonConvert.SerializeObject(body, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            var stringContent = new StringContent(serialized);

            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _client.PostAsync($"query/process-instances", stringContent);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseString = response.Content.ReadAsStringAsync().Result;
                var instanceResponse = JsonConvert.DeserializeObject<WorkflowProcessQueryResult >(responseString);
                if (instanceResponse.Data.Any())
                    return instanceResponse.Data.FirstOrDefault();
                else
                {
                    throw new EntityNotFoundException($"A process instance with dossier Id {dossierId} was not found");
                }
            }
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                throw new ExceptionWithFeedback(new FeedbackItem()
                {
                    Description =
                        $"The processInstance could not be created. Received a bad request from Workflow engine.",
                    Type = FeedbackType.Error
                });
            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                throw new ExceptionWithFeedback(new FeedbackItem()
                {
                    Description =
                        $"The processInstance could not be created. Received an internal server error from workflow engine.",
                    Type = FeedbackType.Error
                });
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                throw new EntityNotFoundException($"The endpoint to create the processInstance could not be found");

            return null;

        }

       

        public async Task<WorkflowTaskVariable> UpdateTaskVariable(int id, WorkflowTaskVariable variable)
        {
            var payload =  new
            {
                Name = variable.Name,
                Value = variable.Value
            };

            var serialized = JsonConvert.SerializeObject(payload, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            var stringContent = new StringContent(serialized);

            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _client.PutAsync($"runtime/tasks/{id}/variables/{variable.Name}", stringContent);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseString = response.Content.ReadAsStringAsync().Result;
                var variablesResponse = JsonConvert.DeserializeObject<WorkflowTaskVariable>(responseString);
                return variablesResponse;
            }
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                throw new ExceptionWithFeedback(new FeedbackItem()
                {
                    Description =
                        $"The task variable {variable.Name} could not be updated. Received a bad request from Workflow engine.",
                    Type = FeedbackType.Error
                });
            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                throw new ExceptionWithFeedback(new FeedbackItem()
                {
                    Description =
                        $"The task variable {variable.Name} could not be updated. Received an internal server error from workflow engine.",
                    Type = FeedbackType.Error
                });
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                throw new EntityNotFoundException($"The endpoint to updae the task variables could not be found");

            return variable;

        }

        public async Task<WorkflowTask> CompleteTask(WorkflowTask workFlowTask)
        {
            var payloadAsJson = (new
            {
                Action = "complete",
                Variables = workFlowTask.Variables
            });


            var serialized = JsonConvert.SerializeObject(payloadAsJson, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            var stringContent = new StringContent(serialized);

            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _client.PostAsync($"runtime/tasks/{workFlowTask.Id}", stringContent);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseString = response.Content.ReadAsStringAsync().Result;
                var workFlowTaskResponse = JsonConvert.DeserializeObject<WorkflowTask>(responseString);
                return workFlowTaskResponse;
            }
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                throw new ExceptionWithFeedback(new FeedbackItem()
                {
                    Description =
                        $"The task could not be completed. Received a bad request from Workflow engine.",
                    Type = FeedbackType.Error
                });
            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                throw new ExceptionWithFeedback(new FeedbackItem()
                {
                    Description =
                        $"The task could not be completed. Received an internal server error from workflow engine.",
                    Type = FeedbackType.Error
                });
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                throw new EntityNotFoundException($"The endpoint to complete the task could not be found");

            if (response.StatusCode != HttpStatusCode.OK)
                throw new EntityNotFoundException($"The endpoint to complete the task could not be found, activity responsecode {response.StatusCode}");

            return workFlowTask;
        }
    }
}