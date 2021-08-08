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
    /// Interaction logic for DrawRectangle.xaml
    /// </summary>
    public partial class DrawRectangle : Window
    {
        private Rectangle tempRectangle;

        public Rectangle TempRectangle { get => tempRectangle; set => tempRectangle = value; }

        public DrawRectangle()
        {
            InitializeComponent();
            cmbColor.ItemsSource = typeof(Brushes).GetProperties();
            cmbBorder.ItemsSource = typeof(Brushes).GetProperties();

        }

        public DrawRectangle(Rectangle rec) : this()
        {
            TempRectangle = rec;
            textWidth.Text = rec.Width.ToString();
            textHeight.Text = rec.Height.ToString();
            cmbColor.SelectedItem = rec.Fill;
            cmbBorder.SelectedItem = rec.Stroke;
            textThick.Text = rec.StrokeThickness.ToString();
        }

        private bool validate()
        {
            bool result = true;

            if (textWidth.Text.Trim().Equals(""))
            {
                result = false;
                labelGreskaW.Content = "Niste uneli sirinu!";
                textWidth.BorderBrush = Brushes.Yellow;
            }
            else
            {
                try
                {
                    if (Int32.Parse(textWidth.Text) > 0)
                    {
                        labelGreskaW.Content = "";
                        textWidth.BorderBrush = Brushes.Gray;
                    }
                    else
                    {
                        result = false;
                        labelGreskaW.Content = "Unesite pozitivan broj za sirinu!";
                        textWidth.BorderBrush = Brushes.Yellow;
                    }
                }
                catch (Exception ex)
                {
                    result = false;
                    labelGreskaW.Content = "Polje mora biti ceo broj!";
                    textWidth.BorderBrush = Brushes.Yellow;

                }

            }

            if (textHeight.Text.Trim().Equals(""))
            {
                result = false;
                labelGreskaH.Content = "Niste uneli visinu!";
                textHeight.BorderBrush = Brushes.Yellow;
            }
            else
            {
                try
                {
                    if (Int32.Parse(textHeight.Text) > 0)
                    {
                        labelGreskaH.Content = "";
                        textHeight.BorderBrush = Brushes.Gray;
                    }
                    else
                    {
                        result = false;
                        labelGreskaH.Content = "Unesite pozitivan broj za visinu!";
                        textHeight.BorderBrush = Brushes.Yellow;
                    }
                }
                catch (Exception ex)
                {
                    result = false;
                    labelGreskaH.Content = "Polje mora biti ceo broj!";
                    textHeight.BorderBrush = Brushes.Yellow;

                }

            }

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

        private void buttonDraw_Click(object sender, RoutedEventArgs e)
        {
            if (validate())
            {

                TempRectangle = new Rectangle()
                {
                    Width = Int32.Parse(textWidth.Text),
                    Height = Int32.Parse(textHeight.Text),
                    Fill = (SolidColorBrush)new BrushConverter().ConvertFromString(cmbColor.SelectedItem.ToString().Split(' ')[1]),
                    Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString(cmbBorder.SelectedItem.ToString().Split(' ')[1]),
                    StrokeThickness = Int32.Parse(textThick.Text)
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
