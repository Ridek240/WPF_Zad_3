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

namespace WPF_Zad_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool lastElementloaded = false;
        int Naklad = 0;
        string Format = "A0";
        public MainWindow()
        {
            InitializeComponent();
            lastElementloaded = true;
        }

        private void Check_correct_value(object sender, TextChangedEventArgs e)
        {
            
            int.TryParse(Cost.Text,out Naklad);
            Sumary();
        }

        private void Format_Changed(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if((int)Format_in.Value==0)
            {
                Formatout.Content = "A0 - cena 19zł 53gr/szt.";
                Format = "A0";
            }
            if ((int)Format_in.Value == 1)
            {
                Formatout.Content = "A1 - cena 7zł 81gr/szt.";
                Format = "A1";
            }
            if ((int)Format_in.Value == 2)
            {
                Formatout.Content = "A2 - cena 3zł 12gr/szt.";
                Format = "A2";
            }
            if ((int)Format_in.Value == 3)
            {
                Formatout.Content = "A3 - cena 1zł 25gr/szt.";
                Format = "A3";
            }
            if ((int)Format_in.Value == 4)
            {
                Formatout.Content = "A4 - 50 cena gr/szt.";
                Format = "A4";
            }
            if ((int)Format_in.Value == 5)
            {
                Formatout.Content = "A5 - cena 20 gr/szt.";
                Format = "A5";
            }
            //lastElementloaded = true;
            Sumary();
        }

        private void Is_color(object sender, RoutedEventArgs e)
        {
            Colors.IsEnabled = ColorCb.IsChecked.Value;
            Sumary();
        }

        private void ColorChosen(object sender, SelectionChangedEventArgs e)
        {
            Sumary();
        }

        private void Sumary()
        {
            double modifier = 1;
            if (lastElementloaded)
            {
                string Sumary_Exit = null;
                Sumary_Exit += "Przedmiot zamówienia: " + Naklad + " szt.";
                Sumary_Exit += ", format " + Format;
                if (ColorCb.IsChecked.Value == true)
                {
                    Sumary_Exit += ", kolor papieru: " + Colors.SelectedValue;
                }
                
                if(Grad1.IsChecked.Value)
                {
                    Sumary_Exit += ", 80g/m^2";
                    modifier = 1;
                }
                if (Grad2.IsChecked.Value)
                {
                    Sumary_Exit += ", 120g/m^2";
                    modifier = 2.0;
                }
                if (Grad3.IsChecked.Value)
                {
                    Sumary_Exit += ", 200g/m^2";
                    modifier = 2.5;
                }
                if (Grad4.IsChecked.Value)
                {
                    Sumary_Exit += ", 240g/m^2";
                    modifier = 3.0;
                }
                if(Opt1.IsChecked.Value)
                {
                    Sumary_Exit += ", druk kolor";
                }
                if (Opt2.IsChecked.Value)
                {
                    Sumary_Exit += ", druk dwustronny";
                }
                if (Opt3.IsChecked.Value)
                {
                    Sumary_Exit += ", Lakier UV";
                }
                if(Real.IsChecked.Value)
                {
                    Sumary_Exit += ", ekspresowo";
                }
                float rozliczenie = 0.20f * (2.5f * (float)(5 - Format_in.Value));
                if (rozliczenie == 0) rozliczenie = 0.2f;
                rozliczenie = Rozliczenie(5-(int)Format_in.Value);
                float cena = 0;
                cena = (float)Naklad * rozliczenie * (float)modifier * (ColorCb.IsChecked.Value ? 1.5f : 1f) * (Opt1.IsChecked.Value ? 1.3f : 1f) * 
                   (Opt2.IsChecked.Value ? 1.5f : 1f) * (Opt3.IsChecked.Value ? 1.2f : 1f) + (Real.IsChecked.Value ? 15f : 0f);

                string fmt = "0.00";
                Sumary_Exit += "\nCena przed rabatem: " + cena.ToString(fmt) + "zł";
                int rabat;
                rabat = Naklad / 100;
                if (rabat > 10) rabat = 10;
                Sumary_Exit += "\nNaliczony rabat: " + rabat+"%";
                Sumary_Exit += "\nCena po rabacie: " + (cena * ((float)(100 - rabat) / 100)).ToString(fmt) + "zł";
                Exitbox.Text = Sumary_Exit;
            }
        }
        private float Rozliczenie(int n)
        {
            if (n == 0)
            {
                return 0.20f;
            }
            else return Rozliczenie(n - 1) * 2.5f;
        }

        private void PointersChanger(object sender, RoutedEventArgs e)
        {
            Sumary();
        }

        private void Ok_Button(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Zamówienie zostało przyjęte");
            Cost.Text = "";
            Format_in.Value = 4;
            ColorCb.IsChecked = false;
            Grad2.IsChecked = true;
            Opt1.IsChecked = false;
            Opt2.IsChecked = false;
            Opt3.IsChecked = false;
            real1.IsChecked = true;
            Colors.Text = "Żółty";

        }
        private void Cancel_button(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
