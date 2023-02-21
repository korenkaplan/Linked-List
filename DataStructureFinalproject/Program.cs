using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace DataStructureFinalproject
{
    internal class Program
    {
        #region Questions 1-17 
        public static int GetListLength<T>(Node<T> currant)
        {
            if (currant is null)
            {
                return 0;
            }
            return 1 + GetListLength(currant.GetNext());
        } //#1 (#2 is head.toString())
        public static Node<T> AddFirst<T>(Node<T> list, T value)
        {
            Node<T> newNode = new Node<T>(value);
            if (list == null)
            {
                list = newNode;
                return list;
            }
            newNode.SetNext(list);
            list = newNode;
            return list;
        } // #3
        public static Node<T> AddLast<T>(Node<T> list, T value)
        {
            if (list.GetValue() == null && !list.HasNext())
            {
                list.SetValue(value);
                return list;
            }
            if (!list.HasNext())
            {
                list.SetNext(new Node<T>(value));
                return list;
            }
            AddLast(list.GetNext(), value);
            return list;
        } //#4
        public static Node<T> AddLast<T>(Node<T> list, Node<T> newTail)
        {
            if (list != null && !list.HasNext())
            {
                newTail.SetNext(null);
                list.SetNext(newTail);
                return list;
            }
            AddLast(list.GetNext(), newTail);
            return list;
        } //#4
        public static void AddAfter<T>(Node<T> prev, T value)
        {
            Node<T> newNode = new Node<T>(value);
            newNode.SetNext(prev.GetNext());
            prev.SetNext(newNode);
        } //#5
        public static Node<T> DeleteFirst<T>(Node<T> list)
        {
            if (list == null || !list.HasNext())
            {
                return null;
            }
            Node<T> newHead = list.GetNext();
            list.SetNext(null);
            return newHead;

        } //#6
        public static void DeleteLast<T>(Node<T> list)
        {

            Node<T> next = list.GetNext();
            if (!next.HasNext())
            {
                list.SetNext(null);
                return;
            }
            DeleteLast<T>(list.GetNext());
            return;
        } //#7
        public static void DeleteAfter<T>(Node<T> prev)
        {
            Node<T> prevNextNode = prev.GetNext();
            prev.SetNext(prevNextNode.GetNext());
            prevNextNode.SetNext(null);
        }//#8
        public static T GetHeadValue<T>(Node<T> head)
        {
            return head.GetValue();
        }//#9
        public static T GetTailValue<T>(Node<T> list)
        {
            if (!list.HasNext())
            {
                return list.GetValue();
            }
            return GetTailValue(list.GetNext());
        }//#10
        public static T GetValueByIndex<T>(Node<T> list, int index)
        {
            try
            {
                return GetValueByIndexAction(list, index);
            }
            catch
            {
                Console.WriteLine("The index you tring to get is out of range, returned first value.");
                return list.GetValue();
            }
        } // #11
        public static T GetValueByIndexAction<T>(Node<T> list, int index)
        {
            if (list != null && index == 0)
            {
                return list.GetValue();
            }
            return GetValueByIndexAction<T>(list.GetNext(), index - 1);
        } //#11
        public static bool IsValueExists<T>(Node<T> list, T value) where T : IComparable<T>
        {
            if (list is null)
            {
                return false;
            }
            return list.GetValue().CompareTo(value) == 0 ? true : IsValueExists(list.GetNext(), value);
        }//# 12

        #region Question 13 check if the list is circular
        public static bool IsSinglyLinked<T>(Node<T> list)
        {
            if (list is null)
            {
                Console.WriteLine("Must be at least one nodes");
                return false;
            }
            return IsSinglyLinkedtAction<T>(list, list);
        }
        public static bool IsSinglyLinkedtAction<T>(Node<T> head, Node<T> currant)
        {
            if (!currant.HasNext()) { return false; }
            Node<T> next = currant.GetNext();
            return next.Equals(head) ? true : IsSinglyLinkedtAction(head, next);
        }
        public static void MakeListSinglyLinked<T>(Node<T> head, Node<T> currant)
        {
            if (!currant.HasNext())
            {
                currant.SetNext(head);
                return;
            }
            MakeListSinglyLinked(head, currant.GetNext());
        }// for testing the function make the linked list singly lilnked
        /*         //Syntax for validation in main
       Node<int> head = new Node<int>(15);
       head = AddLast<int>(head, 30);
           head = AddLast<int>(head, 20);
           head = AddLast<int>(head, 15);
           head = AddLast<int>(head, 20);
           head = AddLast<int>(head, 20);
           Console.WriteLine(IsSinglyLinked(head));
           MakeListSinglyLinked(head, head);
       Console.WriteLine(IsSinglyLinked(head));
           //expect false \n true*/
        #endregion
        #region Question 14 Delete repeated nodes with value
        public static void DeleteAllRepeatedValue<T>(Node<T> list) where T : IComparable<T>
        {
            if (list is null || !list.HasNext())
            {
                return;
            }
            DeleteAllNodesWithValue(list, list.GetValue());
            DeleteAllRepeatedValue<T>(list.GetNext());
        } // 14 main - function will loop through the list
        public static void DeleteAllNodesWithValue<T>(Node<T> list, T value) where T : IComparable<T>
        {
            Node<T> next = list.GetNext();
            if (next is null) // if null finish
                return;

            else if (next.GetValue().CompareTo(value) == 0)
            {
                DeleteAfter(list);
                DeleteAllNodesWithValue(list, value);
            }
            else
                DeleteAllNodesWithValue(next, value);


        } // second function - each round will delete the all the repeated values from currant and forward

        /*//Syntax for validation in main
          Node<int> head = new Node<int>(15);
          head = AddLast<int>(head, 30);
              head = AddLast<int>(head, 20);
              head = AddLast<int>(head, 15);
              head = AddLast<int>(head, 20);
              head = AddLast<int>(head, 20);
              Console.WriteLine(head);
              DeleteAllRepeatedValue(head);
          Console.WriteLine(head);*/
        #endregion
        #region Question 15 Return Duplicate list in new location in memory
        public static Node<T> ReturnADuplicateList<T>(Node<T> list)
        {
            Node<T> newHead = new Node<T>();
            if (list != null)
            {
                newHead.SetValue(list.GetValue());
                return DuplicateNodesAction(list.GetNext(), newHead);
            }
            else
                return newHead;
        }
        public static Node<T> DuplicateNodesAction<T>(Node<T> list, Node<T> newList)
        {
            if (list is null)
            {
                return newList;
            }
            newList = AddLast(newList, list.GetValue());
            return DuplicateNodesAction(list.GetNext(), newList);
        }
        /*// Syntax for validation in main
         Node<int> head = new Node<int>(15);
         head = AddLast<int>(head, 30);
             head = AddLast<int>(head, 20);
             Node<int> single = ReturnADuplicateList(head);
         Console.WriteLine(head);
             Console.WriteLine(single);
             Console.WriteLine(((Object) head).Equals(single));*/
        #endregion
        #region Question 16 Reverse list
        public static Node<T> ReverseList<T>(Node<T> head)
        {
            if (head is null || !head.HasNext())
                return head;
            Node<T> rest = ReverseList(head.GetNext());
            head.GetNext().SetNext(head);
            head.SetNext(null);
            return rest;
        }
        public static Node<T> ReverseListWithOutput<T>(Node<T> head)
        {
            if (head is null || !head.HasNext())
            {
                Console.WriteLine("rest from last round: " + head);
                Console.WriteLine("----------------------------------------------");
                return head;

            }
            Node<T> rest = ReverseList(head.GetNext());
            Console.WriteLine("before head: " + head);
            Console.WriteLine("before rest: " + rest);
            head.GetNext().SetNext(head);
            head.SetNext(null);
            Console.WriteLine("after head: " + head);
            Console.WriteLine("after rest: " + rest);
            Console.WriteLine("----------------------------------------------");
            return rest;
        }
        /*//syntax for validation in main
        Node<int> head = new Node<int>(5);
        head = AddLast(head,10);
        head = AddLast(head, 15);
        Console.WriteLine(head);
            head = ReverseList(head);
        Console.WriteLine(head);*/
        #endregion
        #region Question 17 sort list
        public static Node<T> MergeSort<T>(Node<T> head) where T : IComparable<T>
        {
            if (head == null || !head.HasNext()) // if length is 0 or 1 than its aleady sorted
            {
                return head;
            }

            // devide the list into two seperate lists
            Node<T> middle = GetMiddleNode(head);
            Node<T> nextOfMiddle = middle.GetNext();
            middle.SetNext(null);

            Node<T> left = MergeSort(head); // devide the head (left) list until it at length of 1
            Node<T> right = MergeSort(nextOfMiddle);// devide the nextOfMiddle (rigth) list until it at length of 1
            Node<T> sortedList = MergeSortedLists(left, right); // merge the two sorted lists together
            return sortedList;
        }
        public static Node<T> MergeSortedLists<T>(Node<T> left, Node<T> right) where T : IComparable<T>
        {
            Node<T> result = null;
            if (left == null) // if we finished with all the nodes in left or right add the remaining nodes of the other  array without checking because its already sorted.
            {
                return right;
            }
            if (right == null)
            {
                return left;
            }

            if (left.GetValue().CompareTo(right.GetValue()) <= 0) // if the value of the node in left array is smaller add to main array and replace with next node
            {
                result = left;
                result.SetNext(MergeSortedLists(left.GetNext(), right));
            }
            else // else the value of the node in right array is smaller, add to main array and replace with next node
            {
                result = right;
                result.SetNext(MergeSortedLists(left, right.GetNext()));
            }
            return result;
        }
        public static Node<T> GetMiddleNode<T>(Node<T> head)
        {
            if (head == null)
            {
                return head;
            }

            Node<T> slow = head;
            Node<T> fast = head;

            while (fast.GetNext() != null && fast.GetNext().GetNext() != null)
            {
                slow = slow.GetNext();
                fast = fast.GetNext().GetNext();
            }

            return slow;
        }
        /*        //syntax for validation
        Node<string> head = new Node<string>("zambia");
        head = AddLast(head, "argentina");
        head = AddLast(head, "brazil");
        Node<int> head2 = new Node<int>(20);
        head2 = AddLast(head2, 10);
        head2 = AddLast(head2, 12);

        Console.WriteLine("Original linked lists:");
            Console.WriteLine(head);
            Console.WriteLine(head2);

            head = MergeSort(head);
        head2 = MergeSort(head2);
        Console.WriteLine("-------------------------------------");
            Console.WriteLine("Sorted linked lists:");
            Console.WriteLine(head);
            Console.WriteLine(head2);*/
        #endregion
        #endregion
        #region Questions 18-21
        #region Question 18 check if two lists are equal in value and size
        public static bool IsListEquals<T>(Node<T> list1, Node<T> list2) where T : IComparable<T>
        {
            if (list1 is null && list2 is null) { return true; }
            else if (list1 is null || list2 is null) { return false; }
            else if (GetListLength(list1) != GetListLength(list2)) { return false; }
            return IsListEqualsAction(list1, list2);

        }

        public static bool IsListEqualsAction<T>(Node<T> list1, Node<T> list2) where T : IComparable<T>
        {
            if (list1.GetValue().CompareTo(list2.GetValue()) != 0) { return false; }
            return IsListEquals(list1.GetNext(), list2.GetNext());
        }
        /* // Syntax for validation
                    Node<int> same1 = new Node<int>(20);
            same1 = AddLast(same1, 10);
            same1 = AddLast(same1, 12);

            Node<int> same2= new Node<int>(20);
            same2 = AddLast(same2, 10);
            same2 = AddLast(same2, 12);

            Node<int> sameWithDifferentLength = new Node<int>(20);
            sameWithDifferentLength = AddLast(sameWithDifferentLength, 10);
            sameWithDifferentLength = AddLast(sameWithDifferentLength, 12);
            sameWithDifferentLength = AddLast(sameWithDifferentLength, 10);

            Node<int> different = new Node<int>(20);
            different = AddLast(different, 10);
            different = AddLast(different, 13);
            Console.WriteLine("lists Value:");
            Console.WriteLine("same1 --> "+ same1);
            Console.WriteLine("same2 --> " + same2);
            Console.WriteLine("sameWithDifferentLength --> " + sameWithDifferentLength);
            Console.WriteLine("different --> " + different);
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("Compare same1 and same2 --> " + IsListEquals(same1, same2));
            Console.WriteLine("Compare sameWithDifferentLength and same1 --> " + IsListEquals(same1, sameWithDifferentLength));
            Console.WriteLine("Compare diffarent and same1 --> " + IsListEquals(different, same1));
            Console.WriteLine("Compare diffarent and same2 --> " + IsListEquals(different, same2));*/
        #endregion
        #region Question 19 Merge Two Lists And return a new list
        public static Node<T> MergeTwoLists<T>(Node<T> list1, Node<T> list2)
        {
            if (list1 is null && !(list2 is null)) return list2;

            else if (list2 is null && !(list1 is null)) return list1;

            else if (list1 is null && list2 is null) return null;
            Node<T> joinedList = ReturnADuplicateList(list1);

            MergeTwoListsAction(joinedList, list2);
            return joinedList;
        }

        public static void MergeTwoListsAction<T>(Node<T> list1, Node<T> list2)
        {
            if (!list1.HasNext())
            {
                list1.SetNext(list2);
                return;
            }
           MergeTwoListsAction(list1.GetNext(), list2);
             
        }

        /*        //syntax for validation
        Node<int> list1 = new Node<int>(1);
        list1 = AddLast(list1, 2);
        list1 = AddLast(list1, 3);

        Node<int> list2 = new Node<int>(4);
        list2 = AddLast(list2, 5);
        list2 = AddLast(list2, 6);
        Console.WriteLine("Lists value:");
            Console.WriteLine("List 1 --> " + list1);
            Console.WriteLine("List 2 --> " + list2);
            Console.WriteLine("-----------------------------------------------");
            list1 = MergeTwoLists(list1, list2);
        Console.WriteLine("list1 after connection to list2 --> " + list1);*/
        #endregion
        #region Question 20 Merge to lists without repeats
        public static Node<T> MergeTwoListsWithNoRepeats<T>(Node<T> list1, Node<T> list2) where T : IComparable<T>
        {
            list1 = MergeTwoLists<T>(list1, list2);
            DeleteAllRepeatedValue(list1);
            return list1;
        }
        /*//syntax for validation
        Node<int> list1 = new Node<int>(1);
        list1 = AddLast(list1, 2);
        list1 = AddLast(list1, 3);

        Node<int> list2 = new Node<int>(2);
        list2 = AddLast(list2, 3);
        list2 = AddLast(list2, 6);
        Console.WriteLine("Lists value:");
            Console.WriteLine("List1 --> " + list1);
            Console.WriteLine("List2 --> " + list2);
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("expected output: 1, 2, 3, 6");
            Console.WriteLine("-----------------------------------------------");
            list1 = MergeTwoListsWithNoRepeats(list1, list2);
        Console.WriteLine("list1 value after connection to list2 with no repeats --> " + list1);*/
        #endregion
        #region Question 21 Merge two lists, return a new list thet contains the common values only
        public static Node<T> MergeListsCommonValueOnly<T>(Node<T> list1, Node<T> list2) where T : IComparable<T>
        {
            Node<T> temp1 = ReturnADuplicateList(list1);
            Node<T> temp2 = ReturnADuplicateList(list2);
            DeleteAllRepeatedValue(temp1);
            DeleteAllRepeatedValue(temp2);
            temp1 = MergeTwoLists(temp1, temp2);
            temp1 = MergeSort(temp1);
            Node<T> newList = new Node<T>();
            return MergeListsCommonValueOnlyAction(temp1, newList);

        }
        public static Node<T> MergeListsCommonValueOnlyAction<T>(Node<T> list, Node<T> newList) where T : IComparable<T>
        {
            if (list is null ||!list.HasNext())
            {
                return DeleteFirst(newList);
            }
            Node<T> next = list.GetNext();
            if (list.GetValue().CompareTo(next.GetValue()) == 0)
            {
                newList = AddLast(newList, list);
                next = next.GetNext();
            }
            return MergeListsCommonValueOnlyAction(next, newList);
        }

        /*       //syntax valdation
                   Node<int> list1 = new Node<int>(1);
            Node<int> list2 = new Node<int>(3);

            for (int i = 1, k =3; i < 5; i++,k++)
            {
                for (int j = 0; j < 2; j++)
                {
                    list1 = AddLast(list1, i);
                    list2 = AddLast(list2, k);
                }
            }
            Console.WriteLine("Lists value:");
            Console.WriteLine("List1 --> " + list1);
            Console.WriteLine("List2 --> " + list2);
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("expected output: 3, 4");
            Console.WriteLine("-----------------------------------------------");
            Node<int> connectedRepeatsOnly = MergeListsCommonValueOnly(list1, list2); 
            Console.WriteLine("connectedRepeatsOnly value --> " + connectedRepeatsOnly);*/

        #endregion
        #endregion
        #region Questions 23-25
        #region Question 23 Print Student Grade Avarage
        public static string GetAvarageGradeList(Node<Student> list, string result= "" )
        {
            if (list is null) { return result; }
            result += $"{list.GetValue().GetName()} --> Avarge: {GetStudentAvarageGrade(list.GetValue()):f1} \n";
            return GetAvarageGradeList(list.GetNext(),result);
        }
        private static double GetStudentAvarageGrade(Student currant)
        {
            double sum = GetStudentSumOfGrades(currant.GetCourseList());
            int amount = GetListLength(currant.GetCourseList());
            return sum / amount;
        }
        private static double GetStudentSumOfGrades(Node<Course> courses)
        {
            if (!courses.HasNext())
            {
                return courses.GetValue().GetGrade();
            }
            return courses.GetValue().GetGrade() + GetStudentSumOfGrades(courses.GetNext());
        }
        /*    //Syntax for validation
            Node<Course> korenGrades = new Node<Course>(new Course(1, 99));
            korenGrades = AddLast(korenGrades, new Course(2, 89));
            korenGrades = AddLast(korenGrades, new Course(3, 95));

            Node<Course> OfriGrades = new Node<Course>(new Course(1, 100));
            OfriGrades = AddLast(OfriGrades, new Course(2, 85));
            OfriGrades = AddLast(OfriGrades, new Course(3, 95));

            Node<Student> listOfStudents = new Node<Student>(new Student("koren", korenGrades));
            listOfStudents = AddLast(listOfStudents, new Student("Ofri", OfriGrades));
            Console.WriteLine(GetAvarageGradeList(listOfStudents));*/
        #endregion
        #region Question 24 Get a list of students with the best grades

        public static Node<Student> GetTopStudents(Node<Student[]> allClasses) // main function, returns a list of top students
        {
            Node<Student> topStudents = new Node<Student>();
            if (allClasses is null) { return topStudents; }

            return GetTopStudentsAction(allClasses, topStudents);
        }

        private static Node<Student> GetTopStudentsAction(Node<Student[]> allClasses, Node<Student> topStudents) // loop through the list of classes and return a list of the top students ffrom each class
        {
            if (allClasses is null) {
                return topStudents;
            }
            Student topFromClass = GetTopStudentFromClass(allClasses.GetValue());
            topStudents = AddLast(topStudents, topFromClass);
            return GetTopStudentsAction(allClasses.GetNext(), topStudents);
        }

        private static Student GetTopStudentFromClass(Student[] students) // return top student with max grade from each array of students (class)
        {
            Student TopStudent = null;
            double maxGrade = -1;

            foreach (Student student in students)
            {
                double avg = GetStudentAvarageGrade(student);
                if (avg > maxGrade)
                {
                    TopStudent = student;
                    maxGrade = avg;
                }
            }
            return TopStudent;
        }

        /*     //syntax for validation
        #region create All objects and insert data
        //create grades for courses
        Node<Course> korenGrades = new Node<Course>(new Course(1, 99));
        korenGrades = AddLast(korenGrades, new Course(2, 89));
            korenGrades = AddLast(korenGrades, new Course(3, 95));

            Node<Course> ofriGrades = new Node<Course>(new Course(1, 98));
        ofriGrades = AddLast(ofriGrades, new Course(2, 88));
            ofriGrades = AddLast(ofriGrades, new Course(3, 94));

            Node<Course> yoavGrades = new Node<Course>(new Course(1, 97));
        yoavGrades = AddLast(yoavGrades, new Course(2, 87));
            yoavGrades = AddLast(yoavGrades, new Course(3, 93));

            Node<Course> yardenGrades = new Node<Course>(new Course(1, 96));
        yardenGrades = AddLast(yardenGrades, new Course(2, 85));
            yardenGrades = AddLast(yardenGrades, new Course(3, 92));

            //create Students
            Student koren = new Student("Koren", korenGrades);
        Student ofri = new Student("Ofri", ofriGrades);
        Student yarden = new Student("Yarden", yardenGrades);
        Student yoav = new Student("Yoav", yoavGrades);

        //create classess
        Student[] class1 = new Student[2];
        class1[0] = koren;
            class1[1] = yarden;
            Student[] class2 = new Student[2];
        class2[0] = ofri;
            class2[1] = yoav;

            //for printing the grades before the function
            Node<Student> class12 = new Node<Student>(koren);
        class12 = AddLast(class12, yarden);
        Node<Student> class13 = new Node<Student>(ofri);
        class13 = AddLast(class13, yoav);

        //create list of classes
        Node<Student[]> allClasses = new Node<Student[]>(class1);
        allClasses = AddLast(allClasses, class2);


        #endregion

        //Call function to get top students from each class
        Node<Student> exelStudents = GetTopStudents(allClasses);

        Console.WriteLine("Grades of Class one:");
            PrintAvarageGrade(class12);
        Seperator();
        Console.WriteLine("Grades of Class two:");
            PrintAvarageGrade(class13);
        Seperator();
        Console.WriteLine("Top From Each Class");
            PrintAvarageGrade(exelStudents);
        Seperator();
*/
        #endregion
        #region Question 25: Get The students with the most grades lower then input **upgradeed**
        public static Student[] GetStudentsWithMostGradesLowerThenGrade(Node<Student>[] allClasses, int gradeToCheck)
        {
            Student[] result = new Student[allClasses.Length];

            for (int i = 0; i < allClasses.Length; i++)
            {
                if (allClasses[i] is null) { continue; }
                Student firstStudent = allClasses[i].GetValue();
                Node<Course> courses = firstStudent.GetCourseList();
                result[i] = GetStudentWithMostLowerGrades(allClasses[i].GetNext(), firstStudent, gradeToCheck, AmountOfGradesLower(courses, gradeToCheck));
            }
            return result;

        }
        private static Student GetStudentWithMostLowerGrades(Node<Student> list, Student student, int gradeToCheck, int maxAmountOfGrades)
        {
            if (list is null) { return student; }

            int currantAmountOfGrades = AmountOfGradesLower(list.GetValue().GetCourseList(), gradeToCheck);

            if (currantAmountOfGrades > maxAmountOfGrades)
            {
                student = list.GetValue();
                maxAmountOfGrades = currantAmountOfGrades;
            }
            return GetStudentWithMostLowerGrades(list.GetNext(), student, gradeToCheck, maxAmountOfGrades);
        }
        private static int AmountOfGradesLower(Node<Course> list, int gradeToCheck)
        {
            if (list is null)
                return 0;
            return list.GetValue().GetGrade() < gradeToCheck ? AmountOfGradesLower(list.GetNext(), gradeToCheck) + 1 : AmountOfGradesLower(list.GetNext(), gradeToCheck);
        }
        /*  //syntax for validation
        #region create All objects and insert data
         //create grades for courses
         Node<Course> korenGrades = new Node<Course>(new Course(1, 99));
         korenGrades = AddLast(korenGrades, new Course(2, 44));
             korenGrades = AddLast(korenGrades, new Course(3, 95));

             Node<Course> ofriGrades = new Node<Course>(new Course(1, 98));
         ofriGrades = AddLast(ofriGrades, new Course(2, 88));
             ofriGrades = AddLast(ofriGrades, new Course(3, 94));

             Node<Course> yoavGrades = new Node<Course>(new Course(1, 97));
         yoavGrades = AddLast(yoavGrades, new Course(2, 34));
             yoavGrades = AddLast(yoavGrades, new Course(3, 93));

             Node<Course> yardenGrades = new Node<Course>(new Course(1, 96));
         yardenGrades = AddLast(yardenGrades, new Course(2, 85));
             yardenGrades = AddLast(yardenGrades, new Course(3, 92));

             //create Students
             Student koren = new Student("Koren", korenGrades);
         Student ofri = new Student("Ofri", ofriGrades);
         Student yarden = new Student("Yarden", yardenGrades);
         Student yoav = new Student("Yoav", yoavGrades);

         Node<Student> class1 = new Node<Student>(koren);
         class1 = AddLast(class1, yarden);
         Node<Student> class2 = new Node<Student>(ofri);
         class2 = AddLast(class2, yoav);

         //create array of classes
         Node<Student>[] allClasses = new Node<Student>[2];
         allClasses[0] = class1;
             allClasses[1] = class2;
             int minGrade = 55;
         #endregion

         Student[] arr = GetStudentsWithMostGradesLowerThenGrade(allClasses, minGrade);

             foreach(Student student in arr)
             {
                 PrintTyping(student.ToString());
         Console.WriteLine();
             }
     //expect : koren , yoav*/
        #endregion
        #endregion
        #region function for main Execeution not the homework functions
        #region Init functions
        static Node<Student> InitStudentList(Random rnd, string[] names, int numOfCourses, int minGradeForCourses)
        {
            Node<Student> studentList = new Node<Student>(new Student(names[0], InitCourseList(rnd, numOfCourses, minGradeForCourses)));
            for (int i = 1; i < names.Length; i++)
            {
                studentList = AddLast(studentList, new Student(names[i], InitCourseList(rnd, numOfCourses, minGradeForCourses)));
            }
            return studentList;
        }
        static Student[] InitStudentsArray(Random rnd, string[] names, int numOfCourses, int minGradeForCourses)
        {
            Student[] classOfStudents = new Student[names.Length];

            for (int i = 0; i < classOfStudents.Length; i++)
            {
                classOfStudents[i] = new Student(names[i], InitCourseList(rnd, numOfCourses, minGradeForCourses));
            }
            return classOfStudents;
        }
        static Node<Worker> InitWorkerList(Random rnd, string[] names)
        {
            Node<Worker> workerList = new Node<Worker>(new Worker(names[0], rnd.Next(5000, 10000)));
            for (int i = 1; i < names.Length; i++)
            {
                workerList = AddLast(workerList, new Worker(names[i], rnd.Next(5000, 10000)));
            }
            return workerList;
        }
        static Node<int> InitNumbersList(Random random, int listLength)
        {
            Node<int> list = new Node<int>(random.Next(1, 10));
            for (int i = 0; i < listLength; i++)
            {
                list = AddLast(list, new Node<int>(random.Next(1, 10)));
            }
            return list;
        }
        static Node<Course> InitCourseList(Random rnd, int numOfCourses, int minGradeForCourses)
        {
            Node<Course> course = new Node<Course>(new Course(0, rnd.Next(minGradeForCourses, 100)));
            for (int i = 1; i < numOfCourses; i++)
            {
                course = AddLast(course, new Node<Course>(new Course(i, rnd.Next(minGradeForCourses, 100))));
            }
            return course;
        }
        #endregion
        #region Print Data Creation
        static void PrintNumbersList(Node<int> list, string msg, string listName, int speed = 15)
        {

            msg = $@"Node<int> {listName} = Node<int>({list.GetValue()})";
            PrintTyping(msg, "w", speed);
            while (list.HasNext())
            {
                list = list.GetNext();
                msg = $@"             {listName} = AddLast({listName}, new Node<int>({list.GetValue()}));";
                PrintTyping(msg, "w", speed);
            }
            Console.WriteLine();
        }
        static void PrintWorkerListCreation(Node<Worker> list, string msg, string listName, int speed = 15)
        {
            msg = $@"Node<Worker> {listName} = new Node<Worker>(new Worker(""{list.GetValue().GetName()}"", {list.GetValue().GetSalary()}))";
            PrintTyping(msg, "w", speed);
            while (list.HasNext())
            {
                list = list.GetNext();
                msg = $@"             {listName} = AddLast({listName}, new Node<Worker>(new Worker(""{list.GetValue().GetName()}"", {list.GetValue().GetSalary()})));";
                PrintTyping(msg, "w", speed);
            }
            Console.WriteLine();
        }
        static void PrintStudentListCreation(Node<Student> list, string msg, string listName, int speed = 15)
        {
            msg = $@" Node<Student> {listName} = new Node<Student>(""{list.GetValue().GetName()}"",InitCourseList(rnd, numOfCourses));";
            PrintTyping(msg, "w", speed);
            while (list.HasNext())
            {
                list = list.GetNext();
                msg = $@"               {listName} = AddLast({listName}, new Node<Student> (new Student(""{list.GetValue().GetName()}"",InitCourseList(rnd, numOfCourses))));";
                PrintTyping(msg, "w", speed);
            }
            Console.WriteLine();
        }
        #endregion
        #region Get student info
        static string GetClassGradesArray(Student[] arr)
        {
            string result = "";
            foreach (Student student in arr)
            {
                result += $@"{student.GetGradesToPrint()}
=======================
";
            }
            return result;
        }
        static string GetClassGradesList(Node<Student> list, string result = "")
        {
            if (list is null)
            {
                return result;
            }
            result += $@"{list.GetValue().GetGradesToPrint()}
========================";

            return GetClassGradesList(list, result);
        }
        static string GetStudentsAvargeArray(Student[] arr)
        {
            string result = "";
            foreach (Student s in arr)
            {
                result += $"{s.GetName()} --> Avarge: {GetStudentAvarageGrade(s):f1} \n";
            }
            return result;
        }
        #endregion
        static void PrintTyping(string message, string color = "w", int speed = 20)
        {
            switch (color)
            {
                case "w":
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    }
                case "r":
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    }
                case "g":
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    }
                case "y":
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    }
                case "dg":
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        break;
                    }
                case "dr":
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        break;
                    }
                case "b":
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        break;
                    }
                case "db":
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        break;
                    }
                case "dy":
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        break;
                    }
                default:
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    }
            }

            foreach (char c in message)
            {

                Console.Write(c);
                System.Threading.Thread.Sleep(speed);
            }
            Console.ResetColor();
            Console.WriteLine(); Console.WriteLine();
        }
        static bool ValidateNumInput(string num)
        {
            string pattern = @"[0-9]{1,}";
            if (!Regex.IsMatch(num, pattern))
            {
                CustomErrorMsg("Enter digits only not letters or signs");
                return false;
            }
            int numInt = int.Parse(num);
            if (numInt < 0 || numInt > 100)
            {
                CustomErrorMsg("Must be between 0 - 100");
                return false;
            }
            return true;


        }
        static string GetNumListInARow(Node<int> list, string result = "")
        {
            if (list is null)
            {
                return result.Substring(0, result.Length - 2);
            }
            result += $"{list.GetValue()}, ";
            return GetNumListInARow(list.GetNext(), result);
        }
        static void ErrorMsg()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine("Stop trying messing with the system enter a valid input..");
            Console.ResetColor();
        }
        static void CustomErrorMsg(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ResetColor();
        }
        #endregion
        private static void CheckSpecificFunction()
        {
            // paste here the region "Syntax For Validation in main" of the question you want to check (13 - 25) and remove the comments from inside. 
        }
        static void Main(string[] args)
        {
            /*
            Data Structue final project.
            -------------------------------------------------------------------------------
            Dynamic data:
            1. Names of variables --> change the variables with Name from Parameters region.
            2. Speed of typing --> change the variables: cmSpeed(comments), codeSpeed(code), otSpeed(output)
            3. Numbers of courses --> change variable numOfCourses
            4. Length of numbers list --> change variable numListLength
            5. Length of lists Workers/Students --> by adding names to names array 1, 2 or 3.
            6. Min grade for courses list --> change variable minGradeForCourses
            7. Username --> change teacher variable
            -------------------------------------------------------------------------------
            Change typing speed:
             cmSpeed --> the speed the comments are typed to console.(green color).
             codeSpeed --> the speed the code is written to console.(white color)
             otSpeed  --> the speed the code output is typed to console.(dark yellow color)
           -------------------------------------------------------------------------------
            How to check a speccific function:
            1.Inside the if condition replace false with true
            2.Go to the body of the function CheckSpecificFunction()
            3.Place the region "syntax for validation in main" from the question you want to check (13-25)
            4.Remove the comoments signs from the region
            5. Run the Code
           -------------------------------------------------------------------------------
            */
            #region Check a Specific Function 
            if (true) // True --> checking specific function, False --> run the full script 
            {
                //go to the defenition of CheckSpecificFunction().
                CheckSpecificFunction();
                return;
            }
            #endregion
        #region Init Parameters
            int cmSpeed = 10, codeSpeed = 10, otSpeed = 10; // fast debug mode
            //int cmSpeed = 70, codeSpeed = 50, otSpeed = 50; // Regular speed presentation mode
            int numOfCourses = 8, minGradeToSearch = 55, numListLength = 6, minGradeForCourses = 40;
            string teacher = "Salman";
            string msg, choice, workerName = "worker", workerList1Name1 = "workerList1", workerList1Name2 = "workerList2", joinedListName = "joinedList", commonNumsName = "commonNums", numList1Name = "numList1", numList2Name = "numList2";
            string className1 = "classA", className2 = "classB", className3 = "classC", allClassesName = "allClasses", topStudentsName = "topStudents", arrayOfStudentListsName = "arrayOfStudentLists", worstStudentsName = "worstStudents", tempName1;
            string[] names1 = { "Koren", "Ofri", "Yoav", "Yarden", "Ofek", "Alon" };
            string[] names2 = { "Ido", "Yair", "Daniel", "Michal", "Shaked", "Asaf"};
            string[] names3 = { "Amit", "Gal", "Elad", "Eli", "Salman", "Tami"};
            Node<Worker> workerList1, workerList2, joinedList;
            Node<Student> class1, class2, class3, topStudents;
            Node<Student[]> allClasses,nextStudentArray;
            Node<Student>[] arrayOfStudentLists = new Node<Student>[3]; 
            Node<int> numList1, numList2, commonNums;
            Random random = new Random();
            Student[] worstStudents;
            Worker worker;

            #endregion
        #region Part 1:Welcome
            //print welcome
            msg = $@"Welcome {teacher} Ready To Get Startet?
Press any key To Begin";

            PrintTyping(msg, "g", cmSpeed);
            Console.ReadKey();
            Console.Clear();

        #endregion
        #region Main Menu
        mainMenu:
            Console.Clear();
            choice = "-1";
            #region init Workers List
            workerList1 = InitWorkerList(random, names1);
            workerList2 = InitWorkerList(random, names2);
            #endregion
            msg = $@"Choose which Questions would you like to check:
-----------------------------------------------
1.Questions:  1 – 17
2.Questions: 18 - 21
3.Questions: 23 - 25
4.exit";
            PrintTyping(msg, "w", codeSpeed);
            choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    {
                        goto menuCase1to17;
                    }
                case "2":
                    {
                        goto menuCase18to21;
                    }
                case "3":
                    {
                        goto menuCase23to25;

                    }
                case "4":
                    {
                        goto goodbye;
                    }
                default:
                    {
                        Console.Clear();
                        Console.WriteLine();
                        ErrorMsg();
                        goto mainMenu;
                    }
            }
        #endregion
        #region option 1:case 1-17
        menuCase1to17:
            Console.Clear();
            #region init Workers List
            workerList1 = InitWorkerList(random, names1);
            workerList2 = InitWorkerList(random, names2);
            choice = "-1";
            #endregion
            msg = @"Choose a demonstration:
