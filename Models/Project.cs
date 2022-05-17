using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace TimeRegisterApp.Models
{
    public class Project
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public SpentTime SpentTime { get; set; }
        public List<Checkpoint> Checkpoints { get; set; } = new();
    }   
}
