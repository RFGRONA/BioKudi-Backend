using Biokudi_Backend.Application.DTOs;

namespace Biokudi_Backend.Domain.Services
{
    public interface IEmailService
    {
        Task<EmailDto> CreateAccountAlert(EmailDto emailDto);
        Task<EmailDto> ResetPasswordAlert(EmailDto emailDto);

    }
}
