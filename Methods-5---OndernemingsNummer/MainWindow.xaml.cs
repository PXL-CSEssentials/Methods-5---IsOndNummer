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

namespace Methods_5___OndernemingsNummer
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

        private byte IsVATNumber(string vatNumber)
        {
            uint number;

            // BE 457.033.811
            // 4570338 11

            // Foute vorm
            if (!vatNumber.Substring(0, 3).ToUpper().Equals("BE ") || // niet startend met "BE "
                vatNumber.Length != 14 || // of niet de juiste lengte
                !uint.TryParse(vatNumber.Substring(3).Replace(".", ""), out number)) // of is geen getal 
            {
                return 3;
            }

            // Juiste vorm
            // Ondernemingsnummer splitsen.
            long vatNumberPart1 = long.Parse(number.ToString().Substring(0, 7));
            byte vatNumberPart2 = byte.Parse(number.ToString().Substring(7, 2)); // controlenummer
            //Ondernemingsnummer controleren.
            if ((97 - (vatNumberPart1 % 97)) == vatNumberPart2)
                return 1; // Juist nummer.
            else
                return 2; // Fout nummer.
        }

        private void vatNumberTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            resultTextBox.Clear();
        }

        private void checkButton_Click(object sender, RoutedEventArgs e)
        {
            byte code = IsVATNumber(vatNumberTextBox.Text);

            switch (code)
            {
                case 1:
                    resultTextBox.Text = "Goed gevormd!";
                    break;
                case 2:
                    resultTextBox.Text = "Fout nummer!";
                    MessageBox.Show("Het ondernemingsnummer is niet juist!", "Controle gegevens",
                        MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    vatNumberTextBox.SelectAll();
                    vatNumberTextBox.Focus();
                    break;
                default:
                    resultTextBox.Text = "Fout gevormd!";
                    MessageBox.Show("De vorm waarin het ondernemingsnummer moet gegeven worden is BE xxx.xxx.xxx",
                        "Controle ondernemingsnummer", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    vatNumberTextBox.SelectAll();
                    vatNumberTextBox.Focus();
                    break;
            }
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            vatNumberTextBox.Clear();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Kortste manier
            MessageBoxResult result = MessageBox.Show("Wens je de toepassing te sluiten?",
                "Afsluiten van toepassing", MessageBoxButton.YesNo, MessageBoxImage.Information);
            e.Cancel = (result == MessageBoxResult.No); // als er op nee gedrukt wordt => true => cancellen

            // Langere manier
            /*
            if (MessageBox.Show("Wens je de toepassing te sluiten?", "Afsluiten van toepassing", 
                MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                // Niet cancelen, dus Window sluiten
                e.Cancel = false;
            }
            else
            {
                // Cancelen, dus Window niet sluiten
                e.Cancel = true;
            }
            */
        }
    }
}
