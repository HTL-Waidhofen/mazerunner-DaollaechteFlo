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

namespace objektorientierung
{
    class Rechteck 
    {
        public double laenge = 1;
        public double breite = 2;
        public double x = 0;
        public double y = 0;

        public double FlaechenBerechnen() 
        {
            return laenge * breite;
        }
         public Rechteck(double laenge, double breite, double x, double y) 
        {
            this.laenge = laenge;
            this.breite = breite;
            this.y= y;
            this.x = x;
            
        }
        public override string ToString()
        {
            return $"Rechteck: {laenge}x{breite}";
        }
    }

    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Rechteck> rechtecke = new List<Rechteck>();
        public MainWindow()
        {
            InitializeComponent();
           
        }

        private void TextBox_IsKeyboardFocusWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void btnSpeichern_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string laengeStr = this.tbxlaenge.Text;
                double laenge = double.Parse(laengeStr);
                string breiteStr = this.tbxbreite.Text;
                double breite = double.Parse(breiteStr);
                string xStr = this.tbxx.Text;
                double x = double.Parse(laengeStr);
                string yStr = this.tbxy.Text;
                double y = double.Parse(breiteStr);
                if (lstrechtecke.SelectedItem != null)
                {
                    Rechteck r=(Rechteck)lstrechtecke.SelectedItem;
                    r.breite= breite;
                    r.laenge= laenge;
                    r.x = x;
                    r.y = y;
                    lstrechtecke.Items.Refresh();

                }
                else
                {
                    Rechteck r = new Rechteck(laenge, breite, x, y);
                    lstrechtecke.Items.Add(r);
                    rechtecke.Add(r);
                    lstrechtecke.Items.Refresh();
                   
                    
                }
                
                tbxbreite.Clear();
                tbxlaenge.Clear();
            }
            catch(FormatException) 
            {
                MessageBox.Show("Ungültige Eingabe");  
            };
            
        }

        private void lstrechtecke_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           Rechteck r=(Rechteck)this.lstrechtecke.SelectedItem;
             tbxlaenge.Text= r.laenge.ToString();
            tbxbreite.Text= r.breite.ToString();
        }

        private void btnreckteckeloeschen(object sender, RoutedEventArgs e)
        {

        }

        private void btnrechteckezeichnen(object sender, RoutedEventArgs e)
        {

            for (int i=0; i<rechtecke.Count; i++)
            {

                Rectangle rectangle = new Rectangle();
                double x = rechtecke[i].x;
                double y = rechtecke[i].y;
                rectangle.StrokeThickness = 2;
                rectangle.Stroke = Brushes.Red;
                rectangle.Width = rechtecke[i].breite;
                rectangle.Height = rechtecke[i].laenge;
                rectangle.Margin = new Thickness(5);
                myCanvas.Children.Add(rectangle);
                Canvas.SetLeft(rectangle, y);
                Canvas.SetTop(rectangle, x);
            };
            
        }
    }
}
