using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeIconBack
{
    public class PokeSettingViewModel: ObservableObject
    {
        public ObservableCollection<string> History { get; set; } = new ObservableCollection<string>();




    }
}
