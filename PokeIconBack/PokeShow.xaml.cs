using System;
using System.Collections.Generic;
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
    /// PokeShow.xaml 的交互逻辑
    /// </summary>
    public partial class PokeShow : UserControl
    {
        public MainViewModel ViewModel { get; set; } = new MainViewModel();
        public string[] Files = Directory.GetFiles("img_pokei128");

        public PokeShow()
        {
            DataContext = this;
            InitializeComponent();
        }

        private void NewMethod(int id)
        {
            var poke1 = new PokeSetting($"宝可梦{id}设定");
            poke1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            if (poke1.ShowDialog() == true)
            {
                var dd = $"icon{(poke1.Id):0000}";
                string v = Environment.CurrentDirectory + "/" + (Files.FirstOrDefault(s => s.StartsWith($"img_pokei128\\{dd}")) ?? "img_pokei128/icon0000_f00_s0.png");
                MainViewModel.History.Add((poke1.Id, v));
                ViewModel.Images[id - 1] = v;
            }
        }

        private void Poke1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                NewMethod(int.Parse((sender as AutoGrayImage).Tag.ToString()));

            }
            else
            {
                //ViewModel.Grays[1] = false;
                //ViewModel.Images[1] = "111.png";
                var aa = ViewModel.Grays[int.Parse((sender as AutoGrayImage).Tag.ToString()) - 1];
                ViewModel.Grays[int.Parse((sender as AutoGrayImage).Tag.ToString()) - 1] = !aa;

            }
        }

        private void heng_Checked(object sender, RoutedEventArgs e)
        {
            panel1.Visibility = Visibility.Visible;
        }

        private void heng_Unchecked(object sender, RoutedEventArgs e)
        {
            panel1.Visibility = Visibility.Collapsed;

        }

        private void shu_Checked(object sender, RoutedEventArgs e)
        {
            panel2.Visibility = Visibility.Visible;
        }

        private void shu_Unchecked(object sender, RoutedEventArgs e)
        {
            panel2.Visibility = Visibility.Collapsed;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            heng.IsChecked = shu.IsChecked = true;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            heng.IsChecked = shu.IsChecked = true;

        }
    }


}
