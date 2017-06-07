using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Digipolis.Iod_abs.Taken.Manager.Manager;
using Digipolis.Iod_abs.Taken.Service.Service;
using Xunit;

namespace Digipolis.Iod_abs.Taken.Test
{
    public class ProcessManagerTest
    {
        [Fact]
        public void CreateNewProcessInstanceTest()
        {
            //var procesInstanceManager = new ProcessInstanceManager(new WorkFlowService(new HttpClient(){BaseAddress = new Uri("https://api-gw-o.antwerpen.be/acpaas/workflow/v1/") }));
            //var process = procesInstanceManager.CreateNewProcessInstance("SWG-ET-POC", Guid.NewGuid());
            //Assert.NotNull(process);
            //Assert.True(process.Id != 0);
        }
    }
}
