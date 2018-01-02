﻿using System;
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



    public class LinkedList
    {
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
