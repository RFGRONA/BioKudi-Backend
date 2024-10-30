using Ganss.Xss;

namespace Biokudi_Backend.Infrastructure.Services
{
    public class SanitizerService
    {
        private readonly HtmlSanitizer _sanitizer;

        public SanitizerService()
        {
            _sanitizer = new HtmlSanitizer();
            _sanitizer.AllowedTags.Clear();
        }

        public string Sanitize(string input)
        {
            return _sanitizer.Sanitize(input);
        }
    }
}
