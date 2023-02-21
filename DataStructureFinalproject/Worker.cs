using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureFinalproject
{
    internal class Worker : Person, IComparable<Worker> 
    {
        double salary;
        public int CompareTo(Worker other)
        {
            if (this.Equals(other)) return 0;
            if (this is null) return -1;
            if (other is null) return 1;

            int result = this.name.CompareTo(other.name);
            return result != 0 ? result : this.salary.CompareTo(other.salary);
        }
        #region ctor
        public Worker(string name,double salary) : base(name)
        {
           SetSalary(salary);
        }
        
        public Worker (Worker obj): base(obj.GetName())
        {
            SetSalary(obj.GetSalary());
        }
        #endregion

        #region Get/Set
        public double GetSalary() { return salary; }
        public void SetSalary(double salary) { this.salary = salary; }

        public override string ToString()
        {
            string str =  $"{base.ToString()} ----> Salary: {salary}";
            return str;
        }

   
     

        #endregion
    }
}
