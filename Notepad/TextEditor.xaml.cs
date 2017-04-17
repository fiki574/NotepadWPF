using System.Windows;
using System.Windows.Controls;

namespace Notepad
{
    /// <summary>
    /// Klasa jednog od bezbroj mogućih UserControl layouta
    /// </summary>
    public partial class TextEditor : UserControl
    {
        //još jedan handler, ali ovaj puta za glavni prozor
        public static Utilities.MainWindowHandler handler;

        //glavni konstruktor ovog layouta
        public TextEditor()
        {
            //core funkcija, iscrtava komponente iz danog XAML koda
            InitializeComponent();

            //assign - a ovaj cijeli novokreirani objekt TextEdit - orovom handleru
            MainWindow.handler = new Utilities.TextEditorHandler(this);

            //ovo smanjuje layout od početnih vrijednosti kako bi ScrollBar-ovi stali u prozor
            TextData.Width = 712;
            TextData.Height = 494;

            //dodaje __________ između MenuItem-a (pogledaj: Notepad->Datoteka ili dr.)
            Datoteka.Items.Insert(4, new Separator());
            Datoteka.Items.Insert(7, new Separator());
            Uredivanje.Items.Insert(1, new Separator());
            Uredivanje.Items.Insert(6, new Separator());
            Uredivanje.Items.Insert(11, new Separator());
            Pomoc.Items.Insert(1, new Separator());
        }

        //funckija koja automatski resize-a TextBox kako bi zauzeo širinu i visinu prikladno širini i visini glavnog prozora
        public void Resize(Size s)
        {
            //TextBox smanjujemo za 13 i 56 kako bi ScrollBar-ovi stali u prozor
            TextData.Width = s.Width - 13;
            TextData.Height = s.Height - 56;

            //visina i širina glavnog prozora = visina i širina ovog layouta
            Width = s.Width;
            Height = s.Height;
        }

        //glavni MenuItem_Click handler | NAPOMENA: inače bi za svaki MenuItem trebao poseban MenuItem_Click handler, ali ja sam sve stavio u jedan
        private void Handler_Click(object sender, RoutedEventArgs e)
        {
            switch(Utilities.GetItem(e.Source))
            {
                case Utilities.NotepadMenuItem.Invalid:
                    MessageBox.Show("Invalid menu item!");
                    return;

                case Utilities.NotepadMenuItem.Nova:
                    {
                        break;
                    }

                case Utilities.NotepadMenuItem.Otvori:
                    {
                        break;
                    }

                case Utilities.NotepadMenuItem.Spremi:
                    {
                        break;
                    }

                case Utilities.NotepadMenuItem.SpremiKao:
                    {
                        break;
                    }

                case Utilities.NotepadMenuItem.Postavljanje:
                    {
                        break;
                    }

                case Utilities.NotepadMenuItem.Ispis:
                    {
                        break;
                    }

                case Utilities.NotepadMenuItem.Izlaz:
                    {
                        break;
                    }

                case Utilities.NotepadMenuItem.Ponisti:
                    {
                        break;
                    }

                case Utilities.NotepadMenuItem.Izrezi:
                    {
                        break;
                    }

                case Utilities.NotepadMenuItem.Kopiraj:
                    {
                        break;
                    }

                case Utilities.NotepadMenuItem.Zalijepi:
                    {
                        break;
                    }

                case Utilities.NotepadMenuItem.Izbrisi:
                    {
                        break;
                    }

                case Utilities.NotepadMenuItem.Trazi:
                    {
                        break;
                    }

                case Utilities.NotepadMenuItem.PronadiSljedeci:
                    {
                        break;
                    }

                case Utilities.NotepadMenuItem.Zamijeni:
                    {
                        break;
                    }

                case Utilities.NotepadMenuItem.IdiNa:
                    {
                        break;
                    }

                case Utilities.NotepadMenuItem.OdaberiSve:
                    {
                        break;
                    }

                case Utilities.NotepadMenuItem.VrijemeDatum:
                    {
                        break;
                    }

                case Utilities.NotepadMenuItem.PrelomiRijeci:
                    {
                        if (TextData.TextWrapping == TextWrapping.NoWrap) TextData.TextWrapping = TextWrapping.Wrap;
                        else TextData.TextWrapping = TextWrapping.NoWrap;
                        break;
                    }

                case Utilities.NotepadMenuItem.Font:
                    {
                        break;
                    }

                case Utilities.NotepadMenuItem.TrakaStanja:
                    {
                        break;
                    }

                case Utilities.NotepadMenuItem.PrikaziPomoc:
                    {
                        break;
                    }

                case Utilities.NotepadMenuItem.OProgramu:
                    {
                        break;
                    }
            }
        }
    }
}
