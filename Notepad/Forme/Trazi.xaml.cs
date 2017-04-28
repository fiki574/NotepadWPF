using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Notepad.Forme
{
    /// <summary>
    /// Klasa UserControl layouta
    /// </summary>
    public partial class Trazi : UserControl
    {
        //handler za dinamički prozor u koji se ovaj layout učitava
        public static Utilities.DynamicWindowHandler handler;

        //handler za text editor layout
        public static Utilities.TextEditorHandler thandler;

        //varijable korištene kod pritiska na F3 (tj. shortcut-a za "Traži sljedeće")
        public static string LastSearch;
        public static bool LastCaseSensitive;
        public static bool LastDown;

        //glavni konstruktor ovog layouta
        public Trazi()
        {
            //core funkcija, iscrtava komponente iz danog XAML koda
            InitializeComponent();
        }

        //click event koji zatvara dinamički prozor putem handlera
        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            handler.Close();
        }

        //event koji poziva funkciju putem handler-a koja traži i označava pojave unesenog stringa
        private void TraziSljedece_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(textBox.Text))
                if (!thandler.FindAndSelect(textBox.Text, CaseSensitive.IsChecked == true ? true : false, Gore.IsChecked == true ? false : true))
                    MessageBox.Show("Nije moguće pronaći \"" + textBox.Text + "\"", "Blok za pisanje", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        //kada korisnik upiše broj u text box i stisne enter, ovaj event poziva event od button-a za izvršavanje "upita"
        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) TraziSljedece_Click(sender, e);
        }
    }
}
