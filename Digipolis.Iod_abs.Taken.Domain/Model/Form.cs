using System.Collections.Generic;

namespace Digipolis.Iod_abs.Taken.Domain.Model
{
    public class Form
    {
        public string FormKey { get; set; }
        public List<TaskVariable> FormFields{ get; set; }

        public Form()
        {
            FormFields = new List<TaskVariable>();
        }
    }
}
