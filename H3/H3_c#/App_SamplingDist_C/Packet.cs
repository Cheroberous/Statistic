using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_SamplingDist_C
{
    internal class Packet
    {
        public int number;
        public double time;
        public string sourceAdd;
        public string destAdd;
        public string protocol;
        public int length;
        public string info;

        public Packet(int no, double t, string sa, string da, string pr, int len, string inf)
        {
            number = no;
            time = t;
            sourceAdd = sa;
            destAdd = da;
            protocol = pr;
            length = len;
            info = inf;
        }
    }
}
