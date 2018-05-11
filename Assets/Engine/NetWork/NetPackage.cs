using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Engine
{
    // 网络底层数据包
    public class PackageIn : MemoryStream
    {
        public static int HEADER_SIZE = 4; /// protobuff中一个整数占用的最大字节数 为5 这里设置成8
        public PackageIn(byte[] buff)
            : base(buff)
        {
 
        }
    }

    public class PackageOut : MemoryStream
    {
        public static int SEED = 0;
        private int _code;


        public PackageOut(MemoryStream buff,int nCode)
        {
            Write(buff.GetBuffer(), 0, (int)buff.Length);
            Flush();
            _code = nCode;
        }

        ////临时开放
        public int getCode()
        {
            return _code;
        }

    }
}