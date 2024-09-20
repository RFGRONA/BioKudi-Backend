namespace Biokudi_Backend.Application.Utilities
{
    using BCrypt.Net;
    public class PasswordUtility
    {
        public static string HashPassword(string password)
        {
            return BCrypt.HashPassword(password);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Verify(password, hashedPassword);
        }
    }
}
