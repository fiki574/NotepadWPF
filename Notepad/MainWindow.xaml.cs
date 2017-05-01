using System.Windows;
using System.Windows.Input;

namespace Notepad
{
    /// <summary>
    /// Klasa glavnog prozora
    /// </summary>
    public partial class MainWindow : Window
    {
        //handler isključivo mora biti public static kako bi mu se pristupilo iz svakog dijela aplikacije
        public static Utilities.TextEditorHandler handler;

        //ovo sam koristio umjesto KeyBinding-ove kompleksnosti implementiranja prečaca na tipkovnici
        int count = 0;
        Key[] keys = new Key[2];

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

        //event handler za detektiranje kada korisnik pritisne X u gornjem desnom kutu, preusmjerava ga u standardni dijalog za spremanje promjena, ako ih ima
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //pošto pritiskom na "Odustani" korisnik želi ostati u aplikaciji, mora se sprječiti zatvaranje iste
            MessageBoxResult res = handler.Izlaz();
            if (res == MessageBoxResult.Cancel) e.Cancel = true;
        }

        //event handler koji se koristi kako bi se otkrila kombinacija dvaju pritisnutih tipki kako bi se pozvao ispravan shortcut
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F3)
            {
                handler.Shortcut("PronadiSljedeci");
                return;
            }
            else if (e.Key == Key.Delete)
            {
                handler.Shortcut("Izbrisi");
                return;
            }
            else if (e.Key == Key.F5)
            {
                handler.Shortcut("VrijemeDatum");
                return;
            }
            else
            {
                if (count == 0)
                {
                    if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
                        keys[count++] = e.Key;
                }

                if (count == 1)
                {
                    if (e.Key == Key.N || e.Key == Key.O || e.Key == Key.S || e.Key == Key.P || e.Key == Key.Z || e.Key == Key.X || e.Key == Key.C || e.Key == Key.V || e.Key == Key.F || e.Key == Key.H || e.Key == Key.G || e.Key == Key.A)
                        keys[count++] = e.Key;
                }

                //dva gumba su trenutno pritisnuta, procesuiraj...
                if (count == 2)
                {
                    //prvi gumb
                    switch (keys[0])
                    {
                        //isključivo Control
                        case Key.LeftCtrl:
                        case Key.RightCtrl:
                            {
                                //drugi gumb ovisno o shortcut-u, gore su "izbjegnute" ostale 3 funkcije koje nemaju Control u svojem shortcut-u
                                switch (keys[1])
                                {
                                    case Key.N:
                                        {
                                            handler.Shortcut("Nova");
                                            break;
                                        }

                                    case Key.O:
                                        {
                                            handler.Shortcut("Otvori");
                                            break;
                                        }

                                    case Key.S:
                                        {
                                            handler.Shortcut("Spremi");
                                            break;
                                        }

                                    case Key.P:
                                        {
                                            handler.Shortcut("Ispis");
                                            break;
                                        }

                                    case Key.Z:
                                        {
                                            handler.Shortcut("Ponisti");
                                            break;
                                        }

                                    case Key.X:
                                        {
                                            handler.Shortcut("Izrezi");
                                            break;
                                        }

                                    case Key.C:
                                        {
                                            handler.Shortcut("Kopiraj");
                                            break;
                                        }

                                    case Key.V:
                                        {
                                            handler.Shortcut("Zalijepi");
                                            break;
                                        }

                                    case Key.F:
                                        {
                                            handler.Shortcut("Trazi");
                                            break;
                                        }

                                    case Key.H:
                                        {
                                            handler.Shortcut("Zamijeni");
                                            break;
                                        }

                                    case Key.G:
                                        {
                                            handler.Shortcut("IdiNa");
                                            break;
                                        }

                                    case Key.A:
                                        {
                                            handler.Shortcut("OdaberiSve");
                                            break;
                                        }
                                }
                                break;
                            }

                    }
                    count = 0;
                }
            }
        }
    }
}
