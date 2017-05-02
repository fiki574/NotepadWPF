using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Drawing.Printing;
using System.Drawing;
using Notepad.Forme;

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

        //Idi na, Otvori i Nova: shortcut-i ovih funkcija dodaju zadnje slovo shortcut-a u text box
        public bool[] fixes = new bool[3] { false, false, false };

        //postavke stranice
        public PageSettings PageSettings;

        //font text box-a
        public Font Font;

        //glavni konstruktor ovog layouta
        public TextEditor()
        {
            //core funkcija, iscrtava komponente iz danog XAML koda
            InitializeComponent();

            //assign-a ovaj cijeli novokreirani objekt TextEdit-orovom handleru
            MainWindow.handler = new Utilities.TextEditorHandler(this);
            IdiNa.thandler = new Utilities.TextEditorHandler(this);
            Trazi.thandler = new Utilities.TextEditorHandler(this);
            Zamijeni.thandler = new Utilities.TextEditorHandler(this);

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
            TextData.FontFamily = new System.Windows.Media.FontFamily("Consolas");
            PageSettings = new PageSettings();
            Font = new Font("Consolas", 16);

            //programatsko kreiranje trake stanja
            traka = new System.Windows.Controls.Label();
            traka.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            traka.Margin = new Thickness(0, 0, 100, 38);
            traka.VerticalAlignment = VerticalAlignment.Bottom;
            traka.Content = "Rd 0, St 0";
        }

        #region Metode koje poziva handler

        //funckija koja automatski resize-a TextBox kako bi zauzeo širinu i visinu prikladno širini i visini glavnog prozora
        public void Resize(System.Windows.Size s)
        {
            //TextBox smanjujemo za određene vrijednosti kako bi layout stali u prozor
            TextData.Width = s.Width - Sirina;
            TextData.Height = s.Height - Visina;

            //visina i širina ovog layouta = visina i širina glavnog prozora
            Width = s.Width;
            Height = s.Height;
        }

        //javna methoda koja je pozvana od strane MainWindow-a i koja poziva event handler-e ovisno o pritisnutom shortcut-u
        //BUG: negdje dodaje slovo od shortcuta na prvo mjesto u tekstu zbog zakašnjelog input-a (RJ: boolean-ovi)
        public void Shortcut(string s)
        {
            if (s == "Nova")
            {
                fixes[0] = true;
                Nova_Click(this, null);
               
            }
            else if (s == "Otvori")
            {
                fixes[1] = true;
                Otvori_Click(this, null);
            }
            else if (s == "Spremi") Spremi_Click(this, null);
            else if (s == "Ispis") Ispis_Click(this, null);
            else if (s == "Ponisti") Ponisti_Click(this, null);
            else if (s == "Izrezi") Izrezi_Click(this, null);
            else if (s == "Kopiraj") Kopiraj_Click(this, null);
            else if (s == "Zalijepi") Zalijepi_Click(this, null);
            else if (s == "Trazi") Trazi_Click(this, null);
            else if (s == "Zamijeni") Zamijeni_Click(this, null);
            else if (s == "IdiNa")
            {
                fixes[2] = true;
                IdiNa_Click(this, null);
            }
            else if (s == "OdaberiSve") OdaberiSve_Click(this, null);
            else if (s == "PronadiSljedeci") PronadiSljedeci_Click(this, null);
            else if (s == "Izbrisi") Izbrisi_Click(this, null);
            else if (s == "VrijemeDatum") VrijemeDatum_Click(this, null);
        }

        //BUG: ako je DynamicWindow u fokusu, pritisak na "Traži sljedeće" će radit ispravno, ALI neće highlight-a pojave teksta (TEMP.RJ.: koristiti F3)
        public bool FindAndSelect(string search, bool casesensitive, bool down)
        {
            int index;
            var eStringComparison = casesensitive ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase;
            if (down) index = TextData.Text.IndexOf(search, TextData.SelectionStart + TextData.SelectionLength, eStringComparison);
            else index = TextData.Text.LastIndexOf(search, TextData.SelectionStart, TextData.SelectionStart, eStringComparison);
            if (index == -1) return false;

            Trazi.LastSearch = search;
            Trazi.LastCaseSensitive = casesensitive;
            Trazi.LastDown = down;

            TextData.SelectionStart = index;
            TextData.SelectionLength = search.Length;
            return true;
        }

        #endregion

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
                    else if (res == MessageBoxResult.Cancel) return;
                }

                //basic kreiranje prazne datoteke sa generic nazivom
                FullPath = System.Windows.Forms.Application.StartupPath + "\\Novi tekstni dokument.txt";
                File.Create(FullPath);

                //pobrisati prijašnje podatke
                TextData.Text = null;

                //promjena titla u trenutno ime datoteke
                handler.ChangeTitle(Path.GetFileNameWithoutExtension(FullPath));
            }
            catch (Exception ex)
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
            catch (Exception ex)
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
            catch (Exception ex)
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
            //prikaži pre-made formu za postavljanje stranice
            PageSetupDialog psd = new PageSetupDialog();

            //ovo je potrebno inače je bačen "NullPointerException"
            psd.PageSettings = PageSettings;

            //pohrani nove postavke
            if(psd.ShowDialog() == DialogResult.OK) PageSettings = psd.PageSettings;
        }

        private void Ispis_Click(object sender, RoutedEventArgs e)
        {
            //pre-made forma napravljena za WPF projekte
            System.Windows.Controls.PrintDialog pd = new System.Windows.Controls.PrintDialog();
            PrintDocument doc = new PrintDocument();
            doc.DefaultPageSettings = PageSettings;
            doc.PrintPage += new PrintPageEventHandler(Document_PrintPage);

            //pokaži formu i printaj ako je "Ispis" pritisnut
            bool? show = pd.ShowDialog();
            if (show == true) doc.Print();
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
            //učitaj pravi layout za dinamički prozor
            DynamicWindow window = new DynamicWindow(new Trazi(), "Traži", 400, 125);

            //pokaži prozor
            window.Show();
        }

        private void PronadiSljedeci_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Trazi.LastSearch))
            {
                if (!FindAndSelect(Trazi.LastSearch, Trazi.LastCaseSensitive, Trazi.LastDown))
                    System.Windows.MessageBox.Show("Nije moguće pronaći \"" + Trazi.LastSearch + "\"", "Blok za pisanje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else Trazi_Click(sender, e);
        }

        private void Zamijeni_Click(object sender, RoutedEventArgs e)
        {
            //učitaj pravi layout za dinamički prozor
            DynamicWindow window = new DynamicWindow(new Zamijeni(), "Zamjena", 450, 125);

            //pokaži prozor
            window.Show();
        }

        private void IdiNa_Click(object sender, RoutedEventArgs e)
        {
            //učitaj pravi layout za dinamički prozor
            DynamicWindow window = new DynamicWindow(new IdiNa(), "Idi na redak", 300, 125);

            //pokaži prozor
            window.ShowDialog();
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
            //font dialog i njegove postavke
            FontDialog fd = new FontDialog();

            //promjena skripta
            fd.AllowScriptChange = true;

            //pokaže uzorak fonta
            fd.AllowSimulations = true;

            //postavljanje fonta text box-a
            if(fd.ShowDialog() == DialogResult.OK)
            {
                TextData.FontFamily = new System.Windows.Media.FontFamily(fd.Font.Name);
                TextData.FontSize = fd.Font.Size * 96.0 / 72.0;
                TextData.FontWeight = fd.Font.Bold ? FontWeights.Bold : FontWeights.Regular;
                TextData.FontStyle = fd.Font.Italic ? FontStyles.Italic : FontStyles.Normal;
                Font = fd.Font;
            }
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
            try
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
            catch (Exception ex)
            {
                Utilities.CreateExceptionFile(ex);
            }
        }

        //event korišten primarno kod trake stanja kako bi se detektirala promjena pozicije kursora/caret-a
        private void TextData_SelectionChanged(object sender, RoutedEventArgs e)
        {
            //popravak problema gdje bi korištenje određenih shortcut-a dodalo slovo u text box
            for(int i = 0; i < 3; i++)
            {
                if (fixes[i])
                {
                    fixes[i] = false;
                    int position = TextData.SelectionStart - 1;
                    TextData.Text = TextData.Text.Remove(position);
                    TextData.SelectionStart = position;
                }
            }

            //najjednostavnije je samo pozvat već postojeći event koji radi identičnu stvar
            if(TrakaStanja.IsChecked)
                TextData_TextChanged(sender, null);
        }

        //ovaj event zapravo printa tekst na papir
        private void Document_PrintPage(object sender, PrintPageEventArgs e)
        {
            float linesPerPage = 0, yPos = 0, leftMargin = e.MarginBounds.Left, topMargin = e.MarginBounds.Top;
            int count = 0;
            string line = null;
            StreamReader stream = new StreamReader(FullPath);

            linesPerPage = e.MarginBounds.Height / Font.GetHeight(e.Graphics);

            while (count < linesPerPage && ((line = stream.ReadLine()) != null))
            {
                yPos = topMargin + (count * Font.GetHeight(e.Graphics));
                e.Graphics.DrawString(line, Font, Brushes.Black, leftMargin, yPos, new StringFormat());
                count++;
            }

            if (line != null) e.HasMorePages = true;
            else e.HasMorePages = false;
        }

        #endregion

        #region Funkcije

        public void SpremiKao()
        {
            try
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

                    FullPath = saveFileDialog1.FileName;
                }
            }
            catch (Exception ex)
            {
                Utilities.CreateExceptionFile(ex);
            }
        }

        public MessageBoxResult Izlaz()
        {
            try
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
            catch (Exception ex)
            {
                Utilities.CreateExceptionFile(ex);
                return MessageBoxResult.None;
            }
        }

        #endregion

    }
}
