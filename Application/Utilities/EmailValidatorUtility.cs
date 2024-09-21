using DnsClient;
using System.Text.RegularExpressions;

namespace Biokudi_Backend.Application.Utilities
{
    public static class EmailValidatorUtility
    {
        public static async Task<bool> ValidateEmailAsync(string email)
        {
            if (!IsValidEmailSyntax(email))
                return false;
            return await HasValidMxRecordAsync(email);
        }

        private static bool IsValidEmailSyntax(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            try
            {
                var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
                return emailRegex.IsMatch(email);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static async Task<bool> HasValidMxRecordAsync(string email)
        {
            try
            {
                var domain = email.Split('@')[1];
                var lookup = new LookupClient();
                var result = await lookup.QueryAsync(domain, QueryType.MX);
                return result.Answers.MxRecords().Any();
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

}
