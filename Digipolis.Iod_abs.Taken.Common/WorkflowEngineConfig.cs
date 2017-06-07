using System;
using System.Collections.Generic;
using System.Text;

namespace Digipolis.Iod_abs.Taken.Common
{
    public class WorkflowEngineConfig
    {
        public Guid ApiKey { get; set; }
        public Guid TenantId { get; set; }
        public string BaseUrl { get; set; }
    }
}
