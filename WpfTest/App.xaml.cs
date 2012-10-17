using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Windows;
using System.Windows.Media;
using System.Drawing;

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {

        private MainWindow mainWin, secondWin;

        private void Application_Startup(object sender, System.Windows.StartupEventArgs e)
        {
            mainWin = new MainWindow();

            Screen mainScreen = GetMainScreen();

            mainWin.WindowStartupLocation = WindowStartupLocation.Manual;
            var mainLocation = mainScreen.WorkingArea.Location;
            mainWin.Left = mainLocation.X;
            mainWin.Top = mainLocation.Y;
            //mainWin.Height = mainScreen.Bounds.Height;
            //mainWin.Width = mainScreen.Bounds.Width;
            mainWin.Topmost = true;
            mainWin.Background = new SolidColorBrush(Colors.Black);
            mainWin.WindowStyle = WindowStyle.None;
            mainWin.Show();
            mainWin.WindowState = WindowState.Maximized;

            //mainWin.Focus();

            if (Screen.AllScreens.Length > 1)
            {

                Screen secondScreen = GetSecondaryScreen();
                if (secondScreen != null)
                {
                    secondWin = new MainWindow();

                    secondWin.WindowStartupLocation = WindowStartupLocation.Manual;
                    var location = secondScreen.WorkingArea.Location;
                    secondWin.Left = location.X;
                    secondWin.Top = location.Y;
                    secondWin.Height = secondScreen.Bounds.Height;
                    secondWin.Width = secondScreen.Bounds.Width;
                    secondWin.Topmost = true;
                    secondWin.Background = new SolidColorBrush(Colors.Black);
                    secondWin.WindowStyle = WindowStyle.None;
                    secondWin.Show();
                    //secondWin.Focus();
                }

            }
        }


        private Screen GetMainScreen()
        {
            if (Screen.AllScreens.Length == 1)
            {
                return Screen.PrimaryScreen;
            }

            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen.Primary == true)
                {
                    return screen;
                }
            }

            return null;
        }


        private Screen GetSecondaryScreen()
        {
            if (Screen.AllScreens.Length == 1)
            {
                return null;
            }

            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen.Primary == false)
                {
                    return screen;
                }
            }

            return null;
        }

    }
}
