using System;
using System.Collections.Generic;
using System.Text;
using Digipolis.Iod_abs.Taken.Service.Model;

namespace Digipolis.Iod_abs.Taken.Service.Model
{
    public class WorkflowTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProcessInstanceId { get; set; }
        public List<WorkflowTaskVariable> Variables { get; set; }

        public WorkflowTask()
        {
            Variables = new List<WorkflowTaskVariable>();
        }
    }
}
