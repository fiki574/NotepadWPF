using System.Windows;
using System.Windows.Controls;

namespace Notepad
{
    public partial class TextEditor : UserControl
    {
        public static Utilities.MainWindowHandler handler;

        public TextEditor()
        {
            InitializeComponent();
            MainWindow.handler = new TextEditorHandler(this);
            TextData.Width = 712;
            TextData.Height = 494;
        }

        public void Resize(Size s)
        {
            TextData.Width = s.Width - 13;
            TextData.Height = s.Height - 56;
            Width = s.Width;
            Height = s.Height;
        }

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
