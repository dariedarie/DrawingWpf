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

namespace PredmetniZadatak1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int clickMenu = 0;
        int clickMenuClear = 0;
        int counter = 0;
      
        public Point point;
        public PointCollection points = new PointCollection();

        private List<UIElement> objekata = new List<UIElement>();
        
       
        public MainWindow()
        {
            InitializeComponent();
            clickMenu = 0;
            clickMenuClear = 0;
            counter = 0;
           

        }

        private void buttonElipsa_Click(object sender, RoutedEventArgs e)
        {
            clickMenu = 1;
            points.Clear();
        }
        
        private void buttonRectangle_Click(object sender,RoutedEventArgs e)
        {
            clickMenu = 2;
            points.Clear();
        }

        private void buttonImage_Click(object sender, RoutedEventArgs e)
        {
            clickMenu = 3;
            points.Clear();
        }

        private void buttonPoly_Click(object sender, RoutedEventArgs e)
        {
            clickMenu = 4;
            points.Clear();
        }

        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            clickMenuClear = 5;
            foreach (UIElement item in canvas.Children)
            {
                objekata.Add(item);
                counter++;           
            }
            canvas.Children.Clear();

        }

        private void buttonUndo_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                if (canvas.Children.Count != 0)
                {

                    objekata.Add(canvas.Children[canvas.Children.Count - 1]);
                    canvas.Children.Remove(canvas.Children[canvas.Children.Count - 1]);

                }
                else
                {
                    if (counter > 0)
                    {

                        for (int i = counter; i > 0; i--)
                        {
                            canvas.Children.Add(objekata[objekata.Count - counter]);
                            objekata.Remove(objekata[objekata.Count - counter]);
                            clickMenuClear = 0;
                            counter--;
                        }
                    }
                   

                }

                points.Clear();

            }
            catch (Exception) { }

        }

        private void buttonRedo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (clickMenuClear == 5)
                {
                    MessageBox.Show("Posle Clear-a, ne mozete da koristite REDO!!");
                    return;
                }

            }
            catch (Exception) { }
           

            if (objekata.Count != 0)
            {
                canvas.Children.Add(objekata[objekata.Count - 1]);
                objekata.Remove(objekata[objekata.Count - 1]);
               
            }
            else
            {
                return;
            }
            points.Clear();

        }



        private void rightClick_Canvas(object sender, MouseButtonEventArgs e)
        {
           
            point = Mouse.GetPosition(canvas);
            points.Add(Mouse.GetPosition(canvas));
            

            if (clickMenu == 1)
            {
                EllipseW el = new EllipseW();
                el.ShowDialog();
                Ellipse tempMain = el.TempEllipse;
                if(tempMain != null)
                {
                    canvas.Children.Add(tempMain);
                    Canvas.SetTop(tempMain, point.Y);
                    Canvas.SetLeft(tempMain, point.X);
                    
                    tempMain.MouseLeftButtonDown += ModifyAllE;
                    
                   
                }
            }
            else if(clickMenu == 2)
            {
                DrawRectangle r = new DrawRectangle();
                r.ShowDialog();
                Rectangle tempMain = r.TempRectangle;
                if (tempMain != null)
                {
                    canvas.Children.Add(tempMain);
                    Canvas.SetTop(tempMain, point.Y);
                    Canvas.SetLeft(tempMain, point.X);
                    
                    tempMain.MouseLeftButtonDown += ModifyAllR;
                    

                }

            }
            else if(clickMenu == 3)
            {
                PickImage pi = new PickImage();
                pi.ShowDialog();
                Image tempMain = pi.TempImage;
                if (tempMain != null)
                {
                    canvas.Children.Add(tempMain);
                    Canvas.SetTop(tempMain,point.Y);
                    Canvas.SetLeft(tempMain,point.X-3);
                    tempMain.MouseLeftButtonDown += ModifyAllI;
                    

                }


            }
            
        }

        private void leftClick_Canvas(object sender, MouseButtonEventArgs e)
        {
            point = Mouse.GetPosition(canvas);
           
                if (clickMenu == 4)
                {
                    DPolygon dp = new DPolygon(new PointCollection(points));

                    dp.ShowDialog();
                    Polygon tempMain = dp.TempPolygon;
                    if (tempMain != null)
                    {
                      
                        canvas.Children.Add(tempMain);
                        tempMain.MouseLeftButtonDown += ModifyAllP;
                        points.Clear();
                        
                    }
                } 

        }


        private void ModifyAllE(object sender, MouseButtonEventArgs e)
        {
            
                Ellipse ePick = (Ellipse)sender;

                EllipseW elW = new EllipseW(ePick);
                elW.textWidth.IsReadOnly = true;
                elW.textHeight.IsReadOnly = true;


                elW.ShowDialog();
                Shape shape = elW.TempEllipse;


                canvas.Children[canvas.Children.IndexOf(ePick)].SetValue(Shape.FillProperty, shape.Fill);
                canvas.Children[canvas.Children.IndexOf(ePick)].SetValue(Shape.StrokeProperty, shape.Stroke);
                canvas.Children[canvas.Children.IndexOf(ePick)].SetValue(Shape.StrokeThicknessProperty, shape.StrokeThickness);

        }

        private void ModifyAllR(object sender, MouseButtonEventArgs e)
        {
                Rectangle rPick = (Rectangle)sender;

                DrawRectangle dr = new DrawRectangle(rPick);
                dr.textWidth.IsReadOnly = true;
                dr.textHeight.IsReadOnly = true;


                dr.ShowDialog();
                Shape shape = dr.TempRectangle;


                canvas.Children[canvas.Children.IndexOf(rPick)].SetValue(Shape.FillProperty, shape.Fill);
                canvas.Children[canvas.Children.IndexOf(rPick)].SetValue(Shape.StrokeProperty, shape.Stroke);
                canvas.Children[canvas.Children.IndexOf(rPick)].SetValue(Shape.StrokeThicknessProperty, shape.StrokeThickness);



        }

        private void ModifyAllI(object sender, MouseButtonEventArgs e)
        {
                Image img = (Image)sender;

                PickImage pImg = new PickImage(img);
                pImg.textWidth.IsReadOnly = true;
                pImg.textHeight.IsReadOnly = true;


                pImg.ShowDialog();
                Image image = pImg.TempImage;

                canvas.Children[canvas.Children.IndexOf(img)].SetValue(Image.SourceProperty, image.Source);


        }

        private void ModifyAllP(object sender, MouseButtonEventArgs e)
        {
            Polygon poly = (Polygon)sender;

            DPolygon pp = new DPolygon(poly);
            
            pp.ShowDialog();
            Shape shape = pp.TempPolygon;


            canvas.Children[canvas.Children.IndexOf(poly)].SetValue(Shape.FillProperty, shape.Fill);
            canvas.Children[canvas.Children.IndexOf(poly)].SetValue(Shape.StrokeProperty, shape.Stroke);
            canvas.Children[canvas.Children.IndexOf(poly)].SetValue(Shape.StrokeThicknessProperty, shape.StrokeThickness);

            e.Handled = true;

        }

           

        

    }
}
