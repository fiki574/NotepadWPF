using System.Windows;
using System.Windows.Controls;

namespace Notepad.Forme
{
    /// <summary>
    /// Klasa UserControl layouta
    /// </summary>
    public partial class Zamijeni : UserControl
    {
        //handler za dinamički prozor u koji se ovaj layout učitava
        public static Utilities.DynamicWindowHandler handler;

        //handler za text editor layout
        public static Utilities.TextEditorHandler thandler;

        //glavni konstruktor ovog layouta
        public Zamijeni()
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
            if (!string.IsNullOrWhiteSpace(TraziOvo.Text))
                if (!thandler.FindAndSelect(TraziOvo.Text, CaseSensitive.IsChecked == true ? true : false, true))
                    MessageBox.Show("Nije moguće pronaći \"" + TraziOvo.Text + "\"", "Blok za pisanje", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        //event koji zamjenjue trenutno odabran string
        private void ZamijeniButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TraziOvo.Text))
                thandler.ReplaceString(TraziOvo.Text, ZamijeniSa.Text);
        }

        //event koji zamjenjue apsolutno sve pojave odabranoh string-a
        private void ZamjeniSve_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TraziOvo.Text))
                thandler.ReplaceAll(TraziOvo.Text, ZamijeniSa.Text);
        }
    }
}
