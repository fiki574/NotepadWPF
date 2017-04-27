using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace Notepad.Forme
{
    /// <summary>
    /// Klasa UserControl layouta
    /// </summary>
    public partial class IdiNa : UserControl
    {
        //handler za dinamički prozor u koji se ovaj layout učitava
        public static Utilities.DynamicWindowHandler handler;

        //handler za text editor layout
        public static Utilities.TextEditorHandler thandler;

        //glavni konstruktor ovog layouta
        public IdiNa()
        {
            //core funkcija, iscrtava komponente iz danog XAML koda
            InitializeComponent();
        }

        //click event koji zatvara dinamički prozor putem handlera
        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            handler.Close();
        }

        //event koji "šalje" korisnika na točan red nakon pritiska gumba
        private void Idi_Click(object sender, RoutedEventArgs e)
        {
            //unešeni string se sastoji od samo brojeva
            if (IsTextAllowed(textBox.Text))
            {
                //pozovi funkciju u TextEditor-u putem handler-a
                thandler.GoToRow(Convert.ToInt32(textBox.Text));

                //zatvori prozor
                handler.Close();
            }
            else MessageBox.Show("Ovdje možete jedino upisati broj!", "Neprihvatljiv znak", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        //kada korisnik upiše broj u text box i stisne enter, ovaj event poziva event od button-a za izvršavanje "upita"
        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) Idi_Click(sender, e);
        }

        //funkcija koja pomoću Regex-a utvrđuje da li se unešeni string sastoji od samo brojeva
        private bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9.-]+");
            return !regex.IsMatch(text);
        }
    }
}
