using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureFinalproject
{
    abstract internal class Person 
    {
        protected string name;
        #region ctor
        protected Person(string name)
        {
            this.name = name;
        }
        #endregion
        #region Get/Set
        public string GetName() { return name; }
        public void SetName(string name) { this.name = name; }
        public override string ToString()
        {
            return $"{name}";
        }
        #endregion
    }
}
