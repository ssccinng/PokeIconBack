using CommunityToolkit.Mvvm.ComponentModel;
using ModernWpf.Controls;
using PokeCommon.Models;
using PokeCommon.PokemonShowdownTools;
using PokeCommon.Utils;
using PokemonDataAccess.Models;
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
    [ObservableObject]
    public partial class PokeSetting : Window
    {
        public string Title1 { get; set; }

        [ObservableProperty]
         int _formIdx  = 0;

        public PokeSetting(string title, PokeShowModel pokeShowModel )
        {
            PokeShowModel = pokeShowModel;
            DataContext = this;
            Title1 = title;

            
            InitializeComponent();

            //Item.Text = PokemonDBInMemory.
        }
        public int Id { get => PokeShowModel.Id; set => PokeShowModel.Id = value; }

        public PokeShowModel PokeShowModel { get; set; } = new PokeShowModel();

        public PokeType[] Types { get; set; } = PokemonDBInMemory.Types.Prepend(new PokeType { Id = -1, Name_Chs = "(无)", Name_Eng = "" }).ToArray();

        public PokeModel[] Items { get; set; } = PokeHomeData.ItemTable.Values.ToArray();
        public PokeModel[] ItemsS { get; set; } = PokeHomeData.ItemTable.Values.ToArray();
        [ObservableProperty]

        PokeType? _selectType;
        public PokeModel? SelectItem { get; set; }

        private void Item_TextChanged(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {
            var res = PokeTeamImageTran.TranslateHelper.PokeModels.Select(s => s.Name_Chs).Where(s => s.Contains(Item.Text));
            if (res.Count() == 0 )
            {
                res = PokeTeamImageTran.TranslateHelper.PokeModels.Select(s => s.Name_Eng).Where(s => s.Contains(Item.Text, StringComparison.CurrentCultureIgnoreCase));
                //AutoSuggestBox
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
            if (Id >= 0 && Id <= 1025)
            {
                DialogResult = true;
                PokeShowModel.Tera = SelectType?.Name_Eng?.ToLower();
                PokeShowModel.Item = SelectItem?.Id ?? 0;

                PokeShowModel.Form = FormIdx;



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

            SelectType = Types.FirstOrDefault(s => s.Name_Eng.ToLower() == PokeShowModel.Tera);
            SelectItem = Items.FirstOrDefault(s => s.Id == PokeShowModel.Item);

            ItemI.Text = SelectItem?.Name_Chs;

            aa.Text = Id.ToString();
            FormIdx = PokeShowModel.Form;
            Item.Text = PokemonDBInMemory.Pokemons.Where(s => s.DexId == Id && s.PokeFormId == FormIdx).FirstOrDefault()?.NameChs;





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
                    Width = 50,
                    Height = 50,
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
                if (Id >= 0 && Id <= 1017)
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



        private async void Importps_Click(object sender, RoutedEventArgs e)
        {

            var win = new InputPsText();
            if (win.ShowDialog() == true)
            {
                var poke = await PSConverterWithoutDB.ConvertToPokemonAsync(win.PsText);

                if (poke != null)
                {
                    Id = poke.MetaPokemon.DexId;
                    FormIdx = poke.MetaPokemon.PokeFormId;
                    Item.Text = poke.MetaPokemon.NameChs;
                    aa.Text = Id.ToString();

                    ItemI.Text = poke.Item?.Name_Chs;

                    SelectType = poke.TreaType;

                }
            }

        }

        private void ItemI_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            var res = ItemsS.Where(s => s.Name_Chs.Contains(ItemI.Text));
            if (res.Count() == 0)
            {
           
                //AutoSuggestBox
            }
            else
            {

            }

            ItemI.ItemsSource = res;

            SelectItem = res.FirstOrDefault();

            //Id = PokeTeamImageTran.TranslateHelper.PokeModels.FindIndex(s => s.Name_Chs == Item.Text || s.Name_Eng == Item.Text) + 1;
            //aa.Text = Id.ToString();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Id = 0;
            FormIdx = 0;
            Item.Text = string.Empty;
            aa.Text = string.Empty;

            ItemI.Text = string.Empty;

            SelectType = null;
        }
    }
}