1.Questions  1-5   : Creating, Adding And Printing a list.
2.Questions  6-12  : Deleting From List And Returning Values.
3.Questions  13-17 : linked list manipulations
4.back to main menu";
            PrintTyping(msg, "w", codeSpeed);
            choice = Console.ReadLine();
            Console.Clear();
            switch (choice)
            {
                case "1": // Questions 1-5 :  Creating, Adding And Printing a list.
                    {

                        #region Play 1-5
                        Console.Clear();
                        #region print Data init
                        msg = @"//Now we'll create 2 Linked List of type Worker, and add Nodes with the AddLast() function.";
                        PrintTyping(msg, "g", cmSpeed);
                        PrintWorkerListCreation(workerList1, msg, workerList1Name1);
                        PrintWorkerListCreation(workerList2, msg, workerList1Name2);
                        #endregion
                        #region Print Lists before addition
                        msg = $@"//Now we'll Print {workerList1Name1}";
                        PrintTyping(msg, "g", cmSpeed);

                        msg = $@"Console.WriteLine({workerList1Name1});";
                        PrintTyping(msg, "w", codeSpeed);
                        msg = $@"{workerList1}";
                        PrintTyping(msg, "dy", otSpeed);
                        Console.WriteLine();
                        msg = $@"//Now we'll Print {workerList1Name2} ";
                        PrintTyping(msg, "g", cmSpeed);
                        msg = $@"Console.WriteLine({workerList1Name2});";
                        PrintTyping(msg, "w", codeSpeed);
                        msg = $@"{workerList2}";
                        PrintTyping(msg, "dy", otSpeed);
                        #endregion
                        #region Use AddFirst func
                        Node<Worker> next = workerList2.GetNext();
                        string firstname = workerList2.GetValue().GetName();
                        string nextName = next.GetValue().GetName();
                        msg = $@"//Now Lets Add {firstname} from {workerList1Name2} To the start of {workerList1Name1} ";
                        PrintTyping(msg, "g", cmSpeed);
                        workerList1 = AddFirst(workerList1, workerList2.GetValue());
                        msg = $@"{workerList1Name1} = AddFirst({workerList1Name1}, {workerList1Name2}.GetValue());";
                        PrintTyping(msg, "w", codeSpeed);
                        #endregion
                        #region use Add After func
                        msg = $@"//Great now Lets Add {nextName} from {workerList1Name2} after {firstname} using the AddAfter() function";
                        PrintTyping(msg, "g", cmSpeed);
                        AddAfter(workerList1, workerList2.GetNext().GetValue());
                        msg = $@"AddAfter({workerList1Name1}, {workerList1Name2}.GetNext().GetValue());";
                        PrintTyping(msg, "w", codeSpeed);
                        #endregion
                        #region Print result and wait for press
                        msg = $@"//Now Lets Print {workerList1Name1} and see the changes...";
                        PrintTyping(msg, "g", cmSpeed);
                        msg = $@"Console.WriteLine({workerList1Name1});";
                        PrintTyping(msg, "w", codeSpeed);
                        msg = $@"{workerList1}";
                        PrintTyping(msg, "dy", otSpeed);
                        //stage 5:wait for press to return to menu
                        msg = $@"----------------------------------------------------";
                        PrintTyping(msg, "w", codeSpeed);
                        msg = $@"Thats all for the demonstration of questions 1-5, including:
1.Creating a Node Linked list
2.Adding to start using AddFirst() function
3.Adding to end using AddLast() function
4.Adding to the middle using AddAfter() function
5.Printing a List

Press any key to return to menu and Check out the other examples..";
                        PrintTyping(msg, "r", cmSpeed);
                        Console.ReadKey();
                        choice = "-1";
                        goto menuCase1to17;
                        #endregion
                        #endregion
                    }
                case "2": //Questions 6-12: Deleting From List And Returning Values.
                    {
                        #region Play 6-12
                        Console.Clear();
                        string firstNameWorkerList1 = workerList1.GetValue().GetName();
                        string firstNameWorkerList2 = workerList2.GetValue().GetName();
                        string lastNameWorkerList2 = GetTailValue(workerList2).GetName();
                        int index = 2;
                        #region Print init and list before changes
                        msg = @"//First Lets Create 2 Linked list of type Worker and add some new workers.";
                        PrintTyping(msg, "g", cmSpeed);
                        PrintWorkerListCreation(workerList1, msg, workerList1Name1);
                        PrintWorkerListCreation(workerList2, msg, workerList1Name2);
                        // print list
                        msg = $@"//So now lets print {workerList1Name2}";
                        PrintTyping(msg, "g", cmSpeed);
                        msg = $@"Console.WriteLine({workerList1Name2});";
                        PrintTyping(msg, "w", codeSpeed);
                        msg = $@"{workerList2}";
                        PrintTyping(msg, "dy", otSpeed);
                        #endregion
                        #region Delete functions and print again to see changes
                        // show delete from start end 
                        msg = $@"//Now we will use The Function DeleteFirst() to remove {firstNameWorkerList2} , and DeleteLast() to remove {lastNameWorkerList2} from the list";
                        PrintTyping(msg, "g", cmSpeed);
                        workerList2 = DeleteFirst(workerList2);
                        DeleteLast(workerList2);
                        msg = $@"{workerList1Name2} = DeleteFirst({workerList1Name2});
DeleteLast({workerList1Name2});";
                        PrintTyping(msg, "w", codeSpeed);
                        Node<Worker> next = workerList2.GetNext();
                        string nextName = next.GetValue().GetName();
                        msg = $@"//Great now lets use the DeleteAfter() function to remove the second worker on the list which is {nextName}";
                        PrintTyping(msg, "g", cmSpeed);
                        DeleteAfter(workerList2);
                        msg = $@"DeleteAfter({workerList1Name2});";
                        PrintTyping(msg, "w", codeSpeed);
                        // print list again
                        msg = $@"//Ok so lets print {workerList1Name2} to see all the changes we did..remember we removed {firstNameWorkerList2}, {lastNameWorkerList2} and {nextName} from the list";
                        PrintTyping(msg, "g", cmSpeed);
                        msg = $@"Console.WriteLine({workerList1Name2});";
                        PrintTyping(msg, "w", codeSpeed);
                        msg = $@"{workerList2}";
                        PrintTyping(msg, "dy", otSpeed);
                        #endregion
                        #region fetching values from linked list
                        // GetHeadValue()
                        msg = $@"//Now we will look how we can fetch a value from {workerList1Name2}
//Lets say we want to save {workerList1Name2} head's value to a variable we will use the function GetHeadValue()";
                        PrintTyping(msg, "g", cmSpeed);
                         worker = GetHeadValue(workerList2);
                        msg = $@"Worker {workerName} = GetHeadValue({workerList1Name2});
Console.WriteLine({workerName})";
                        PrintTyping(msg, "w", codeSpeed);
                        msg = $@"{worker}";
                        PrintTyping(msg, "dy", otSpeed);
                        //GetTailValue()
                        msg = $@"// Now lets say we wanted to get the last value on the list and print it then we will use the function GetTailValue()";
                        PrintTyping(msg, "g", cmSpeed);
                        msg = $@"{workerName} = GetTailValue({workerList1Name2});
Console.WriteLine({workerName});";
                        PrintTyping(msg, "w", codeSpeed);
                        worker = GetTailValue(workerList2);
                        lastNameWorkerList2 = GetTailValue(workerList2).GetName();
                        msg = $@"{worker}";
                        PrintTyping(msg, "dy", otSpeed);
                        //Get value by Index
                        msg = $@"//Lets Remember {workerList1Name1} for a second..  ";
                        PrintTyping(msg, "g", cmSpeed);
                        msg = $@"Console.WriteLIne({workerList1Name1})";
                        PrintTyping(msg, "w", codeSpeed);
                        msg = $@"{workerList1}";
                        PrintTyping(msg, "dy", otSpeed);
                        worker = GetValueByIndex(workerList1, index);
                        msg = $@"//To get a value in the middle of the list we'll use GetValueByIndex({workerList1Name1}, index), for our example we'll use index {index} on {workerList1Name1} and the expected result will be {worker.GetName()}";
                        PrintTyping(msg, "g", cmSpeed);
                        msg = $@"{workerName} = GetValueByIndex({workerList1Name1}, {index});
Console.WriteLine({workerName});";
                        PrintTyping(msg, "w", codeSpeed);
                        msg = $@"{worker}";
                        PrintTyping(msg, "dy", otSpeed);
                        #endregion
                        #region Check if Exists
                        //check if exists
                        worker = GetValueByIndex(workerList2, 0);
                        msg = $@"//To check if a certain value exists in the list we'll use the IsValueExists(T value) function
//this function implements the interface IComparable<T>, in order to compare betweeen objects the class  need to implement ICompareable and its method CompareTo.
//lets Test it with {worker.GetName()} and {workerList1Name1} and expect a false return value";
                        PrintTyping(msg, "g", 60);
                        msg = $@"Console.WriteLine(IsValueExists({workerList1Name1},{workerName}));";
                        PrintTyping(msg, "w", codeSpeed);
                        msg = $@"{IsValueExists(workerList1, worker)}";
                        PrintTyping(msg, "dy", otSpeed);
                        worker = GetValueByIndex(workerList1, index);
                        msg = $@"//Now lets test it with a true case, lets give {workerName} a value of a node from {workerList1Name1} for example {worker.GetName()} and run it again";
                        PrintTyping(msg, "g", 60);
                        msg = $@"{workerName} = GetValueByIndex({workerList1Name1}, {index});
Console.WriteLine(IsValueExists({workerList1Name1},{workerName}));";
                        PrintTyping(msg, "w", codeSpeed);
                        msg = $@"{IsValueExists(workerList1, worker)}";
                        PrintTyping(msg, "dy", otSpeed);
                        #endregion
                        #region finish
                        msg = @"----------------------------------------------------";
                        PrintTyping(msg, "w", codeSpeed);
                        msg = @"Thats all for the demonstration of questions 6-12, including:
1.Removing Node From: Start, End and middle of the list
2.Retrieving valus from start, end and by index
3.Checking if a value exists in the list.
Press any key to return to menu and Check out the other examples..";
                        PrintTyping(msg, "r", codeSpeed);
                        Console.ReadKey();
                        goto menuCase1to17;
                        #endregion
                        #endregion
                    }
                case "3":// Questions 13-17: linked list manipulations
                    {
                        Console.Clear();
                        #region Play 13 - 17
                        #region print data
                        msg = $@"//First Lets Create a list of Workers";
                        PrintTyping(msg, "g", cmSpeed);
                        PrintWorkerListCreation(workerList1, msg, workerList1Name1);
                        #endregion
                        #region Circular list
                        //test before
                        msg = @"//A singly linked list is a linked list that the tail points to the head of the list
//To check if a list is singly linked we have a funcion called IsSinglyLinked(), lets test it on our list and we'll expect to get false back.";
                        PrintTyping(msg, "g", cmSpeed);
                        msg = $@"Console.WriteLine(IsSinglyLinked({workerList1Name1}));";
                        PrintTyping(msg, "w", codeSpeed);
                        msg = $@"{IsSinglyLinked(workerList1)}";
                        PrintTyping(msg, "dy", otSpeed);
                        //use make list singly linked
                        msg = @"//As you can see we got a false value back. But in order To validate the function 
//Iv'e created a Function that makes a list singly linked Take a look.. 
/It called itself recrusivley until it reaches the tail of the list,
//Then it set the tail's pointer to the head of the list and makes it singly linked";
                        PrintTyping(msg, "g", cmSpeed);
                        msg = @"public static void MakeListSinglyLinked<T>(Node<T> head, Node<T> currant)
                           {
                               if (!currant.HasNext())
                               {
                                   currant.SetNext(head);
                                   return;
                               }
                               MakeListSinglyLinked(head, currant.GetNext());
                           }";
                        PrintTyping(msg, "w", codeSpeed);

                        msg = $@"//Now Let use it on  {workerList1Name1} and check if the result has changed";
                        PrintTyping(msg, "g", cmSpeed);
                        MakeListSinglyLinked(workerList1, workerList1);
                        msg = $@"MakeListSinglyLinked({workerList1Name1}, {workerList1Name1});
Console.WriteLine(IsSinglyLinked({workerList1Name1}));";
                        PrintTyping(msg, "w", codeSpeed);
                        //print after
                        msg = $@"{IsSinglyLinked(workerList1)}";
                        PrintTyping(msg, "dy", otSpeed);
                        msg = $@"//Now {workerList1Name1} is singly linked.";
                        PrintTyping(msg, "g", cmSpeed);
                        workerList1 = InitWorkerList(random, names1);
                        Console.WriteLine();
                        #endregion
                        #region Duplicate list
                        msg = $@"//Now we'll see how we can dupliacte a list's values to a new unrelated list using the ReturnADuplicateList() function,
//To verify we'll use the Equals function of class object which compare the adress in memory. Lets run some code...";
                        PrintTyping(msg, "g", cmSpeed);
                        workerList2 = workerList1;
                        msg = $@"Node<Worker> {workerList1Name2} = {workerList1Name1}
Console.WriteLine({workerList1Name1}.Equals({workerList1Name2}))";
                        PrintTyping(msg, "w", codeSpeed);
                        msg = $@"{workerList1.Equals(workerList2)}";
                        PrintTyping(msg, "dy", otSpeed);
                        msg = $@"//Now we we'll use the function and see the result ";
                        PrintTyping(msg, "g", cmSpeed);
                        workerList2 = ReturnADuplicateList(workerList1);
                        msg = $@"Node<Worker> workerList2 = ReturnADuplicateList({workerList1Name1});
Console.WriteLine({workerList1Name1}.Equals({workerList1Name2}))";
                        PrintTyping(msg, "w", codeSpeed);
                        msg = $@"{workerList1.Equals(workerList2)}";
                        PrintTyping(msg, "dy", otSpeed);

                        msg = $@"//As you can see we got false back which indicates the list is placed in a different adress in memory.
//But how we know its really duplicated?, lets print and see";
                        PrintTyping(msg, "g", cmSpeed);

                        msg = $@"Console.WriteLine({workerList1Name1})";
                        PrintTyping(msg, "w", codeSpeed);

                        msg = $@"{workerList1}";
                        PrintTyping(msg, "dy", otSpeed);
                        Console.WriteLine();
                        msg = $@"Console.WriteLine({workerList1Name2})";
                        PrintTyping(msg, "w", codeSpeed);

                        msg = $@"{workerList2}";
                        PrintTyping(msg, "dy", otSpeed);
                        #endregion
                        #region Sort list

                        msg = $@"//Now lets see how to sort a list of objects. 
// stage 1: In order to do this we'll need to implement ICompareable in the class we want to use it and define the sorting order.
// In my case I decided to sort the workers by name and then by salary this is the funciton in Worker class";
                        PrintTyping(msg, "g", cmSpeed);

                        msg = @"internal class Worker : Person, IComparable<Worker> 
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
          }";
                        PrintTyping(msg, "w", codeSpeed);


                        msg = @"//Stage 2 You need a sorting algorithm, I used Merge sort algorithm as it best suited for Linked list.
//the Merge sort algorithm start from the middle and split the list to two halves recursively until each node is seprated from the rest.
//then it merges them back together sorted by our defenition of the CompareTo function.";
                        PrintTyping(msg, "g", cmSpeed);

                        msg = $@"Console.WriteLine({workerList1Name1});";
                        PrintTyping(msg, "w", codeSpeed);
                        msg = $@"{workerList1}";
                        PrintTyping(msg, "dy", otSpeed);
                        Console.WriteLine();
                        msg = $@"{workerList1Name1} = MergeSort({workerList1Name1});";
                        workerList1 = MergeSort(workerList1);
                        PrintTyping(msg, "w", codeSpeed);
                        msg = $@"Console.WriteLine({workerList1Name1});";
                        PrintTyping(msg, "w", codeSpeed);
                        msg = $@"{workerList1}";
                        PrintTyping(msg, "dy", otSpeed);

                        msg = $@"//Now you can see that the list is sorted by name first and then by salary.Each class has its own sorting preferences but the MergeSort function is Generic.";
                        PrintTyping(msg, "g", cmSpeed);
                        Console.WriteLine();
                        #endregion
                        #region remove repeats on sorted list
                         worker = workerList1.GetValue();

                        msg = $@"//Now we'll see how we can remove all repeated values in our list,let add {worker.GetName()} from {workerList1Name1} to the end of the list to create duplication";
                        PrintTyping(msg, "g", cmSpeed);
                        //add to list and print before delete
                        msg = $@"Worker worker = {workerList1Name1}.GetValue();
     {workerList1Name1} = AddLast({workerList1Name1}, {workerName});
Console.WriteLine({workerList1Name1});";
                        workerList1 = AddLast(workerList1, worker);
                        PrintTyping(msg, "w", codeSpeed);
                        msg = $@"{workerList1}";
                        PrintTyping(msg, "dy", otSpeed);

                        //delete and print
                        msg = $@"//Now you can see {worker.GetName()} appears twice at the head and tail of the list, now lets call the function DeleteAllRepeatedValue() and print again ";
                        PrintTyping(msg, "g", cmSpeed);

                        msg = $@" DeleteAllRepeatedValue({workerList1Name1});
Console.WriteLine({workerList1Name1});";
                        DeleteAllRepeatedValue(workerList1);
                        PrintTyping(msg, "w", codeSpeed);
                        msg = $@"{workerList1}";
                        PrintTyping(msg, "dy", otSpeed);

                        msg = $@"//And you can see {worker.GetName()} is shown only onee";
                        PrintTyping(msg, "g", cmSpeed);
                        #endregion
                        #region Reverse list

                        msg = $@"//Great Now lets reverse the list using the function ReverseList()";
                        PrintTyping(msg, "g", cmSpeed);
                        Console.WriteLine();
                        msg = $@"{workerList1Name1} = ReverseList({workerList1Name1});
Console.WriteLine({workerList1Name1});";
                        workerList1 = ReverseList(workerList1);
                        PrintTyping(msg, "w", codeSpeed);
                        Console.WriteLine();
                        msg = $@"{workerList1}";
                        PrintTyping(msg, "dy", otSpeed);
                        #endregion
                        #region finish
                        msg = @"----------------------------------------------------";
                        PrintTyping(msg, "w", codeSpeed);
                        msg = @"Thats all for the demonstration of questions 13-17, including:
1.What is singly linked and to check if our list is one.
2.How to duplicate a list values into a new list.
3.How to sort a list using the Merge Sort algorithm.
4.What is the ICompareable interface and how to implenent it
5.How to remove any repeated values from our list using CompareTo().
6.How to reverese a list without creating a new list.
Press any key to return to menu and Check out the other examples...";
                        PrintTyping(msg, "r", codeSpeed);
                        Console.ReadKey();
                        goto menuCase1to17;
                        #endregion
                        #endregion
                    }

                case "4":// back to main menu
                    {
                        goto mainMenu;
                    }
                default:
                    {
                        Console.Clear();
                        Console.WriteLine();
                        ErrorMsg();
                        goto menuCase1to17;
                    }
            }
        #endregion
        #region option 2:case 18-21
        menuCase18to21:
            Console.Clear();
            #region init Workers List
            workerList1 = InitWorkerList(random, names1);
            workerList2 = InitWorkerList(random, names2);
            #endregion
            #region Check Equal Lists
            //init lists 
            #region crearte list print
            msg = $@"//Lets create {workerList1Name1} and use ReturnADuplicateList() function to create a replica,and print them to see the results";
            PrintTyping(msg, "g", cmSpeed);
            PrintWorkerListCreation(workerList1, msg, workerList1Name1);
            #endregion
            #region duplicate and print before change
            msg = $@"//Call IsListEquals({workerList1Name1}, {workerList1Name2}) and expect a true value back.";
            PrintTyping(msg, "g", cmSpeed);
            msg = $@"Node<Worker> {workerList1Name2} = ReturnADuplicateList({workerList1Name1});
