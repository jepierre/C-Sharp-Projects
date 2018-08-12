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
//using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.ObjectModel;

namespace SimpleDrawingApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void inkCanvasGesture(object sender, InkCanvasGestureEventArgs e)
        {

        }

        private void mnuNewClick(object sender, RoutedEventArgs e)
        {
            inkCanvas.Background = System.Windows.Media.Brushes.White;
            btnCanvasColor.Background = System.Windows.Media.Brushes.White;
            btnPenColor.Background = System.Windows.Media.Brushes.Black;
            inkCanvas.DefaultDrawingAttributes.Color = Colors.Black;

            // clears the canvas
            inkCanvas.Strokes.Clear();

        }

        private void mnuOpenClick(object sender, RoutedEventArgs e)
        {

        }

        private void mnuSaveClick(object sender, RoutedEventArgs e)
        {

        }

        private void mnuExitClick(object sender, RoutedEventArgs e)
        {
            // can also use this code to exith the application: this.Close();
            if (System.Windows.MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButton.OKCancel, MessageBoxImage.Information) == MessageBoxResult.OK)
            {
                System.Windows.Application.Current.Shutdown();
            }
        }

        private void mnuAboutClick(object sender, RoutedEventArgs e)
        {
            PrimeAbout primeAbout = new PrimeAbout();
            primeAbout.Name = "Prime Paint";
            primeAbout.ShowDialog();
        }

        private void btnSetPenColor(object sender, RoutedEventArgs e)
        {
            ColorDialog c = new ColorDialog();

            if (c.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var wpfColor = System.Windows.Media.Color.FromArgb(c.Color.A, c.Color.R,
                                                                   c.Color.G, c.Color.B);
                btnPenColor.Background = new SolidColorBrush(wpfColor);
                inkCanvas.DefaultDrawingAttributes.Color = wpfColor;

            }
        }

        // setting the canvas color
        private void btnSetCanvasColor(object sender, RoutedEventArgs e)
        {
            ColorDialog c = new ColorDialog();
            if (c.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var wpfColor = System.Windows.Media.Color.FromArgb(c.Color.A, c.Color.R,
                                              c.Color.G, c.Color.B);
                inkCanvas.Background = new SolidColorBrush(wpfColor);
                btnCanvasColor.Background = new SolidColorBrush(wpfColor);
            }


        }

        private void cbSetPenWidth(object sender, SelectionChangedEventArgs e)
        {
            if (inkCanvas == null)
                return;
            ComboBoxItem item = (ComboBoxItem)cbPenWidth.SelectedItem as ComboBoxItem;
            inkCanvas.DefaultDrawingAttributes.Width = Double.Parse(item.Content.ToString());
        }
    }

}
