using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Interview
{

    public class ArrayString
    {
        
        //125. Valid Palindrome
        //Given a string, determine if it is a palindrome, considering only alphanumeric characters and ignoring cases.
        //For example, "A man, a plan, a canal: Panama" is a palindrome. "race a car" is not a palindrome.
        // Note:Have you consider that the string might be empty? This is a good question to ask during an interview.
        // For the purpose of this problem, we define empty string as valid palindrome.
        public bool IsPalindrome(string s)
        {
            if (string.IsNullOrEmpty(s))
                return true;
            
            int i = 0;
            int j = s.Length - 1;

            while (i < j)
            {
                while (i < s.Length && !(char.IsLetter(s[i]) || char.IsNumber(s[i])))
                    i++;

                while (j >= 0 && !(char.IsLetter(s[j]) || char.IsNumber(s[j])))
                    j--;

                if (i >= j)
                    return true;

                if (s[i].ToString().ToUpper() != s[j].ToString().ToUpper())
                    return false;
                i++;
                j--;
            }
            return true;
        }

        
        //161. One Edit Distance(substring)
        //Given two strings S and T, determine if they are both one edit distance apart.
        //如果字符串长度相等，那么判断对应位置不同的字符数是不是1即可。
        //如果字符串长度相差1，那么肯定是要在长的那个串删掉一个，所以两个字符串一起加加，一旦遇到一个不同，
        //那么剩下的子串就要是一样，否则就是不止一个不同，false。
        public bool isOneEditDistance(string s, string t)
        {
            if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(t))
                return false;
            if (Math.Abs(s.Length - t.Length) == 1)
                return true;
            if(s.Length==t.Length)
            {
                if (s == t)
                    return false;
                for (int i = 0; i<s.Length; i++)
                {
                    if (s[i] != t[i])
                    {
                        if (i + 1 == s.Length)
                            return true;
                        return s.Substring(i + 1, s.Length - i) == t.Substring(i + 1, s.Length - i);
                    }   
                }
            }
            return false;
        }


        //157. Read N Characters Given Read4  (not resoved yet)
        //The API: int read4(char *buf) reads 4 characters at a time from a file. 
        //The return value is the actual number of characters read.For example, it returns 3 
        //if there is only 3 characters left in the file.
        //By using the read4 API, implement the function int read(char* buf, int n) that reads n characters from the file.
        //Note:The read function will only be called once for each test case.
        int read(char[] buf, int n)
        {
            return 0;
        }


        //Given an array of meeting time intervals consisting of start and end times[[s1, e1],[s2, e2],...] 
        //(si<ei), find the minimum number of conference rooms required.
        //e.g. Given[[0, 30],[5, 10],[15, 20]], return 2.
        int minMeetingRooms(Interval[] intervals)
        {
            if (intervals == null || intervals.Length == 0)
                return 0;

            Dictionary<int, int> map = new Dictionary<int, int>();
            for (int i = 0; i < intervals.Length; i++)
            {
                map[intervals[i].start]++;
                map[intervals[i].end]--;
            }
            int ret = 0;
            int max = 0;
            foreach (var x in map)
            {
                max += x.Value;
                ret = Math.Max(ret, max);
            }
            return ret;
        }


        //252. Meeting room
        //Given an array of meeting time intervals consisting of start and end times
        //[[s1, e1],[s2, e2],...] (si<ei), determine if a person could attend all meetings.
        //For example,
        //Given[[0, 30],[5, 10],[15, 20]],return false.
        public bool canAttendMeetings(Interval[] intervals)
        {
            if (intervals == null || intervals.Length == 0)
                return false;
            if (intervals.Length == 1)
                return true;

            Array.Sort(intervals, (Interval a, Interval b) =>
            {
                return a.start - b.start;
            }
            );

            for (int i = 1; i < intervals.Length; i++)
            {
                if (intervals[i].start > intervals[i - 1].end)
                    return false;
            }
            return true;
        }


        //22. Generate Parentheses
        //Given n pairs of parentheses, write a function to generate all combinations of well-formed parentheses.
        //        For example, given n = 3, a solution set is:
        //[
        //  "((()))",
        //  "(()())",
        //  "(())()",
        //  "()(())",
        //  "()()()"
        //]
        public IList<string> GenerateParenthesis(int n)
        {
            List<string> ret = new List<string>();
            if (n == 0)
                return ret;

            helper(ret, n, "", 0, 0);
            return ret;
        }


        void helper(List<string> ret, int n, string cur, int st, int end)
        {
            if (cur.Length == n * 2)
            {
                ret.Add(cur);
                return;
            }
            if (st < n)
            {
                helper(ret, n, cur + "(", st + 1, end);
            }
            if (end < st)
            {
                cur += ")";
                helper(ret, n, cur, st, end + 1);
            }
        }

        //67. Add Binary
        //Given two binary strings, return their sum (also a binary string).
        // For example,        a = "11"  b = "1", return "100".
        public string AddBinary(string a, string b)
        {
            if (string.IsNullOrEmpty(a) && string.IsNullOrEmpty(b))
                return "0";
            if (string.IsNullOrEmpty(a) && !string.IsNullOrEmpty(b))
                return b;
            if (!string.IsNullOrEmpty(a) && string.IsNullOrEmpty(b))
                return a;

            int idxA = a.Length - 1;
            int idxB = b.Length - 1;
            int carry = 0;
            string ret = "";

            while (idxA >= 0 || idxB >= 0 || carry > 0)
            {
                int valA = 0;
                int valB = 0;
                if (idxA >= 0)
                    valA = a[idxA] - '0';
                if (idxB >= 0)
                    valB = b[idxB] - '0';

                ret = (valA + valB + carry) % 2 + ret;
                carry = (valA + valB + carry) / 2;
                idxA--;
                idxB--;
            }

            return ret;
        }

        //17. Letter Combinations of a Phone Number
        //Given a digit string, return all possible letter combinations that the number could represent.        
        //Input:Digit string "23"
        //Output: ["ad", "ae", "af", "bd", "be", "bf", "cd", "ce", "cf"].
        public IList<string> LetterCombinations(string digits)
        {
            string[] keymap = new string[10] { "", "", "abc", "def", "ghi", "jkl", "mno", "pqrs", "tuv", "wxyz" };

            List<string> ret = new List<string>();
            if (digits.Length == 0)
                return ret;

            List<string> keySets = new List<string>();
            for (int i = 0; i < digits.Length; i++)
            {
                if (!string.IsNullOrEmpty(keymap[digits[i] - '0']))
                    keySets.Add(keymap[digits[i] - '0']);
            }

            backtrackingHelp(ret, digits.Length, keySets, "", 0);
            return ret;
        }

        void backtrackingHelp(List<string> ret, int len, List<string> keySets, string cur, int idx)
        {
            if (cur.Length == len)
            {
                ret.Add(cur);
                return;
            }

            for (int i = idx; i < keySets.Count; i++)
            {
                for (int j = 0; j < keySets[i].Length; j++)
                {
                    cur += keySets[i][j];
                    backtrackingHelp(ret, len, keySets, cur, i + 1);
                    cur = cur.Remove(cur.Length - 1);
                }
            }
        }

        //242. Valid Anagram
        //For example,  s = "anagram", t = "nagaram", return true.
        //s = "rat", t = "car", return false.
        public bool IsAnagram(string s, string t)
        {
            if (s == null || t == null || s.Length != t.Length)
                return false;
            if (s.Length == 0)
                return true;

            var map = new Dictionary<char, int>();

            for(int i =0; i<s.Length; i++)
            {
                if (map.ContainsKey(s[i]))
                    map[s[i]]++;
                else
                    map.Add(s[i], 1);                
            }
            for (int i = 0; i < t.Length; i++)
            {
                if (map.ContainsKey(t[i]))
                {
                    if (map[t[i]] == 0)
                        return false;
                    map[t[i]]--;
                }
                else
                    return false;
            }

            return !map.Any(x => x.Value != 0);
        }

        //49. Group Anagrams  (Amazon onsite)
        //Given an array of strings, group anagrams together.
        //For example, given: ["eat", "tea", "tan", "ate", "nat", "bat"], 
        //Return:
        //  [  ["ate", "eat","tea"], ["nat","tan"], ["bat"] ]
        public IList<IList<string>> GroupAnagrams(string[] strs)
        {
            List<IList<string>> ret = new List<IList<string>>();
            List<string> list = new List<string>();
            for (int i = 0; i < strs.Length; i++)
            {
                list.Add(new String(strs[i].OrderBy(c => c).ToArray()));
            }

            var map = new Dictionary<string, List<int>>();
            for (int i = 0; i < list.Count; i++)
            {
                if (!map.ContainsKey(list[i]))
                    map.Add(list[i], new List<int>() { i });
                else
                    map[list[i]].Add(i);
            }

            foreach (var pair in map)
            {
                ret.Add(new List<string>());
                for (int i = 0; i < pair.Value.Count; i++)
                {
                    ret.ElementAt(ret.Count - 1).Add(strs[pair.Value[i]]);
                }
            }

            return ret;
        }


        //14. Longest Common Prefix
        //Write a function to find the longest common prefix string amongst an array of strings
        public string LongestCommonPrefix(string[] strs)
        {
            if (strs == null || strs.Length == 0)
                return "";
            if (strs.Length == 1)
                return strs[0];

            int len = strs.Min(x => x.Length);

            string ret = "";

            for (int strIdx = 0; strIdx < len; strIdx++)
            {
                char curCH = strs[0][strIdx];
                for (int arrIdx = 1; arrIdx < strs.Length; arrIdx++)
                {
                    if (strs[arrIdx][strIdx] == curCH && arrIdx == strs.Length - 1)
                    {
                        ret += curCH;
                    }
                    else if (strs[arrIdx][strIdx] == curCH)
                    {
                        continue;
                    }
                    else if (strs[arrIdx][strIdx] != curCH)
                    {
                        return ret;
                    }
                }

            }
            return ret;
        }


        //151. Reverse Words in a String
        //Given an input string, reverse the string word by word.
        //For example,Given s = "the sky is blue",return "blue is sky the".
        //For C programmers: Try to solve it in-place in O(1) spac
        public string ReverseWords(string s)
        {
            string[] temp = s.Split(' ');

            int st = 0;
            int end = temp.Length - 1;
            while (st < end)
            {
                swap(st, end, temp);
                st++;
                end--;
            }
            string ret = "";

            foreach (var x in temp)
            {
                if (x == " " || x == "")
                    continue;
                else
                    ret += x + " ";
            }
            return ret.Trim();
        }
        void swap(int i, int j, string[] strs)
        {
            string temp = strs[i].Trim();
            strs[i] = strs[j].Trim();
            strs[j] = temp.Trim();
        }


        //NVIDIA Put 1-9 into 3x3 matrix, and have the same sum in every rows and columns and two diagonal


        //NVIDIA Compress string such as 'AAABBCCCCCCAAAAA' to '3A2B6C5A'  
        public string Compress(string str)
        {
            if (string.IsNullOrEmpty(str))
                return "";

            char[] ch = str.ToArray();
            StringBuilder sb = new StringBuilder();

            int count = 1;
            char curCh = ch[0];
            for (int i = 1; i < ch.Length; i++)
            {
                if (curCh == ch[i])
                    count++;
                else
                {
                    sb.Append(count.ToString());
                    sb.Append(curCh);

                    curCh = ch[i];
                    count = 1;
                }
            }
            if (count >= 1)
            {
                sb.Append(count.ToString());
                sb.Append(curCh);
            }
            return sb.ToString();
        }


        //NVIDIA round up number by multiple
        public int roundUp(int numToRound, int multiple)
        {
            if (multiple == 0)
                return numToRound;

            int remainder = Math.Abs(numToRound) % multiple;

            if (numToRound > 0)
                return numToRound - remainder + multiple;
            else
                return -(Math.Abs(numToRound) - remainder + multiple);
        }

        //5. Longest Palindromic Substring
        int startIdx = 0;
        int maxLen = 0;
        public string LongestPalindrome(string s)
        {
            if (s.Length == 0)
                return "";
            if (s.Length == 1)
                return s;
            if (s.Length == 2 && s[0] == s[1])
                return s;

            for (int i = 0; i < s.Length - 1; i++)
            {
                maxCheck(s, i, i);
                maxCheck(s, i, i + 1);
            }
            return s.Substring(startIdx, maxLen);
        }
        void maxCheck(string s, int st, int ed)
        {
            while (st >= 0 && ed < s.Length && s[st] == s[ed])
            {
                st--;
                ed++;
            }
            if (maxLen < ed - st - 1)
            {
                startIdx = st + 1;
                maxLen = ed - st - 1;
            }
        }

        //(amazon) Find the longest unbroken series of increasing numbers in a list of random numbers 
        //i.e. if given[15, 2, 38, 71, 2, 524, 98], return [2, 38, 71] (longest increasing sub array)
        public int[] LongestIncreasingSubArray(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return null;

            int st = 0;
            int end = 0;
            int max = 0;
            int maxStart = 0;

            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[i] > nums[i - 1])
                {
                    end++;
                    if (end - st > max)
                    {
                        max = end - st;
                        maxStart = st;
                    }
                }
                else
                {
                    st = i;
                    end = st;
                }
            }

            if (max > 0)
            {
                int[] ret = new int[max + 1];
                for (int i = maxStart; i <= maxStart + max; i++)
                    ret[i - maxStart] = nums[i];
                return ret;
            }
            else
                return new int[] { };
        }

        //238. Product of Array Except Self
        //Given an array of n integers where n > 1, nums, return an array output such that output[i] is equal to the product of all the elements of nums except nums[i].
        //Solve it without division and in O(n).
        //For example, given[1, 2, 3, 4], return [24,12,8,6].
        public int[] ProductExceptSelf(int[] nums)
        {
            int[] result = new int[nums.Length];
            //go from left multiply
            for (int i = 0, tmp = 1; i < nums.Length; i++)
            {
                result[i] = tmp;
                tmp *= nums[i];
            }
            //go from right to left 
            for (int i = nums.Length - 1, tmp = 1; i >= 0; i--)
            {
                result[i] *= tmp;
                tmp *= nums[i];
            }
            return result;
        }

        //35. Search Insert Position
        //Given a sorted array and a target value, return the index if the target is found. If not, return the index where it would be if it were inserted in order.
        //You may assume no duplicates in the array.  Here are few examples.
        //[1, 3, 5, 6], 5 → 2
        //[1,3,5,6], 2 → 1
        //[1,3,5,6], 7 → 4
        //[1,3,5,6], 0 → 0
        public int SearchInsert(int[] nums, int target)
        {
            if (nums == null || nums.Length == 0)
                return -1;

            int st = 0;
            int end = nums.Length - 1;

            while (st <= end)
            {
                int piv = (st + end) / 2;
                if (nums[piv] == target)
                    return piv;

                if (target > nums[piv])
                    st = piv + 1;
                else
                    end = piv - 1;
            }
            if (end < 0)
                return 0;
            if (st == nums.Length)
                return nums.Length;

            return st;
        }

        //66. Plus One
        //Given a non-negative integer represented as a non-empty array of digits, plus one to the integer.
        //You may assume the integer do not contain any leading zero, except the number 0 itself.
        //The digits are stored such that the most significant digit is at the head of the list.
        public int[] PlusOne(int[] digits)
        {
            if (digits == null || digits.Length == 0)
                return digits;

            if (digits[digits.Length - 1] < 9)
            {
                digits[digits.Length - 1] += 1;
                return digits;
            }
            else
            {
                for (int i = digits.Length - 1; i >= 0; i--)
                {
                    if (digits[i] == 9)
                        digits[i] = 0;
                    else
                    {
                        digits[i] += 1;
                        return digits;
                    }
                }
            }
            int[] ret = new int[digits.Length + 1];
            ret[0] = 1;
            return ret;
        }

        //81. Search in Rotated Sorted Array II  with duplicate number
        public bool SearchRotatedSortedArray2(int[] nums, int target)
        {
            if (nums == null || nums.Length == 0)
                return false;

            int lo = 0;
            int hi = nums.Length - 1;

            while (lo <= hi)
            {
                int piv = (lo + hi) / 2;

                if (nums[piv] == target)
                    return true;
                if (nums[piv] > nums[hi])
                {
                    if (target < nums[piv] && target >= nums[lo])
                        hi = piv - 1;
                    else
                        lo = piv + 1;
                }
                else if (nums[piv] < nums[hi])
                {
                    if (target > nums[piv] && target <= nums[hi])
                        lo = piv + 1;
                    else
                        hi = piv - 1;
                }
                else
                    hi--;
            }
            return false;
        }


        //33. Search in Rotated Sorted Array
        //Suppose an array sorted in ascending order is rotated at some pivot unknown to you beforehand.
        //(i.e., 0 1 2 4 5 6 7 might become 4 5 6 7 0 1 2).
        //You are given a target value to search.If found in the array return its index, otherwise return -1.
        //You may assume no duplicate exists in the array.
        public int SearchRotatedSortedArray(int[] nums, int target)
        {
            if (nums == null || nums.Length == 0)
                return -1;

            int lo = 0;
            int hi = nums.Length - 1;
            //find min value index, which is rotated index
            while (lo < hi)
            {
                int mid = (lo + hi) / 2;
                if (nums[mid] > nums[hi])
                    lo = mid + 1;
                else
                    hi = mid;
            }
            // lo==hi is the index of the smallest value and also the number of places rotated.
            int rot = lo;
            lo = 0; hi = nums.Length - 1;
            while (lo <= hi)
            {
                int mid = (lo + hi) / 2;
                int realmid = (mid + rot) % nums.Length;
                if (nums[realmid] == target) return realmid;
                if (nums[realmid] < target) lo = mid + 1;
                else hi = mid - 1;
            }
            return -1;

        }

        //277. Find the Celebrity
        //Suppose you are at a party with n people (labeled from 0 to n - 1) and among them, there may 
        //exist one celebrity. The definition of a celebrity is that all the other n - 1 people know 
        //him/her but he/she does not know any of them.
        //Note: There will be exactly one celebrity if he/she is in the party. Return the celebrity's 
        //label if there is a celebrity in the party. If there is no celebrity, return -1.
        public int FindCelebrity(int n)
        {
            int maybeCelerity = 0;

            for (int i = 1; i < n; i++)
            {
                if (Knows(maybeCelerity, i))
                    maybeCelerity = i;
            }
            for (int i = 0; i < n; i++)
            {
                if (i != maybeCelerity && (Knows(maybeCelerity, i) || !Knows(i, maybeCelerity)))
                    return -1;
            }

            return maybeCelerity;
        }
        //mock API you can call
        bool Knows(int a, int b)
        {
            return true;
        }


        //311. Sparse Matrix Multiplication
        //Given two sparse matrices A and B, return the result of A*B.        
        public int[,] Multiply(int[,] A, int[,] B)
        {
            int rowA = A.GetLength(0);
            int colA = A.GetLength(1);
            int colB = B.GetLength(1);

            int[,] ret = new int[rowA, colB];

            for (int i = 0; i < rowA; i++)
            {
                for (int j = 0; j < colA; j++)
                {
                    if (A[i, j] != 0)
                    {
                        for (int k = 0; k < colB; k++)
                        {
                            if (B[j, k] != 0)
                                ret[i, k] += A[i, j] * B[j, k];
                        }
                    }
                }
            }
            return ret;
        }

        //56. Merge Intervals
        //Given a collection of intervals, merge all overlapping intervals.
        //For example,  Given[1, 3],[2, 6],[8, 10],[15, 18],  return [1,6],[8,10],[15,18].
        public IList<Interval> Merge(IList<Interval> intervals)
        {
            List<Interval> ret = new List<Interval>();
            if (intervals == null || intervals.Count == 0)
                return ret;

            intervals = intervals.OrderBy(x => x.start).ToList();

            int curEnd = intervals[0].end;
            int curStart = intervals[0].start;

            for (int i = 1; i < intervals.Count; i++)
            {
                if (intervals[i].start <= curEnd)
                {
                    curEnd = Math.Max(curEnd, intervals[i].end);
                }
                else
                {
                    ret.Add(new Interval(curStart, curEnd));
                    curEnd = intervals[i].end;
                    curStart = intervals[i].start;
                }
            }
            ret.Add(new Interval(curStart, curEnd));
            return ret;
        }
        public class Interval
        {
            public int start;
            public int end;
            public Interval() { start = 0; end = 0; }
            public Interval(int s, int e) { start = s; end = e; }
        }

        //205. Isomorphic Strings
        //Given two strings s and t, determine if they are isomorphic.
        //For example, Given "egg", "add", return true.  Given "foo", "bar", return false.  Given "paper", "title", return true.
        //You may assume both s and t have the same length.
        public bool IsIsomorphic(string s, string t)
        {
            Dictionary<char, List<int>> map1 = new Dictionary<char, List<int>>();
            Dictionary<char, List<int>> map2 = new Dictionary<char, List<int>>();

            for (int i = 0; i < s.Length; i++)
            {
                if (map1.ContainsKey(s[i]))
                    map1[s[i]].Add(i);
                else
                    map1.Add(s[i], new List<int>() { i });

                if (map2.ContainsKey(t[i]))
                    map2[t[i]].Add(i);
                else
                    map2.Add(t[i], new List<int>() { i });
            }

            for (int j = 0; j < map1.Count; j++)
            {
                if (!map1.ElementAt(j).Value.SequenceEqual(map2.ElementAt(j).Value))
                    return false;
            }
            return true;
        }


        //243. Shortest Word Distance
        //For example, Assume that words = ["practice", "makes", "perfect", "coding", "makes"].
        //Given word1 = “coding”, word2 = “practice”, return 3.
        //Given word1 = "makes", word2 = "coding", return 1.
        //Note:You may assume that word1 does not equal to word2, and word1 and word2 are both in the list.
        public int ShortestDistance(string[] words, string word1, string word2)
        {
            if (words == null)
                return 0;

            int k = -1;
            int j = -1;
            int ret = int.MaxValue;

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i] == word1)
                    k = i;
                if (words[i] == word2)
                    j = i;

                if (k != -1 && j != -1)
                    ret = Math.Min(Math.Abs(k - j), ret);
            }
            return ret;
        }
        //244. Shortest Word Distance II
        //This is a follow up of Shortest Word Distance. The only difference is now you are given the list 
        //of words and your method will be called repeatedly many times with different parameters. How would you optimize it?
        public class WordDistance
        {
            Dictionary<string, List<int>> map;
            public WordDistance(string[] words)
            {
                map = new Dictionary<string, List<int>>();

                for (int i = 0; i < words.Length; i++)
                {
                    if (map.ContainsKey(words[i]))
                        map[words[i]].Add(i);
                    else
                        map.Add(words[i], new List<int>() { i });
                }
            }
            public int Shortest(string word1, string word2)
            {
                int ret = int.MaxValue;
                for (int i = 0, j = 0; i < map[word1].Count && j < map[word2].Count;)
                {
                    if (map[word1][i] < map[word2][j])
                    {
                        ret = Math.Min(ret, Math.Abs(map[word2][j] - map[word1][i]));
                        i++;
                    }
                    else
                    {
                        ret = Math.Min(ret, Math.Abs(map[word1][i] - map[word2][j]));
                        j++;
                    }
                }
                return ret;
            }
        }


        //283. Move Zeroes  (NVIDIA)
        //Given an array nums, write a function to move all 0's to the end of it while maintaining the relative order of the non-zero elements.
        //For example, given nums = [0, 1, 0, 3, 12], after calling your function, nums should be[1, 3, 12, 0, 0].
        //Note:You must do this in-place without making a copy of the array.Minimize the total number of operations.
        public void MoveZeroes(int[] nums)
        { //smart solution~
            int zIdx = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] != 0)
                {
                    swap(nums, i, zIdx);
                    zIdx++;
                }
            }
        }


        //153. Find Minimum in Rotated Sorted Array
        //Suppose an array sorted in ascending order is rotated at some pivot unknown to you beforehand.
        //(i.e., 0 1 2 4 5 6 7 might become 4 5 6 7 0 1 2).Find the minimum element.
        //You may assume no duplicate exists in the array.
        public int FindMin(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return -1;

            if (nums.Length == 1)
                return nums[0];

            for (int i = 0; i < nums.Length - 1; i++)
            {
                if (nums[i] > nums[i + 1])
                    return nums[i + 1];
            }
            return nums[0];
        }

        //191. Number of 1 Bits
        //Write a function that takes an unsigned integer and returns the number of ’1' bits it has (also known as the Hamming weight).
        //For example, the 32-bit integer ’11' has binary representation 00000000000000000000000000001011, so the function should return 3.
        public int HammingWeight(uint n)
        {
            if (n == 0)
                return 0;

            int ret = 0;
            while (n > 0)
            {
                if (n % 2 != 0)
                {
                    n = n - 1;
                    ret += 1;
                }
                else
                    n /= 2;
            }
            return ret;
        }

        //26. Remove Duplicates from Sorted Array  
        //do it in-space and put duplicate to tail, return non-repeated length
        public int RemoveDuplicates(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return 0;
            int ret = 1;

            int jumpCount = 0;
            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[i] == nums[i - 1])
                    jumpCount++;
                else
                {
                    nums[i - jumpCount] = nums[i];
                    ret += 1;
                }
            }
            return ret;
        }

        //Codelity 3. Rotate string by any index, see if it is the same as original one.
        //return how many index can satisfy         
        public int RotateStringAreTheSame(string S)
        {
            if (string.IsNullOrEmpty(S))
                return 0;

            if (S.Length == 1)
                return 1;

            int ret = 1;
            for (int i = S.Length - 1; i > 0; i--)
            {
                string ss = S.Substring(i) + S.Substring(0, i);
                if (S == ss)
                    ret += 1;
            }
            return ret;
        }

        //Codelity 2. input is binary 0/1 string, if see 1, minus 1, if see 0, divid by 2 
        //see how many steps to make it to zero
        //e.g. 00011100 (28)  -> 7 step to zero. 
        public int StepsNeedToZero(string S)
        {
            if (S.Length == 0)
                return 0;

            //prefix remove 0      
            string ss = "";
            bool flag = false;
            for (int i = 0; i < S.Length; i++)
            {
                if (S[i] != '0' || flag)
                {
                    ss += S[i];
                    flag = true;
                }
                else if (S[i] == '0' && !flag)
                    continue;
            }
            if (ss.Length == 0 || ss == "0")
                return 0;

            int step = 0;
            for (int i = ss.Length - 1; i >= 0;)
            {
                if (ss[i] == '0')
                {
                    step += 1;
                    i--;
                }
                else if (ss[i] == '1' && i > 0)
                {
                    step += 2;
                    i--;
                }
                else if (ss[i] == '1' && i == 0)
                {
                    step += 1;
                    i--;
                }
            }
            return step;
        }

        //Find use 1 or 2 digit only to show time, e.g. 15:15:15, 12:11:11, 11:11:31 
        //find interesting point in period of time
        //not passed
        public int FindInterestingPointsTime(string S, string T)
        {
            string[] STIme = S.Split(':');
            string[] TTime = T.Split(':');

            //find all possible in 1 day 
            var list = findSortedAllTimeLength();
            int SLength = CalLength(STIme);
            int TLength = CalLength(TTime);
            int ss = 0;
            int tt = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] > SLength)
                {
                    ss = i;
                    break;
                }
            }
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] > TLength)
                {
                    tt = i;
                    break;
                }
            }
            return tt - ss;
        }
        int CalLength(string[] s)
        {
            return 60 * Convert.ToInt32(s[0]) + 60 * Convert.ToInt32(s[1]) + Convert.ToInt32(s[2]);
        }
        List<int> findSortedAllTimeLength()
        {

            HashSet<int> hsRet = new HashSet<int>();  //record intersting time length
            HashSet<int> hs = new HashSet<int>();

            for (int m = 0; m < 60; m++)
            {
                int mFirst = m / 10;
                int mSecd = m % 10;
                hs.Add(mFirst);
                hs.Add(mSecd);

                //first = mFirst;
                //secd = mSecd;

                for (int s = 0; s < 60; s++)
                {
                    int sFirst = s / 10;
                    int sSecd = s % 10;

                    if (hs.Count == 1)  //use only 1 digit
                    {
                        if (hs.Add(sFirst))  //use 2 digits
                        {
                            if (hs.Contains(sFirst) && hs.Contains(sSecd))
                            {
                                for (int h = 0; h < 24; h++)
                                {
                                    int hFirst = h / 10;
                                    int hSecd = h % 10;

                                    if (hs.Contains(hFirst) && hs.Contains(hSecd))
                                    {
                                        int val = (h * 60) + (m * 60) + s;
                                        hsRet.Add(val);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (hs.Add(sSecd))  //use 2 digits
                            {
                                if (hs.Contains(sFirst) && hs.Contains(sSecd))
                                {
                                    for (int h = 0; h < 24; h++)
                                    {
                                        int hFirst = h / 10;
                                        int hSecd = h % 10;

                                        if (hs.Contains(hFirst) && hs.Contains(hSecd))
                                        {
                                            int val = (h * 60) + (m * 60) + s;
                                            hsRet.Add(val);
                                        }
                                    }
                                }
                            }
                            else  //only 1 
                            {
                                if (hs.Contains(sSecd))
                                {
                                    for (int h = 0; h < 24; h++)
                                    {
                                        int hFirst = h / 10;
                                        int hSecd = h % 10;

                                        if (hs.Contains(hFirst) && hs.Contains(hSecd))
                                        {
                                            int val = (h * 60) + (m * 60) + s;
                                            hsRet.Add(val);
                                        }
                                    }
                                }
                            }
                        }

                        //hs.Add(sSecd);
                        if (hs.Count == 2)  //use 2 disits
                        {
                            //if (!((sFirst != mFirst && sFirst != sSecd) && (sSecd != mFirst && sSecd != sFirst)))
                            if (hs.Contains(sFirst) && hs.Contains(sSecd))
                            {
                                for (int h = 0; h < 24; h++)
                                {
                                    int hFirst = h / 10;
                                    int hSecd = h % 10;

                                    if (hs.Contains(hFirst) && hs.Contains(hSecd))
                                    {
                                        int val = (h * 60) + (m * 60) + s;
                                        hsRet.Add(val);
                                    }
                                }
                            }
                        }
                    }
                    else if (hs.Count == 2) //use 2 digits
                    {
                        //if ((sFirst == mFirst || sFirst == mSecd) && (sSecd == mFirst || sSecd == mSecd))
                        if (hs.Contains(sFirst) && hs.Contains(sSecd))
                        {
                            for (int h = 0; h < 24; h++)
                            {
                                int hFirst = h / 10;
                                int hSecd = h % 10;

                                if (hs.Contains(hFirst) && hs.Contains(hSecd))
                                {
                                    int val = (h * 60) + (m * 60) + s;
                                    hsRet.Add(val);
                                }
                            }
                        }
                    }
                }
            }

            var ret = hsRet.OrderBy(x => x).ToList();
            return ret;
        }

        //169. Majority Element  (in space O(1))
        //Given an array of size n, find the majority element. The majority element is the element that appears more than ⌊ n/2 ⌋ times.
        //You may assume that the array is non-empty and the majority element always exist in the array.
        public int MajorityElement(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return int.MinValue;

            int major = nums[0], count = 1;
            for (int i = 1; i < nums.Length; i++)
            {
                if (count == 0)
                {
                    count++;
                    major = nums[i];
                }
                else if (major == nums[i])
                    count++;
                else
                    count--;
            }
            return major;
        }

        //Codility1. Equi
        //Find an index in an array such that its prefix sum equals its suffix sum.
        //int[] { -1, 3, -4, 5, 1, -6, 2, 1 }
        //P = 1 is an equilibrium index of this array, because:
        //   A[0] = −1 = A[2] + A[3] + A[4] + A[5] + A[6] + A[7]
        //P = 3 is an equilibrium index of this array, because:
        //   A[0] + A[1] + A[2] = −2 = A[4] + A[5] + A[6] + A[7]
        //P = 7 is also an equilibrium index, because:
        //  A[0] + A[1] + A[2] + A[3] + A[4] + A[5] + A[6] = 0
        //time & space both in O(n) 
        public int Equi(int[] A)
        {
            if (A == null || A.Length == 0)
                return -1;
            Dictionary<int, int> map = new Dictionary<int, int>(); //idex: cur sum
            map.Add(-1, 0);
            int curSum = 0;
            for (int i = 0; i < A.Length; i++)
            {
                curSum += A[i];
                map.Add(i, curSum);
            }
            int ret = -1;
            for (int j = 0; j < map.Count; j++)
            {
                if (map[j - 1] == map.Last().Value - map[j])
                    return j;
            }
            return ret;
        }

        //581. Shortest Unsorted Continuous Subarray  leetcode contest 32
        //Given an integer array, you need to find one continuous subarray that if you only sort this subarray in ascending order, then the whole array will be sorted in ascending order, too.
        //You need to find the shortest such subarray and output its length.
        //Example 1: Input: [2, 6, 4, 8, 10, 9, 15]  Output: 5
        //Explanation: You need to sort [6, 4, 8, 10, 9] in ascending order to make the whole array sorted in ascending order.
        public int FindUnsortedSubarray(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return 0;

            int len = nums.Length;
            int[] copy1 = new int[len];
            for (int i = 0; i < nums.Length; i++)
            {
                copy1[i] = nums[i];
            }
            Array.Sort(copy1);

            int st = 0;
            int end = len - 1;

            while (st < len && copy1[st] == nums[st])
                st++;

            while (end > st && copy1[end] == nums[end])  //key note: end > st
                end--;

            return end - st + 1;
        }

        //leetcode contest 32 
        //public IList<int> KillProcess(IList<int> pid, IList<int> ppid, int kill)
        //{
        //    HashSet<int> ret = new HashSet<int>();

        //    if (!ppid.Any(p => p == kill))
        //    {
        //        if (pid.Any(p => p == kill))
        //            return new List<int>() { kill };
        //    }
        //    else
        //    {
        //        for (int i = 0; i < ppid.Count; i++)
        //        {
        //            if (ppid[i] == kill)
        //            {
        //                ret.Add(kill);
        //                findAll(pid, ppid, ret, kill, i);
        //            }
        //        }

        //    }
        //    return ret.ToList();
        //}
        //void findAll(IList<int> pid, IList<int> ppid, HashSet<int> ret, int kill, int ppidKilledIdx)
        //{

        //    if (ppid.Any(p => p == kill))
        //    {
        //        //ret.Add(kill);    
        //        int pidIdx = -1;

        //        for (int i = ppidKilledIdx; i < pid.Count; i++)
        //        {
        //            if (pid[i] == kill)
        //                pidIdx = i;
        //        }
        //        if (pidIdx == -1)
        //            return;

        //        ret.Add(pid[pidIdx]);
        //        findAll(pid, ppid, ret, pid[pidIdx], pidIdx);
        //    }
        //    else
        //        return;
        //}

        //return sqaure 
        private void Run(int[] testData)
        {
            if (testData == null || testData.Length == 0)
                return;
            for (int i = 0; i < testData.Length; i++)
            {
                var target = testData[i];
                //Square(target);
                if (target >= Math.Pow(int.MaxValue, 0.5) || target <= -1 * Math.Pow(int.MaxValue, 0.5))
                {
                    Console.WriteLine(int.MaxValue);
                    return;
                }
                target *= target;
                Console.WriteLine(target);
            }
        }
        void StairCase(int n)
        {
            if (n == 0)
                return;
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= n; i++)
            {
                int j = 0;
                for (j = 0; j < n - i; j++)
                    sb.Append(" ");
                for (int k = j; k < n; k++)
                    sb.Append("#");

                Console.WriteLine();
            }
        }


        //20. Valid Parentheses
        //Given a string containing just the characters '(', ')', '{', '}', '[' and ']', determine if the input string is valid.
        //The brackets must close in the correct order, "()" and "()[]{}" are all valid but "(]" and "([)]" are not.
        public bool IsValid(string s)
        {
            if (s == null || s.Length == 0)
                return true;

            Stack<char> st = new Stack<char>();
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '{' || s[i] == '[' || s[i] == '(')
                {
                    st.Push(s[i]);
                }
                else if (s[i] == '}')
                {
                    if (st.Count > 0 && st.Peek() == '{')
                        st.Pop();
                    else
                        return false;
                }
                else if (s[i] == ']')
                {
                    if (st.Count > 0 && st.Peek() == '[')
                        st.Pop();
                    else
                        return false;
                }
                else if (s[i] == ')')
                {
                    if (st.Count > 0 && st.Peek() == '(')
                        st.Pop();
                    else
                        return false;
                }
            }
            return st.Count == 0;
        }

        //13. Roman to Integer
        public int RomanToInt(string s)
        {
            int ret = 0;
            for (int i = s.Length - 1; i >= 0; i--)
            {
                if (s[i] == 'I') //*1
                {
                    ret += ret >= 5 ? -1 : 1;
                }
                if (s[i] == 'V') //5
                {
                    ret += 5;
                }
                if (s[i] == 'X') //*10
                {
                    ret += ret >= 50 ? -10 : 10;
                }
                if (s[i] == 'L') //50
                {
                    ret += 50;
                }
                if (s[i] == 'C') //*100
                {
                    ret += ret >= 500 ? -100 : 100;
                }
                if (s[i] == 'D') //500
                {
                    ret += 500;
                }
                if (s[i] == 'M') //*1000
                {
                    ret += 1000;
                }
            }
            return ret;
        }

        //88. Merge Sorted Array  
        //Given two sorted integer arrays nums1 and nums2, merge nums2 into nums1 as one sorted array.
        //Note:You may assume that nums1 has enough space(size that is greater or equal to m + n) to 
        //hold additional elements from nums2.The number of elements initialized in nums1 and nums2 are m and n respectively.        
        public void Merge(int[] nums1, int m, int[] nums2, int n)
        {
            if (nums1 == null || nums2 == null || n <= 0)
                return;

            int k = m + n - 1;
            int i = m - 1;
            int j = n - 1;

            while (i >= 0 && j >= 0)
            {
                if (nums2[j] > nums1[i])
                {
                    nums1[k] = nums2[j];
                    j--;
                }
                else
                {
                    nums1[k] = nums1[i];
                    i--;
                }
                k--;
            }
            while (j >= 0)
            {
                nums1[k] = nums2[j];
                j--;
                k--;
            }
        }

        //3. Longest Substring Without Repeating Characters
        //Examples:  Given "abcabcbb", the answer is "abc", which the length is 3.
        //Given "bbbbb", the answer is "b", with the length of 1.
        //Given "pwwkew", the answer is "wke", with the length of 3. Note that the answer must be a substring, "pwke" is a subsequence and not a substring.
        public int LengthOfLongestSubstring(string s)
        {
            if (string.IsNullOrEmpty(s))
                return 0;

            Dictionary<char, int> map = new Dictionary<char, int>();
            int max = 0;

            int i = 0;
            while (i < s.Length)
            {
                if (!map.ContainsKey(s[i]))
                {
                    map.Add(s[i], i);
                    max = Math.Max(max, map.Count);
                    i++;
                }
                else
                {
                    int repeatedIdx = map[s[i]];
                    i = repeatedIdx + 1;
                    map.Clear();
                }
            }
            return max;
        }

        //54. Spiral Matrix
        //Given a matrix of m x n elements (m rows, n columns), return all elements of the matrix in spiral order.
        //For example,  Given the following matrix:
        //[ [ 1, 2, 3 ],
        //  [ 4, 5, 6 ],
        //  [ 7, 8, 9 ]
        //]                        You should return [1,2,3,6,9,8,7,4,5].
        public IList<int> SpiralOrder(int[,] matrix)
        {
            int leftFlag = 0;
            int downFlag = matrix.GetLength(0) - 1;
            int upFlag = 0;
            int rightFlag = matrix.GetLength(1) - 1;

            List<int> ret = new List<int>();

            if (matrix.Length == 0)
                return ret;

            while (leftFlag <= rightFlag && upFlag <= downFlag)
            {
                //visit through most up row 
                for (int i = leftFlag; i <= rightFlag; i++)
                {
                    ret.Add(matrix[upFlag, i]);
                }
                upFlag++;

                //visit through most right col
                for (int i = upFlag; i <= downFlag; i++)
                {
                    ret.Add(matrix[i, rightFlag]);
                }
                rightFlag--;

                //visit back through down row
                if (upFlag <= downFlag)  //need to check is it no row left
                {
                    for (int i = rightFlag; i >= leftFlag; i--)
                    {
                        ret.Add(matrix[downFlag, i]);
                    }
                }
                downFlag--;
                //visit back through up col
                if (leftFlag <= rightFlag) //need to check is it no col left
                {
                    for (int i = downFlag; i >= upFlag; i--)
                    {
                        ret.Add(matrix[i, leftFlag]);
                    }
                }
                leftFlag++;
            }
            return ret;

        }



        //186. Reverse Words in a String II  
        //Given an input string, reverse the string word by word. A word is defined as a sequence of non-space characters.
        //The input string does not contain leading or trailing spaces and the words are always separated by a single space.
        //For example, Given s = "the sky is blue", return "blue is sky the".
        // Could you do it in-place without allocating extra space?
        public void ReverseWords(char[] s)
        {
            if (s.All(c => c != ' '))
                return;
            //reverse whole 
            reverseParial(s, 0, s.Length - 1);
            //reverse each word
            int stIdx = 0;
            for (int i = 0; i < s.Length;)
            {
                if (s[i] == ' ')
                {
                    if (i > 0)
                        reverseParial(s, stIdx, i - 1);

                    stIdx = i + 1;
                }
                i++;
            }
            reverseParial(s, stIdx, s.Length - 1);

        }

        void reverseParial(char[] s, int start, int end)
        {
            while (start < end)
            {
                char temp = s[start];
                s[start] = s[end];
                s[end] = temp;
                start++;
                end--;
            }
        }


        //leetcode 201705 53. Maximum Subarray
        //Find the contiguous subarray within an array (containing at least one number) which has the largest sum.
        //For example, given the array[-2, 1, -3, 4, -1, 2, 1, -5, 4],
        //the contiguous subarray[4, -1, 2, 1] has the largest sum = 6.        
        public int MaxSubArray(int[] nums)
        {
            if (nums.Length == 1)
                return nums[0];

            int max = int.MinValue;
            int pre = nums[0];

            for (int i = 1; i < nums.Length; i++)
            {
                pre = System.Math.Max(nums[i], pre + nums[i]);
                max = System.Math.Max(max, pre);
            }

            max = System.Math.Max(nums[0], max);
            return max;
        }


        //leetcode 201705 121. Best Time to Buy and Sell Stock
        //Say you have an array for which the ith element is the price of a given stock on day i.
        //If you were only permitted to complete at most one transaction(ie, buy one and sell one share of the stock), design an algorithm to find the maximum profit.
        //Example 1: Input: [7, 1, 5, 3, 6, 4]  Output: 5
        // max.difference = 6 - 1 = 5(not 7 - 1 = 6, as selling price needs to be larger than buying price)
        //Input: [7, 6, 4, 3, 1]        Output: 0        
        public int MaxProfit(int[] prices)
        {
            if (prices == null || prices.Length <= 1)
                return 0;

            int min = prices[0];
            int max = 0;
            for (int i = 1; i < prices.Length; i++)
            {
                max = Math.Max(prices[i] - min, max);
                min = Math.Min(prices[i], min);
            }
            return max;
        }

        //leetcode 122. Best Time to Buy and Sell Stock II
        //Say you have an array for which the ith element is the price of a given stock on day i.
        //Design an algorithm to find the maximum profit.You may complete as many transactions as 
        //you like (ie, buy one and sell one share of the stock multiple times). 
        //However, you may not engage in multiple transactions at the same time(ie, you must sell the stock before you buy again).
        public int MaxProfit2(int[] prices)
        {
            int ret = 0;
            int curMax = 0;

            for (int i = 0; i < prices.Length; i++)
            {
                while (i + 1 < prices.Length && prices[i + 1] > prices[i])
                {
                    curMax += prices[i + 1] - prices[i];
                    i++;
                }
                ret += curMax;
                curMax = 0;
            }
            return ret;
        }

        //leetcode 1. Two Sum
        //Given an array of integers, return indices of the two numbers such that they add up to a specific target.
        //You may assume that each input would have exactly one solution, and you may not use the same element twice.
        //Example: Given nums = [2, 7, 11, 15], target = 9,  Because nums[0] + nums[1] = 2 + 7 = 9,return [0, 1].       
        public int[] TwoSum(int[] nums, int target)
        {
            List<int> ret = new List<int>();
            Dictionary<int, int> map = new Dictionary<int, int>();

            for (int i = 0; i < nums.Length; i++)
            {
                if (map.ContainsKey(target - nums[i]))
                {
                    ret.Add(map[target - nums[i]]);
                    ret.Add(i);
                    return ret.ToArray();
                }
                if (!map.ContainsKey(nums[i]))
                    map.Add(nums[i], i);
            }
            return ret.ToArray();
        }

            
        //leetcode 15. 3Sum
        //Given an array S of n integers, are there elements a, b, c in S such that a + b + c = 0? 
        //Find all unique triplets in the array which gives the sum of zero.
        //For example, given array S = [-1, 0, 1, 2, -1, -4],  A solution set is:
        //[  [-1, 0, 1],  [-1, -1, 2]  ]
        public IList<IList<int>> ThreeSum2(int[] nums)
        {
            List<IList<int>> ret = new List<IList<int>>();
            if (nums == null)
                return ret;

            Array.Sort(nums);
            for (int i = 0; i < nums.Length; i++)
            {
                if (i > 0 && nums[i] == nums[i - 1])
                    continue;

                int j = i + 1;
                int k = nums.Length - 1;
                while (j < k)
                {
                    if (nums[i] + nums[j] + nums[k] == 0)
                    {
                        ret.Add(new List<int> { nums[i], nums[j], nums[k] });
                        j++;
                        k--;
                        while (j < k && nums[j] == nums[j - 1])
                            j++;
                        while (j < k && nums[k] == nums[k + 1])
                            k--;
                    }
                    else if (nums[i] + nums[j] + nums[k] > 0)
                        k--;
                    else
                        j++;
                }
            }
            return ret;
        }

        //backtracking
        public IList<IList<int>> ThreeSum(int[] nums)
        {
            List<IList<int>> ret = new List<IList<int>>();

            if (nums == null || nums.Length == 0)
                return ret;
            Array.Sort(nums);
            //var hs = new HashSet<string>();

            ThreeSumHelp(nums, 0, ret, new List<int>());
            return ret;
        }

        void ThreeSumHelp(int[] nums, int curIdx, List<IList<int>> ret, List<int> curList)
        {
            //string checkRep = string.Join(",", curList);

            if (curList.Count == 3 && curList.Sum() == 0 && !ret.Any(item => item[0] == curList[0] && item[1] == curList[1] && item[2] == curList[2]))
            {
                //hs.Add(checkRep);
                ret.Add(new List<int>(curList));
                return;
            }

            for (int i = curIdx; i < nums.Length; i++)
            {
                curList.Add(nums[i]);
                ThreeSumHelp(nums, i + 1, ret, curList);
                curList.Remove(curList.Last());
            }
        }

        //268. Missing Number
        //Given an array containing n distinct numbers taken from 0, 1, 2, ..., n, find the one that is missing from the array.
        //For example, Given nums = [0, 1, 3] return 2.
        //Your algorithm should run in linear runtime complexity. Could you implement it using only constant extra space complexity?
        public int MissingNumber(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return -1;

            if (nums.Length == 1 && nums[0] == 0)
                return 1;
            if (nums.Length == 1 && nums[0] == 1)
                return 0;

            int realsum = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                realsum += nums[i];
            }
            int ideasum = ((nums.Length) * (1 + nums.Length)) / 2;

            return ideasum - realsum;
        }

        //8. String to Integer (atoi)
        //Implement atoi to convert a string to an integer.
        //Hint: Carefully consider all possible input cases.If you want a challenge, please do not see below and ask yourself what are the possible input cases.
        //If no valid conversion could be performed, a zero value is returned.If the correct value is out of the range of representable values, INT_MAX (2147483647) or INT_MIN(-2147483648) is returned.
        //e.g. -12a445  ->  -12 
        public int MyAtoi(string str)
        {
            if (str.Length == 0)
                return 0;

            str = str.Trim();
            int len = str.Length;
            double ret = 0;
            int comp = 1;
            for (int i = str.Length - 1; i >= 0; i--)
            {
                if (i == 0 && str[0] == '+')
                    break;
                if (i == 0 && str[0] == '-')
                {
                    ret = ret * -1;
                    break;
                }
                if (str[i] == ' ')
                {
                    ret = 0;
                    comp = len - i + 1;
                    continue;
                }
                int num = (int)(str[i] - '0');
                if (num > 9 || num < 0)
                {
                    ret = 0;
                    comp = len - i + 1;
                }
                else
                    ret += num * Math.Pow(10, len - i - comp);
            }
            if (ret > int.MaxValue)
                return int.MaxValue;
            if (ret < int.MinValue)
                return int.MinValue;

            return (int)ret;
        }

        public void SortColors(int[] nums)
        {
            int rIdx = 0;
            int bIdx = nums.Length - 1;

            for (int i = rIdx; i <= bIdx;)
            {
                if (nums[i] == 0)
                {
                    swap(nums, i, rIdx);
                    rIdx += 1;
                    i++;
                }
                else if (nums[i] == 2)
                {
                    swap(nums, i, bIdx);
                    bIdx -= 1;
                }
                else
                    i++;
            }
        }
        void swap(int[] nums, int i, int j)
        {
            int temp = nums[i];
            nums[i] = nums[j];
            nums[j] = temp;
        }


        //48. Rotate Image
        //You are given an n x n 2D matrix representing an image.Rotate the image by 90 degrees(clockwise).
        //Follow up: Could you do this in-place?
        /* clockwise rotate
        * first reverse up to down, then swap the symmetry 
        * 1 2 3     7 8 9     7 4 1
        * 4 5 6  => 4 5 6  => 8 5 2
        * 7 8 9     1 2 3     9 6 3
        */
        public void Rotate(int[,] matrix)
        {
            if (matrix == null)
                return;

            int len = matrix.GetLength(0);
            // swap up down
            int st = 0;
            int end = len - 1;
            while (st < end)
            {
                int[] temp = new int[len];
                for (int i = 0; i < len; i++)
                {
                    temp[i] = matrix[st, i];
                    matrix[st, i] = matrix[end, i];
                    matrix[end, i] = temp[i];
                }
                st++;
                end--;
            }
            //swap symmetric 
            for (int i = 0; i < len; ++i)
            {
                for (int j = i + 1; j < len; ++j)
                    swap(i, j, matrix);
            }
        }
        void swap(int i, int j, int[,] matrix)
        {
            int temp = matrix[i, j];
            matrix[i, j] = matrix[j, i];
            matrix[j, i] = temp;
        }

        //73. Set Matrix Zeroes
        //Given a m x n matrix, if an element is 0, set its entire row and column to 0. Do it in place.
        public void SetZeroes(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            HashSet<int> zRow = new HashSet<int>();
            HashSet<int> zCol = new HashSet<int>();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (matrix[i, j] == 0)
                    {
                        zRow.Add(i);
                        zCol.Add(j);
                    }
                }
            }

            for (int i = 0; i < zRow.Count; i++)
            {
                for (int j = 0; j < cols; j++)
                    matrix[zRow.ElementAt(i), j] = 0;
            }
            for (int j = 0; j < zCol.Count; j++)
            {
                for (int i = 0; i < rows; i++)
                    matrix[i, zCol.ElementAt(j)] = 0;
            }
        }
    }
}
