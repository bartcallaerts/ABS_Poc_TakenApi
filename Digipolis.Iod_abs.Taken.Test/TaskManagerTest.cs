using System;
using System.Linq;
using System.Net.Http;
using Digipolis.Iod_abs.Taken.Manager.Manager;
using Digipolis.Iod_abs.Taken.Service.Service;
using Xunit;

namespace Digipolis.Iod_abs.Taken.Test
{
    public class TaskManagerTest
    {
        [Fact]
        public void GetTasksForRoleAndProcessInstanceIdTest()
        {
            //var taskManager = new TaskManager(new WorkFlowService(new HttpClient(){BaseAddress = new Uri("https://api-gw-o.antwerpen.be/acpaas/workflow/v1/")}));
            //var tasks = taskManager.GetTasksForProcessInstanceIdAndRole(4654, "Aanvrager");
            //Assert.True(tasks.Any());
        }
    }
}
