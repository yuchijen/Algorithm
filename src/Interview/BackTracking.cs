using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interview
{
    public class BackTracking
    {
        //254. Factor Combinations
        //Numbers can be regarded as product of its factors.For example,
        //input: 12        output:
        //[  [2, 6],  [2, 2, 3],  [3, 4] ]

        public IList<IList<int>> GetFactors(int n)
        {
            List<IList<int>> ret = new List<IList<int>>();
            if (n <= 3)
                return ret;
         
            factorHelper(ret, new List<int>(), n, 2);
            return ret;
        }
        void factorHelper(IList<IList<int>> ret, List<int> curList, int cur, int curIdx)
        {
            if (cur == 1)
            {
                if (curList.Count > 1)
                    ret.Add(new List<int>(curList));
                return;
            }
            for (int i = curIdx; i <= cur; i++)
            {
                if (cur % i == 0)
                {
                    curList.Add(i);
                    factorHelper(ret, curList, cur / i, i);
                    curList.RemoveAt(curList.Count - 1);
                }
            }
        }

        #region General term 

        //78. Subsets
        //If nums = [1,2,3], a solution is:
        //  [
        //[]
        //[3],
        //[1],
        //[2],
        //[1,2,3],
        //[1,3],
        //[2,3],
        //[1,2], 
        //]
        public IList<IList<int>> Subsets(int[] nums)
        {
            var ret = new List<IList<int>>();
            backtracking(0, nums, new List<int>(), ret);
            return ret;
        }
        void backtracking(int curIdx, int[] nums, List<int> list, List<IList<int>> ret)
        {
            ret.Add(new List<int>(list));
            for (int i = curIdx; i < nums.Length; i++)
            {
                list.Add(nums[i]);
                backtracking(i + 1, nums, list, ret);
                list.RemoveAt(list.Count - 1);
            }
        }
        

        //90. Subsets II
        //Given a collection of integers that might contain duplicates, nums, return all possible subsets.
        //Note: The solution set must not contain duplicate subsets.
        //If nums = [1,2,2], a solution is:
        //[
        //  [2],
        // [1],
        //[1,2,2],
        //[2,2],
        //[1,2],
        //[]
        //]
        public IList<IList<int>> SubsetsWithDup(int[] nums)
        {
            var ret = new List<IList<int>>();
            var hashsets = new HashSet<string>();
            Array.Sort(nums);
            subsetHelperWithoutDuplicate2(0, nums, new List<int>(), ret, hashsets);
            return ret;
        }
        void subsetHelperWithoutDuplicate2(int curIdx, int[] nums, List<int> list, IList<IList<int>> ret, HashSet<string> hashsets)
        {
            string temp = string.Join(",", list);

            if (!hashsets.Contains(temp))
            {
                hashsets.Add(temp);
                ret.Add(new List<int>(list));
            }
            for (int i = curIdx; i < nums.Length; i++)
            {
                list.Add(nums[i]);
                subsetHelperWithoutDuplicate2(i + 1, nums, list, ret, hashsets);
                list.Remove(nums[i]);
            }
        }


        //77. Combinations
        //Given two integers n and k, return all possible combinations of k numbers out of 1 ... n.
        //If n = 4 and k = 2, a solution is:
        //[
        //    [2,4],
        //    [3,4],
        //    [2,3],
        //    [1,2],
        //    [1,3],
        //    [1,4],
        //]
        public IList<IList<int>> Combine(int n, int k)
        {
            List<IList<int>> ret = new List<IList<int>>();

            helper(1, new List<int>(), ret, n, k);
            return ret;
        }
        void helper(int curIdx, List<int> curList, List<IList<int>> ret, int n, int k)
        {
            if (curList.Count == k)
            {
                ret.Add(new List<int>(curList));
                return;
            }
            for (int i = curIdx; i <= n; i++)
            {
                curList.Add(i);
                helper(curIdx + 1, curList, ret, n, k);
                curList.Remove(curList.Last());
            }
        }

        //40. Combination Sum II
        //All numbers (including target) will be positive integers.
        //The solution set must not contain duplicate combinations.
        //For example, given candidate set[10, 1, 2, 7, 6, 1, 5] and target 8,
        //A solution set is: 
        //[
        //  [1, 7],
        //  [1, 2, 5],
        //  [2, 6],
        //  [1, 1, 6]
        //]
        public IList<IList<int>> CombinationSum2(int[] candidates, int target)
        {
            Array.Sort(candidates);
            List<IList<int>> ret = new List<IList<int>>();
            helper(candidates, target, 0, new List<int>(), ret);
            return ret;
        }
        void helper(int[] nums, int target, int idx, List<int> curList, List<IList<int>> ret)
        {
            if (target == 0)
            {
                ret.Add(new List<int>(curList));
                return;
            }

            for (int i = idx; i < nums.Length; i++)
            {
                if (nums[i] > target)
                    return;
                if (i > idx && nums[i] == nums[i - 1])
                    continue;
                curList.Add(nums[i]);
                helper(nums, target - nums[i], i + 1, curList, ret);
                curList.Remove(curList.Last());
            }
        }

        //46. Permutations
        //Given a collection of Distinct numbers, return all possible permutations.
        //      For example,      [1, 2, 3] have the following permutations:
        //[
        //[1,2,3],
        //[1,3,2],
        //[2,1,3],
        //[2,3,1],
        //[3,1,2],
        //[3,2,1]
        //]
        public IList<IList<int>> Permute(int[] nums)
        {
            var ret = new List<IList<int>>();
            PermuBackTrack1(nums, 0, ret);
            return ret;
        }
        void PermuBackTrack1(int[] nums, int idx, List<IList<int>> ret)
        {
            if (idx == nums.Length)
            {
                ret.Add(new List<int>(nums.ToList()));
                return;
            }
            for (int i = idx; i < nums.Length; i++)
            {
                swap(nums, idx, i);
                PermuBackTrack1(nums, idx + 1, ret);
                swap(nums, i, idx);
            }
        }
        void swap(int[] arr, int i, int j)
        {
            if (i == j)
                return;
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

        //47. Permutations II
        //Given a collection of numbers that might contain duplicates, return all possible unique permutations.
        //For example,[1,1,2] have the following unique permutations:
        //[
        //[1,1,2],
        //[1,2,1],
        //[2,1,1]
        //]
        public IList<IList<int>> PermuteUnique(int[] nums)
        {
            var ret = new List<IList<int>>();
            Array.Sort(nums);
            backtrack(nums, 0, ret);
            return ret;
        }
        HashSet<string> hashSet = new HashSet<string>();
        void backtrack(int[] nums, int idx, IList<IList<int>> ret)
        {
            if (idx == nums.Length)
            {
                string temp = string.Join(",", nums.ToList());
                if (!hashSet.Contains(temp))
                {
                    hashSet.Add(temp);
                    ret.Add(new List<int>(nums.ToList()));
                }
                return;
            }
            for (int i = idx; i < nums.Length; i++)
            {
                if (i > idx && nums[i] == nums[i - 1])
                    continue;

                swap(nums, idx, i);
                backtrack(nums, idx + 1, ret);
                swap(nums, i, idx);
            }
        }
    


    #endregion
    }
}

