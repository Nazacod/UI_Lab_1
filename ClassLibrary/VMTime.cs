using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public struct VMTime
    {
        public VMGrid grid { get; set; }
        public double time_VML_HA { get; set; }
        public double time_VML_EP { get; set; }
        public double coef_boost { get; set; }
        public override string ToString()
        {   
            return $"Function used: {grid.function}\nArray length: {grid.length}," +
                $" x[0]:{grid.limits[0]} x[1]:{grid.limits[1]}\n" +
                $"Time on VML_HA: {time_VML_HA} time on VML_EP: {time_VML_EP}\nRatio: {coef_boost:F4}";
        }
    }
}
