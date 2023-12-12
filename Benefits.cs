using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haag_CourseProject_Part2
{
    [Serializable]
    public class Benefits
        //class attributes
    {
        private string healthInsurance;
        private int lifeInsurance;
        private int vacation;


        //class constructors
        public Benefits()
        {
            healthInsurance = "N/A";
            lifeInsurance = 0;
            vacation = 0;
        }

        public Benefits(string healthInsurance, int lifeInsurance, int vacation)
        {
            this.healthInsurance = healthInsurance;
            this.lifeInsurance = lifeInsurance;
            this.vacation = vacation;
        }

        // class behaviors
        public override string ToString()
        { 
            return "health insurance=" + healthInsurance +
                ", life insurance="+lifeInsurance
                + ", vacation=" + vacation;
        }
        //properties
        public string HealthInsurance
        { 
            get { return healthInsurance; } 
            set { healthInsurance = value; }
        }

        public int LifeInsurance
        { 
            get { return lifeInsurance;}
            set { lifeInsurance = value;}
        }

        public int Vacation
        {
            get { return vacation; }    
            set { vacation = value; }
        }


    }
}
