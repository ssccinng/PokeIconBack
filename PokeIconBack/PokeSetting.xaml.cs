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
using System.Windows.Shapes;

namespace PokeIconBack
{
    /// <summary>
    /// PokeSetting.xaml 的交互逻辑
    /// </summary>
    public partial class PokeSetting : Window
    {
        public string Title1 { get; set; }

        public PokeSetting(string title)
        {
            DataContext = this;
            Title1 = title;
            InitializeComponent();
            
        }
        public int Id { get; set; }

        private void Item_TextChanged(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {
            var res = PokeTeamImageTran.TranslateHelper.PokeModels.Select(s => s.Name_Chs).Where(s => s.Contains(Item.Text));
            if (res.Count() == 0 )
            {
                res = PokeTeamImageTran.TranslateHelper.PokeModels.Select(s => s.Name_Eng).Where(s => s.Contains(Item.Text, StringComparison.CurrentCultureIgnoreCase));
                
            }
            else
            {

            }

            Item.ItemsSource = res;
            Id = PokeTeamImageTran.TranslateHelper.PokeModels.FindIndex(s => s.Name_Chs == Item.Text || s.Name_Eng == Item.Text) + 1;
            aa.Text = Id.ToString();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Id = PokeTeamImageTran.TranslateHelper.PokeModels.FindIndex(s => s.Name_Chs == Item.Text || s.Name_Eng == Item.Text);
            if (Id >= 0 && Id <= 1008)
            {
                DialogResult = true;
                Close();
            }
            else
            {
                Id= 0;
                Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title = Title1?? string.Empty;


            foreach (var item in MainViewModel.History)
            {
                BitmapImage myBitmapImage = new BitmapImage();

                // BitmapImage.UriSource must be in a BeginInit/EndInit block
                myBitmapImage.BeginInit();
                myBitmapImage.UriSource = new Uri(item.path);

                // To save significant application memory, set the DecodePixelWidth or
                // DecodePixelHeight of the BitmapImage value of the image source to the desired
                // height or width of the rendered image. If you don't do this, the application will
                // cache the image as though it were rendered as its normal size rather than just
                // the size that is displayed.
                // Note: In order to preserve aspect ratio, set DecodePixelWidth
                // or DecodePixelHeight but not both.
                myBitmapImage.DecodePixelWidth = 200;
                myBitmapImage.EndInit();
                var img = new Image()
                {
                    Width = 100,
                    Height = 100,
                    Source = myBitmapImage,
                };
                img.MouseDown += (s, e) => { 
                    //Id = item.id;
                    aa.Text = item.id.ToString();
                };
                DaniList.Children.Add(img);
            }
        }

        private void aa_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(aa.Text, out int id))
            {
                Id = id;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (Id >= 0 && Id <= 1008)
                {
                    DialogResult = true;
                    Close();
                }
                else {
                    MessageBox.Show("id不合理desu");
                }

            }
            else if (e.Key == Key.Escape)
            {
                Id = 0;
                Close();
            }
        }
    }
}