Console.WriteLine({workerList1Name1});
Console.WriteLine({workerList1Name2});
Console.WriteLine(IsListEquals({workerList1Name1}, {workerList1Name2}));";
            PrintTyping(msg, "w", codeSpeed);
            workerList2 = ReturnADuplicateList(workerList1); //use function to duplicate list to new list    
            msg = $@"{workerList1}";
            PrintTyping(msg, "dy", otSpeed);
            Console.WriteLine();
            msg = $@"{workerList2}";
            PrintTyping(msg, "dy", otSpeed);
            Console.WriteLine();
            msg = $@"{IsListEquals(workerList1, workerList2)}";
            PrintTyping(msg, "dy", otSpeed);
            Console.WriteLine();
            #endregion
            #region Make changes and print result
            msg = @"//Great now lets make changes and try again";
            PrintTyping(msg, "g", cmSpeed);
            msg = $@" {workerList1Name2} = DeleteFirst({workerList1Name2}
);
Console.WriteLine(IsListEquals({workerList1Name1}, {workerList1Name2}));";
            PrintTyping(msg, "w", codeSpeed);
            workerList2 = DeleteFirst(workerList2);
            msg = $@"{IsListEquals(workerList1, workerList2)}";
            PrintTyping(msg, "dy", otSpeed);
            #endregion
            #endregion
            #region Merge Two Lists
            msg = $@"//Now lets see how we can merge two lists of the same type and return a new list.
