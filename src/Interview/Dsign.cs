using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Interview
{
    public class Dsign
    {
        //218. The Skyline Problem
        //A city's skyline is the outer contour of the silhouette formed by all the buildings in that city when 
        //viewed from a distance. Now suppose you are given the locations and height of all the buildings as 
        //shown on a cityscape photo (Figure A), write a program to output the skyline formed by these buildings 
        //collectively (Figure B).
        //The geometric information of each building is represented by a triplet of integers[Li, Ri, Hi], 
        //where Li and Ri are the x coordinates of the left and right edge of the ith building, respectively, 
        //and Hi is its height.It is guaranteed that 0 ≤ Li, Ri ≤ INT_MAX, 0 < Hi ≤ INT_MAX, and Ri - Li > 0. 
        //You may assume all buildings are perfect rectangles grounded on an absolutely flat surface at height 0.
        //For instance, the dimensions of all buildings in Figure A are recorded as: 
        //[ [2 9 10], [3 7 15], [5 12 12], [15 20 10], [19 24 8] ] .
        //The output is a list of "key points" (red dots in Figure B) in the format 
        //of[[x1, y1], [x2, y2], [x3, y3], ... ] that uniquely defines a skyline.A key point is the left endpoint 
        //of a horizontal line segment.Note that the last key point, where the rightmost building ends, is merely 
        //used to mark the termination of the skyline, and always has zero height.Also, the ground in between any 
        //two adjacent buildings should be considered part of the skyline contour.
        //For instance, the skyline in Figure B should be represented 
        //as:[ [2 10], [3 15], [7 12], [12 0], [15 10], [20 8], [24, 0] ].
        //Notes:
        //The number of buildings in any input list is guaranteed to be in the range[0, 10000].
        //The input list is already sorted in ascending order by the left x position Li.
        //The output list must be sorted by the x position.
        //There must be no consecutive horizontal lines of equal height in the output skyline.For instance, 
        //[...[2 3], [4 5], [7 5], [11 5], [12 7]...] is not acceptable; the three lines of height 5 should be 
        //merged into one in the final output as such: [...[2 3], [4 5], [12 7], ...]

        public IList<int[]> GetSkyline(int[,] buildings)
        {
            if (buildings == null)
                return null;

            var ret = new List<int[]>();

            int preL = 0;
            int preR = 0;
            int preH = 0;

            for (int i = 0; i < buildings.Length; i++)
            {
                //if next X.L position in previous X range and next hight > previous hight
                if (buildings[i, 0] <= preR && buildings[i, 0] > preL && buildings[i, 2] > preH)
                {
                    ret.Add(new int[] { buildings[i, 0], buildings[i, 2] });
                }
                else if (buildings[i, 0] <= preR && buildings[i, 0] > preL && buildings[i, 2] <= preH)
                {
                    ret.Add(new int[] { preR, buildings[i, 2] });
                }
                else if (buildings[i, 0] > preR)
                {
                    ret.Add(new int[] { preR, 0 });
                    ret.Add(new int[] { buildings[i, 0], buildings[i, 2] });
                }
                preL = buildings[i, 0];
                preR = buildings[i, 1];
                preH = buildings[i, 2];
            }
            ret.Add(new int[] { buildings[buildings.Length - 1, 1], 0 });
            return ret;
        }

        
        //348. Design a Tic-tac-toe game that is played between two players on a n x n grid.
        //You may assume the following rules:
        //A move is guaranteed to be valid and is placed on an empty block.
        //Once a winning condition is reached, no more moves is allowed. A player who succeeds in placing 
        //n of their marks in a horizontal, vertical, or diagonal row wins the game.
        //Example:Given n = 3, assume that player 1 is "X" and player 2 is "O" in the board.
        //Hint: Could you trade extra space such that move() operation can be done in O(1)?
        //You need two arrays: int rows[n], int cols[n], plus two variables: diagonal, anti_diagonal.
        public class TicTacToe
        {
            char[,] board;
            int len;
            public TicTacToe(int n)
            {
                len = n;
                board = new char[n, n];
            }

            int isWin(char t, int i, int j)
            {
                if (checkVerticle(t,i,j) || checkHarizonal(t, i, j) || checkDiag(t, i, j))
                    return t == 'X' ? 1 : 2;

                return 0;
            }

            bool checkVerticle(char t, int i, int j)
            {
                for (int k = 0; k < len; k++)
                {
                    if (board[k, j] != t)
                        return false;
                }
                return true;
            }
            bool checkHarizonal(char t, int i, int j)
            {
                for (int k = 0; k < len; k++)
                {
                    if (board[i,k] != t)
                        return false;
                }
                return true;
            }
            bool checkDiag(char t, int i, int j)
            {
                for (int k = 0; k < len; k++)
                {
                    if (board[k, k] != t)
                        return false;
                }
                for (int k = 0; k < len; k++)
                {
                    if(board[k,len-1-k] ==t)
                        return false;
                }
                return true;
            }

            public void Move(int player, int i, int j)
            {
                char move = player == 1 ? 'X' : 'O';
                if (board[i, j] == '\0')
                {
                    board[i, j] = move;
                    int result = isWin(move, i, j);
                    if (result == 1)
                        Console.Write("player 1 wins");
                    else if(result == 2)
                        Console.Write("player 2 wins");
                }
            }

        }

        //Hint: Could you trade extra space such that move() operation can be done in O(1)?
        //You need two arrays: int rows[n], int cols[n], plus two variables: diagonal, anti_diagonal.
        public class TicTacToe2
        {
            int len, Darr, RDarr;
            int[] Varr, Harr;
            public TicTacToe2(int n)
            {
                len = n;
                Varr = new int[n];
                Harr = new int[n];
            }

            int move(int player, int i, int j)
            {
                int add = player == 1 ? 1 : -1;

                Harr[i] += add;
                Varr[j] += add;
                if (i == j)
                    Darr += add;
                if (i == len - 1 - j)
                    RDarr += add;

                if (Math.Abs(Harr.Sum()) == len || Math.Abs(Varr.Sum()) == len || Math.Abs(Darr) == len || Math.Abs(RDarr) == len)
                    return player;
                else
                    return 0;
            }

        }
        //341. Flatten Nested List Iterator
        //Given a nested list of integers, implement an iterator to flatten it.
        //Each element is either an integer, or a list -- whose elements may also be integers or other lists.
        //Example 1: Given the list[[1, 1],2,[1,1]],
        //By calling next repeatedly until hasNext returns false, the order of elements returned by next should be: [1,1,2,1,1].
        //Example 2: Given the list[1,[4,[6]]],
        //By calling next repeatedly until hasNext returns false, the order of elements returned by next should be: [1,4,6].         
        public class NestedIterator
        {
            Stack<NestedInteger> stack;

            public NestedIterator(IList<NestedInteger> nestedList)
            {
                stack = new Stack<NestedInteger>();
                if (nestedList != null)
                {
                    for (int i = nestedList.Count - 1; i >= 0; i--)
                    {
                        stack.Push(nestedList[i]);
                    }
                }
            }

            public bool HasNext()
            {
                while (stack.Count != 0)
                {
                    if (stack.Peek().IsInteger())
                        return true;
                    var list = stack.Pop();
                    for (int i = list.GetList().Count - 1; i >= 0; i--)
                    {
                        stack.Push(list.GetList()[i]);
                    }
                }
                return false;
            }
            public int Next()
            {
                if (stack.Peek().IsInteger())
                    return stack.Pop().GetInteger();
                else
                    throw new Exception("");
            }
        }
        //alternative way, not real iterator, spend more memory O(n)
        public class NestedIterator2
        {
            List<int> q;
            int curIdx;
            public NestedIterator2(IList<NestedInteger> nestedList)
            {
                q = new List<int>();
                //DFS to save to q
                DFSHelper(nestedList);
            }
            void DFSHelper(IList<NestedInteger> nestedList)
            {
                if (nestedList != null)
                {
                    foreach (var nest in nestedList)
                    {
                        if (nest.IsInteger())
                            q.Add(nest.GetInteger());
                        else
                            DFSHelper(nest.GetList());
                    }
                }
            }

            public bool HasNext()
            {
                return curIdx < q.Count;
            }
            public int Next()
            {
                if (q.Count > 0)
                {
                    int ret = q[curIdx];
                    curIdx++;
                    return ret;
                }
                else
                    throw new Exception("");
            }
        }

        // This is the interface that allows for creating nested lists.
        // You should not implement it, or speculate about its implementation
        public interface NestedInteger
        {
            // @return true if this NestedInteger holds a single integer, rather than a nested list.
            bool IsInteger();
            // @return the single integer that this NestedInteger holds, if it holds a single integer
            // Return null if this NestedInteger holds a nested list
            int GetInteger();
            // @return the nested list that this NestedInteger holds, if it holds a nested list
            // Return null if this NestedInteger holds a single integer
            IList<NestedInteger> GetList();
            //Your NestedIterator will be called like this:
            //NestedIterator i = new NestedIterator(nestedList);
            //while (i.HasNext()) v[f()] = i.Next();
        }



        public class BSTIterator
        {
            int curIdx = 0;
            List<int> list;
            public BSTIterator(TreeNode root)
            {
                list = new List<int>();
                getSort(root, list);
            }
            public void getSort(TreeNode node, List<int> list)
            {
                if (node == null)
                    return;
                getSort(node.left, list);
                list.Add(node.val);

                getSort(node.right, list);
            }
            /** @return whether we have a next smallest number */
            public bool HasNext()
            {
                return curIdx < list.Count;
            }
            /** @return the next smallest number */
            public int Next()
            {
                int ret = list[curIdx];
                curIdx += 1;
                return ret;
            }
        }


        //208. Implement Trie (Prefix Tree)    
        //Implement a trie with insert, search, and startsWith methods.
        //Note: You may assume that all inputs are consist of lowercase letters a-z.
        public class Trie
        {
            private TrieNode root;
            public Trie()
            {
                root = new TrieNode();
            }
            // Inserts a word into the trie.
            public void Insert(String word)
            {
                TrieNode trieNode = root;
                for (int i = 0; i < word.Length; i++)
                {
                    if (trieNode.children[word[i] - 'a'] == null)
                        trieNode.children[word[i] - 'a'] = new TrieNode();

                    trieNode = trieNode.children[word[i] - 'a'];
                }
                trieNode.word = word;
            }

            // Returns if the word is in the trie.
            public bool Search(string word)
            {
                return match(root, 0, word);
            }

            private bool match(TrieNode root, int idx, string word)
            {
                if (root == null)
                    return false;

                if (idx == word.Length)
                {
                    return root.word == word;
                }
                root = root.children[word[idx] - 'a'];
                return match(root, idx + 1, word);
            }

            //Returns if there is any word in the trie, that starts with the given prefix.
            public bool StartsWith(string pre)
            {
                return matchPrefix(root, 0, pre);
            }

            private bool matchPrefix(TrieNode root, int idx, string pre)
            {
                if (root == null)
                    return false;

                if (idx == pre.Length)
                    return true;
                root = root.children[pre[idx] - 'a'];

                return matchPrefix(root, idx + 1, pre);
            }
        }
        class TrieNode
        {
            // Initialize your data structure here.
            public TrieNode()
            {
                children = new TrieNode[26];
            }
            public TrieNode[] children { get; set; }
            public string word { get; set; }
        }

    }


    //535. Encode and Decode TinyURL
    //TinyURL is a URL shortening service where you enter a URL such as https://leetcode.com/problems/design-tinyurl 
    //and it returns a short URL such as http://tinyurl.com/4e9iAk.
    //Design the encode and decode methods for the TinyURL service.There is no restriction on how your encode/decode 
    //algorithm should work.You just need to ensure that a URL can be encoded to a tiny URL and the tiny URL can be 
    //decoded to the original URL.
    public class Codec
    {
        Dictionary<string, string> encodeMap;
        Dictionary<string, string> decodeMap;
        const string baseURL = "http://tinyurl.com/";

        public Codec()
        {
            encodeMap = new Dictionary<string, string>();
            decodeMap = new Dictionary<string, string>();
        }
        // Encodes a URL to a shortened URL
        public string encode(string longUrl)
        {
            if (encodeMap.ContainsKey(longUrl))
                return encodeMap[longUrl];

            string charSet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder encodeVal = new StringBuilder();
            do
            {
                encodeVal.Clear();
                Random rd = new Random();
                for (int i = 0; i < 6; i++)
                {
                    int idx = rd.Next(0, charSet.Length);
                    encodeVal.Append(charSet[idx]);
                }
            } while (decodeMap.ContainsKey(baseURL + encodeVal.ToString()));

            string completeEncodeUrl = baseURL + encodeVal.ToString();
            encodeMap.Add(longUrl, completeEncodeUrl);
            decodeMap.Add(completeEncodeUrl, longUrl);
            return completeEncodeUrl;
        }

        // Decodes a shortened URL to its original URL.
        public string decode(string shortUrl)
        {
            return decodeMap[shortUrl];
        }
    }


    /**
     * Your LRUCache object will be instantiated and called as such:
     * LRUCache obj = new LRUCache(capacity);
     * int param_1 = obj.Get(key);
     * obj.Put(key,value);
     */
    //146. LRU Cache
    //Design and implement a data structure for Least Recently Used (LRU) cache. It should support the following operations: get and put.
    //get(key) - Get the value(will always be positive) of the key if the key exists in the cache, otherwise return -1.
    //put(key, value) - Set or insert the value if the key is not already present.When the cache reached its capacity, it should invalidate the least recently used item before inserting a new item.

    public class LRUCache
    {
        class DLinkedList
        {
            public int key { get; set; }
            public int value { get; set; }
            public DLinkedList pre { get; set; }
            public DLinkedList post { get; set; }
        }


        int cap = 0;
        int count = 0;
        Dictionary<int, DLinkedList> map;
        DLinkedList head;
        DLinkedList tail;
        public LRUCache(int capacity)
        {
            cap = capacity;
            map = new Dictionary<int, DLinkedList>();

            head = new DLinkedList();
            tail = new DLinkedList();

            head.post = tail;
            tail.pre = head;
        }

        public int Get(int key)
        {
            DLinkedList node = map[key];
            if (node == null)
                return -1;

            moveToHead(node);
            return node.value;
        }

        void Add(DLinkedList node)
        {
            node.pre = head;
            node.post = head.post;
            head.post.pre = node;
            head.post = node;
        }

        void moveToHead(DLinkedList node)
        {
            deleteNode(node);
            Add(node);
        }

        void deleteNode(DLinkedList node)
        {
            node.pre.post = node.post;
            node.post.pre = node.pre;
        }

        void removeTail()
        {
            DLinkedList node = tail.pre;
            deleteNode(node);
        }

        public void Put(int key, int value)
        {
            if (map.ContainsKey(key))
            {
                map[key].value = value;
                moveToHead(map[key]);
            }
            else
            {
                DLinkedList newNode = new DLinkedList();
                newNode.key = key;
                newNode.value = value;
                map.Add(key, newNode);
                Add(newNode);
                count++;

                if (count > cap)
                {
                    removeTail();
                    map.Remove(key);
                    count--;
                }
            }
        }
    }


    //interview of Agoda in Hackerrank, practice of virtual , abstract, override 
    public abstract class CalculatorBase
    {
        private double balance;

        public void Execute(double unitPrice, int numberOfItems)
        {
            this.balance = this.CalculatePrice(unitPrice, numberOfItems);
            this.balance += this.CalculateTax(this.balance);
            Console.WriteLine(string.Format("{0:N2}", this.balance));
        }

        public abstract double CalculatePrice(double unitPrice, int numberOfItems);

        public virtual double CalculateTax(double balance)
        {
            return balance;
        }
    }
    public class Thailand : CalculatorBase
    {
        double tax = 0.07;

        public override double CalculatePrice(double unitPrice, int numberOfItems)
        {
            return unitPrice * numberOfItems;
        }
        public override double CalculateTax(double balance)
        {
            balance = balance * tax;
            return balance;
        }
    }

    //170. Two Sum III - Data structure design  (not easy! actually)
    //Design and implement a TwoSum class. It should support the following operations: add and find.
    //    add - Add the number to an internal data structure.
    //find - Find if there exists any pair of numbers which sum is equal to the value.
    //For example, add(1); add(3); add(5);   ,  find(4) -> true   find(7) -> false
    public class TwoSum
    {
        List<int> list;
        Dictionary<int, int> map;
        int count;
        /** Initialize your data structure here. */
        public TwoSum()
        {
            list = new List<int>();
            map = new Dictionary<int, int>();

            count = 0;
        }

        /** Add the number to an internal data structure.. */
        public void Add(int number)
        {
            if (map.ContainsKey(number))
                map[number] += 1;
            else
            {
                map.Add(number, 1);
                list.Add(number);
            }
        }
        /** Find if there exists any pair of numbers which sum is equal to the value. */
        public bool Find(int value)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int num1 = list[i];
                int num2 = value - num1;
                //for cases like 3,3, 0,0 repeated number input
                if ((map.ContainsKey(num2) && num1 != num2) || (num1 == num2 && map[num1] > 1))
                    return true;
            }
            return false;
        }
    }


    //Houlihan Lokey  onsite
    //write code to calculate the circumference of the circle without modifying class itself
    //(closure )
    public sealed class Circle
    {
        private double radius;
        public Circle()
        {
            radius = 10;
        }
        public double Calculate(Func<double, double> op)
        {
            return op(radius);
        }
    }

    //Nested class example
    public interface IFoo
    {
        int Foo { get; }
    }
    public class Factory
    {
        private class MyFoo : IFoo
        {
            public int Foo { get; set; }
        }
        public IFoo CreateFoo(int value)
        {
            return new MyFoo { Foo = value };
        }
        //=> new MyFoo { Foo = value };
    }


    public class AsyncTest
    {
        public async void TestAsync()
        {
            Task<string> tt = longRunTaskAsync();
            Console.WriteLine("doesn't block main thread");
            Thread.Sleep(5000);
            //await AsyncMethod();

            string x = await tt;

            Console.WriteLine(x);
            Console.WriteLine("waiting for long run thread feedback...");
        }

        public async Task<string> longRunTaskAsync()
        {
            await Task.Delay(5000);
            return "long run finished";

        }

    }

    //design hashtable class without using the built-in classes
    public class HashTable<TKey, TValue>
    {
        private LinkedList<Tuple<TKey, TValue>>[] _items;
        private int _fillFactor = 3;
        private int _size;

        public HashTable()
        {
            _items = new LinkedList<Tuple<TKey, TValue>>[4];
        }

        public void Add(TKey key, TValue value)
        {
            var pos = GetPosition(key, _items.Length);
            if (_items[pos] == null)
            {
                _items[pos] = new LinkedList<Tuple<TKey, TValue>>();
            }
            if (_items[pos].Any(x => x.Item1.Equals(key)))
            {
                throw new Exception("Duplicate key, cannot insert.");
            }
            _size++;
            if (NeedToGrow())
            {
                GrowAndReHash();
            }
            pos = GetPosition(key, _items.Length);
            if (_items[pos] == null)
            {
                _items[pos] = new LinkedList<Tuple<TKey, TValue>>();
            }
            _items[pos].AddFirst(new Tuple<TKey, TValue>(key, value));
        }

        public void Remove(TKey key)
        {
            var pos = GetPosition(key, _items.Length);
            if (_items[pos] != null)
            {
                var objToRemove = _items[pos].FirstOrDefault(item => item.Item1.Equals(key));
                if (objToRemove == null) return;
                _items[pos].Remove(objToRemove);
                _size--;
            }
            else
            {
                throw new Exception("Value not in HashTable.");
            }
        }

        public TValue Get(TKey key)
        {
            var pos = GetPosition(key, _items.Length);
            foreach (var item in _items[pos].Where(item => item.Item1.Equals(key)))
            {
                return item.Item2;
            }
            throw new Exception("Key does not exist in HashTable.");
        }

        private void GrowAndReHash()
        {
            _fillFactor *= 2;
            var newItems = new LinkedList<Tuple<TKey, TValue>>[_items.Length * 2];
            foreach (var item in _items.Where(x => x != null))
            {
                foreach (var value in item)
                {
                    var pos = GetPosition(value.Item1, newItems.Length);
                    if (newItems[pos] == null)
                    {
                        newItems[pos] = new LinkedList<Tuple<TKey, TValue>>();
                    }
                    newItems[pos].AddFirst(new Tuple<TKey, TValue>(value.Item1, value.Item2));
                }
            }
            _items = newItems;
        }

        private int GetPosition(TKey key, int length)
        {
            var hash = key.GetHashCode();
            var pos = Math.Abs(hash % length);
            return pos;
        }

        private bool NeedToGrow()
        {
            return _size >= _fillFactor;
        }
    }
}
