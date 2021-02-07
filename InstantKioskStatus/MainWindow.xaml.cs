
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
using System.Windows.Media.Animation;
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

            //CanvasMain.Background = 
            
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource =
                new BitmapImage(new Uri("C://Users/Diyar/Downloads/instant-cooler-600-5-800x1726-removebg.png"));

            ImageBrush check = new ImageBrush();
            check.ImageSource =
                new BitmapImage(new Uri("C://Users/Diyar/Downloads/good_or_tick.png"));

            ImageBrush fail = new ImageBrush();
            fail.ImageSource =
                new BitmapImage(new Uri("C://Users/Diyar/Downloads/alert.png"));

            ImageBrush neutral = new ImageBrush();
            neutral.ImageSource =
                new BitmapImage(new Uri("C://Users/Diyar/Downloads/questionmark.png"));



            var posValue = 10;
            Grid[] kiosks = new Grid[5];
            for (int i = 0; i < kiosks.Length; i++)
            {
                kiosks[i] = new Grid();

                var rectangle = new Rectangle()
                {

                    //Stroke = Brushes.Black,
                    Fill = myBrush,
                    Width = 100,
                    Height = 200,
                    Margin = new Thickness(
                    left: 0,
                    top: 20,
                    right: 0,
                    bottom: 0),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top

                };
                posValue += 50;
                kiosks[i].Children.Add(rectangle);
                //kiosks[i] = new Grid() { Width = 100, Height = 100};
                
                Canvas.SetTop(kiosks[i], posValue);
                Canvas.SetLeft(kiosks[i], 20);
                kiosks[i].PreviewMouseDown += UserCTRL_PreviewMouseDown;
                TextBlock textblock = new TextBlock();
                textblock.Text = "Kiosk ID: " + (i + 1).ToString();
                

                textblock.HorizontalAlignment = HorizontalAlignment.Center;
                Ellipse approvedIcon = new Ellipse() { Height = 40, Width = 40, Fill = check };
                Ellipse failIcon = new Ellipse() { Height = 40, Width = 40, Fill = fail };
                Ellipse neutralIcon = new Ellipse() { Height = 40, Width = 40, Fill = neutral };


                approvedIcon.VerticalAlignment = VerticalAlignment.Center;
                approvedIcon.HorizontalAlignment = HorizontalAlignment.Center;
                failIcon.VerticalAlignment = VerticalAlignment.Center;
                failIcon.HorizontalAlignment = HorizontalAlignment.Center;

                ScaleTransform trans = new ScaleTransform();
                failIcon.RenderTransform = trans;
                DoubleAnimation anim = new DoubleAnimation(1, 1.2, TimeSpan.FromMilliseconds(5000));
                anim.RepeatBehavior = RepeatBehavior.Forever;
                BounceEase b = new BounceEase();
                b.Bounces = 5;
                b.Bounciness = 1;
                b.EasingMode = EasingMode.EaseIn;

                anim.EasingFunction = b;
                trans.BeginAnimation(ScaleTransform.ScaleXProperty, anim);
                trans.BeginAnimation(ScaleTransform.ScaleYProperty, anim);


                kiosks[i].Children.Add(textblock);
                kiosks[i].Children.Add(approvedIcon);

                if (i == 1)
                {
                    kiosks[i].Children.Remove(approvedIcon);

                    kiosks[i].Children.Add(neutralIcon);
                }

                if (i == kiosks.Length - 1)
                {
                    kiosks[i].Children.Remove(approvedIcon);

                    kiosks[i].Children.Add(failIcon);

                }



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
