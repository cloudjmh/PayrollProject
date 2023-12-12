using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haag_CourseProject_Part2
{
    [Serializable]
    public abstract class Employee
    {
        // attributes
        private string firstName;
        private string lastName;
        private string ssn;
        private DateTime hireDate;
        private Benefits benefits;
        // constructors
        public Employee()
        {
            firstName = "N/A";
            lastName = "N/A";
            ssn = "N/A";
            hireDate = new DateTime();
            benefits = new Benefits();
        }

        public Employee(string firstName, string lastName, string ssn, DateTime hireDate, Benefits benefits)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.ssn = ssn;
            this.hireDate = hireDate;
            this.benefits = benefits;
        }

        // behaviors
        public override string ToString()
        {
            return "First Name=" + firstName + ", Last Name=" + lastName + ", SSN =" + ssn 
                + ", Hire Date=" + hireDate.ToShortDateString()
                + ",Health Ins=" + benefits.HealthInsurance
                + ", Life Ins=" + benefits.LifeInsurance
                + ", Vacations=" + benefits.Vacation;
        }

        public abstract double CalculatePay();
       

        // properties
        public string FirstName
        {
            get { return firstName; } 
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public string SSN
        {
            get { return ssn; }
            set { ssn = value; }
        }

        public DateTime HireDate
        {
            get { return hireDate; }
            set { hireDate = value; }
        }

        public Benefits BenefitsEmp
        {
            get { return benefits; }
            set { benefits = value; }
        }

    }
}
