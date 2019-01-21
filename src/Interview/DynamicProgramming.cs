using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interview
{
    public class DynamicProgramming
    {
        //5.Longest Palindromic Substring
        //Given a string S, find the longest palindromic substring in S. You may assume that the 
        //maximum length of S is 1000, and there exists one unique longest palindromic substring.
        //Example 1: Input: "babad"
        //Output: "bab"
        //Note: "aba" is also a valid answer.
        public string LongestPalindrome(string s)
        {
            if (string.IsNullOrEmpty(s))
                return "";
            if (s.Length == 1)
                return s;

            int maxLen = 0;
            int startIdx = 0;
            //record i to j is palindrom or not
            var dpPalindrom = new bool[s.Length, s.Length];
            //for (int i = 0; i < s.Length; i++)
            //    dpPalindrom[i, i] = true;

            for(int j =0; j < s.Length; j++)
            {
                for(int i=0; i<=j; i++)
                {
                    if (s[i] == s[j] && (j - i <= 2 || dpPalindrom[i + 1, j - 1]))
                    {
                        dpPalindrom[i, j] = true;
                        if (maxLen < j - i + 1)
                        {
                            maxLen = j - i + 1;
                            startIdx = i;
                        }
                    }
                    else
                        dpPalindrom[i, j] = false;
                }
            }
            
            return s.Substring(startIdx, maxLen);            
        }


        //322. Coin Change
        //You are given coins of different denominations and a total amount of money amount.
        //Write a function to compute the fewest number of coins that you need to make up that 
        //amount.If that amount of money cannot be made up by any combination of the coins, return -1.
        //Example 1:coins = [1, 2, 5], amount = 11
        //return 3 (11 = 5 + 5 + 1)
        //Example 2:coins = [2], amount = 3  return -1.
        //用dp存储硬币数量，dp[i] 表示凑齐钱数 i 需要的最少硬币数，那么凑齐钱数 amount 最少硬币数为：
        //固定钱数为 coins[j] 一枚硬币，另外的钱数为 amount - coins[j] 它的数量为dp[amount - coins[j]]，j 从0遍历到coins.length - 1：
        public int CoinChange(int[] coins, int amount)
        {
            if (coins == null || amount == 0)
                return 0;

            int[] ret = new int[amount + 1];
            for (int i = 0; i <= amount; i++)
                ret[i] = int.MaxValue;
            ret[0] = 0;

            //iterate coin value , (pick 1 of coin, the rest is ret[amount - this coin value] )
            for(int i=0; i< coins.Length; i++)
            {
                for(int j = coins[i]; j <= amount; j++)
                {
                    if(ret[j-coins[i]] < int.MaxValue)
                    ret[j] = Math.Min(ret[j - coins[i]] + 1, ret[j]);
                }
            }

            return ret[amount] == int.MaxValue ? -1 : ret[amount];
        }


        //Reverse Fibonacci MS OTS
        //given 2 first number. 80 50 -> 80 50 30 20 10 10 0
        public int[] ReverseFibonacci(int i, int j)
        {
            //assume this is for positive 
            if (i <= 0 || j <= 0 || i<j)
                return null;

            var ret = new List<int>();
            ret.Add(i);
            ret.Add(j);
            int k = i - j;
            while(k >=0)
            {
                ret.Add(k);
                k = ret.Last() - k;
            }
            return ret.ToArray();
        }


        //leetcode 201705 53. Maximum Subarray
        //Find the contiguous subarray within an array (containing at least one number) which has the largest sum.
        //For example, given the array[-2, 1, -3, 4, -1, 2, 1, -5, 4],
        //the contiguous subarray[4, -1, 2, 1] has the largest sum = 6.                
        public int MaxSubArray(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return 0;
            int curSum = nums[0];
            int curMax = nums[0];

            int[] dp = new int[nums.Length];

            for(int i =1; i<nums.Length; i++)
            {
                dp[i] = dp[i - 1] + nums[i] < nums[i] ? nums[i] : dp[i - 1] + nums[i];
                curMax=Math.Max(curMax, dp[i]);
            }
            return curMax;
        }

        //91. Decode Ways
        //A message containing letters from A-Z is being encoded to numbers using the following mapping:
        //'A' -> 1,'B' -> 2,...'Z' -> 26
        //Given an encoded message containing digits, determine the total number of ways to decode it.
        //For example,Given encoded message "12", it could be decoded as "AB" (1 2) or "L" (12).
        //The number of ways decoding "12" is 2.
        public int NumDecodings(string s)
        {
            if (s == null || s.Length == 0 || s[0] == '0')
                return 0;

            int len = s.Length;
            int[] ret = new int[len + 1];
            ret[0] = 1;
            ret[1] = 1;

            for (int i = 2; i <= len; i++)
            {
                int digi2 = 0;
                int digi1 = 0;
                int prev2 = 0;
                int prev1 = 0;
                int.TryParse(s.Substring(i - 2, 2), out digi2);
                int.TryParse(s.Substring(i - 1, 1), out digi1);

                if (digi2 >= 10 && digi2 <= 26)
                    prev2 = ret[i - 2];

                if (digi1 != 0)
                    prev1 = ret[i - 1];

                ret[i] = prev2 + prev1;
            }
            return ret[len];
        }

        //70. Climbing Stairs
        //You are climbing a stair case. It takes n steps to reach to the top.
        // Each time you can either climb 1 or 2 steps.In how many distinct ways can you climb to the top?
        //       Note: Given n will be a positive integer.
        public int ClimbStairs(int n)
        {
            if (n < 3)
                return n;

            int[] ret = new int[n + 1];
            ret[0] = 0;
            ret[1] = 1;
            ret[2] = 2;

            for (int i = 3; i <= n; i++)
            {
                ret[i] = ret[i - 1] + ret[i - 2];
            }
            return ret[n];
        }


        //256. Paint House
        //There are a row of n houses, each house can be painted with one of the three colors: red, blue or green. 
        //The cost of painting each house with a certain color is different. You have to paint all the houses such 
        //that no two adjacent houses have the same color.
        //The cost of painting each house with a certain color is represented by a n x 3 cost matrix.For example, 
        //costs[0][0] is the cost of painting house 0 with color red; costs[1][2] is the cost of painting house 1 
        //with color green, and so on...Find the minimum cost to paint all houses.
        public int MinCost(int[,] costs)
        {
            if (costs == null || costs.Length == 0)
                return 0;

            int rows = costs.GetLength(0);
            for (int r = 1; r < costs.GetLength(0); r++)
            {
                costs[r, 0] += Math.Min(costs[r - 1, 1], costs[r - 1, 2]);
                costs[r, 1] += Math.Min(costs[r - 1, 0], costs[r - 1, 2]);
                costs[r, 2] += Math.Min(costs[r - 1, 0], costs[r - 1, 1]);
            }

            return Math.Min(costs[rows - 1, 0], Math.Min(costs[rows - 1, 1], costs[rows - 1, 2]));
        }
    }


}
