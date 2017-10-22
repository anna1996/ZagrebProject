using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace NuclearProject
{
    /// <summary>
    /// Interaction logic for StartModelWindow.xaml
    /// </summary>
    public partial class StartModelWindow : Window
    {
        List<EnvironmentPreset> environments;
        Vector3D position;
        double energy;
        int count;
        EnvironmentPreset env;

        public StartModelWindow()
        {
            InitializeComponent();
            System.Uri pdf = new System.Uri(String.Format("file:///{0}/REFERENCE.pdf", Directory.GetCurrentDirectory()));
            webHelp.Navigate(pdf);
        }
    }
}
