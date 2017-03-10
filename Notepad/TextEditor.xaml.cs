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

namespace Notepad
{
    public partial class TextEditor : UserControl
    {
        public static MainWindowHandler handler;

        public TextEditor()
        {
            InitializeComponent();
            MainWindow.handler = new TextEditorHandler(this);
            TextData.Width = 725;
            TextData.Height = 550;
        }

        public void Resize(Size s)
        {
            TextData.Width = s.Width;
            TextData.Height = s.Height;
            Width = s.Width;
            Height = s.Height;
        }
    }

    public class MainWindowHandler
    {
        private MainWindow window;

        public MainWindowHandler(MainWindow window)
        {
            this.window = window;
        }

        public void ChangeTitle(string title)
        {
            window.ChangeTitle(title);
        }
    }
}
