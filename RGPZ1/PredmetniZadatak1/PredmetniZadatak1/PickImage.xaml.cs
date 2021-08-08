using Microsoft.Win32;
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
    /// Interaction logic for PickImage.xaml
    /// </summary>
    public partial class PickImage : Window
    {
        private Image tempImage;
        private string putanjaDoSlike = null;

        public Image TempImage { get => tempImage; set => tempImage = value; }

        public PickImage()
        {
            InitializeComponent();
        }

        public PickImage(Image imagePick) : this()
        {
            TempImage = imagePick;

            textWidth.Text = imagePick.Width.ToString();
            textHeight.Text = imagePick.Height.ToString();
            slika.Source = imagePick.Source;
            
        }


        private bool validate()
        {
            bool result = true;

            if (putanjaDoSlike == null)
            {
                result = false;
                labelSlikaGreska.Content = "Morate odabrati sliku!";
            }
            else
            {
                labelSlikaGreska.Content = "";
            }

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


            return result;
        }

        private void buttonOpen_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Odaberi sliku";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                putanjaDoSlike = op.FileName;
                slika.Source = new BitmapImage(new Uri(putanjaDoSlike));
            }



        }


        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            if (validate())
            {

                TempImage = new Image()
                {
                    Width = Int32.Parse(textWidth.Text),
                    Height = Int32.Parse(textHeight.Text),
                    Source = slika.Source
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
