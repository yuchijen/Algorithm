﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview
{
    public class HashTable
    {
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

            for(int i =0; i< nums.Length; i++)
            {
                int sum = nums[i];
                for(int j =i+1; j<nums.Length; j++)
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
            for(int i=0; i<nums.Length; i++)
            {
                sum += nums[i];
                if(!ht.ContainsKey(sum))
                {
                    ht.Add(sum, i);
                }
                if(ht.ContainsKey(sum-k))
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
