namespace GreenGrey.GDPR
{
    public interface IGGGdprLogger
    {
        void Log(string _text);
        void Warning(string _text);
        void Error(string _text);
    }
}