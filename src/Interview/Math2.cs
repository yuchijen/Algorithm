using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interview
{
    public class Math2
    {
        //50. Pow(x, n)
        //Implement pow(x, n). in log(n)
        public double MyPow(double x, int n)
        {
            if (n == 0)
                return 1;
            if (n < 0)
                return (1 / x) * MyPow((1 / x), -(n + 1));
            if (n % 2 == 0)
                return MyPow(x * x, n >> 1);
            else
                return x * MyPow(x * x, n >> 1);
        }


        //leetcode  273. Integer to English Words
        //Convert a non-negative integer to its english words representation. Given input is guaranteed to 
        //be less than 2^31 - 1.
        // For example, 123 -> "One Hundred Twenty Three"  12345 -> "Twelve Thousand Three Hundred Forty Five"
        //1234567 -> "One Million Two Hundred Thirty Four Thousand Five Hundred Sixty Seven"
        public string NumberToWords(int num)
        {
            string[] digitStr1 = new string[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
            string[] digitStrTeen = new string[] { "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
            string[] digitStr2 = new string[] { "zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

            if (num <= 0)
                return "Zero";

            string ret = "";

            int level = 0;

            while (num % 1000 != 0 || num / 1000 != 0)
            {
                int curSet = num % 1000;
                string curSetStr = curSet.ToString();

                string curRet = "";
                for (int i = curSetStr.Length - 1; i >= 0; i--)
                {
                    if (curSetStr.Length - i == 1 && curSetStr[i] - '0' != 0)
                    {
                        curRet = " " + digitStr1[curSetStr[i] - '0'] + curRet;
                    }
                    else if (curSetStr.Length - i == 2 && curSetStr[i] - '0' != 0)
                    {
                        if (curSetStr[i] - '0' == 1)
                            curRet = " " + digitStrTeen[curSetStr[i + 1] - '0'];
                        else
                            curRet = " " + digitStr2[curSetStr[i] - '0'] + curRet;
                    }
                    else if (curSetStr.Length - i == 3)
                        curRet = " " + digitStr1[curSetStr[i] - '0'] + " Hundred" + curRet;
                }

                if (level == 0)
                    ret = curRet;
                else if (level == 1 && !string.IsNullOrWhiteSpace(curRet))
                    ret = curRet + " Thousand" + ret;
                else if (level == 2 && !string.IsNullOrWhiteSpace(curRet))
                    ret = curRet + " Million" + ret;
                else if (level == 3)
                    ret = curRet + " Billion" + ret;

                num /= 1000;
                level += 1;
            }
            return ret.Trim();
        }


        //leetcode 171. Excel Sheet Column Number
        //Given a column title as appear in an Excel sheet, return its corresponding column number.
        // For example:  A -> 1, B -> 2, ... Z -> 26,  AA -> 27,    AB -> 28 
        public int TitleToNumber(string s)
        {
            if (string.IsNullOrEmpty(s))
                return 0;

            int level = 0;
            double ret = 0;
            for (int i = s.Length - 1; i >= 0; i--)
            {
                level = s.Length - 1 - i;
                int letter = (s[i] - 'A') + 1;
                ret += letter * Math.Pow(26, level);
            }
            return Convert.ToInt32(ret);
        }
    }
}
