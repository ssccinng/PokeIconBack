using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Globalization;
using System.IO;
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

namespace PokeIconBack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewModel ViewModel { get; set; }  = new MainViewModel();
        public string[] Files = Directory.GetFiles("img_pokei128");

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            return;
            switch (e.Key)
            {
                case Key.D1:
                case Key.NumPad1:
                    NewMethod(1);
                    ;

                    break;
                case Key.D2:
                case Key.NumPad2:
                    NewMethod(2);

                    break;
                case Key.D3:
                case Key.NumPad3:
                    NewMethod(3);

                    break;
                case Key.D4:
                case Key.NumPad4:
                    NewMethod(4);

                    break;
                case Key.D5: 
                case Key.NumPad5:
                    NewMethod(5);
                    break;

                case Key.D6: 
                case Key.NumPad6:
                    NewMethod(6);
                    break;
                default: break;
            }
        }

        private void NewMethod(int id)
        {
            //var poke1 = new PokeSetting($"宝可梦{id}设定");
            //poke1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //if (poke1.ShowDialog() == true)
            //{
            //    var dd = $"icon{(poke1.Id):0000}";
            //    ViewModel.Pokes[id - 1].Image = Environment.CurrentDirectory + "/" + (Files.FirstOrDefault(s => s.StartsWith($"img_pokei128\\{dd}")) ?? "img_pokei128/icon0000_f00_s0.png");
            //}
        }

        private void Poke1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                NewMethod(int.Parse((sender as Image).Tag.ToString()));

            }
            else
            {
                //ViewModel.Grays[1] = false;
                //ViewModel.Images[1] = "111.png";
                var aa = ViewModel.Grays[int.Parse((sender as Image).Tag.ToString()) - 1];
                ViewModel.Grays[int.Parse((sender as Image).Tag.ToString()) - 1] = !aa;

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }

    public class WidthToMarginConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Double width = (Double)value;
            return new Thickness(width, 0, 0, 0);
        }

        //没有用到
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

    public class HeightToMarginConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Double width = (Double)value;
            return new Thickness(0, width, 0, 0);
        }

        //没有用到
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }


    public class MultiplierConverter : IValueConverter
    {
        public double Multiplier { get; set; } = 0.5; // 默认倍数

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double width)
            {
                return width * Multiplier;
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}