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
            Title1 = title;
            InitializeComponent();
            
        }
        public int Id { get; set; }

        private void Item_TextChanged(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {
            var res = PokeTeamImageTran.TranslateHelper.PokeModels.Select(s => s.Name_Chs).Where(s => s.StartsWith(Item.Text));
            if (res.Count() == 0 )
            {
                res = PokeTeamImageTran.TranslateHelper.PokeModels.Select(s => s.Name_Eng).Where(s => s.StartsWith(Item.Text, StringComparison.CurrentCultureIgnoreCase));
                
            }
            else
            {

            }

            Item.ItemsSource = res;
            Id = PokeTeamImageTran.TranslateHelper.PokeModels.FindIndex(s => s.Name_Chs == Item.Text || s.Name_Eng == Item.Text);
            aa.Text = Id.ToString();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Id = PokeTeamImageTran.TranslateHelper.PokeModels.FindIndex(s => s.Name_Chs == Item.Text || s.Name_Eng == Item.Text);
            if (Id > 0 && Id <= 1008)
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
        }

        private void aa_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(aa.Text, out int id))
            {
                Id = id;
            }
        }
    }
}
