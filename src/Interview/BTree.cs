﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interview
{
    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int x) { val = x; }
    }
    public class TreeLinkNode
    {
        int val;
        public TreeLinkNode left, right, next;
        public TreeLinkNode(int x) { val = x; }
    }
    public class BTree
    {
        //110. Balanced Binary Tree
        //Given a binary tree, determine if it is height-balanced.
        //For this problem, a height-balanced binary tree is defined as:
        //a binary tree in which the depth of the two subtrees of every node never differ by more than 1.
        //Example 1:Given the following tree [3,9,20,null,null,15,7]:
        //       3
        //      / \
        //     9  20
        //       /  \
        //      15   7      return true
        public bool IsBalanced(TreeNode root)
        {
            if (root == null)
                return true;

            if (Math.Abs(MaxDepth(root.right) - MaxDepth(root.left)) > 1)
                return false;

            return IsBalanced(root.left) && IsBalanced(root.right);
        }
       
        //105. Construct Binary Tree from Preorder and Inorder Traversal
        //Given preorder and inorder traversal of a tree, construct the binary tree.
        //Note:You may assume that duplicates do not exist in the tree.
        //For example, given preorder = [3, 9, 20, 15, 7]
        //inorder = [9, 3, 15, 20, 7]
        //Return the following binary tree:
        //          3
        //         / \
        //        9  20
        //          /  \
        //         15   7
        public TreeNode BuildTree(int[] preorder, int[] inorder)
        {
            if (preorder == null || preorder.Length == 0 || inorder == null || inorder.Length == 0)
                return null;

            int preStart = 0;
            int inStart = 0;
            int inEnd = inorder.Length - 1;
            int preEnd = preorder.Length - 1;

            return buildTreeHelper(preorder, preStart, preEnd, inorder, inStart, inEnd);
        }
        TreeNode buildTreeHelper(int[] preorder, int preStart, int preEnd, int[] inorder, int inStart, int inEnd)
        {
            int pivValue = preorder[preStart];

            int inOrderPivIdx = Array.FindIndex(inorder, v => v == pivValue);
            
            var rootNode = new TreeNode(pivValue);
            rootNode.left = buildTreeHelper(preorder, preStart + 1, preStart + (inOrderPivIdx - inStart), inorder, inStart, inOrderPivIdx - 1);
            rootNode.right = buildTreeHelper(preorder, preStart + (inOrderPivIdx - inStart) + 1, preEnd, inorder, inOrderPivIdx + 1, inEnd);
            return rootNode;

        }

        //285.	Inorder Successor in BST
        //Given a binary search tree and a node in it, find the in-order successor of that node in the BST.
        //Note: If the given node has no in-order successor in the tree, return null.
        public TreeNode inorderSuccessor(TreeNode root, TreeNode p)
        {
            if (root == null || p == null)
                return null;

            TreeNode successor = null;
            while(root!=null)
            {

                if (root.val > p.val)
                {
                    successor = root;
                    root = root.left;
                }
                else
                    root = root.right;
                
            }
            return successor;
        }

        // Inorder Successor in regular BT
        public TreeNode inorderSuccessorBTree(TreeNode root, TreeNode p)
        {
            if (root == null || p == null)
                return null;

            Stack<TreeNode> st = new Stack<TreeNode>();
            bool isFoundNode = false;

            while (root != null || st.Count!=0)
            {
                if(root!=null)
                {
                    var rootPt = root;
                    st.Push(rootPt);
                    root = root.left;
                }
                else
                {
                    root = st.Pop();
                    if (isFoundNode)  //found p in previous round, this node from stack should be successor
                        return root;
                    if(p == root)
                        isFoundNode = true;
                    
                    root = root.right;
                }
            }
            return null;
        }

        public TreeNode inorderSuccessorBTree2(TreeNode root, TreeNode p)
        {
            if (root == null || p == null)
                return null;
            var st = new Stack<TreeNode>();
            saveLeftSubTree(root, st);

            while(st.Count>0)
            {
                var curNode = st.Pop();
                if (curNode!=null && curNode == p)
                {
                    if (st.Count > 0)
                        return st.Pop();
                    else
                        return null;
                }
                else
                {
                    saveLeftSubTree(curNode.right, st);
                }
            }
            return null;
        }
        void saveLeftSubTree(TreeNode node, Stack<TreeNode> st)
        {
            if (node == null)
                return;
            st.Push(node);
            saveLeftSubTree(node.left, st);
        TreeNode findMostLeftChildHelper(TreeNode node)
        }

        {
            if (node == null)
                return null;
            if (node.left != null)
                return findMostLeftChildHelper(node.left);
            else if (node.left == null && node.right == null)
                return node;
            else if (node.left == null && node.right != null)
                return node;
            else
                return null;
        }


        //173. Binary Search Tree Iterator
        //Implement an iterator over a binary search tree (BST). Your iterator will be initialized with the root node of a BST.
        //Calling next() will return the next smallest number in the BST.
        //Note: next() and hasNext() should run in average O(1) time and uses O(h) memory, where h is the height of the tree.
        /** * Your BSTIterator will be called like this:
            BSTIterator i = new BSTIterator(root);
            while (i.HasNext()) v[f()] = i.Next();*/
        //
        public class BSTIterator
        {
            Stack<TreeNode> st;
            public BSTIterator(TreeNode root)
            {
                st = new Stack<TreeNode>();
                saveLeftTreeToStack(root, st);
            }

            void saveLeftTreeToStack(TreeNode node, Stack<TreeNode> stack)
            {
                if (node == null)
                    return;
                stack.Push(node);
                saveLeftTreeToStack(node.left, stack);
            }

            /** @return whether we have a next smallest number */
            public bool HasNext()
            {
                return st.Count != 0;                    
            }

            /** @return the next smallest number */
            public int Next()
            {
                if (st.Count > 0)
                {
                    TreeNode curNode = st.Pop();
                    
                    if(curNode.right!=null)
                    {
                        saveLeftTreeToStack(curNode.right, st);
                    }
                    return curNode.val;
                }
                else
                    throw new Exception("end of road");
            }

        }

        //314.	Binary Tree Vertical Order Traversal
        //Given a binary tree, return the vertical order traversal of its nodes' values. (ie, from top to bottom, column by column).
        //If two nodes are in the same row and column, the order should be from left to right.
        //Examples: Given binary tree[3, 9, 20, null, null, 15, 7],
        //      3
        //     / \
        //    9  20
        //      /  \
        //     15   7
        //return its vertical order traversal as:
        //[  [9],  [3,15],  [20],  [7] ]

        //Given binary tree [3,9,20,4,5,2,7],
        //        3
        //      /   \
        //     9    20
        //    / \   / \
        //   4   5 2   7
        //return its vertical order traversal as:
        //[  [4],  [9],  [3,5,2],  [20],  [7]  ]
        public List<List<int>> verticalOrder(TreeNode root)
        {
            var ret = new List<List<int>>();
            if (root == null)
                return ret;

            var map = new Dictionary<int, List<int>>();
            var columnQ = new Queue<int>();
            var nodeQ = new Queue<TreeNode>();

            int min = 0;
            int max = 0;

            columnQ.Enqueue(0);
            nodeQ.Enqueue(root);

            while (nodeQ.Count != 0)
            {
                TreeNode curNode = nodeQ.Dequeue();
                int curColumn = columnQ.Dequeue();
                if (map.ContainsKey(curColumn))
                    map[curColumn].Add(curNode.val);
                else
                    map.Add(curColumn, new List<int>(curNode.val));

                if (curNode.left != null)
                {
                    nodeQ.Enqueue(curNode.left);
                    columnQ.Enqueue(curColumn - 1);
                    min = Math.Min(curColumn - 1, min);
                }
                if (curNode.right != null)
                {
                    nodeQ.Enqueue(curNode.right);
                    columnQ.Enqueue(curColumn + 1);
                    max = Math.Max(curColumn + 1, max);
                }
            }

            for (int i = min; i <= max; i++)
            {
                ret.Add(map[i]);
            }
            return ret;
        }


        //257. Binary Tree Paths
        //Given a binary tree, return all root-to-leaf paths.
        //For example, given the following binary tree:
        //        1              All root-to-leaf paths are:["1->2->5", "1->3"]
        //      /   \
        //     2     3
        //      \
        //       5
        public IList<string> BinaryTreePaths(TreeNode root)
        {
            var ret = new List<string>();
            if (root == null)
                return ret;

            AllPathHelper(root, "", ret);

            return ret;
        }
        void AllPathHelper(TreeNode root, string curPath, IList<string> ret)
        {
            if (root.left == null && root.right == null)
            {
                ret.Add(curPath + root.val);
                return;
            }
            curPath += root.val + "->";

            if (root.left != null)
                AllPathHelper(root.left, curPath, ret);
            if (root.right != null)
                AllPathHelper(root.right, curPath, ret);
        }


        //501. Find Mode in Binary Search Tree
        //Given a binary search tree (BST) with duplicates, find all the mode(s) (the most frequently occurred element) in the given BST.
        // Assume a BST is defined as follows:
        public int[] FindMode(TreeNode root)
        {
            if (root == null)
                return null;

            Dictionary<int, int> map = new Dictionary<int, int>();
            helper(map, root);

            var list = map.OrderByDescending(p => p.Value).ToList();

            int max = list[0].Value;
            List<int> ret = new List<int>();
            foreach (var x in list)
            {
                if (x.Value == max)
                    ret.Add(x.Key);
            }
            return ret.ToArray();
        }
        void helper(Dictionary<int, int> map, TreeNode root)
        {
            if (root == null)
                return;

            if (map.ContainsKey(root.val))
                map[root.val] += 1;
            else
                map.Add(root.val, 1);

            helper(map, root.left);
            helper(map, root.right);
        }

        //108. Convert Sorted Array to Binary Search Tree
        //Given an array where elements are sorted in ascending order, convert it to a height balanced BST.        
        public TreeNode SortedArrayToBST(int[] nums)
        {
            int st = 0;
            int end = nums.Length - 1;
            return travelArrayToTree(st, end, nums);
        }

        TreeNode travelArrayToTree(int st, int end, int[] nums)
        {
            if (st <= end)
            {
                int pivol = (end + st) >> 1;
                TreeNode newNode = new TreeNode(nums[pivol]);

                newNode.left = travelArrayToTree(st, pivol - 1, nums);
                newNode.right = travelArrayToTree(pivol + 1, end, nums);
                return newNode;
            }
            return null;
        }

        //96. Unique Binary Search Trees
        //Given n, how many structurally unique BST's (binary search trees) that store values 1...n?
        //For example,  Given n = 3, there are a total of 5 unique BST's.
        //      1         3     3      2      1
        //       \       /     /      / \      \
        //        3     2     1      1   3      2
        //       /     /       \                 \
        //      2     1         2                 3
        public int NumTrees(int n)
        {
            if (n <= 1)
                return 1;

            int[] dp = new int[n + 1];
            dp[0] = 1;
            dp[1] = 1;
            dp[2] = 2;

            for (int i = 3; i <= n; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    dp[i] += dp[j] * dp[i - j - 1];
                }
            }
            return dp[n];
        }


        //Amazon: is b-tree mirror
        public bool IsMirror(TreeNode node)
        {
            if (node == null)
                return true;

            return isMirrorHelp(node.left, node.right);
        }
        bool isMirrorHelp(TreeNode left, TreeNode right)
        {
            if (left == null || right == null)
                return left == null && right == null;
            return left.val == right.val && isMirrorHelp(left.left, right.right) && isMirrorHelp(left.right, right.left);

        }

        //Amazon Given a Binary Search Tree, Find the distance between 2 nodes
        public int DistanceOf2Nodes(TreeNode root, TreeNode node1, TreeNode node2)
        {
            TreeNode croot = lowerestCommonAncestor(root, node1, node2);
            return findDistOfAncestor(croot, node1, 0) + findDistOfAncestor(croot, node2, 0);

        }
        int findDistOfAncestor(TreeNode root, TreeNode node1, int depth)
        {
            if (root == null)
                return -1;
            if (root == node1)
                return depth;

            int d = findDistOfAncestor(root.left, node1, depth + 1);

            return d != -1 ? d : findDistOfAncestor(root.right, node1, depth + 1);
        }

        TreeNode lowerestCommonAncestor(TreeNode root, TreeNode node1, TreeNode node2)
        {
            if (root == null)
                return null;

            if (isCover(root.left, node1) && isCover(root.right, node2))
                return root;
            if (isCover(root.left, node1) && isCover(root.left, node2))
                return lowerestCommonAncestor(root.left, node1, node2);

            if (isCover(root.right, node1) && isCover(root.right, node2))
                return lowerestCommonAncestor(root.right, node1, node2);

            return null;
        }
        bool isCover(TreeNode root, TreeNode node1)
        {
            if (root == null)
                return false;

            if (root == node1)
                return true;
            return isCover(root.left, node1) || isCover(root.right, node1);
        }

        //226. Invert Binary Tree
        //     4                    4
        //   /   \                /   \
        //  2     7       ->     7     2
        // / \   / \            / \   / \
        //1   3 6   9          9   6  3  1
        public TreeNode InvertTree(TreeNode root)
        {
            if (root == null)
                return null;

            InvertTree(root.left);
            InvertTree(root.right);

            TreeNode temp = root.left;
            root.left = root.right;
            root.right = temp;

            return root;
        }


        //297. Serialize and Deserialize Binary Tree  (Not passed yet)
        //For example, you may serialize the following tree
        //    1         as "[1,2,3,null,null,4,5]", just the same as how LeetCode OJ 
        //   / \        serializes a binary tree.You do not necessarily need to follow 
        //  2   3       this format, so please be creative and come up with different approaches yourself.
        //     / \
        //    4   5

        public string serialize(TreeNode root)
        {
            if (root == null)
                return "";

            StringBuilder sb = new StringBuilder();
            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root);

            while (q.Count != 0)
            {
                var curNode = q.Dequeue();
                if (curNode == null)
                {
                    sb.Append("# ");
                    continue;
                }
                else
                    sb.Append(curNode.val).Append(" ");

                q.Enqueue(curNode.left);
                q.Enqueue(curNode.right);
            }

            return sb.ToString();
        }

        // Decodes your encoded data to tree.
        public TreeNode deserialize(string data)
        {
            if (string.IsNullOrEmpty(data))
                return null;

            string[] strs = data.Split(' ');
            TreeNode root = new TreeNode(Convert.ToInt32(strs[0]));
            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root);

            for (int i = 1; i < strs.Length; i++)
            {
                if (q.Count > 0)
                {
                    TreeNode parent = q.Dequeue();
                    if (strs[i] != "#")
                    {
                        TreeNode left = new TreeNode(Convert.ToInt32(strs[i]));
                        parent.left = left;
                        q.Enqueue(left);
                    }
                    if (strs[++i] != "#")
                    {
                        TreeNode right = new TreeNode(Convert.ToInt32(strs[i]));
                        parent.right = right;
                        q.Enqueue(right);
                    }
                }
            }
            return root;
        }


        //156. Binary Tree Upside Down
        //Given a binary tree where all the right nodes are either leaf nodes with a sibling (a left node that shares the same parent node) or empty, flip it upside down and turn it into a tree where the original right nodes turned into left leaf nodes. Return the new root.
        //For example:  Given a binary tree {1,2,3,4,5}, return the root of the binary tree[4, 5, 2,#,#,3,1].
        //    1               4
        //   / \             / \  
        //  2   3           5   2
        // / \                 / \
        //4   5               3   1        
        public TreeNode UpsideDownBinaryTree(TreeNode root)
        {
            if (root == null || root.left == null)
                return root;

            TreeNode newRoot = UpsideDownBinaryTree(root.left);

            root.left.left = root.right;
            root.left.right = root;
            root.left = null;
            root.right = null;

            return newRoot;
        }


        //104. Maximum Depth of Binary Tree
        public int MaxDepth(TreeNode root)
        {
            if (root == null)
                return 0;

            return 1 + Math.Max(MaxDepth(root.left), MaxDepth(root.right));
        }

        //366. Find Leaves of Binary Tree
        //Given a binary tree, collect a tree's nodes as if you were doing this: Collect and remove all leaves, repeat until the tree is empty.
        // Example:  Given binary tree 
        //       1
        //      / \
        //     2   3
        //    / \     
        //   4   5    Returns[4, 5, 3], [2], [1].   so smart !!
        public IList<IList<int>> FindLeaves(TreeNode root)
        {
            List<IList<int>> ret = new List<IList<int>>();
            if (root == null)
                return ret;

            leafHelper(ret, root);
            return ret;
        }
        int leafHelper(IList<IList<int>> ret, TreeNode node)
        {
            if (null == node)
                return -1;
            int level = 1 + Math.Max(leafHelper(ret, node.left), leafHelper(ret, node.right));

            if (ret.Count() < level + 1)
                ret.Add(new List<int>());

            ret[level].Add(node.val);
            return level;
        }

        //116. Populating Next Right Pointers in Each Node
        //For example,  Given the following perfect binary tree,
        //             1
        //            /  \
        //           2    3
        //          / \  / \
        //         4  5  6  7
        //    After calling your function, the tree should look like:
        //     1 -> NULL
        //   /  \
        //  2 -> 3 -> NULL
        // / \  / \
        //4->5->6->7 -> NULL        
        public void connect(TreeLinkNode root)
        {
            if (root == null)
                return;
            
            while (root != null)
            {
                TreeLinkNode curNode = root;
                while (curNode != null)
                {
                    if (curNode.left != null)
                        curNode.left.next = curNode.right;
                    if (curNode.right != null && curNode.next != null)
                        curNode.right.next = curNode.next.left;
                    curNode = curNode.next;
                }
                root = root.left;
            }
        }


        //117. Populating Next Right Pointers in Each Node II
        //Follow up for problem "Populating Next Right Pointers in Each Node".
        //What if the given tree could be any binary tree? Would your previous solution still work?
        //Note:You may only use constant extra space.
        //For example, Given the following binary tree,
        //       1
        //     /  \
        //    2    3
        //   / \    \
        //  4   5    7
        //After calling your function, the tree should look like:
        //       1 -> NULL
        //      /  \
        //     2 -> 3 -> NULL
        //    / \    \
        //   4-> 5 -> 7 -> NULL
        public void connect2(TreeLinkNode root)
        {
            if (root == null)
                return;
            while (root != null)
            {
                TreeLinkNode tempChild = new TreeLinkNode(-1);
                TreeLinkNode currentChild = tempChild;
                while (root != null)
                {
                    if (root.left != null) { currentChild.next = root.left; currentChild = currentChild.next; }
                    if (root.right != null) { currentChild.next = root.right; currentChild = currentChild.next; }
                    root = root.next;
                }
                root = tempChild.next;
            }
        }



        //102. Binary Tree Level Order Traversal
        public IList<IList<int>> LevelOrder(TreeNode root)
        {
            var ret = new List<IList<int>>();
            if (root == null)
                return ret;

            levelHelp(ret, root, 0);
            return ret;
        }
        public void levelHelp(IList<IList<int>> ret, TreeNode root, int level)
        {
            if (root == null)
                return;

            if (level >= ret.Count)
            {
                ret.Add(new List<int>());
            }

            ret[level].Add(root.val);

            levelHelp(ret, root.left, level + 1);
            levelHelp(ret, root.right, level + 1);
        }


        //103. Binary Tree Zigzag Level Order Traversal
        //Given a binary tree, return the zigzag level order traversal of its nodes' values. (ie, from left to right, then right to left for the next level and alternate between).
        // For example: Given binary tree[3, 9, 20, null, null, 15, 7],
        //      3
        //     / \
        //    9  20
        //      /  \
        //     15   7
        //return its zigzag level order traversal as:
        //[  [3],
        //   [20,9],
        //   [15,7]
        //]        
        public IList<IList<int>> ZigzagLevelOrder(TreeNode root)
        {
            List<IList<int>> ret = new List<IList<int>>();
            if (root == null)
                return ret;

            treeLevelOrder(ret, root, 0);
            return ret;
        }
        void treeLevelOrder(IList<IList<int>> ret, TreeNode node, int level)
        {
            if (node == null)
                return;

            if (ret.Count <= level)
                ret.Add(new List<int>());

            if (level % 2 == 0)
                ret[level].Add(node.val);
            else
                ret[level].Insert(0, node.val);

            treeLevelOrder(ret, node.left, level + 1);
            treeLevelOrder(ret, node.right, level + 1);
        }

        //235. Lowest Common Ancestor of a Binary Search Tree
        //Given a binary search tree (BST), find the lowest common ancestor (LCA) of two given nodes in the BST.
        //According to the definition of LCA on Wikipedia: “The lowest common ancestor is defined between two 
        //nodes v and w as the lowest node in T that has both v and w as descendants(where we allow a node to be a descendant of itself).”
        public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
        {
            if (root == null)
                return null;

            if (isChild(root.left, p) && isChild(root.left, q))
                return LowestCommonAncestor(root.left, p, q);

            if (isChild(root.right, p) && isChild(root.right, q))
                return LowestCommonAncestor(root.right, p, q);

            return root;
        }
        bool isChild(TreeNode root, TreeNode n)
        {
            if (root == null)
                return false;
            if (root == n)
                return true;

            return isChild(root.left, n) || isChild(root.right, n);
        }

        public TreeNode LowestCommonAncestor2(TreeNode root, TreeNode p, TreeNode q)
        {
            if (root == null || root == p || root == q)
                return root;

            TreeNode left = LowestCommonAncestor2(root.left, p, q);
            TreeNode right = LowestCommonAncestor2(root.right, p, q);
            
            if (left == null)
                return right;
            else if (right == null)
                return left;
            else
                return root;
        }

        //98. Validate Binary Search Tree
        //Given a binary tree, determine if it is a valid binary search tree (BST).        
        public bool IsValidBST(TreeNode root)
        {
            return helper(root, null, null);
        }
        bool helper(TreeNode root, int? min, int? max)
        {
            if (root == null)
                return true;

            if ((min != null && root.val <= min) || (max != null && root.val >= max))
                return false;

            return helper(root.left, min, root.val) && helper(root.right, root.val, max);
        }

        //270. Closest Binary Search Tree Value
        //Given a non-empty binary search tree and a target value, find the value in the BST that is closest to the target.
        //Note: Given target value is a floating point. You are guaranteed to have only one unique 
        //value in the BST that is closest to the target.
        int minVal = int.MaxValue;
        double absDist = double.MaxValue;
        public int ClosestValue(TreeNode root, double target)
        {
            if (root == null)
                return minVal;

            if (absDist > Math.Abs(root.val - target))
            {
                absDist = Math.Abs(root.val - target);
                minVal = root.val;
            }
            if (root.val > target)
                return ClosestValue(root.left, target);
            else if (root.val < target)
                return ClosestValue(root.right, target);

            return root.val;
        }




    }
}
