using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaOne.Entities.DbSet
{
    public class Driver : BaseEntity
    {
        public Driver() 
        {
            Achivements = new List<Achivement>();
        }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int DriverNumber { get; set; }
        public DateTime DOB { get; set; }
        public List<Achivement>? Achivements { get; set; }
    }
}
