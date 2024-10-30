using Biokudi_Backend.Application.DTOs.Response;
using Newtonsoft.Json;

namespace Biokudi_Backend.Infrastructure.Services
{
    public class CaptchaService
    {
        private readonly string _secretKey;
        private readonly HttpClient _httpClient;

        public CaptchaService(IConfiguration configuration, HttpClient httpClient)
        {
            _secretKey = configuration["ApiKeys:CaptchaKey"] ?? throw new ArgumentNullException(nameof(configuration), "CaptchaKey cannot be null");
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<bool> VerifyCaptcha(string captchaToken)
        {
            if (string.IsNullOrEmpty(captchaToken))
                throw new ArgumentException("Captcha token cannot be null or empty", nameof(captchaToken));
            var postData = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("secret", _secretKey),
                new KeyValuePair<string, string>("response", captchaToken)
            });
            var response = await _httpClient.PostAsync("https://www.google.com/recaptcha/api/siteverify", postData);
            if (!response.IsSuccessStatusCode)
                return false;
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var captchaResult = JsonConvert.DeserializeObject<CaptchaResponseDto>(jsonResponse);
            return captchaResult?.Success == true;
        }

    }
}
