using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Timers;
using System.Windows.Threading;
using System.Windows.Media.Animation;

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        private Random random;
        private String[] images;

        public MainWindow()
        {
            InitializeComponent();


            random = new Random();
            images = Directory.GetFiles("D:\\Documents\\CallistoDataStore\\StartupImages", "*.jpg");

        }


        //private void timer_Tick(object sender, EventArgs e)
        //{
        //    string chosen = images[random.Next(0, images.Length)];
        //    this.mainImage.Source = new BitmapImage(new Uri(chosen, UriKind.Absolute));
        
        
        //}

        private void VisbleToInvisible_Completed(object sender, EventArgs e)
        {
            // change the source of the myImage1 to the next image to be shown 
            // and increase the nextImageIndex
            //this.myImage1.Source = images[nextImageIndex++];
            string chosen = images[random.Next(0, images.Length)];
            this.firstImage.Source = new BitmapImage(new Uri(chosen, UriKind.Absolute));


            // if the nextImageIndex exceeds the top bound of the ocllection, 
            // get it to 0 so as to show the first image next time


            // get the InvisibleToVisible storyboard and start it
            Storyboard sb = this.FindResource("InvisibleToVisible") as Storyboard;
            sb.Begin(this);

        }

        private void InvisibleToVisible_Completed(object sender, EventArgs e)
        {
            // change the source of the myImage2 to the next image to be shown
            // and increase the nextImageIndex
            string chosen = images[random.Next(0, images.Length)];
            this.secondImage.Source = new BitmapImage(new Uri(chosen, UriKind.Absolute));

            // get the VisibleToInvisible storyboard and start it
            Storyboard sb = this.FindResource("VisibleToInvisible") as Storyboard;
            sb.Begin(this);
        }  

    }
}
