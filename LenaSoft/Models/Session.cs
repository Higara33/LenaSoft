using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LenaSoft.Models
{
    public class Session
    {
        public string Name { get; set; }
        public int Pit { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime BusinessDay { get; set; }
    }
}
