using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PokeIconBack
{
    //[ObservableObject]
    //public partial record Data1;
    //[ObservableObject]

    public partial class PokeShowModel : ObservableObject
    {
        [ObservableProperty]

        private int _id;

        [ObservableProperty]
        private int _form = 0;

        [ObservableProperty]
        private string _image = string.Empty;

        [ObservableProperty]
        [NotifyPropertyChangedFor("ItemPath")]
        private int _item = 151;
        [JsonIgnore]
        public string ItemPath => $"{Environment.CurrentDirectory}/HomeIconImage/Item/item{Item:0000}.png";

        [ObservableProperty]
        [NotifyPropertyChangedFor("TeraPath")]
        private string _tera = "fire";
        [JsonIgnore]

        public string TeraPath => $"{Environment.CurrentDirectory}/HomeIconImage/Terastal/icon_terastal_type_{Tera}.png";

    }
}
