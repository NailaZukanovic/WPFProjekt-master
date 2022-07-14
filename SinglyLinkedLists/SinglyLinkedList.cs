using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SinglyLinkedLists
{
    public class SinglyLinkedList
    {
        private SinglyLinkedListNode firstNode;
        public SinglyLinkedList()
        {
            // NOTE: This constructor isn't necessary, once you've implemented the constructor below.
        }

        // READ: http://msdn.microsoft.com/en-us/library/aa691335(v=vs.71).aspx
        public SinglyLinkedList(params object[] values)
        {
            foreach (object thing in values)
            {
                this.AddLast(thing.ToString());
            }
        }

        // READ: http://msdn.microsoft.com/en-us/library/6x16t2tx.aspx
        public string this[int i]
        {
            get { return this.ElementAt(i); }
            set
            {
                if (i == 0){
                    AddFirst(value);
                    Remove(ElementAt(1));
                }
                else
                {
                    var temp = NodeAt(i).Next;
                    NodeAt(i - 1).Next = new SinglyLinkedListNode(value);
                    NodeAt(i).Next = temp;
                }
            }
        }

        public override string ToString()
        {
            StringBuilder res = new StringBuilder("{ ");
            SinglyLinkedListNode cur = firstNode;
            while(cur != null)
            {
                res.Append(String.Format("\"{0}\"", cur.Value));
                if (cur.Next != null)
                    res.Append(", ");
                else
                    res.Append(" ");
                cur = cur.Next;
            }
            res.Append("}");
            return res.ToString();
        }
        
        public void AddAfter(string existingValue, string value)
        {
            var cur = firstNode;
            while (cur != null)
            {
                if (cur.Value == existingValue)
                {
                    var temp = cur.Next;
                    cur.Next = new SinglyLinkedListNode(value);
                    cur.Next.Next = temp;
                    return;
                }
                cur = cur.Next;
            }
            throw new ArgumentException("Value not found");
        }

        public void AddFirst(string value)
        {
            SinglyLinkedListNode temp = firstNode;
            firstNode = new SinglyLinkedListNode(value);
            firstNode.Next = temp;

        }

        public void AddLast(string value)
        {
            if (firstNode == null)
                firstNode = new SinglyLinkedListNode(value);

            else
                this.LastNode().Next = new SinglyLinkedListNode(value);
        }

        // NOTE: There is more than one way to accomplish this.  One is O(n).  The other is O(1).
        public int Count()
        {
            int res = 0;
            var cur = firstNode;
            while (cur != null)
            {
                res++;
                cur = cur.Next;
            }
            return res;
        }

        public string ElementAt(int index)
        {
            SinglyLinkedListNode res = firstNode;
            for (int i = 0; i < index; i++)
            {
                res = res != null ? res.Next : null;
                if (res == null)
                    throw new ArgumentOutOfRangeException();
            }
            if (res != null)
                return res.Value;
            else
                throw new ArgumentOutOfRangeException();
        }


        public string First()
        {
            return firstNode != null ? firstNode.Value : null;
        }

        public int IndexOf(string value)
        {
            var cur = firstNode;
            int res = 0;
            while (cur != null)
            {
                if (cur.Value == value)
                {
                    return res;
                }
                cur = cur.Next;
                res += 1;
            }
            return -1;
        }

        public bool IsSorted()
        {
            var cur = firstNode;
            while (cur != null)
            {
                if (cur.Next != null && string.CompareOrdinal(cur.Value, cur.Next.Value) > 0)
                    return false;
                cur = cur.Next;
            }
            return true;
        }

        // HINT 1: You can extract this functionality (finding the last item in the list) from a method you've already written!
        // HINT 2: I suggest writing a private helper method LastNode()
        // HINT 3: If you highlight code and right click, you can use the refactor menu to extract a method for you...
        public string Last()
        {
            SinglyLinkedListNode last = this.LastNode();
            return last != null ? last.Value : null;
        }

        private SinglyLinkedListNode LastNode()
        {
            SinglyLinkedListNode lastNode = this.firstNode;
            if (lastNode == null)
                return lastNode;
            while (true)
            {
                if (lastNode.Next == null)
                    return lastNode;
                else
                    lastNode = lastNode.Next;
            }
        }

        public void Remove(string value)
        {
            var curNode = firstNode;
            SinglyLinkedListNode prevNode = null;
            do
            {
                if (curNode.Value == value)
                {
                    if (prevNode == null)
                        firstNode = curNode.Next;
                    else
                        prevNode.Next = curNode.Next;
                    return;
                }
                prevNode = curNode;
                curNode = curNode.Next;
            } while (curNode != null);
            return;
        }

        public void Sort()
        {
            if (firstNode == null || firstNode.Next == null)
                return;
            int length = this.Count();
            for (int i = 0; i < length-1; i++)
            {
                int lowest = i;
                for (int j = 1+i; j < length; j++)
                {
                    if (NodeAt(j) < NodeAt(lowest))
                        lowest = j;
                }
                if (lowest != i)
                    Swap(i, lowest);
            }
            return;
        }

        private void Swap(int one, int two)
        {
            var swap = this[one];
            var swapTwo = this[two];
            this[two] = swap;
            this[one] = swapTwo;
        }


        private SinglyLinkedListNode NodeAt(int i)
        {
            var res = firstNode;
            for (int j = 0; j < i; j++)
            {
                res = res.Next;
            }
            return res;
        }

        public string[] ToArray()
        {
            List<string> res = new List<string>();
            SinglyLinkedListNode cur = firstNode;
            while (cur != null)
            {
                res.Add(cur.Value);
                cur = cur.Next;
            }
            return res.ToArray();
        }
    }
}
