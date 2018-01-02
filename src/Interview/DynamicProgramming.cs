using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interview
{
    public class DynamicProgramming
    {
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
