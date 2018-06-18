using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interview
{
    public class RandomListNode
    {
        public int label;
        public RandomListNode next, random;
        public RandomListNode(int x) { this.label = x; }
    }
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int x) { val = x; }
    }


    public class DoublyListNode
    {
        public int val;
        public DoublyListNode(int i)
        {
            val = i;
        }
        public DoublyListNode pre;
        public DoublyListNode next;
    }

    public class LinkedList
    {
        //Doubly linkedlist contains 0 and 1 only, sort it in O(n), in-space
        // e.g.  0 <-> 1 <-> 1 <-> 0 <-> 1 <-> 0  
        //return 0 <-> 0 <-> 0 <-> 1 <-> 1 <-> 1
        DoublyListNode SortDoublyList(DoublyListNode head)
        {
            while (head == null)
                return null;

            //get tail ptr 
            var ptrEnd = head;
            var ptrStart = head;

            while (ptrEnd != null && ptrEnd.next != null)
                ptrEnd = ptrEnd.next;

            while(ptrStart!= ptrEnd)
            {
                if (ptrStart.val == 1)//swap with end
                {
                    int temp = ptrEnd.val;
                    ptrEnd.val = ptrStart.val;
                    ptrStart.val = temp;
                    ptrEnd = ptrEnd.pre;   // end index left shift one
                }
                else
                    ptrStart = ptrStart.next;
            }
            return head;
        }

        //Sort 2 Sorted LinkedList
        public ListNode SortTwo(ListNode n1, ListNode n2)
        {
            if (n1 == null && n2 == null)
                return null;
            if (n1 == null)
                return n2;
            if (n2 == null)
                return n1;

            var pt1 = n1;
            var pt2 = n2;

            var ret = new ListNode(-1);
            var retPt = ret;
            while (pt1 != null && pt2 != null)
            {
                if (pt1.val < pt2.val)
                {
                    retPt.next = new ListNode(pt1.val);
                    pt1 = pt1.next;
                }
                else
                {
                    retPt.next = new ListNode(pt2.val);
                    pt2 = pt2.next;
                }
                retPt = retPt.next;
            }

            while (pt1 != null)
            {
                retPt.next = new ListNode(pt1.val);
                pt1 = pt1.next;
                retPt = retPt.next;
            }
            while (pt2 != null)
            {
                retPt.next = new ListNode(pt2.val);
                pt2 = pt2.next;
                retPt = retPt.next;
            }
            return ret.next;
        }
       
        
        //328. Odd Even Linked List
        //Given a singly linked list, group all odd nodes together followed by the even nodes. 
        //Please note here we are talking about the node number and not the value in the nodes.
        //You should try to do it in place.The program should run in O(1) space complexity and O(nodes) time complexity.
        //Example: Given 1->2->3->4->5->NULL,
        //return         1->3->5->2->4->NULL.
        //Note:The relative order inside both the even and odd groups should remain as it was in the input. 
        //The first node is considered odd, the second node even and so on...
        public ListNode OddEvenList(ListNode head)
        {
            if (head == null || head.next == null)
                return head;

            var ptOdd = head;
            var ptEven = head.next;
            var ptrEven = ptEven;

            while (ptEven != null && ptEven.next != null)
            {
                ptOdd.next = ptEven.next;
                ptOdd = ptOdd.next;

                ptEven.next = ptOdd.next;
                ptEven = ptEven.next;
            }
            ptOdd.next = ptrEven;
            return head;
        }


        //61. Rotate List
        //Given a list, rotate the list to the right by k places, where k is non-negative.
        // Example: Given 1->2->3->4->5->NULL and k = 2,
        //return 4->5->1->2->3->NULL.        
        public ListNode RotateRight(ListNode head, int k)
        {
            if (head == null || head.next == null || k == 0) return head;

            //获取链表的总长度
            ListNode ptr1 = head; int len = 1;
            while (ptr1.next != null)
            { ptr1 = ptr1.next; len++; }
            //将链表首尾相连形成环
            ptr1.next = head;

            //找到需要截断的位置，因为k可能大于链表总长度。所以这里使用取余操作
            for (int i = 0; i < len - k % len; i++)
            {
                ptr1 = ptr1.next;
            }
            //将该处截断，指向空指针即可
            ListNode result = ptr1.next;
            ptr1.next = null;
            return result;
        }


        //206. Reverse Linked List
        //Reverse a singly linked list.
        public ListNode ReverseList(ListNode head)
        {
            if (head == null)
                return null;

            ListNode ptr = head;
            Stack<int> st = new Stack<int>();

            while (ptr != null)
            {
                st.Push(ptr.val);
                ptr = ptr.next;
            }

            ListNode ret = new ListNode(-1);
            ListNode retPtr = ret;
            while (st.Count != 0)
            {
                retPtr.next = new ListNode(st.Pop());
                retPtr = retPtr.next;
            }

            return ret.next;
        }

        //24. Swap Nodes in Pairs
        //Given a linked list, swap every two adjacent nodes and return its head.
        //For example        Given 1->2->3->4, you should return the list as 2->1->4->3.
        public ListNode SwapPairs(ListNode head)
        {
            if (head == null || head.next == null)
                return head;

            ListNode front = head.next;
            head.next = SwapPairs(head.next.next);
            front.next = head;
            return front;
        }


        //141. Linked List Cycle
        public bool HasCycle(ListNode head)
        {
            ListNode ptr1 = head;
            ListNode ptr2 = head;

            while (ptr1 != null && ptr2 != null && ptr2.next != null)
            {
                ptr1 = ptr1.next;
                ptr2 = ptr2.next.next;
                if (ptr1 == ptr2)
                    return true;
            }
            return false;
        }

        //237. Delete Node in a Linked List (given only the node to be deleted)
        public void DeleteNode(ListNode node)
        {
            if (node == null)
                return;
            if (node.next != null)
            {
                node.val = node.next.val;
                node.next = node.next.next;
            }
            else
                node = null;
        }

        //445. Add Two Numbers II
        //Input: (7 -> 2 -> 4 -> 3) + (5 -> 6 -> 4)
        //Output: 7 -> 8 -> 0 -> 7
        public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            if (l1 == null && l2 == null)
                return null;

            Stack<int> st1 = new Stack<int>();
            Stack<int> st2 = new Stack<int>();
            Stack<int> st3 = new Stack<int>();

            while (l1 != null)
            {
                st1.Push(l1.val);
                l1 = l1.next;
            }

            while (l2 != null)
            {
                st2.Push(l2.val);
                l2 = l2.next;
            }
            int carry = 0;
            while (st1.Count != 0 || st2.Count != 0 || carry != 0)
            {
                int v1 = st1.Count == 0 ? 0 : st1.Pop();
                int v2 = st2.Count == 0 ? 0 : st2.Pop();
                int v3 = (v1 + v2 + carry) % 10;
                carry = (v1 + v2 + carry) / 10;
                st3.Push(v3);
            }
            ListNode ret = new ListNode(-1);
            ListNode ptr = ret;
            while (st3.Count != 0)
            {
                ptr.next = new ListNode(st3.Pop());
                ptr = ptr.next;
            }
            return ret.next;
        }

        //leetcode 138. Copy List with Random Pointer 
        //A linked list is given such that each node contains an additional random pointer which could point to any node in the list or null.
        //Return a Deep copy of the list.
        public RandomListNode CopyRandomList(RandomListNode head)
        {
            if (head == null)
                return null;

            Dictionary<RandomListNode, RandomListNode> map = new Dictionary<RandomListNode, RandomListNode>();
            RandomListNode ptr = head;

            while (ptr != null)
            {
                map.Add(ptr, new RandomListNode(ptr.label));
                ptr = ptr.next;
            }

            ptr = head;
            while (ptr != null)
            {
                map[ptr].next = ptr.next == null ? null : map[ptr.next];
                map[ptr].random = ptr.random == null ? null : map[ptr.random];
                ptr = ptr.next;
            }
            return map[head];
        }
    }
}
