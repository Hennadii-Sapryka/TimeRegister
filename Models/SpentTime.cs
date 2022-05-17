using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeRegisterApp.Models
{
    public class SpentTime
    {
        public int Id { get; set; }
        public string Duration { get; set; }
        public List<Project> Projects { get; set; } = new();

    }
}
