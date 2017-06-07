using System;
using System.Collections.Generic;
using System.Text;

namespace Digipolis.Iod_abs.Taken.Service.Model
{
    public class WorkflowResponse<T>
    {
        public List<T> Data { get; set; }
        public int Total { get; set; }
        public int Start { get; set; }
        public string Sort { get; set; }
        public string Order { get; set; }
        public int Size { get; set; }

    }
}
