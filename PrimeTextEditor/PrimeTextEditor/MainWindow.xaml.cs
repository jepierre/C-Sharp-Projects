using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.IO;
//using Microsoft.Win32;

namespace PrimeTextEditor
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

        private void mnuOpenClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = Directory.GetCurrentDirectory(); // can also set to @"c:\temp" or "c:\\temp"
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1; // selects which filter from above to apply
            openFileDialog.RestoreDirectory = true; // set to false to remember the last place OpenFileDialog was open on

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    string open = File.ReadAllText(openFileDialog.FileName); // reads the text and saves it as a string
                    // displays file location in status bar
                    tbCurrentFileLocation.Text = openFileDialog.FileName.ToString();
                    tbEditor.Text = open;

                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }

            }
        }
        private void mnuFontClick(object sender, RoutedEventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            if(fontDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.Windows.MessageBox.Show("Font Selected: " + fontDialog.Font.ToString());
                //tbEditor.FontStyle = fontDialog.Font;
            }
        }

        private void mnuSaveClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            saveFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1; // select only text files
            saveFileDialog.RestoreDirectory = true;

            if(saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string saveFileName = saveFileDialog.FileName; // save the file as a text file
                // get text from texteditor
                File.WriteAllText(saveFileName, tbEditor.Text);
            }

        }

        // creates new text document
        private void mnuNewClick(object sender, RoutedEventArgs e)
        {
            // clears text in editor
            tbEditor.Clear();
        }

        // updates status bar when event in text box changes
        private void tbSelectionChanged(object sender, RoutedEventArgs e)
        {
            int row = tbEditor.GetLineIndexFromCharacterIndex(tbEditor.CaretIndex);
            int col = tbEditor.CaretIndex - tbEditor.GetCharacterIndexFromLineIndex(row);
            lblCursorPosition.Text = "Line " + (row + 1) + ", Char " + (col + 1);
        }

        // closes the app
        private void mnuCloseClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        // enable word wrap
        private void WordWrap_Checked(object sender, RoutedEventArgs e)
        {
            tbEditor.TextWrapping = TextWrapping.Wrap;
        }

        // disable word wrap
        private void WordWrap_Unchecked(object sender, RoutedEventArgs e)
        {
            tbEditor.TextWrapping = TextWrapping.NoWrap;
        }

    }
}