//For this we'll use the function  MergeTwoLists() on the lists {workerList1Name1} and  {workerList1Name2} and print the result";
            PrintTyping(msg, "g", cmSpeed);
            msg = $@"Node<Worker> {joinedListName} = MergeTwoLists({workerList1Name1}, {workerList1Name2});
Console.WriteLine({joinedListName});";
            joinedList = MergeTwoLists(workerList1, workerList2);
            PrintTyping(msg, "w", codeSpeed);
            msg = $@"{joinedList}";
            PrintTyping(msg, "dy", otSpeed);
            msg = $@"//As you can see the new list {joinedListName} is now containing {workerList1Name1} and {workerList1Name2}  ";
            PrintTyping(msg, "g", cmSpeed);
            Console.WriteLine();
            #endregion
            #region Merge no Repeats
            msg = $@"//For removing all repeated valus from list I have a function called MergeTwoListsWithNoRepeats(),This function uses DeleteAllRepeatedValue() as a sub function to remove all the duplications
//Combined with the MergeList() function from earlier.Now lets try  to join {workerList1Name1} and {workerList1Name2} again and see the result";
            ; // explain
            PrintTyping(msg, "g", cmSpeed);
            msg = $@"{joinedListName} = MergeTwoListsWithNoRepeats({workerList1Name1}, {workerList1Name2});
