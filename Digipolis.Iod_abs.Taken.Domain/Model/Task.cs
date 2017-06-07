using System;
using System.Collections.Generic;
using Digipolis.Iod_abs.Taken.Domain.Model;
using Digipolis.Iod_abs.Taken.Service.Model;

namespace Digipolis.Iod_abs.Taken.Domain.Model
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProcessInstanceId { get; set; }
        public List<TaskVariable> Variables{ get; set; }
        public Form Form { get; set; }

        public Task()
        {
            Variables = new List<TaskVariable>();
        }
    }
}
