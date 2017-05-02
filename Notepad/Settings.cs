namespace Notepad
{
    public struct Settings
    {
        public bool TrakaStanja;
        public bool WordWrap;
        public bool PageSettingsColor;
        public bool PageSettingsLandscape;
        public string FontFamily;
        public int FontSize;
        public bool FontWeight;
        public bool FontStyle;

        public override string ToString()
        {
            return $"[TrakaStanja: { TrakaStanja } | WordWrap: { WordWrap } | FontFamily: { FontFamily } | FontSize: { FontSize }";
        }
    }
}
