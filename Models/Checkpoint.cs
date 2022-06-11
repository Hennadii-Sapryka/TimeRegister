using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeRegisterApp.Models
{
    public class Checkpoint
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public string MarkedTime { get; set; }
    }
}
