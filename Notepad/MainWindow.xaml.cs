using System.Windows;

namespace Notepad
{
    public partial class MainWindow : Window
    {
        public static TextEditorHandler handler;

        public MainWindow()
        {
            InitializeComponent();
            TextEditor.handler = new Utilities.MainWindowHandler(this);
            Content = new TextEditor();
        }

        public void TextData_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            handler.Resize(e.NewSize);
        }

        public void ChangeTitle(string title)
        {
            Title = title;
        }
    }

    public class TextEditorHandler
    {
        private TextEditor window;

        public TextEditorHandler(TextEditor window)
        {
            this.window = window;
        }

        public void Resize(Size s)
        {
            window.Resize(s);
        }
    }
}
