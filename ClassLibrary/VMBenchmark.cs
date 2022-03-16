using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;

using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace ClassLibrary
{
    public class VMBenchmark : INotifyPropertyChanged
    {
        public ObservableCollection<VMTime> collection_time { get; set; }
        public ObservableCollection<VMAccuracy> collection_acc { get; set; }
        public VMBenchmark()
        {
            collection_time = new ObservableCollection<VMTime>();
            collection_acc = new ObservableCollection<VMAccuracy>();
            min_max_diff = new double[2];
        }
        public void AddVMTime(VMGrid grid)
        {
            if (grid.length < 1)
                throw new Exception("Length must be more 0!");
            if (grid.limits[0] >= grid.limits[1])
                throw new Exception("Left boundary must be more right boundary!");

            VMTime vmtime = new VMTime();
            vmtime.grid = new VMGrid();

            vmtime.grid.length = grid.length;
            vmtime.grid.function = grid.function;
            vmtime.grid.limits = new double[2] { grid.limits[0], grid.limits[1] };

            int ret = -1;

            float[] x_f = new float[grid.length];
            double[] x_d = new double[grid.length];

            float[] y_f_HA = new float[grid.length];
            float[] y_f_EP = new float[grid.length];

            double[] y_d_HA = new double[grid.length];
            double[] y_d_EP = new double[grid.length];

            double[] time_f = new double[2];
            double[] time_d = new double[2];

            for (int i = 0; i < grid.length; i++)
            {
                x_d[i] = grid.limits[0] + grid.step * i;
                x_f[i] = (float)x_d[i];
            }
            switch (grid.function)
            {
                case VMf.vmsLn:
                    VMS_Ln(grid.length, x_f, y_f_HA, y_f_EP, time_f, ref ret);
                    if (ret != 0) throw new Exception("vmsLn return -1");
                    vmtime.time_VML_HA = time_f[0];
                    vmtime.time_VML_EP = time_f[1];
                    break;
                case VMf.vmdLn:
                    VMD_Ln(grid.length, x_d, y_d_HA, y_d_EP, time_d, ref ret);
                    if (ret != 0) throw new Exception("vmdLn return -1");
                    vmtime.time_VML_HA = time_d[0];
                    vmtime.time_VML_EP = time_d[1];
                    break;
                case VMf.vmsLGamma:
                    VMS_LGamma(grid.length, x_f, y_f_HA, y_f_EP, time_f, ref ret);
                    if (ret != 0) throw new Exception("vmsLGamma return -1");
                    vmtime.time_VML_HA = time_f[0];
                    vmtime.time_VML_EP = time_f[1];
                    break;
                case VMf.vmdLGamma:
                    VMD_LGamma(grid.length, x_d, y_d_HA, y_d_EP, time_d, ref ret);
                    if (ret != 0) throw new Exception("vmdLGamma return -1");
                    vmtime.time_VML_HA = time_d[0];
                    vmtime.time_VML_EP = time_d[1];
                    break;
                default: 
                    throw new Exception("Wrong name function!");
            }
            vmtime.coef_boost = (vmtime.time_VML_EP / vmtime.time_VML_HA);

            collection_time.Add(vmtime);
            update_min_max();
            OnPropertyChanged("min_max_diff");
        }
        public void AddVMAccuracy(VMGrid grid)
        {
            if (grid.length < 1)
                throw new Exception("Length must be more 0!");
            if (grid.limits[0] >= grid.limits[1])
                throw new Exception("Left boundary must be more right boundary!");
            VMAccuracy vmaccuracy = new VMAccuracy();
            vmaccuracy.x_VML_HA_VML_EP = new double[3];
            vmaccuracy.grid = new VMGrid();

            vmaccuracy.grid.length = grid.length;
            vmaccuracy.grid.function = grid.function;
            vmaccuracy.grid.limits = new double[2] { grid.limits[0], grid.limits[1] };

            int ret = -1;

            float[] x_f = new float[grid.length];
            double[] x_d = new double[grid.length];

            float[] y_f_HA = new float[grid.length];
            float[] y_f_EP = new float[grid.length];

            double[] y_d_HA = new double[grid.length];
            double[] y_d_EP = new double[grid.length];

            double[] time_f = new double[2];
            double[] time_d = new double[2];

            for (int i = 0; i < grid.length; i++)
            {
                x_d[i] = grid.limits[0] + grid.step * i;
                x_f[i] = (float)x_d[i];
            }

            switch (grid.function)
            {
                case VMf.vmsLn:
                    VMS_Ln(grid.length, x_f, y_f_HA, y_f_EP, time_f, ref ret);
                    if (ret != 0) throw new Exception("vmsLn return -1");
                    break;
                case VMf.vmdLn:
                    VMD_Ln(grid.length, x_d, y_d_HA, y_d_EP, time_d, ref ret);
                    if (ret != 0) throw new Exception("vmdLn return -1");
                    break;
                case VMf.vmsLGamma:
                    VMS_LGamma(grid.length, x_f, y_f_HA, y_f_EP, time_f, ref ret);
                    if (ret != 0) throw new Exception("vmsLGamma return -1");
                    break;
                case VMf.vmdLGamma:
                    VMD_LGamma(grid.length, x_d, y_d_HA, y_d_EP, time_d, ref ret);
                    if (ret != 0) throw new Exception("vmdLGamma return -1");
                    break;
                default:
                    throw new Exception("Wrong name function!");
            }
            //
            if ((grid.function == VMf.vmsLn) || (grid.function == VMf.vmsLGamma))
            {
                float max_diff = Math.Abs(y_f_HA[0] - y_f_EP[0]);
                int max_ind = 0;
                float tmp;

                for (int i = 1; i < grid.length; i++)
                {
                    tmp = Math.Abs(y_f_HA[i] - y_f_EP[i]);
                    if (tmp > max_diff)
                    {
                        max_diff = tmp;
                        max_ind = i;
                    }
                }
                vmaccuracy.difference = max_diff;
                vmaccuracy.x_VML_HA_VML_EP[0] = x_f[max_ind];
                vmaccuracy.x_VML_HA_VML_EP[1] = y_f_HA[max_ind];
                vmaccuracy.x_VML_HA_VML_EP[2] = y_f_EP[max_ind];
            }
            else
            {
                double max_diff = Math.Abs(y_d_HA[0] - y_d_EP[0]);
                int max_ind = 0;
                double tmp;

                for (int i = 1; i < grid.length; i++)
                {
                    tmp = Math.Abs(y_d_HA[i] - y_d_EP[i]);
                    if (tmp > max_diff)
                    {
                        max_diff = tmp;
                        max_ind = i;
                    }
                }
                vmaccuracy.difference = max_diff;
                vmaccuracy.x_VML_HA_VML_EP[0] = x_d[max_ind];
                vmaccuracy.x_VML_HA_VML_EP[1] = y_d_HA[max_ind];
                vmaccuracy.x_VML_HA_VML_EP[2] = y_d_EP[max_ind];
            }
            //
            collection_acc.Add(vmaccuracy);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public double[] min_max_diff { get; private set; }
        private void update_min_max()
        {
            min_max_diff = calculate_min_max();
        }
        private double[] calculate_min_max()
        {
            double[] res = new double[2];

            res[0] = collection_time[0].coef_boost;
            res[1] = collection_time[0].coef_boost;

            for (int i = 1; i < collection_time.Count; i++)
            {
                if (collection_time[i].coef_boost < res[0])
                    res[0] = collection_time[i].coef_boost;
                if (collection_time[i].coef_boost > res[1])
                    res[1] = collection_time[i].coef_boost;
            }

            return res;
        }

        [DllImport("..\\..\\..\\..\\x64\\DEBUG\\CPP_DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern
        void VMS_Ln(int n, float[] x, float[] y_HA, float[] y_EP, double[] time, ref int ret);
        [DllImport("..\\..\\..\\..\\x64\\DEBUG\\CPP_DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern
        void VMD_Ln(int n, double[] x, double[] y_HA, double[] y_EP, double[] time, ref int ret);
        [DllImport("..\\..\\..\\..\\x64\\DEBUG\\CPP_DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern
        void VMS_LGamma(int n, float[] x, float[] y_HA, float[] y_EP, double[] time, ref int ret);
        [DllImport("..\\..\\..\\..\\x64\\DEBUG\\CPP_DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern
        void VMD_LGamma(int n, double[] x, double[] y_HA, double[] y_EP, double[] time, ref int ret);
    }
}
