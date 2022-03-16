using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ClassLibrary;

using System.IO;
using System.Windows;
using System.Runtime.Serialization.Formatters.Binary;

namespace UI_Lab_1
{
    public class ViewData
    {
        public VMBenchmark benchmark { get; set; }
        public VMGrid grid { get; set; }
        public bool have_changes { get; set; }
        public ViewData()
        {
            benchmark = new VMBenchmark();
            grid = new VMGrid();
            have_changes = false;
        }
        public void AddVMTime(VMGrid grid)
        {
            //TODO
            benchmark.AddVMTime(grid);
            have_changes = true;
        }
        public void AddVMAccuracy(VMGrid grid)
        {
            //TODO
            benchmark.AddVMAccuracy(grid);
            have_changes = true;
        }
        public void Save(string filename)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filename, FileMode.Create);
                StreamWriter writer = new StreamWriter(fs);

                writer.WriteLine($"{benchmark.min_max_diff[0]} {benchmark.min_max_diff[1]}");

                foreach (VMTime elem in benchmark.collection_time)
                {
                    writer.Write($"{elem.grid.function} {elem.grid.length} {elem.grid.limits[0]} {elem.grid.limits[1]}");
                    writer.WriteLine($" {elem.time_VML_EP} {elem.time_VML_HA} {elem.coef_boost}");
                }

                writer.WriteLine("END_COLLECTION_TIME");

                foreach (VMAccuracy elem in benchmark.collection_acc)
                {
                    writer.Write($"{elem.grid.function} {elem.grid.length} {elem.grid.limits[0]} {elem.grid.limits[1]}");
                    writer.WriteLine($" {elem.difference} {elem.x_VML_HA_VML_EP[0]} {elem.x_VML_HA_VML_EP[1]} {elem.x_VML_HA_VML_EP[2]}");
                }

                writer.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (fs != null) fs.Close();
            }
            have_changes = false;
        }
        public void Load(string filename)
        {
            FileStream fs = null;

            try
            {
                fs = new FileStream(filename, FileMode.Open);
                StreamReader reader = new StreamReader(fs);

                benchmark.collection_time.Clear();
                benchmark.collection_acc.Clear();

                string[] array = reader.ReadLine().Split(' ');

                benchmark.min_max_diff[0] = double.Parse(array[0]);
                benchmark.min_max_diff[1] = double.Parse(array[1]);
                benchmark.OnPropertyChanged("min_max_diff");

                string str = reader.ReadLine();

                while (str != "END_COLLECTION_TIME")
                {
                    array = str.Split(' ');
                    VMTime vmtime = new VMTime();
                    VMGrid grid = new VMGrid();
                    grid.limits = new double[2];

                    grid.function = (VMf)Enum.Parse(typeof(VMf), array[0]);
                    grid.length = int.Parse(array[1]);
                    grid.limits[0] = double.Parse(array[2]);
                    grid.limits[1] = double.Parse(array[3]);

                    vmtime.grid = grid;
                    vmtime.time_VML_EP = double.Parse(array[4]);
                    vmtime.time_VML_HA = double.Parse(array[5]);
                    vmtime.coef_boost = double.Parse(array[6]);

                    benchmark.collection_time.Add(vmtime);

                    str = reader.ReadLine();
                }

                str = reader.ReadLine();

                while (str != null)
                {
                    array = str.Split(' ');
                    VMAccuracy vmaccuracy = new VMAccuracy();
                    VMGrid grid = new VMGrid();
                    grid.limits = new double[2];


                    grid.function = (VMf)Enum.Parse(typeof(VMf), array[0]);
                    grid.length = int.Parse(array[1]);
                    grid.limits[0] = double.Parse(array[2]);
                    grid.limits[1] = double.Parse(array[3]);

                    vmaccuracy.grid = grid;
                    vmaccuracy.difference = double.Parse(array[4]);

                    vmaccuracy.x_VML_HA_VML_EP = new double[3];
                    vmaccuracy.x_VML_HA_VML_EP[0] = double.Parse(array[5]);
                    vmaccuracy.x_VML_HA_VML_EP[1] = double.Parse(array[6]);
                    vmaccuracy.x_VML_HA_VML_EP[2] = double.Parse(array[7]);

                    benchmark.collection_acc.Add(vmaccuracy);

                    str = reader.ReadLine();
                }
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }
        public void Clear()
        {
            benchmark.collection_time.Clear();
            benchmark.collection_acc.Clear();
            benchmark.min_max_diff[0] = 0.0;
            benchmark.min_max_diff[1] = 0.0;
            have_changes = false;
            grid.function = VMf.vmsLn;
            grid.length = 0;
            grid.limits[0] = 0.0;
            grid.limits[1] = 0.0;
            benchmark.OnPropertyChanged("min_max_diff");
        }
    }
}
