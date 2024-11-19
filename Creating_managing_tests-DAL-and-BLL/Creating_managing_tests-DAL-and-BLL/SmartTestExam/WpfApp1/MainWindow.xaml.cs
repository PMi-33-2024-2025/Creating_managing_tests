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

using System.Windows;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public string UserInput { get; set; }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Відкрити файл");
        }

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Зберегти файл");
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Option1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Опція 1 обрана");
        }

        private void Option2_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Опція 2 обрана");
        }
    }
}

