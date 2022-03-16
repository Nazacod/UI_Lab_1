using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Globalization;
using System.ComponentModel;

namespace UI_Lab_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            viewdata = new ViewData();

            //this.DataContext = viewdata;
            this.GridParam.DataContext = viewdata.grid;

            //viewdata.benchmark.AddVMTime(viewdata.grid);
            this.timesList.ItemsSource = viewdata.benchmark.collection_time;
            this.accurList.ItemsSource = viewdata.benchmark.collection_acc;

            this.minmaxBlock.DataContext = viewdata.benchmark;
        }
        public ViewData viewdata { get; set; }
        private void addVMTimeSelected(object sender, RoutedEventArgs e)
        {
            try
            {
                viewdata.AddVMTime(viewdata.grid);
                timeLastChanges.Text = DateTime.Now.ToString() + "   VMTime";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void addVMAccuracySelected(object sender, RoutedEventArgs e)
        {
            try
            {
                viewdata.AddVMAccuracy(viewdata.grid);
                timeLastChanges.Text = DateTime.Now.ToString() + "   VMAccuracy";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void menuItemNew_Selected(object sender, RoutedEventArgs e)
        {
            //TODO
            if (viewdata.have_changes)
            {
                MessageBoxResult dialogResult = MessageBox.Show("Your data is not saved. Do you want to save them?", "Warning", MessageBoxButton.YesNo);

                if (dialogResult == MessageBoxResult.Yes)
                {
                    Microsoft.Win32.SaveFileDialog dlg_save = new Microsoft.Win32.SaveFileDialog();
                    if ((bool)dlg_save.ShowDialog())
                    {
                        viewdata.Save(dlg_save.FileName);
                        viewdata.have_changes = false;
                    }
                }
            }

            viewdata.Clear();
            timeLastChanges.Text = DateTime.Now.ToString() + "   Data cleared";

            //this.GridParam.DataContext = viewdata.grid;

            //this.timesList.ItemsSource = viewdata.benchmark.collection_time;

            //this.minmaxBlock.DataContext = viewdata.benchmark;
        }
        private void menuItemOpen_Selected(object sender, RoutedEventArgs e)
        {
            //TODO
            if (viewdata.have_changes)
            {
                MessageBoxResult dialogResult = MessageBox.Show("Your data is not saved. Do you want to save them?", "Warning", MessageBoxButton.YesNo);

                if (dialogResult == MessageBoxResult.Yes)
                {
                    Microsoft.Win32.SaveFileDialog dlg_save = new Microsoft.Win32.SaveFileDialog();
                    if ((bool)dlg_save.ShowDialog())
                    {
                        viewdata.Save(dlg_save.FileName);
                        viewdata.have_changes = false;
                    }
                }
            }

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            if ((bool)dlg.ShowDialog())
            {
                viewdata.Load(dlg.FileName);
                timeLastChanges.Text = DateTime.Now.ToString() + "   Data loaded";
            }
        }
        private void menuItemSave_Selected(object sender, RoutedEventArgs e)
        {
            //TODO
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            if ((bool)dlg.ShowDialog())
            {
                viewdata.Save(dlg.FileName);
                viewdata.have_changes = false;
            }
        }
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (viewdata.have_changes)
            {
                MessageBoxResult dialogResult = MessageBox.Show("Your data is not saved. Do you want to save them?", "Warning", MessageBoxButton.YesNo);

                if (dialogResult == MessageBoxResult.Yes)
                {
                    Microsoft.Win32.SaveFileDialog dlg_save = new Microsoft.Win32.SaveFileDialog();
                    if ((bool)dlg_save.ShowDialog())
                    {
                        viewdata.Save(dlg_save.FileName);
                        viewdata.have_changes = false;
                    }
                }
            }
        }
    }
    [ValueConversion(typeof(Double []), typeof(String))]
    public class StringToDoubleSConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string[] words = ((string)value).Split(';');
                if (words.Length != 2)
                    return new double[2] { 0.0, 0.0 };
                double[] values = new double[2];
                values[0] = double.Parse(words[0]);
                values[1] = double.Parse(words[1]);
                return values;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return DependencyProperty.UnsetValue;
            }
        }
    }
    [ValueConversion(typeof(Double[]), typeof(String))]
    public class DoubleArrToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value != null)
                {
                    double[] tmp = (double[])value;
                    return $"Min: {tmp[0]} Max: {tmp[1]}";
                }
                return DependencyProperty.UnsetValue;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return DependencyProperty.UnsetValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
