using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Planner_SLR
{
    class Planner
    {
        //Get and Set let you read and write values
        public string Description { get; set; }
        //Get allows you to read only the value. DateTime is also a class. It contains the time (including the date)
        public DateTime CreatedAt { get; }
        //Constructor
        public Planner(string description)
        {
            Description = description;
            //DateTime.Now returns the current time
            CreatedAt = DateTime.Now;
        }
    }
}