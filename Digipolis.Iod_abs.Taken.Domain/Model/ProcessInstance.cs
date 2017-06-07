using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;

namespace Digipolis.Iod_abs.Taken.Domain.Model
{
    public class ProcessInstance
    {
        public int Id { get; set; }
        public string ProcessDefinitionKey { get; set; }
        public string BusinessKey { get; set; }
        public Guid TenantKey { get; set; }
        public List<ProcessVariable> Variables { get; set; }

        public ProcessInstance()
        {
            Variables = new List<ProcessVariable>();
        }
    }
}
