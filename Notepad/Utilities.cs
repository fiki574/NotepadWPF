﻿using System;
using System.IO;
using System.Text;

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

        //funkcija koja kreira datoteke koje sadržavaju informacije o Exception-ima
        public static void CreateExceptionFile(Exception ex)
        {
            try
            {
                string name = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + "_" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second + ".exception";
                FileStream f = File.OpenWrite(name);
                byte[] data = Encoding.ASCII.GetBytes(ex.ToString());
                f.Write(data, 0, data.Length);
                f.Close();
            }
            catch
            {
                return;
            }
        }
    }
}
