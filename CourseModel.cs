using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16_db_csharp
{
    public class CourseModel
    {
        public int Id { get; set; }
        public string? Course_name { get; set; }
        public int Points { get; set; }
        public string? Start_date { get; set; }
        public string? End_date { get; set; }
    }
}
