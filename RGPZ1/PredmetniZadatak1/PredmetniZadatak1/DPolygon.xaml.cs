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

namespace PredmetniZadatak1
{
    /// <summary>
    /// Interaction logic for DPolygon.xaml
    /// </summary>
    public partial class DPolygon : Window
    {
        private Polygon tempPolygon;

        private PointCollection points;

        public Polygon TempPolygon { get => tempPolygon; set => tempPolygon = value; }

        public DPolygon()
        {
            InitializeComponent();
            cmbColor.ItemsSource = typeof(Brushes).GetProperties();
            cmbBorder.ItemsSource = typeof(Brushes).GetProperties();
        }

        public DPolygon(PointCollection points) : this()
        {
            this.points = points;
        }

        public DPolygon(Polygon poly) : this()
        {
            TempPolygon = poly;
            cmbColor.SelectedItem = poly.Fill;
            cmbBorder.SelectedItem = poly.Stroke;
            textThick.Text = poly.StrokeThickness.ToString();
        }


        private bool validate()
        {
            bool result = true;

            if (cmbColor.SelectedItem == null)
            {
                result = false;
                labelGreskaC.Content = "Morate odabrati opciju!";
                cmbColor.BorderThickness = new Thickness(3);
                cmbColor.BorderBrush = Brushes.Yellow;
            }
            else
            {
                labelGreskaC.Content = "";
                cmbColor.BorderBrush = Brushes.Gray;
            }


            if (cmbBorder.SelectedItem == null)
            {
                result = false;
                labelGreskaB.Content = "Morate odabrati opciju!";
                cmbBorder.BorderThickness = new Thickness(3);
                cmbBorder.BorderBrush = Brushes.Yellow;
            }
            else
            {
                labelGreskaB.Content = "";
                cmbBorder.BorderBrush = Brushes.Gray;
            }



            if (textThick.Text.Trim().Equals(""))
            {
                result = false;
                labelGreskaT.Content = "Ostavili ste prazno polje!";
                textThick.BorderBrush = Brushes.Yellow;
            }
            else
            {
                try
                {
                    if (Int32.Parse(textThick.Text) > 0)
                    {
                        labelGreskaT.Content = "";
                        textThick.BorderBrush = Brushes.Gray;
                    }
                    else
                    {
                        result = false;
                        labelGreskaT.Content = "Unesite pozitivan broj!";
                        textThick.BorderBrush = Brushes.Yellow;
                    }
                }
                catch (Exception ex)
                {
                    result = false;
                    labelGreskaT.Content = "Polje mora biti ceo broj!";
                    textThick.BorderBrush = Brushes.Yellow;

                }

            }


            return result;
        }



        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            if (validate())
            {

                TempPolygon = new Polygon()
                {
                    Fill = (SolidColorBrush)new BrushConverter().ConvertFromString(cmbColor.SelectedItem.ToString().Split(' ')[1]),
                    Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString(cmbBorder.SelectedItem.ToString().Split(' ')[1]),
                    StrokeThickness = Int32.Parse(textThick.Text),
                    Points=points
                };

                this.Close();
            }

        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
