using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using CommonClassLib;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string rece = "TJSETREQ@@@0@00000@K000086@@@0@&@K000086@@@CME@6J1810@@@@@@@@H@K0000086@@@0@0@@20@TJM20180918001@1@1@1@0.0089550@1@1@0.0092@1@2@0@0@0@1@00010101 00:00:00@00010101 00:00:00";
            
            NetInfo netInfo  = new NetInfo();

            netInfo.MyReadString(rece);

            ConditionReqInfo c = new ConditionReqInfo();

            c.MyReadString(netInfo.infoT);

            Thread.Sleep(-1);
        }
    }
}
