using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
                var dd = $"icon{(poke1.Id):0000}_f{poke1.FormIdx:00}";
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

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            heng.IsChecked = shu.IsChecked = true;

            for (int i = 0; i < 16; i++)
            {
                int ii = i;
                Button button = new Button
                {
                    Content = ii,
                    Width = 40
                };
                button.Click += async (s, e) =>
                {
                    await File.WriteAllTextAsync($"quicksave_{ii}.json", JsonSerializer.Serialize(ViewModel.Images));

                };

                QuickSave.Children.Add(button);

                Button button1 = new Button
                {
                    Content = ii,
                    Width = 40
                };
                button1.Click += async (s, e) =>
                {
                    try
                    {
                        var data = await File.ReadAllTextAsync($"quicksave_{ii}.json");
                        var imgdata = JsonSerializer.Deserialize<string[]>(data);
                        for (global::System.Int32 j = 0; j < imgdata.Length; j++)
                        {
                            ViewModel.Images[j] = imgdata[j];
                        }
                    }
                    catch (Exception)
                    {

                        //throw;
                    }
                    
                };

                QuickLoad.Children.Add(button1);

            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 6; i++)
            {
                ViewModel.Images[i] = Environment.CurrentDirectory + "/" + "img_pokei128/icon0000_f00_s0.png";
                ViewModel.Grays[i] = false;

            }
        }

        private async void Load_Click(object sender, RoutedEventArgs e)
        {
           OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Team|*.json";
            dialog.Title = "Open an Team File";
            var res = dialog.ShowDialog();
            if (res ?? false)
            {
                if (dialog.FileName != "")
                {
                    try
                    {
                        var data = await File.ReadAllTextAsync(dialog.FileName);
                        var imgdata = JsonSerializer.Deserialize<string[]>(data);
                        for (global::System.Int32 i = 0; i < imgdata.Length; i++)
                        {
                            ViewModel.Images[i] = imgdata[i];    
                        }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }

                }

            }

        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Team|*.json";
            saveFileDialog1.Title = "Save an Team File";
            var res = saveFileDialog1.ShowDialog();

            if (res ?? false)
            {
                if (saveFileDialog1.FileName != "")
                {
                    await File.WriteAllTextAsync(saveFileDialog1.FileName, JsonSerializer.Serialize(ViewModel.Images));
                    
                }

            }

        }
    }


}