Console.WriteLine({joinedListName});"; // merge
            joinedList = MergeTwoListsWithNoRepeats(workerList1, workerList2);
            PrintTyping(msg, "w", codeSpeed);
            msg = $@"{joinedList}"; // print result
            PrintTyping(msg, "dy", otSpeed);
            msg = $@"//As you can see all There is no duplicated values in {joinedListName}. We have successfuly joined two list and removed all repeated value
//But what if wanted a list cotntains all common values between two lists without repeats?.
//Well then we'll use the function MergeListsCommonValueOnly() lets see an example..this time we'll handle a list of intigers so lets create two...";
            PrintTyping(msg, "g", cmSpeed);
            #endregion
            #region Merge Repeats Only
            numList1 = InitNumbersList(random, numListLength);
            numList2 = InitNumbersList(random, numListLength);
            PrintNumbersList(numList1, msg, numList1Name);
            PrintNumbersList(numList2, msg, numList2Name);
            msg = $@"//Now we will use the function to get all the common numbers from both lists, save it to a new list and print all the lists to the result";
            PrintTyping(msg, "g", cmSpeed);
            msg = $@"Node<int> {commonNumsName} = MergeListsCommonValueOnly({numList1Name}, {numList2Name});
Console.WriteLine({numList1Name});
Console.WriteLine({numList2Name});
Console.WriteLine({commonNumsName});";
            PrintTyping(msg, "w", codeSpeed);
            commonNums = MergeListsCommonValueOnly(numList1, numList2);
            msg = $@"{numList1Name}: {GetNumListInARow(numList1)}";
            PrintTyping(msg, "dy", otSpeed);
            Console.WriteLine();
            msg = $@"{numList2Name}: {GetNumListInARow(numList2)}";
            PrintTyping(msg, "dy", otSpeed);
            Console.WriteLine();
            msg = $@"{commonNumsName}: {GetNumListInARow(commonNums)}";
            PrintTyping(msg, "dy", otSpeed);
            Console.WriteLine();
            msg = $@"//As you can see the common numbers between the two lists are: {GetNumListInARow(commonNums)}.
