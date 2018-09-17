﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interview
{
    public class BinarySearch
    {

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
                if (nums[realmid] == target)
                    return realmid;
                if (nums[realmid] < target)
                    lo = mid + 1;
                else
                    hi = mid - 1;
            }
            return -1;
        }
        //anayse 2 cases : 
        // 34567012   start to mid are sorted 
        // 67012345   mid to end are sorted
        public int SearchRotatedSortedArray2(int[] nums, int target)
        {
            if (nums == null || nums.Length == 0)
                return -1;
            int st = 0;
            int end = nums.Length-1;

            while (st <= end)
            {
                int mid = st + (end - st) / 2;
                if (nums[mid] == target)
                    return mid;
                if(nums[mid] < nums[end]) //right side sorted
                {
                    if (nums[mid] < target && target < nums[end])
                        st = mid + 1;  //go right
                    else
                        end = mid - 1;
                }
                else //left side sorted
                {
                    if (target >= nums[st] && target < nums[mid])
                        end = mid - 1;  //go left
                    else
                        st = mid + 1;
                }
            }
            return -1;            
        }

        //81. Search in Rotated Sorted Array II  with duplicate number
        public bool SearchRotatedSortedArrayII(int[] nums, int target)
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


        //278. First Bad Version
        //You are a product manager and currently leading a team to develop a new product. Unfortunately, the 
        //latest version of your product fails the quality check. Since each version is developed based on the 
        //previous version, all the versions after a bad version are also bad.
        //Suppose you have n versions[1, 2, ..., n] and you want to find out the first bad one, which causes 
        //all the following ones to be bad.
        //You are given an API bool isBadVersion(version) which will return whether version is bad.Implement a 
        //function to find the first bad version.You should minimize the number of calls to the API.
        public int FirstBadVersion(int n)
        {
            if (IsBadVersion(1))
                return 1;

            int st = 1;
            int end = n;

            while (st <= end)
            {
                int mid = st + (end - st) / 2;
                if (mid > 1 && IsBadVersion(mid) && !IsBadVersion(mid - 1))
                    return mid;
                if (IsBadVersion(mid))
                    end = mid - 1;
                else
                    st = mid + 1;
            }
            return -1;
        }

        bool IsBadVersion(int version)
        {
            return true;
        }

        //374. Guess Number Higher or Lower
        //We are playing the Guess Game. The game is as follows:
        //I pick a number from 1 to n.You have to guess which number I picked.
        //Every time you guess wrong, I'll tell you whether the number is higher or lower.
        //You call a pre-defined API guess(int num) which returns 3 possible results (-1, 1, or 0):
        public int guessNumber(int n)
        {
            if (n == 0)
                return 0;

            int st = 1, end = n;
            
            while(st <= end)
            {
                int piv = (st + end) / 2;

                if (guess(piv) == 0)
                    return piv;
                else if(guess(piv) >0)
                {
                    end = piv - 1;
                }
                else
                {
                    st = piv + 1;
                }
            }
            return -1;
        }
        int guess(int num)
        {
            return -1;
        }

        
        //69. Sqrt(x)
        //Implement int sqrt(int x).
        //Compute and return the square root of x.
        public int MySqrt(int x)
        {
            if (x <= 0)
                return 0;

            int st = 1, end = x;

            while (true)
            {
                int piv = (st + end) / 2;

                if (piv > x / piv)
                    end = piv - 1;
                else
                {
                    if ((piv + 1) > x / (piv + 1))
                        return piv;
                    st = piv + 1;
                }                 
            }
        }


        //162. Find Peak Element
        //A peak element is an element that is greater than its neighbors.
        //Given an input array where num[i] ≠ num[i + 1], find a peak element and return its index.
        //The array may contain multiple peaks, in that case return the index to any one of the peaks is fine.
        //You may imagine that num[-1] = num[n] = -∞.
        //For example, in array[1, 2, 3, 1], 3 is a peak element and your function should return the index number 2.
        public int FindPeakElement(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return -1;

            int st = 0, end = nums.Length - 1;

            while (st <= end)
            {
                int piv = (st + end) >> 1;
                if (piv==0 || piv==nums.Length-1 || ( piv < nums.Length-1 && piv > 1 && nums[piv] > nums[piv + 1] && nums[piv] > nums[piv - 1]))
                    return piv;
                if (piv < nums.Length - 1 && nums[piv] < nums[piv + 1])
                    st = piv + 1;
                else if (piv > 0 && nums[piv] < nums[piv - 1])
                    end = piv - 1;
            }
            return -1;
        }


        //349. Intersection of Two Arrays
        //Given two arrays, write a function to compute their intersection.
        // Example:Given nums1 = [1, 2, 2, 1], nums2 = [2, 2], return [2].
        public int[] Intersection(int[] nums1, int[] nums2)
        {            
            var l1 = nums1.ToList();
            var l2 = nums2.ToList();

            var cc = from item in l1
                    where (nums2.Contains(item))
                    select item;
            
            HashSet<int> hs = new HashSet<int>();

            foreach (var x in cc)
                hs.Add(x);
            return hs.ToArray();
            
        }


        //287. Find the Duplicate Number
        //Given an array nums containing n + 1 integers where each integer is between 1 and n (inclusive), prove that at least one duplicate number must exist. Assume that there is only one duplicate number, find the duplicate one.
        //Note: You must not modify the array(assume the array is read only).
        //You must use only constant, O(1) extra space., less than O(n^2) 
        public int FindDuplicate(int[] nums)
        {

            if (nums == null && nums.Length == 0)
                return -1;

            Array.Sort(nums);

            for (int i = 0; i < nums.Length - 1; i++)
            {
                if (nums[i] == nums[i + 1])
                    return nums[i];
            }
            return nums[nums.Length - 1];
        }

        //50. Pow(x, n)
        //Implement pow(x, n). smart solution
        public double MyPow(double x, int n)
        {
            if (n == 0)
                return 1;
            if (n < 0)
            {
                return 1 / x * MyPow(1 / x, -(n + 1));
            }
            else
            {
                if (n % 2 == 0)
                    return MyPow(x * x, n >> 1);
                else
                    return x * MyPow(x * x, n >> 1);
            }
        }

        // 153. Find Minimum in Rotated Sorted Array
        //Suppose an array sorted in ascending order is rotated at some pivot unknown to you beforehand.
        //(i.e., 0 1 2 4 5 6 7 might become 4 5 6 7 0 1 2).Find the minimum element.
        //You may assume no duplicate exists in the array.
        public int FindMin(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return 0;
            if (nums.Length == 1)
                return nums[0];

            int st = 0; int end = nums.Length - 1;

            while (st < end)
            {
                int mid = (st + end) >> 1;

                if (mid + 1 <= end && nums[mid] > nums[mid + 1])
                    return nums[mid + 1];
                else if (nums[mid] < nums[end])
                    end = mid;
                else if (nums[st] < nums[mid])
                    st = mid;
                else
                    end = mid;
            }
            return nums[0];
        }

        //Amazon: search value in increasing and decreasing array
        public int searchIncreasingDecreasing(int[] nums, int target)
        {
            if (nums == null || nums.Length == 0)
                return -1;

            int st = 0;
            int end = nums.Length - 1;

            int maxIdx = findMaxValIdx(nums);
            if (maxIdx == -1)
                return -1;
            int section1 = BSearch(nums, 0, maxIdx, target);
            if (section1 != -1)
                return section1;

            int section2 = BInverseSearch(nums, maxIdx + 1, nums.Length - 1, target);
            if (section2 != -1)
                return section2;

            return -1;
        }
        int BSearch(int[] nums, int st, int end, int target)
        {
            while (st <= end)
            {
                int pivol = (st + end) / 2;
                if (target == nums[pivol])
                    return pivol;
                if (nums[pivol] < target)
                    st = pivol + 1;
                else
                    end = pivol - 1;
            }
            return -1;
        }
        int BInverseSearch(int[] nums, int st, int end, int target)
        {
            while (st <= end)
            {
                int pivol = (st + end) / 2;
                if (target == nums[pivol])
                    return pivol;
                if (nums[pivol] < target)
                    end = pivol - 1;
                else
                    st = pivol + 1;
            }
            return -1;

        }
        int findMaxValIdx(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return -1;

            int st = 0;
            int end = nums.Length - 1;

            while (st <= end)
            {
                int pivol = (st + end) / 2;
                if (pivol == 0 || pivol == nums.Length - 1 || (pivol > 0 && pivol < nums.Length-1 && nums[pivol] > nums[pivol - 1] && nums[pivol] > nums[pivol + 1]))
                    return pivol;

                if (pivol < nums.Length - 1 && nums[pivol] > nums[pivol + 1])
                {
                    end = pivol - 1;
                }
                if (pivol > 0 && nums[pivol] > nums[pivol - 1])
                {
                    st = pivol + 1;
                }
            }
            return -1;
        }

        //33. Search in Rotated Sorted Array
        //Suppose an array sorted in ascending order is rotated at some pivot unknown to you beforehand.
        //  (i.e., 0 1 2 4 5 6 7 might become 4 5 6 7 0 1 2).
        //You are given a target value to search.If found in the array return its index, otherwise return -1.
        public int SearchRotatedArray(int[] nums, int target)
        {
            if (nums == null || nums.Length == 0)
                return -1;

            int st = 0;
            int end = nums.Length - 1;

            while (st <= end)
            {
                int pivol = (st + end) / 2;
                if (target == nums[pivol])
                    return pivol;

                if (nums[pivol] >= nums[st])
                {
                    if (target < nums[pivol] && target >= nums[st])
                        end = pivol - 1;
                    else
                        st = pivol + 1;
                }
                else
                {
                    if (target > nums[pivol] && target <= nums[end])
                        st = pivol + 1;
                    else
                        end = pivol - 1;
                }
            }
            return -1;
        }


    }
}
