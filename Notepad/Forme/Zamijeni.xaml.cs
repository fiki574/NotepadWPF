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

        private void TraziSljedece_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        private void ZamijeniButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        private void ZamjeniSve_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }
    }
}