//Remember THe interface ICompareable<T>, which we use every time we want to compare values between two nodes? well it used here as well.";
            PrintTyping(msg, "g", cmSpeed);
            #endregion
            #region finish
            msg = @"----------------------------------------------------";
            PrintTyping(msg, "w", codeSpeed);
            msg = @"Thats all for the demonstration of question 18 - 21 including:
1. Check if lists are equal
2. Merge two lists full join
3. Merge and removed duplicated data
4. Merge and get all common values
Press any key to return to menu and Check out the other examples..";
            PrintTyping(msg, "r", codeSpeed);
            Console.ReadKey();
            Console.Clear();
            goto mainMenu;
        #endregion
        #endregion
        #region option 3: case 23-25
        menuCase23to25:
         #region  print avg of a list of students
            Console.Clear();
            //init lists: student courses
            class1 = InitStudentList(random, names1, numOfCourses, minGradeForCourses);
            Student head = class1.GetValue();
            tempName1 = head.GetName();
            double headAvg = GetStudentAvarageGrade(head);

            msg = $@"//First lets create a list of students with the help of a function that generates a new random list of grades for each student";
            PrintTyping(msg, "g", cmSpeed);
            PrintStudentListCreation(class1, msg, className1);

            // show an example of avg calculation
            msg = $@"//Now lets see for example {tempName1}'s grades, and calculate the avarage";
            PrintTyping(msg, "g", cmSpeed);
            msg = $@"Student {tempName1} = {className1}.GetValue();
