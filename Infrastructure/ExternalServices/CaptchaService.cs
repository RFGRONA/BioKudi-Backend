using Biokudi_Backend.Application.DTOs.Response;
using Newtonsoft.Json;

namespace Biokudi_Backend.Infrastructure.ExternalServices
{
    public class CaptchaService
    {
        private readonly string _secretKey;
        private readonly HttpClient _httpClient;

        public CaptchaService(IConfiguration configuration, HttpClient httpClient)
        {
            _secretKey = configuration["ApiKeys:CaptchaKey"] ?? throw new ArgumentNullException(nameof(configuration), "CaptchaKey cannot be null");
            _httpClient = httpClient;
        }

        public async Task<bool> VerifyCaptcha(string captchaToken)
        {
            var response = await _httpClient.PostAsync($"https://www.google.com/recaptcha/api/siteverify?secret={_secretKey}&response={captchaToken}", null);
            if (!response.IsSuccessStatusCode)
                return false;

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var captchaResult = JsonConvert.DeserializeObject<CaptchaResponseDto>(jsonResponse);

            return captchaResult?.Success == true && captchaResult.Score > 0.5; 
        }
    }
}
