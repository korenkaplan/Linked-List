using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureFinalproject
{
    internal class Student : Person
    {
        Node<Course> courseList;

        #region Ctor
        public Student(string name) : base(name)
        {
            SetCourseList(new Node<Course>());
        }
        public Student(string name, Node<Course> courseList) : base(name)
        {
            SetCourseList(courseList);
        }
        public Student(Student obj):base(obj.GetName())
        {
            SetCourseList(obj.GetCourseList());
        }
        public string GetGradesToPrint()
        {
            return $@"{base.ToString()}'s Grades:
{courseList}";

        }
        public override string ToString()
        {
            return base.ToString();
        }
        #endregion

        #region Get/Set
        public void SetCourseList(Node<Course> courseList) { this.courseList = courseList; }
        public Node<Course> GetCourseList() { return this.courseList; }
        #endregion
    }
}
