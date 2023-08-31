namespace UrlShortner.Services
{
    public class UrlShorteningService
    {
        public const int NumberofCharsInShortLink = 7;
        public const string Alpabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        private readonly Random _random = new();
        private readonly ApplicationContext _applicationContext;
        public UrlShorteningService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        public async Task<string> GenerateUniqueCode()
        {
            var codeChars = new Char[NumberofCharsInShortLink];

            while (true)             
            {
                for (int i = 0; i < NumberofCharsInShortLink; i++)
                {
                    var radnomIndex = _random.Next(Alpabets.Length - 1);

                    codeChars[i] = Alpabets[radnomIndex];
                }
                var code = new string(codeChars);

                if (!_applicationContext.ShortenedUrl.Any(x => x.Code == code))
                {
                    return code;
                }
            }
        }
    }
}
