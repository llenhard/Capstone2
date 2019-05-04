using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone_2
{
    class Task
    {
        public string Name;
        public string Desc;
        public DateTime Due;
        public bool Complete;

        public Task(string name, string desc, DateTime due)
        {
            Name = name;
            Desc = desc;
            Due = due.Date;
        }

        public void ToggleComplete()
        {
            Complete = Complete ? false : true;
            
            return;
        }
    }
}
