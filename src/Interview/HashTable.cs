using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview
{
    public class HashTable
    {
        //438. Find All Anagrams in a String
        //Given a string s and a non-empty string p, find all the start indices of p's anagrams in s.
        //Strings consists of lowercase English letters only and the length of both strings s and p will not be larger than 20,100.
        //The order of output does not matter. 
        //Example 1: Input: s: "cbaebabacd" p: "abc"
        //Output: [0, 6]
        //Explanation:
        //The substring with start index = 0 is "cba", which is an anagram of "abc".
        //The substring with start index = 6 is "bac", which is an anagram of "abc".
        public IList<int> FindAnagrams(string s, string p)
        {
            var ret = new List<int>();
            if (string.IsNullOrEmpty(s) || s.Length < p.Length)
                return ret;

            int n = s.Length;
            int l = p.Length;

            var arrP = new int[26];
            var arrS = new int[26];

            for(int i=0; i< p.Length; i++)
            {
                arrP[p[i] - 'a']++;
            }

            for(int i = 0; i < n; i++)
            {
                if (i >= l)
                    arrS[s[i - l] - 'a']--;
                arrS[s[i] - 'a']++;
                if (arrP.SequenceEqual(arrS))
                    ret.Add(i - l + 1);
            }

            return ret;
            //too slow
            //string patterm = string.Concat( p.OrderBy(c => c));
            //for(int i=0; i<=s.Length-p.Length; i++)
            //{
            //    string curSubStr = s.Substring(i, p.Length);
            //    if (patterm.Equals(string.Concat(curSubStr.OrderBy(c => c))))
            //        ret.Add(i);

            //}
            //return ret;

        }


        //159. Given a string s, find the length of the longest substring t that contains at most 2 distinct characters.
        //Example 1:  Input: "eceba"  Output: 3
        //Explanation: t is "ece" which its length is 3.
        //Example 2: Input: "ccaabbb" Output: 5
        //Explanation: t is "aabbb" which its length is 5.
        public int lengthOfLongestSubstringTwoDistinct(String s)
        {
            if (string.IsNullOrEmpty(s))
                return 0;

            int ret = 0;
            int backIdx = 0;
            var map = new Dictionary<char, int>();
            for (int i = 0; i < s.Length; i++)
            {
                if (!map.ContainsKey(s[i]))
                    map.Add(s[i], 1);
                else
                    map[s[i]]++;

                if (map.Count <= 2 )
                {
                    ret = Math.Max(ret, i - backIdx + 1);
                }
                else  //current char out of 2 distinct. backIdx start to run 
                {
                    while (map.Count > 2)
                    {
                        if (map[s[backIdx]] == 1)
                            map.Remove(s[backIdx]);
                        else
                            map[s[backIdx]]--;

                        backIdx++;
                    }
                }
            }
            return ret;
        }


        //76. Minimum Window Substring
        //Given a string S and a string T, find the minimum window in S which will contain all the characters in T in complexity O(n).
        //Example: Input: S = "ADOBECODEBANC", T = "ABC"
        //Output: "BANC"
        public string minWindow2(string s, string t)
        {
            var map = new Dictionary<char, int>();

            for (int i = 0; i < t.Length; i++)
            {
                if (map.ContainsKey(t[i]))
                    map[t[i]]++;
                else
                    map.Add(t[i], 1);
            }

            int len = int.MaxValue, cnt = 0;
            int stIdx = -1;
            int backPtr = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (map.ContainsKey(s[i]))
                {
                    if (map[s[i]] > 0)
                        cnt++;

                    map[s[i]]--;
                }

                while (cnt == t.Length)
                {
                    if (i - backPtr + 1 < len)
                    {
                        stIdx = backPtr;
                        len = i - backPtr + 1;
                    }
                    if (map.ContainsKey(s[backPtr]))
                    {
                        if (map[s[backPtr]] >= 0)
                            cnt--;
                        map[s[backPtr]]++;
                    }
                    backPtr++;
                }
            }
            return len == int.MaxValue ? "" : s.Substring(stIdx, len);
        }


        //525. Contiguous Array
        //Given a binary array, find the maximum length of a contiguous subarray with equal number of 0 and 1.
        //Example 1:Input: [0,1]        
        //Output: 2  Explanation: [0, 1] is the longest contiguous subarray with equal number of 0 and 1.
        public int FindMaxLength(int[] nums)
        {
            if (nums == null)
                return 0;

            var map = new Dictionary<int, int>();
            map.Add(0, -1);
            int curSum = 0;
            int ret = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                curSum += nums[i] == 0 ? -1 : 1;

                if (!map.ContainsKey(curSum))
                    map.Add(curSum, i);
                else
                {
                    ret = Math.Max(ret, i - map[curSum]);

                }
            }
            return ret;
        }

        //523. Continuous Subarray Sum
        //Given a list of non-negative numbers and a target integer k, write a function to check if the array 
        //has a continuous subarray of size at least 2 that sums up to the multiple of k, that is, sums up 
        //to n*k where n is also an integer.
        // Example 1:input: [23, 2, 4, 6, 7],  k=6
        //Output: True Explanation: Because[2, 4] is a continuous subarray of size 2 and sums up to 6. or 6xn
        public bool CheckSubarraySum(int[] nums, int k)
        {
            if (nums == null)
                return false;

            for (int i = 0; i < nums.Length; i++)
            {
                int sum = nums[i];
                for (int j = i + 1; j < nums.Length; j++)
                {
                    sum += nums[j];
                    if (sum == k)  //case k =sum=0
                        return true;
                    if (k != 0 && sum % k == 0)
                        return true;
                }
            }
            return false;
        }


        //560. Subarray Sum Equals K
        //Given an array of integers and an integer k, you need to find the total number of 
        //continuous subarrays whose sum equals to k.
        //Example 1: Input:nums = [1,1,1], k = 2
        //Output: 2
        //Note:The length of the array is in range[1, 20, 000].
        //The range of numbers in the array is [-1000, 1000] and the range of the integer k is [-1e7, 1e7].
        public int SubarraySum(int[] nums, int k)
        {
            if (nums == null || nums.Length == 0)
                return 0;

            Dictionary<int, int> map = new Dictionary<int, int>();
            map.Add(0, 1);

            int curSum = 0;
            int ret = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                curSum += nums[i];
                //sum2- sum1 =k correct results is between 2 parts of sum curSum = sum2 
                if (map.ContainsKey(curSum - k))
                    ret += map[curSum - k];

                if (!map.ContainsKey(curSum))
                    map.Add(curSum, 1);
                else
                    map[curSum] += 1;
            }
            return ret;
        }


        //325.Maximum Size Subarray Sum Equals k 
        //Given an array nums and a target value k, find the maximum length of a subarray that sums to k.
        //If there isn't one, return 0 instead.
        //Example 1: Given nums = [1, -1, 5, -2, 3], k = 3,
        //return 4. (because the subarray[1, -1, 5, -2] sums to 3 and is the longest)
        //Example 2: Given nums = [-2, -1, 2, 1], k = 1,
        //return 2. (because the subarray[-1, 2] sums to 1 and is the longest)
        public int maxSubArrayLen(int[] nums, int k)
        {
            if (nums == null || nums.Length == 0)
                return 0;

            var ht = new Dictionary<int, int>();
            ht.Add(0, -1);
            int sum = 0;
            int maxLength = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                sum += nums[i];
                if (!ht.ContainsKey(sum))
                {
                    ht.Add(sum, i);
                }
                if (ht.ContainsKey(sum - k))
                {
                    maxLength = Math.Max(maxLength, i - ht[sum - k]);
                }
            }
            return maxLength;
        }


        //409. Longest Palindrome
        //Given a string which consists of lowercase or uppercase letters, find the length of the longest palindromes 
        //that can be built with those letters.
        //This is case sensitive, for example "Aa" is not considered a palindrome here.
        // Note:Assume the length of given string will not exceed 1,010.
        //e.g. Input: "abccccdd"   Output:7
        //Explanation: One longest palindrome that can be built is "dccaccd", whose length is 7.






        //389. Find the Difference
        //Given two strings s and t which consist of only lowercase letters.
        //String t is generated by random shuffling string s and then add one more letter at a random position.
        //Find the letter that was added in t.
        //Input: s = "abcd"  t = "abcde"  ; Output:Explanation:'e' is the letter that was added.





        //349. Intersection of Two Arrays   (106/260)
        //Given two arrays, write a function to compute their intersection.
        //Example: Given nums1 = [1, 2, 2, 1], nums2 = [2, 2], return [2].
        //Note: Each element in the result must be unique. The result can be in any order.
        public int[] Intersection(int[] nums1, int[] nums2)
        {
            var l1 = nums1.ToList();
            var l2 = nums2.ToList();

            var cc = from item in l1
                     where (nums2.Contains(item))
                     select item;
            /*
                        var c = from i in Enumerable.Range(0, l1.Count)
                                from j in Enumerable.Range(0, l2.Count)
                                where l1[i] == l2[j]
                                select l1[i];
            */

            HashSet<int> hs = new HashSet<int>();

            foreach (var x in cc)
                hs.Add(x);
            return hs.ToArray();
        }




        //350. Intersection of Two Arrays II
        //Given two arrays, write a function to compute their intersection.
        //Example: Given nums1 = [1, 2, 2, 1], nums2 = [2, 2], return [2, 2].
        //Note:Each element in the result should appear as many times as it shows in both arrays.
        //The result can be in any order.




    }
}
