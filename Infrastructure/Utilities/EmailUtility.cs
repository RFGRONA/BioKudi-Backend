using Biokudi_Backend.Application.DTOs;
using Biokudi_Backend.Domain.Services;

namespace Biokudi_Backend.Infrastructure.Utilities
{
    public class EmailUtility : IEmailUtility
    {
        public Task<EmailDto> CreateAccountAlert(EmailDto emailDto)
        {
            throw new NotImplementedException();
        }

        public Task<EmailDto> ResetPasswordAlert(EmailDto emailDto)
        {
            throw new NotImplementedException();
        }
    }
}
