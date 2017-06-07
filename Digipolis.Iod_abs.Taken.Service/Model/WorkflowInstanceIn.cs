using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;
using Digipolis.Iod_abs.Taken.Service.Model;

namespace Digipolis.Iod_abs.Taken.Service.Model
{
    public class WorkflowInstanceIn
    {
        public string ProcessDefinitionKey { get; set; }
        public string BusinessKey { get; set; }
        public Guid TenantId { get; set; }
        public List<WorkflowInstanceVariable> Variables { get; set; }

        public WorkflowInstanceIn()
        {
            Variables = new List<WorkflowInstanceVariable>();
            ProcessDefinitionKey = string.Empty;
            BusinessKey = string.Empty;
        }
    }
}
