using System;
using System.Collections.Generic;
using System.Text;

using SQLite;

namespace Sensus.Model_data
{
    public class Registration
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public bool didSocial { get; set; }
        public int didMood { get; set; }
        public DateTime didDate { get; set; }
        public DateTime didEdit { get; set; }

 


    }
}
