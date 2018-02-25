using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interview
{
    public class DynamicProgramming
    {
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
