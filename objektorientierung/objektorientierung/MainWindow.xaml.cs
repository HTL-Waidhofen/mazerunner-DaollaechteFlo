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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

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
            return $"Rechteck: {laenge}x{breite} [{x}] [{y}]";
        }
    }

    class Spieler 
    {
        public int xplayer;
        public int yplayer;
        public Image image;
        public MainWindow.Direction direction=MainWindow.Direction.None;
        public List<Rechteck> rechtecke;
        public Spieler(List<Rechteck> rechtecke)
        {
            xplayer = 1;
            yplayer = 1;
            this.rechtecke = rechtecke;
        }
        public void SetDirection(MainWindow.Direction direction)
        {
            this.direction = direction;
        }
        public void Move()
        {
            int currentx = xplayer;
            int currenty = yplayer;


            if (direction == MainWindow.Direction.Left)
            {
                xplayer--;
            } else if (direction == MainWindow.Direction.Right)
            {
                xplayer++;
            } else if(direction == MainWindow.Direction.Up)
            {
                yplayer--;
            }else if (direction == MainWindow.Direction.Down)
            {
                yplayer++;
            }
            bool collision = false;
            foreach (Rechteck r in rechtecke)
            {
                if(r.x == xplayer*MainWindow.GRID_SIZE && r.y == yplayer*MainWindow.GRID_SIZE)
                {
                    collision = true;
                }
            }
            if(collision)
            {
                xplayer = currentx;
                yplayer = currenty;
            }
            Canvas.SetLeft(image, xplayer*MainWindow.GRID_SIZE);
            Canvas.SetTop(image, yplayer*MainWindow.GRID_SIZE);
        }

    }

    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public enum Direction
        {
            Up,
            Down,
            Left,
            Right,
            None
        }
        DispatcherTimer timer = null;
        List<Rechteck> rechtecke = new List<Rechteck>();

        Spieler spieler;
        public static int GRID_SIZE = 25;
        public MainWindow()
        {
            spieler = new Spieler(rechtecke);

            InitializeComponent();
            StreamReader reader = new StreamReader("wallsList.txt");
            string wallist = reader.ReadToEnd();
            string[] walls = wallist.Split( '\n' );
            for(int i =0; i< walls.Length; i++)
            {
                int x = int.Parse(walls[i].Split(',')[0])* GRID_SIZE;
                int y = int.Parse(walls[i].Split(',')[1])* GRID_SIZE;
                Rechteck r = new Rechteck(GRID_SIZE, GRID_SIZE, x, y);
                rechtecke.Add(r);
                lstrechtecke.Items.Add(r);
            }
        }


        private void Update(object sender, EventArgs e)
        {
            spieler.Move();

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
                double x = double.Parse(xStr);
                string yStr = this.tbxy.Text;
                double y = double.Parse(yStr);
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
                tbxx.Clear();
                tbxy.Clear();
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
            tbxx.Text= r.x.ToString();
            tbxy.Text= r.y.ToString();
        }

        private void btnreckteckeloeschen(object sender, RoutedEventArgs e)
        {
            myCanvas.Children.Clear();
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
                Canvas.SetLeft(rectangle, x);
                Canvas.SetTop(rectangle, y);
            };

            spieler.image = new Image();
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("image.png", UriKind.Relative);
            bitmap.EndInit();
            spieler.image.Source= bitmap;
            spieler.image.Width = GRID_SIZE;
            spieler.image.Height = GRID_SIZE;
            Canvas.SetTop(spieler.image, spieler.yplayer* GRID_SIZE);
            Canvas.SetLeft(spieler.image, spieler.xplayer*GRID_SIZE);
           
            myCanvas.Children.Add(spieler.image);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Left)
            {
                spieler.SetDirection(Direction.Left);
            }else if(e.Key == Key.Right)
            {
                spieler.SetDirection(Direction.Right);
            }else if(e.Key == Key.Up)
            {
                spieler.SetDirection(Direction.Up);
            }else if (e.Key == Key.Down)
            {
                spieler.SetDirection(Direction.Down);
            }
           
        }

        private void btnspielstarten(object sender, RoutedEventArgs e)
        {
            if (stp_sidebar.Visibility == Visibility.Collapsed) stp_sidebar.Visibility = Visibility.Visible;
            else
            {
                stp_sidebar.Visibility = Visibility.Collapsed;
                timer = new DispatcherTimer(DispatcherPriority.Render);
                timer.Interval = TimeSpan.FromMilliseconds(300);
                timer.Tick += Update;
                timer.Start();
            }
        }
    }
}
