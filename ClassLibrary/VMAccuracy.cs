using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class VMAccuracy
    {
        public VMGrid grid { get; set; }
        public double difference { get; set; }
        public double[] x_VML_HA_VML_EP { get; set; }
        public override string ToString()
        {
            return $"Function used: {grid.function}\nArray length: {grid.length}," +
                $" x[0]:{grid.limits[0]} x[1]:{grid.limits[1]}\n" +
                $"Maximum absolute diffrence: {difference}" +
                $"\nFor x: {x_VML_HA_VML_EP[0]}\n" +
                $"F(x, VML_HA): {x_VML_HA_VML_EP[1]} F(x, VML_EP): {x_VML_HA_VML_EP[2]}";
        }
    }
}
