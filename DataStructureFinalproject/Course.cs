using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureFinalproject
{
    internal class Course
    {
        int courseCode;
        int courseGrade;

        #region ctor
        public Course(int courseCode, int grade)
        {
            this.courseCode = courseCode;
            this.courseGrade = grade;
        }


        #endregion
        #region Get/Set
        public int GetGrade() { return this.courseGrade; }
        public void SetGrade(int grade) { this.courseGrade = grade; }

        public int GetCourseCode() { return this.courseCode; }
        public void SellGrade(int grade) { this.courseGrade = grade; }
        public override string ToString()
        {
            return $"Code: {courseCode} --> {courseGrade}";
        }
        #endregion
    }
}
