using System;
using System.Collections.Generic;
using System.Text;
using Digipolis.Iod_abs.Taken.Domain.Model;

namespace Digipolis.Iod_abs.Taken.Manager.Interface
{
    public interface IProcessInstanceManager
    {
        ProcessInstance CreateNewProcessInstance(string processDefinitionKey, Guid dossierId);
    }
}
