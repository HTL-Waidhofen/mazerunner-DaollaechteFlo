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

        public double FlaechenBerechnen() 
        {
            return laenge * breite;
        }
         public Rechteck(double laenge, double breite) 
        {
            this.laenge = laenge;
            this.breite = breite;
            
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
                if(lstrechtecke.SelectedItem != null)
                {
                    Rechteck r=(Rechteck)lstrechtecke.SelectedItem;
                    r.breite= breite;
                    r.laenge= laenge;
                    lstrechtecke.Items.Refresh();

                }
                else
                {
                    Rechteck r = new Rechteck(laenge, breite);
                    lstrechtecke.Items.Add(r);
                    rechtecke.Add(r);
                    lstrechtecke.Items.Refresh();
                    Rectangle rectangle = new Rectangle();
                    rectangle.StrokeThickness = 2;
                    rectangle.Stroke = Brushes.Red;
                    
                    rectangle.Width = breite;
                    rectangle.Height = laenge;
                    
                    myCanvas.Children.Add(rectangle);
                    
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
    }
}
