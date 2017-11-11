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
        List<Fuel> lFuels;
        List<Coolant> lCoolants;
        ModelNuclearReactor modelReactor;

        private class Fuel
        {
            private string name;
            private float C, P, V, a, F;

            public Fuel(string name, float C, float P, float V, float a, float F)
            {
                this.name = name;
                this.C = C;
                this.P = P;
                this.V = V;
                this.a = a;
                this.F = F;
            }

            public string getName() { return this.name; }

            public float getP() { return this.P; }
            public float getC() { return this.C; }
            public float getV() { return this.V; }
            public float getA() { return this.a; }
            public float getF() { return this.F; }
        }

        private class Coolant
        {
            private string name;
            private float C, P, V;

            public Coolant(string name, float C, float P, float V)
            {
                this.name = name;
                this.P = P;
                this.C = C;
                this.V = V;
            }

            public string getName() { return this.name; }

            public float getP() { return this.P; }
            public float getC() { return this.C; }
            public float getV() { return this.V; }
        }

        public MainWindow()
        {
            InitializeComponent();

            lFuels = new List<Fuel>();
            lFuels.Add(new Fuel("Оксид урана", 318, 10960, 7.026f, 1240, 4850));
            lFuels.Add(new Fuel("Металлический уран", 0, 0, 0, 0, 0));
            lFuels.Add(new Fuel("Торий", 0, 0, 0, 0, 0));

            lCoolants = new List<Coolant>();
            lCoolants.Add(new Coolant("Вода", 5670, 620, 180));
            lCoolants.Add(new Coolant("Тяжёлая вода", 0, 0, 0));
            lCoolants.Add(new Coolant("Свинец", 0, 0, 0));
            lCoolants.Add(new Coolant("Натрий", 0, 0, 0));

            foreach (Fuel itemFuels in lFuels)
            {
                cmbFuel.Items.Add(itemFuels.getName());
            }

            foreach (Coolant itemCoolants in lCoolants)
            {
                cmbCoolant.Items.Add(itemCoolants.getName());
            }
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
            try
            {
                float startPower = float.Parse(txtStartPower.Text);
                float coeffA = float.Parse(txtCoeffA.Text.Replace('.',','));
                float coeffB = float.Parse(txtCoeffB.Text.Replace('.', ','));
                float coeffC = float.Parse(txtCoeffC.Text.Replace('.', ','));

                float fuelC = float.Parse(txtFuelC.Text.Replace('.', ','));
                float fuelP = float.Parse(txtFuelP.Text.Replace('.', ','));
                float fuelV = float.Parse(txtFuelV.Text.Replace('.', ','));

                float coolantС = float.Parse(txtCoolantС.Text.Replace('.', ','));
                float coolantP = float.Parse(txtCoolantP.Text.Replace('.', ','));
                float coolantV = float.Parse(txtCoolantV.Text.Replace('.', ','));
                float coolantA = float.Parse(txtCoolantA.Text.Replace('.', ','));
                float coolantF = float.Parse(txtCoolantF.Text.Replace('.', ','));
                float coolantT = float.Parse(txtCoolantT.Text.Replace('.', ','));

                this.modelReactor = new ModelNuclearReactor(fuelC, fuelP, fuelV, coolantС, coolantP, coolantV, coolantA, coolantF, coolantT);

            } catch (System.FormatException exc)
            {
                MessageBox.Show("Введены не все параметры или введены неверно!", "Ошибка");
                Console.WriteLine(exc.ToString() + " : Не все поля заполнены или заполены неверно.");
            }

        }

        private void Change_Coolant(object sender, SelectionChangedEventArgs e)
        {
            foreach(Coolant itemCoolants in lCoolants)
            {
                if ((string)cmbCoolant.SelectedValue == itemCoolants.getName())
                {
                    txtCoolantV.Text = itemCoolants.getV().ToString();
                    txtCoolantP.Text = itemCoolants.getP().ToString();
                    txtCoolantС.Text = itemCoolants.getC().ToString();
                    break;
                }
            }
        }

        private void Change_Fuel(object sender, SelectionChangedEventArgs e)
        {

            foreach (Fuel itemFuels in lFuels)
            {
                if ((string)cmbFuel.SelectedValue == itemFuels.getName())
                {
                    txtFuelC.Text = itemFuels.getC().ToString();
                    txtFuelP.Text = itemFuels.getP().ToString();
                    txtFuelV.Text = itemFuels.getV().ToString();
                    txtCoolantA.Text = itemFuels.getA().ToString();
                    txtCoolantF.Text = itemFuels.getF().ToString();
                    break;
                }
            }
        }

        private void TxtCoefficient_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char ch in e.Text)
            {
                if (!char.IsDigit(ch) && ch != '.')
                    e.Handled = true;
            }
        }

        ModelNuclearReactor getModel()
        {
            return modelReactor;
        }
    }
}
