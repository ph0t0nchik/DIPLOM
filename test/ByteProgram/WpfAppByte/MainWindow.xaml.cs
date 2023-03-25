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

namespace WpfAppByte
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel vm = new ViewModel();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = vm;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            vm.Items.Add(new ListItem() { A = new Random().Next(), S = DateTime.Now.ToString() });
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            vm.Items.Clear();
        }

        private void listP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show($"selected  {listP.SelectedItem}");
        }
    }
}
