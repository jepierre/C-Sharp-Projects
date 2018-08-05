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
using System.Diagnostics;
using NCalc;




namespace Calculator
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

        bool resultCalculated = false;

        private void Number_Button_Click(object sender, RoutedEventArgs e)
        {
            if(resultCalculated)
            {
                tb_result.Text = "";
                resultCalculated = false;
            }
            Button b = (Button)sender;
            Debug.WriteLine("Button " + b.Content.ToString());
            
            tb_result.Text += b.Content.ToString();
        }

        private void Operator_Button_Click(object sender, RoutedEventArgs e)
        {
            if(tb_result.Text.Length <= 0)
            {
                Debug.WriteLine("Number is needed to calculate");
                return;
            }

            String lastChar = tb_result.Text.Substring(tb_result.Text.Length - 1, 1);
            if (lastChar.Equals("/") || lastChar.Equals("*") || lastChar.Equals("+") || lastChar.Equals("-"))
            {
                Debug.WriteLine("Operation Not Allowed");
                return;
            }
            Button b = (Button)sender;
            Debug.WriteLine("Button " + b.Content.ToString());
            tb_result.Text += b.Content.ToString();


        }

        private void Off_Click(object sender, RoutedEventArgs e)
        {
            // closes the app
            Debug.WriteLine("Shutting Down!");
            Application.Current.Shutdown();
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Deleting Line");
            tb_result.Text = "";

        }

        private void R_Click(object sender, RoutedEventArgs e)
        {
            if(tb_result.Text.Length > 0)
            {
                Debug.WriteLine("Erasing a number");
                tb_result.Text = tb_result.Text.Substring(0, tb_result.Text.Length - 1);
            }
        }

        private void Result_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Result();
            }
            catch (Exception exc)
            {
                tb_result.Text = "Error: " + exc + "!";
            }
        }

        private void Result()
        {
            if (tb_result.Text.Length <= 0)
            {
                Debug.WriteLine("Operation Not Allowed");
                return;
            }

            String lastChar = tb_result.Text.Substring(tb_result.Text.Length - 1, 1);
            if (lastChar.Equals("/") || lastChar.Equals("*") || lastChar.Equals("+") || lastChar.Equals("-"))
            {
                Debug.WriteLine("Operation Not Allowed");
                return;
            }
            NCalc.Expression e = new NCalc.Expression(tb_result.Text);
            tb_result.Text = e.Evaluate().ToString();

            // clears displays next evaluation
            resultCalculated = true;
        }

        private void tb_result_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
