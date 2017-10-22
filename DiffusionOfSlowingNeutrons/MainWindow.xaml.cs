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
using System.Windows.Media.Media3D;
using System.Collections;
using OxyPlot;
using HelixToolkit;
using HelixToolkit.Wpf;
using OxyPlot.Wpf;
using System.IO;

namespace NuclearProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ModellingSession session;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_Ref(object sender, RoutedEventArgs e) //кнопка "Справка"
        {
            StartModelWindow win = new StartModelWindow(); //вызываем окно справки
            win.ShowDialog();
        }

        private void Button_Click_Tests(object sender, RoutedEventArgs e)
        {
            TestsWindow win = new TestsWindow();
            win.ShowDialog();
        }

        private void Button_Click_Start(object sender, RoutedEventArgs e)
        {

        }

        private void txtCoefficient_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach(char ch in e.Text)
            {
                if (!char.IsDigit(ch))
                    e.Handled = true;
            }
        }
    }
}
