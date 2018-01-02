using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interview
{
    public class BitManipulate
    {

        //NVIDIA Given a 32 bit unsigned integer, write a function(in C) that returns a count of how many bits are "1".
        public int CountOneBit(uint num)
        {
            int ret = 0;
            while(num!=0)
            {
                if ((num & 1) ==1)
                    ret++;

                num >>= 1;
            }
            return ret;
        }

        //NVIDIA: Swap even and odd bits of a 32 bit integer.  
        public uint SwapBits(uint x)
        {
            //get all even bit
            uint even = x & 0xAAAAAAAA;
            uint odd  = x & 0x55555555;

            even >>= 1;
            odd <<= 1;
            return even | odd;
        }
    }
}