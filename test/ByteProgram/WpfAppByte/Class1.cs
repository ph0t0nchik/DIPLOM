using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppByte
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class ViewModel : BaseViewModel
    {
        private string text;

        public ObservableCollection<ListItem> Items { get; set; } = new ObservableCollection<ListItem>(new List<ListItem>() 
        { 
            new ListItem() {A = 2},
            new ListItem() {A = 20, S = "hui"},
        });


       

        public string Text
        {
            get => text;
            set
            {
                text = value;
                OnPropertyChanged();
            }
        }
    }

    public class ListItem
    {
        public int A { get; set; }
        public int B { get; set; }
        public string S { get; set; }

        public override string ToString()
        {
            return $"{A}, {B}, {S}";
        }
    }
}
