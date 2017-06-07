using System;
using System.Collections.Generic;
using System.Text;
using Digipolis.Iod_abs.Taken.Domain.Model;
using Narato.Common.Models;

namespace Digipolis.Iod_abs.Taken.Manager.Interface
{
    public interface ITaskManager
    {
        PagedCollectionResponse<IEnumerable<Task>> GetTasksForDOssierIdAndRole(Guid dossierId, string role, int? page, int? size);
        List<TaskVariable> CompleteTask(int id, List<TaskVariable> taskvariables);
        List<TaskVariable> UpdateTaskVariables(int id, List<TaskVariable> taskVariables);
    }
}
