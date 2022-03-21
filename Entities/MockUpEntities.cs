using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class Staff
    {
        public int StaffID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Position { get; set; }
    }

    public class Assessment
    {
        public int ID { get; set; }
        public int StaffID { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Goal { get; set; }
        public string Activity { get; set; }
        public string Remarks { get; set; }
        public int Score { get; set; }
        public int Rating { get; set; }        
        public DateTime DateCreated { get; set; }
    }

    public class NewAssessment
    {
        public IEnumerable<NewGoal> goals { get; set; }       
    }

    public class NewGoal
    {
        public int id { get; set; }
        public string type { get; set; }
        public string goal { get; set; }
        //public string measures { get; set; }
        public string activity { get; set; }
        public string comment { get; set; }
        public int? score { get; set; }
        public int? rating { get; set; }
    }

    public class NewPerformance
    {
        public string Goal { get; set; }
        public string Measures { get; set; }
        public string Comment { get; set; }
        public int Score { get; set; }
        public int Rating { get; set; }
    }

    public class NewPersonal
    {
        public string Goal { get; set; }
        public string Activity { get; set; }
        public string Comment { get; set; }
        public int Score { get; set; }
        public int Rating { get; set; }
    }

    public class NewCareer
    {
        public string Goal { get; set; }
        public string Activity { get; set; }
        public string Comment { get; set; }
        public int Score { get; set; }
        public int Rating { get; set; }
    }
}
