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

namespace NuclearProject
{
    /// <summary>
    /// Логика взаимодействия для TestsWindow.xaml
    /// </summary>
    public partial class TestsWindow : Window
    {
        List<string> lThemes;

        public TestsWindow()
        {
            InitializeComponent();

            lThemes = new List<string>();
            lThemes.Add("Коэффициент размножения");
            lThemes.Add("Критичность");
            lThemes.Add("Состав ЯР");
            lThemes.Add("Деление");

            foreach(string itemThemes in lThemes)
            {
                cmbThemes.Items.Add(itemThemes);
            }
        }

        private void cmbThemes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
