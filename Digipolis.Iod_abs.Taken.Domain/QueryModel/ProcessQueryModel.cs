using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace Digipolis.Iod_abs.Taken.Domain.QueryModel
{
    public class ProcessInstanceQueryModel
    {
        [Required]
        public string ProcessDefinitionId { get; set; }
        [Required]
        public Guid DossierId { get; set; }
    }
}