Console.WriteLine({tempName1}.GetGradesToPrint())
Console.WriteLine(GetStudentAvarageGrade({tempName1}));";
            PrintTyping(msg, "w", codeSpeed);
            msg = $@"{head.GetGradesToPrint()}";
            PrintTyping(msg, "dy", otSpeed);
            msg = $@"{tempName1} --> Avarage: {headAvg:f1}";
            PrintTyping(msg, "dy", otSpeed);

            //print avg of all the class
            msg = $@"//As you can see the avarage of {tempName1}'s grades is {headAvg:f1},Now let print the avarage of the entire list using the GetAvarageGradeList() function."; 
            PrintTyping(msg, "g", cmSpeed);

            msg = $@"Console.WriteLine(GetAvarageGradeList({className1}));";
            PrintTyping(msg, "w", codeSpeed);

            msg = $@"{GetAvarageGradeList(class1)}";
            PrintTyping(msg, "dy", otSpeed);
        #endregion
         #region Top Student in each class
            // print create array of student func
            msg = $@"//Ok now that we can calculate an avarge lets start getting some data...
//For this example I have a List of Students[], each Node reprsents a class in school, lets say I want to get the top student 
//From each class, for this first I'll have to create the arrays of students right??..lets write a simple function that returns an array of students";
            PrintTyping(msg, "g", cmSpeed);
            msg = $@"static Student[] InitStudentsArray(Random rnd, string[] names, int numOfCourses)
{{
Student[] classOfStudents = new Student[names.Length];

    for (int i = 0; i < classOfStudents.Length; i++)
         {{
             classOfStudents[i] = new Student(names[i],InitCourseList(rnd, numOfCourses));
         }}
return classOfStudents;
}}";
            PrintTyping(msg, "w", codeSpeed);
            //craeting 3 arrays
            msg = $@"//Now lets create a new list of type  Student[] containing 3 arrays. ";
            allClasses = new Node<Student[]>(InitStudentsArray(random, names1, numOfCourses, minGradeForCourses));
            allClasses = AddLast(allClasses, InitStudentsArray(random, names2, numOfCourses, minGradeForCourses));
            allClasses = AddLast(allClasses, InitStudentsArray(random, names3, numOfCourses, minGradeForCourses));
            PrintTyping(msg, "g", cmSpeed);
            msg = $@"Node<Student[]> {allClassesName} = new Node<Student[]>(InitStudentsArray(random, names1, numOfCourses));
            {allClassesName} = AddLast({allClassesName}, InitStudentsArray(random, names2, numOfCourses));
            {allClassesName} = AddLast({allClassesName}, InitStudentsArray(random, names3, numOfCourses));";
            PrintTyping(msg, "w", codeSpeed);
            #endregion
         #region Print students array
            #region Print students array
            msg = $@"//Lets see the avarage of students from each array in the list, which we'll call class A, class B and class C .";
            PrintTyping(msg, "g", cmSpeed);

            msg = $@"Console.WriteLine(GetStudentsAvargeArray(allClasses.GetValue()));
 nextStudentArray = allClasses.GetNext();
