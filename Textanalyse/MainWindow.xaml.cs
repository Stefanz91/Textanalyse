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
using System.Xml;

namespace Textanalyse
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        XMLDocumentAnalyse Analyse;
        private string path;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Cast the sender object to TextBox to access its Text property
            TextBox textBox = (TextBox)sender;

            // Get the text from the TextBox
            path = textBox.Text;

           
            //make a key-enter event to take the input
            if(Keyboard.IsKeyDown(Key.Enter) && TeBlOutput != null)
            {
                // Set the text of TeBlOutput to the retrieved text
                //TeBlOutput.Text = newText;
            }
        }

        private void btnPath_Click(object sender, RoutedEventArgs e)
        {
            if(TeBlOutput != null)
            {
               
                String xmlPath = path;
                Analyse = new XMLDocumentAnalyse(xmlPath);
                if (Analyse.getXML())
                {
                    TeBlOutput.Text = "XMLDatei eingelesen!";
                }
                else
                {
                    TeBlOutput.Text = "XMLPfad inkorrekt!";
                    Analyse = null;
                }
                
            }
        }

        private void btnSentecelength_Click(object sender, RoutedEventArgs e)
        {
            if(Analyse != null)
            {
                MessageBox.Show(Analyse.CountWordsInSentence());
            }
        }

        private void btnShowText_Click(object sender, RoutedEventArgs e)
        {
            if (Analyse != null)
            {
                MessageBox.Show(Analyse.ShowText());
            }
        }
    }
}
