using System;
using System.Collections.Generic;
using System.Linq;

namespace RobotTest
{
    //Horoscope assessment test 
    
    /// <summary>
    ///we can use state pattern in this chip switching robot 
    ///design an interface as a container to contain different type of chips
    /// </summary>
    public interface IChip
    {
        string ChipName();
    }

    /// <summary>
    /// Sort chip implements IChip and has its own sort method.
    /// </summary>
    public class SortChip : IChip
    {
        public void executeSort(int[] arr, bool isAscendent)
        {
            if (isAscendent)
                Array.Sort(arr);
            else
                arr = arr.OrderByDescending(i => i).ToArray();

            foreach (var x in arr)
                Console.Write(x+ " ");
            Console.WriteLine();
        }
        public string ChipName()
        {
            Console.WriteLine("Sort chip is ON");
            return "SortChip";
        }
    }

    /// <summary>
    /// Sum chip implements IChip and has its own sum method.
    /// </summary>
    public class SumArrayChip : IChip
    {
        public int executeSumArray(int[] arr)
        {
            if (arr == null || arr.Length == 0)
                return 0;

            return arr.Sum(x => x);
        }
        public string ChipName()
        {
            Console.WriteLine("Sum chip is ON");
            return "SumChip";
        }
    }

    /// <summary>
    /// Robot accepts different kind of chips in an interface , use AcceptChip to switch chip 
    /// use HashSet to remember how many kinds of chips been used.
    /// </summary>
    public class Robot
    {
        HashSet<string> hs = new HashSet<string>();

        IChip container;
        public int TotalChipBeenUsed
        {
            get { return hs.Count; }
        }

        public void AcceptChip(IChip chip)
        {
            container = chip;            
            hs.Add(container.ChipName());
        }

        public IChip GetChip()
        {
            return container;
        }
    }

    /// <summary>
    /// in Test case, we use switch different chips and calculate how many chips been used without duplicate 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var robot = new Robot();

            SortChip sortChip = new SortChip();            
            
            robot.AcceptChip(sortChip);

            int[] testArray = new int[] { 4, 1, 3, 7, 9, 2, 6 };
            (robot.GetChip() as SortChip).executeSort(testArray, true);

            Console.Write("Total Chips Been Used: ");
            Console.WriteLine(robot.TotalChipBeenUsed);    

            SumArrayChip sumChip = new SumArrayChip();
            robot.AcceptChip(sumChip);

            Console.Write("Sum of Array: ");
            Console.WriteLine((robot.GetChip() as SumArrayChip).executeSumArray(testArray));

            Console.Write("Total Chips Been Used: ");
            Console.WriteLine(robot.TotalChipBeenUsed);

            robot.AcceptChip(sortChip);            
            Console.Write("Total Chips Been Used: ");
            Console.WriteLine(robot.TotalChipBeenUsed);

        }
    }
}