GetStudentsAvargeArray(nextStudentArray.GetValue());
 nextStudentArray = nextStudentArray.GetNext();
GetStudentsAvargeArray(nextStudentArray.GetValue());
";
            PrintTyping(msg, "w", codeSpeed);

            msg = $@"class A: 
{GetStudentsAvargeArray(allClasses.GetValue())}";
            nextStudentArray = allClasses.GetNext();
            PrintTyping(msg, "dy", otSpeed);

            msg = $@"class B: 
{GetStudentsAvargeArray(nextStudentArray.GetValue())}";
            nextStudentArray = nextStudentArray.GetNext();
            PrintTyping(msg, "dy", otSpeed);

            msg = $@"class C: 
{GetStudentsAvargeArray(nextStudentArray.GetValue())}";
            PrintTyping(msg, "dy", otSpeed);


            #endregion

            #region Get top student
            msg = $@"//Now lets use GetTopStudents() to get the top student of each array of student from allClasses list, save to a new list and print to see the result...";
            PrintTyping(msg, "g", cmSpeed);

            msg = $@"Node<Student> {topStudentsName} = GetTopStudents({allClassesName});
Console.WriteLine(GetAvarageGradeList({topStudentsName}));";
            topStudents = GetTopStudents(allClasses);
            PrintTyping(msg, "w", codeSpeed);

            msg = $@"{GetAvarageGradeList(topStudents)}";
            PrintTyping(msg, "dy", otSpeed);

            msg = $@"//As you can see we successfuly created a new list containing the top student from each class";
            PrintTyping(msg, "g", cmSpeed);
            #endregion

            #endregion
         #region Get Students with most grades under certain grade
            class2 = InitStudentList(random, names2, numOfCourses, minGradeForCourses);
            class3 = InitStudentList(random, names3, numOfCourses, minGradeForCourses);
            arrayOfStudentLists = new Node<Student>[3];
            arrayOfStudentLists[0] = class1;
            arrayOfStudentLists[1] = class2;
            arrayOfStudentLists[2] = class3;
            msg = $@"//Now Lets create two more lists of students and add all the lists to an array of lists of students";
            PrintTyping(msg, "g", cmSpeed);
            PrintStudentListCreation(class2, msg, className2);
            PrintStudentListCreation(class3, msg, className3);

            msg = $@"Node<Student>[] {arrayOfStudentListsName} = new Node<Student>[3];
{arrayOfStudentListsName}[0] = {className1};
{arrayOfStudentListsName}[1] = {className2};
{arrayOfStudentListsName}[2] = {className3};";
            PrintTyping(msg, "w", codeSpeed);


            msg = $@"//Great now if we wanted to find the student who faild the most curses from every class, 
//We'll use the function GetStudentsWithMostGradesLowerThenGrade() and give it the parameters of our array of lists and the grade 55. 
//We'll get in return an array with one student from every class lets test it...";
            PrintTyping(msg, "g", cmSpeed);
            worstStudents = GetStudentsWithMostGradesLowerThenGrade(arrayOfStudentLists, minGradeToSearch);
            msg = $@"Student[] {worstStudentsName} =  GetStudentsWithMostGradesLowerThenGrade({arrayOfStudentListsName}, {minGradeToSearch});
Console.WriteLine($@""
from {className1} --> {worstStudentsName}[0] with {{AmountOfGradesLower({worstStudentsName}[0].GetCourseList(), minGradeToSearch)}}
from {className2} --> {worstStudentsName}[1] with {{AmountOfGradesLower({worstStudentsName}[1].GetCourseList(), minGradeToSearch)}}
from {className3} --> {worstStudentsName}[2] with {{AmountOfGradesLower({worstStudentsName}[2].GetCourseList(), minGradeToSearch)}}
"")";
            PrintTyping(msg, "w", codeSpeed);

            //print students

            msg = $@"from {className1} --> {worstStudents[0]} with: {AmountOfGradesLower(worstStudents[0].GetCourseList(), minGradeToSearch)};
from {className2} --> {worstStudents[1]} with: {AmountOfGradesLower(worstStudents[1].GetCourseList(), minGradeToSearch)};
from {className3} --> {worstStudents[2]} with: {AmountOfGradesLower(worstStudents[2].GetCourseList(), minGradeToSearch)};
";
            PrintTyping(msg, "dy", otSpeed);
            //show grades of students to validate

            msg = $@"//Here are the grades of these students just to valitae our function: ";
            PrintTyping(msg, "g", cmSpeed);


            msg = $@"{GetClassGradesArray(worstStudents)}";
            PrintTyping(msg, "dy", otSpeed);

            msg = $@"//As you can see it matches and our function is working correctly ";
            PrintTyping(msg, "g", cmSpeed);
            //finish

            #endregion
         #region finish
            msg = @"----------------------------------------------------";
            PrintTyping(msg, "w", codeSpeed);
            msg = @"Thats all for the demonstration of question 23 - 25 including:
1. Calculating the avarge of a student.
2. Finding the top student from every class
3. finindg the worst student from every class
4. Combining list and arrays to create a efficiant data storage.
Press any key to return to menu and Check out the other examples..";
            PrintTyping(msg, "r", codeSpeed);
            Console.ReadKey();
            goto mainMenu;
        #endregion
        #endregion
        #region option 4: exit
        goodbye:
            Console.Clear();
            msg = $@"Bye {teacher} Hope You Enjoyed...";
            PrintTyping(msg, "r", cmSpeed);
            return;
            #endregion
        }

      
    }
}

