using System.Windows;
using System.Windows.Controls;
using Notepad.Forme;

namespace Notepad
{
    /// <summary>
    /// Klasa dinamičkog prozora
    /// </summary>
    public partial class DynamicWindow : Window
    {
        //glavni kontstruktor ovo prozora sa par dodatnih parametara
        public DynamicWindow(UserControl content, string title, int width, int height)
        {
            InitializeComponent();
            Title = title;
            Content = content;
            Width = width;
            Height = height;
            IdiNa.handler = new Utilities.DynamicWindowHandler(this);
            Trazi.handler = new Utilities.DynamicWindowHandler(this);
            Zamijeni.handler = new Utilities.DynamicWindowHandler(this);
        }
    }
}
