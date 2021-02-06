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

namespace InstantKioskStatus
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource =
                new BitmapImage(new Uri("https://instantsystems.se/wp-content/uploads/2019/10/padel-kiosk-2-600x1142.png"));

            var posValue = 10;
            Ellipse[] kiosks = new Ellipse[5];
            for (int i = 0; i < kiosks.Length; i++)
            {
                posValue += 50; 
                kiosks[i] = new Ellipse() { Width = 100, Height = 100, Fill = myBrush };
                Canvas.SetTop(kiosks[i], posValue);
                Canvas.SetLeft(kiosks[i], 20);
                kiosks[i].PreviewMouseDown += UserCTRL_PreviewMouseDown;
                CanvasMain.Children.Add(kiosks[i]);
            }


            //Ellipse userCtrl = new Ellipse();
            //userCtrl.Fill = myBrush;


            //userCtrl.Width = 200;
            //userCtrl.Height = 200;
            //Canvas.SetTop(userCtrl, 20);
            //Canvas.SetLeft(userCtrl, 20);
            //userCtrl.PreviewMouseDown += UserCTRL_PreviewMouseDown;
            //CanvasMain.Children.Add(userCtrl);
            
        }

        UIElement dragObject = null;
        Point offset;


        private void UserCTRL_PreviewMouseDown(object sender, MouseEventArgs e)
        {
            this.dragObject = sender as UIElement;
            this.offset = e.GetPosition(this.CanvasMain);

            this.offset.Y -= Canvas.GetTop(this.dragObject);
            this.offset.X -= Canvas.GetLeft(this.dragObject);
            this.CanvasMain.CaptureMouse();

        }
        private void CanvasMain_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if(this.dragObject == null)
            {
                return;
            }

            var position = e.GetPosition(sender as IInputElement);
            Canvas.SetTop(this.dragObject, position.Y - this.offset.Y);
            Canvas.SetLeft(this.dragObject, position.X - this.offset.X);

        }

        private void CanvasMain_PreviewMouseUp(object sender, MouseEventArgs e)
        {
            this.dragObject = null;
            this.CanvasMain.ReleaseMouseCapture();
        }
       
    }

    public class Kiosk
    {
        public string Name { get; set; }

        public string Img { get; set; }

    }
}
