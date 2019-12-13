using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudAPI.Models
{
    public class TodoElement
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool Checked { get; set; }
        public string Name { get; set; }
    }
}
