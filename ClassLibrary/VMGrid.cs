using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public enum VMf { vmsLn, vmdLn, vmsLGamma, vmdLGamma }
    public class VMGrid
    {
        public int length { get; set; }
        public double [] limits { get; set; }
        public double step { 
            get { return (limits[1] - limits[0]) / length; }
            private set { step = value; }
        }
        public VMf function { get; set; }
        //public VMGrid(int len, double[] lim, VMf fun)
        //{
        //    length = len;
        //    limits = lim;
        //    function = fun;
        //}
    }
}
