﻿using System;
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

namespace WpfAppV1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 

    public class ViewModel
    {
        public int Width { get; set; }
        public string Text { get; set; }
    }
    public partial class MainWindow : Window
    {
        public int A { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new ViewModel();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("SSSSSS");
        }
    }
}
