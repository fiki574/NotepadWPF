namespace Notepad
{
    //korišteno u FDB-u

    public struct Settings
    {
        public bool PrijelomiRijeci;
        public bool TrakaStanja;
        public byte FontImeIndex;
        public byte FontStilIndex;
        public byte FontVeličinaIndex;
        public byte FontSkriptIndex;
        public byte StranicaVeličinaIndex;
        public bool StranicaUsmjerenjeGore;
        public int StranicaMarginaLijevo;
        public int StranicaMarginaDesno;
        public int StranicaMarginaGore;
        public int StranicaMarginaDolje;
        public string StranicaZaglavlje;
        public string StranicaPodnožje;
    }

    public enum Files
    {
        Generic = 0,
        Postavke = 1 
    }
}
