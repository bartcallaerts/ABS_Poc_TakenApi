using System;
using System.Collections.Generic;
using System.Text;

namespace Digipolis.Iod_abs.Taken.Domain.Model
{
    public class TaskVariable
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool Readable { get; set; }
        public bool Writable { get; set; }
        public bool Required { get; set; }
        public List<string> EnumValues{ get; set; }
        public  string Value { get; set; }

        public TaskVariable()
        {
            EnumValues = new List<string>();
        }
    }
}
