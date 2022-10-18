namespace GreenGrey.GDPR
{
    public static class GGGdpr
    {
        private static IGGGdprInstance m_instance;

        public static IGGGdprInstance Instance => m_instance ??= new GGGdprInstance();
    }
}