using System.Windows;

namespace Notepad
{
    /// <summary>
    /// Klasa glavnog prozora
    /// </summary>
    public partial class MainWindow : Window
    {
        //handler isključivo mora biti public static kako bi mu se pristupilo iz svakog dijela aplikacije
        public static Utilities.TextEditorHandler handler;

        //konstruktor glavnog prozora
        public MainWindow()
        {
            //core funkcija, iscrtava komponente iz danog XAML koda
            InitializeComponent();

            //assign-a ovaj cijeli novokreirani objekt TextEdit-orovom handleru
            TextEditor.handler = new Utilities.MainWindowHandler(this);

            //postavljanje sadržaja glavnog prozora
            Content = new TextEditor();
        }

        //event koji se poziva kada se mijenja visina i/ili širina glavnog prozora
        public void TextData_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //handler poziva funkciju iz druge klase / drugog objekta koji može, a i ne mora biti postojeći
            handler.Resize(e.NewSize);
        }

        //funkcija za mijenjanje name bar-a aplikacije, poziva ju MainWindoHandler ali iz TextEditor klase
        public void ChangeTitle(string title)
        {
            Title = title;
        }
    }
}
