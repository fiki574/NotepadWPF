namespace Notepad
{
    public class Utilities
    {
        //Handler za glavni prozor
        public class MainWindowHandler
        {
            private MainWindow window;

            public MainWindowHandler(MainWindow window)
            {
                this.window = window;
            }

            public void ChangeTitle(string title)
            {
                window.ChangeTitle(title + " - NotepadWPF");
            }
        }

        //Handler za TextEditor layout
        public class TextEditorHandler
        {
            private TextEditor window;

            public TextEditorHandler(TextEditor window)
            {
                this.window = window;
            }

            public void Resize(System.Windows.Size s)
            {
                window.Resize(s);
            }

            public System.Windows.MessageBoxResult Izlaz()
            {
                return window.Izlaz();
            }
        }

        //enumeracija sa svim MenuItem članovima reprezentiranim numeričkim vrijednostima
        public enum NotepadMenuItem
        {
            Invalid = -1,
            Nova = 0,
            Otvori = 1,
            Spremi = 2,
            SpremiKao = 3,
            Postavljanje = 4,
            Ispis = 5,
            Izlaz = 6,
            Ponisti = 7,
            Izrezi = 8,
            Kopiraj = 9,
            Zalijepi = 10,
            Izbrisi = 11,
            Trazi = 12,
            PronadiSljedeci = 13,
            Zamijeni = 14,
            IdiNa = 15,
            OdaberiSve = 16,
            VrijemeDatum = 17,
            PrelomiRijeci = 18,
            Font = 19,
            TrakaStanja = 20,
            PrikaziPomoc = 21,
            OProgramu = 22
        }

        //funckija za pretvaranje object source-a u enum vrijednost | NAPOMENA: nepotrebno i viška, ali eto... reda radi dodano
        public static NotepadMenuItem GetItem(object source)
        {
            var o = source as System.Windows.Controls.MenuItem;
            string s = o.Header.ToString();
            if (s.Contains("_"))
            {
                if (s.Contains("..."))
                    s = s.Substring(0, s.Length - 3);

                s = s.Substring(1);

                if (s == "Nova")
                    return NotepadMenuItem.Nova;
                else if (s == "Otvori")
                    return NotepadMenuItem.Otvori;
                else if (s == "Spremi")
                    return NotepadMenuItem.Spremi;
                else if (s == "Spremi kao")
                    return NotepadMenuItem.SpremiKao;
                else if (s == "Postavljanje stranice")
                    return NotepadMenuItem.Postavljanje;
                else if (s == "Ispis")
                    return NotepadMenuItem.Ispis;
                else if (s == "Izlaz")
                    return NotepadMenuItem.Izlaz;
                else if (s == "Poništi")
                    return NotepadMenuItem.Ponisti;
                else if (s == "Izreži")
                    return NotepadMenuItem.Izrezi;
                else if (s == "Kopiraj")
                    return NotepadMenuItem.Kopiraj;
                else if (s == "Zalijepi")
                    return NotepadMenuItem.Zalijepi;
                else if (s == "Izbriši")
                    return NotepadMenuItem.Izbrisi;
                else if (s == "Traži")
                    return NotepadMenuItem.Trazi;
                else if (s == "Pronađi sljedeći")
                    return NotepadMenuItem.PronadiSljedeci;
                else if (s == "Zamijeni")
                    return NotepadMenuItem.Zamijeni;
                else if (s == "Idi na")
                    return NotepadMenuItem.IdiNa;
                else if (s == "Odaberi sve")
                    return NotepadMenuItem.OdaberiSve;
                else if (s == "Vrijeme/Datum")
                    return NotepadMenuItem.VrijemeDatum;
                else if (s == "Prelomi riječi")
                    return NotepadMenuItem.PrelomiRijeci;
                else if (s == "Font")
                    return NotepadMenuItem.Font;
                else if (s == "Traka stanja")
                    return NotepadMenuItem.TrakaStanja;
                else if (s == "Prikaži pomoć")
                    return NotepadMenuItem.PrikaziPomoc;
                else if (s == "O programu Notepad WPF")
                    return NotepadMenuItem.OProgramu;
                else
                    return NotepadMenuItem.Invalid;
            }
            else
                return NotepadMenuItem.Invalid;
        }
    }
}
