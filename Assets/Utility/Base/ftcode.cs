using System;
using System.Collections.Generic;
using System.Collections;
using System.Runtime.InteropServices;

namespace Utility
{
    public class FTCode
    {
        public const string LibName = "ftcode";
        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ftcode(int _seed,int type, [MarshalAs(UnmanagedType.LPArray)] ref byte[] data,int length);

        static uint tyrand(uint randseed)
        {
            uint randomNext;
            randomNext = randseed * 1103515245 + 12345;
            return (uint)((randomNext / 65536) % 32768);
        }

        public static void xorCodeing(uint _seed, ref byte[] data)
        {
            uint grandom = _seed;
            for (uint i = 0; i < data.Length; i++)
            {
                grandom = tyrand(grandom);
                data[i] = System.Convert.ToByte(data[i] ^ (grandom % 255));
            }
        }
    }
}
