using System;
using System.Collections.Generic;
using System.Text;

namespace Digipolis.Iod_abs.Taken.Service.Model
{
    public class WorkFlowForm
    {
        public string FormKey { get; set; }
        public int? DeploymentId { get; set; }
        public int? ProcessDefinitionId { get; set; }
        public int? TaskId { get; set; }
        public List<WorkFlowFormField> FormProperties { get; set; }
    }
}
