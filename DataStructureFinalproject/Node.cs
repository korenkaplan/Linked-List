using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureFinalproject
{
    internal class Node<T> 
    {
        #region fields
        T value;
        Node<T> next;
        #endregion 

        #region c'tor

        public Node(T value)
        {
            this.value = value;
            this.next = null;
        }
        public Node() { }
        public Node(T value, Node<T> next)
        {
            this.value = value;
            this.next = next;
        }
        #endregion

        #region Get/Set value/next functions
        public T GetValue() { return this.value; }
        public Node<T> GetNext() { return this.next; }

        public void SetValue(T value) { this.value = value; }

        public void SetNext(Node<T> next) { this.next = next; }
        #endregion

        #region basic Functions
        public bool HasNext() { return this.next != null; }

        public override string ToString()
        {
            if (!this.HasNext())
            {
                return $"{this.value}";
            }

            return $"{this.value} \n{this.next.ToString()}";

        }

        public string PrintNodes()
        {
            if (!this.HasNext())
            {
                return $"{this.value}";
            }

            return $"{this.value} \n{this.next.PrintNodes()}";
        }

        #endregion

   
       
    }
}

