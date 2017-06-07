using System;
using System.Collections.Generic;
using System.Text;

namespace Digipolis.Iod_abs.Taken.Service.Model
{
    public class WorkflowInstanceOut
    {
        public int Id { get; set; }
        public string ProcessDefinitionKey { get; set; }
        public string BusinessKey { get; set; }
        public Guid TenantKey { get; set; }
        public List<WorkflowInstanceVariable> Variables { get; set; }

        public WorkflowInstanceOut()
        {
            Variables = new List<WorkflowInstanceVariable>();
            ProcessDefinitionKey = string.Empty;
            BusinessKey = string.Empty;
        }
    }
}
