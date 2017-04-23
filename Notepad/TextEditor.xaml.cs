using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace Notepad
{
    /// <summary>
    /// Klasa UserControl layouta
    /// </summary>
    public partial class TextEditor : System.Windows.Controls.UserControl
    {
        //još jedan handler, ali ovaj puta za glavni prozor
        public static Utilities.MainWindowHandler handler;

        //puna putanja trenutno otvorene datoteke
        public static string FullPath;

        //Boolean koji koristi prilikom izlaza iz aplikacije kako bi se utvrdilo je li došlo do promjene datoteke
        public static bool Changed;

        //vrijednosti korištene kod resize-anja i trake stanja
        public static int Sirina = 13;
        public static int Visina = 56;
        public static int TrakaVisina = 25;

        //label koji se koristi kod trake stanja
        public System.Windows.Controls.Label traka;

        //glavni konstruktor ovog layouta
        public TextEditor()
        {
            //core funkcija, iscrtava komponente iz danog XAML koda
            InitializeComponent();

            //assign-a ovaj cijeli novokreirani objekt TextEdit-orovom handleru
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

            //postavljanje varijabli na default vrijednosti
            FullPath = null;
            Changed = false;

            //programatsko kreiranje trake stanja
            traka = new System.Windows.Controls.Label();
            traka.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            traka.Margin = new Thickness(0, 0, 100, 38);
            traka.VerticalAlignment = VerticalAlignment.Bottom;
            traka.Content = "Rd 0, St 0";
        }

        //funckija koja automatski resize-a TextBox kako bi zauzeo širinu i visinu prikladno širini i visini glavnog prozora
        public void Resize(Size s)
        {
            //TextBox smanjujemo za određene vrijednosti kako bi layout stali u prozor
            TextData.Width = s.Width - Sirina;
            TextData.Height = s.Height - Visina;

            //visina i širina ovog layouta = visina i širina glavnog prozora
            Width = s.Width;
            Height = s.Height;
        }

        #region Click Handler-i

        private void Nova_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Changed)
                {
                    if (FullPath == null) FullPath = "Novi tekstni dokument.txt";
                    MessageBoxResult res = System.Windows.MessageBox.Show($"Želite li spremiti promjene u '{FullPath}'?", "NotepadWPF", MessageBoxButton.YesNoCancel);
                    if (res == MessageBoxResult.Yes)
                    {
                        //ovisno o postojanju datoteke odabrati prikladnu metodu za spremanje
                        if (FullPath != null) File.WriteAllText(FullPath, TextData.Text);
                        else SpremiKao();
                    }
                }

                //basic kreiranje prazne datoteke sa generic nazivom
                FullPath = System.Windows.Forms.Application.StartupPath + "\\Novi tekstni dokument.txt";
                File.Create(FullPath);

                //pobrisat prijašnje podatke
                TextData.Text = null;

                //promjena titla u trenutno ime datoteke
                handler.ChangeTitle(Path.GetFileNameWithoutExtension(FullPath));
            }
            catch(Exception ex)
            {
                Utilities.CreateExceptionFile(ex);
            }
        }

        private void Otvori_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //OpenFileDialog je pre-made klasa koja služi za browse-anje i odabir datoteke koja se želi prikazat
                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                //postavljanje direktorija od kojeg će browse-anje krenut
                if (FullPath == null) openFileDialog1.InitialDirectory = System.Windows.Forms.Application.StartupPath;
                else openFileDialog1.InitialDirectory = FullPath;

                //filtriranje ekstenzija koje su dopuštene za učitavanje
                openFileDialog1.Filter = "Tekstni dokument (*.txt)|*.txt|Sve datoteke (*.*)|*.*";
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.RestoreDirectory = true;

                //korisnik je pritisnuo OK, složivši se sa otvaranjem izabrane datoteke
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    //pohranjivanje putanje
                    FullPath = openFileDialog1.FileName;

                    //učitavanje datoteke
                    TextData.Text = File.ReadAllText(FullPath, Encoding.Default);

                    //promjena titla u trenutno ime datoteke
                    handler.ChangeTitle(Path.GetFileNameWithoutExtension(FullPath));

                    //pošto je TextData_TextChanged event poslan, ali još nije došlo do promjene datoteke, vraćanje na default-nu vrijedost
                    Changed = false;
                }
            }
            catch(Exception ex)
            {
                Utilities.CreateExceptionFile(ex);
            }
        }

        private void Spremi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (FullPath == null) SpremiKao();
                else
                {
                    File.WriteAllText(FullPath, TextData.Text);
                    Changed = false;
                }
            }
            catch(Exception ex)
            {
                Utilities.CreateExceptionFile(ex);
            }
        }

        private void SpremiKao_Click(object sender, RoutedEventArgs e)
        {
            SpremiKao();
        }

        private void Postavljanje_Click(object sender, RoutedEventArgs e)
        {
            //TODO: dizajn layout-a
        }

        private void Ispis_Click(object sender, RoutedEventArgs e)
        {
            //TODO: dizajn layout-a
        }

        private void Izlaz_Click(object sender, RoutedEventArgs e)
        {
            //ako nema promjena izađi iz aplikacije, ako ima spremi promjene
            Izlaz();
        }

        private void Ponisti_Click(object sender, RoutedEventArgs e)
        {
            //poništi prijašnju akciju
            TextData.Undo();
        }

        private void Izrezi_Click(object sender, RoutedEventArgs e)
        {
            //izrezivanje odabranog teksta
            TextData.Cut();
        }

        private void Kopiraj_Click(object sender, RoutedEventArgs e)
        {
            //kopiranje odabranog teksta
            TextData.Copy();
        }

        private void Zalijepi_Click(object sender, RoutedEventArgs e)
        {
            //paste-anje iz clipboard-a
            TextData.Paste();
        }

        private void Izbrisi_Click(object sender, RoutedEventArgs e)
        {
            //izbriši odabran/highlight-an dio teksta (ako takav postoji)
            if (!string.IsNullOrWhiteSpace(TextData.Text) && !string.IsNullOrWhiteSpace(TextData.SelectedText))
                TextData.Text = TextData.Text.Replace(TextData.SelectedText, null);
        }

        private void Trazi_Click(object sender, RoutedEventArgs e)
        {
            //TODO: dizajn layout-a
        }

        private void PronadiSljedeci_Click(object sender, RoutedEventArgs e)
        {
            //TODO: zahtjeva "Trazi" layout
        }

        private void Zamijeni_Click(object sender, RoutedEventArgs e)
        {
            //TODO: dizajn layout-a
        }

        private void IdiNa_Click(object sender, RoutedEventArgs e)
        {
            //TODO: dizajn layout-a
        }

        private void OdaberiSve_Click(object sender, RoutedEventArgs e)
        {
            //odabir svega
            TextData.SelectAll();
        }

        private void VrijemeDatum_Click(object sender, RoutedEventArgs e)
        {
            //nalijepi datum na postojeći tekst od trenutne pozicije caret-a
            TextData.Text += DateTime.Now.Hour + ":" + DateTime.Now.Minute + " " + DateTime.Now.Day + "." + DateTime.Now.Month + "." + DateTime.Now.Year;
        }

        private void PrelomiRijeci_Click(object sender, RoutedEventArgs e)
        {
            //jednostavno mijenjanje prijeloma riječi, čak se ne treba IsChecked koristit
            if (TextData.TextWrapping == TextWrapping.NoWrap) TextData.TextWrapping = TextWrapping.Wrap;
            else TextData.TextWrapping = TextWrapping.NoWrap;
        }

        private void Font_Click(object sender, RoutedEventArgs e)
        {
            //TODO: dizajn layout-a
        }

        private void TrakaStanja_Click(object sender, RoutedEventArgs e)
        {
            //mora se promijenit visina layout-a kako bi traka stanja stala, isto tako treba programatski dodati label-e koji se koriste
            if (TrakaStanja.IsChecked)
            {
                Visina += TrakaVisina;
                TextData.Height -= TrakaVisina;
                traka.Content = "Rd 0, St 0";
                TextData_TextChanged(sender, null);
                MainGrid.Children.Add(traka);
            }
            else
            {
                Visina -= TrakaVisina;
                TextData.Height += TrakaVisina;
                MainGrid.Children.Remove(traka);
            }
        }

        private void PrikaziPomoc_Click(object sender, RoutedEventArgs e)
        {
            //otvori Wiki stranicu na GitHub-u za pomoć u default browser-u
            System.Diagnostics.Process.Start("https://github.com/fiki574/NotepadWPF/wiki");
        }

        private void OProgramu_Click(object sender, RoutedEventArgs e)
        {
            //otvori main page na GitHub-u u default browser-u
            System.Diagnostics.Process.Start("https://github.com/fiki574/NotepadWPF");
        }

        #endregion

        #region Ostali Handler-i

        //event koji je trigger-an kad god se promijeni nešto u TextBox-u
        private void TextData_TextChanged(object sender, TextChangedEventArgs e)
        {
            //provjera je li traka stanja uključena
            if (TrakaStanja.IsChecked)
            {
                //dohvati trenutni red
                int line = TextData.GetLineIndexFromCharacterIndex(TextData.SelectionStart) + 1;

                //dohvati trenutni stupac
                int col = TextData.GetCharacterIndexFromLineIndex(line - 1);
                int acol = TextData.SelectionStart - col;

                //pohrani u label
                traka.Content = $"Rd {line}, St {acol}";
            }

            //dogodila se promjena
            Changed = true;
        }

        //event korišten primarno kod trake stanja kako bi se detektirala promjena pozicije kursora/caret-a
        private void TextData_SelectionChanged(object sender, RoutedEventArgs e)
        {
            //najjednostavnije je samo pozvat već postojeći event koji radi identičnu stvar
            TextData_TextChanged(sender, null);
        }

        #endregion

        #region Funkcije

        public void SpremiKao()
        {
            //SaveFileDialog je pre-made klasa koja služi za browse-anje i spremanje sadržaja u datoteke
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            //filtriranje ekstenzija koje su dopuštene za spremanje
            saveFileDialog1.Filter = "Tekstni dokument (*.txt)|*.txt|Sve datoteke (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            //korisnik je prisnuo OK i time save-o datoteku
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //spremi podatke
                File.WriteAllText(saveFileDialog1.FileName, TextData.Text);

                //promjena titla u trenutno ime datoteke
                handler.ChangeTitle(Path.GetFileNameWithoutExtension(saveFileDialog1.FileName));
            }
        }

        public MessageBoxResult Izlaz()
        {
            if (!Changed || string.IsNullOrWhiteSpace(TextData.Text))
            {
                Environment.Exit(0);
                return MessageBoxResult.None;
            }
            else
            {
                if (FullPath == null) FullPath = "Novi tekstni dokument.txt";

                //prikazivanje jednostavnog dijaloga u kojem se korisnika pita želi li pospremiti promjene
                MessageBoxResult res = System.Windows.MessageBox.Show($"Želite li spremiti promjene u '{FullPath}'?", "NotepadWPF", MessageBoxButton.YesNoCancel);
                if (res == MessageBoxResult.Yes)
                {
                    //ovisno o postojanju datoteke odabrati prikladnu metodu za spremanje
                    if (FullPath != null) File.WriteAllText(FullPath, TextData.Text);
                    else SpremiKao();

                    //izlaz iz aplikacije
                    Environment.Exit(1);
                }
                else if (res == MessageBoxResult.No) Environment.Exit(2); //korisnik ne želi spremiti promjene stoga samo treba izaći iz aplikacije
                return res;
            }
        }

        #endregion
    }
}
