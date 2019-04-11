using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ClassEquipment
{
    public class Equipment
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int ClassroomID { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
    }
}
