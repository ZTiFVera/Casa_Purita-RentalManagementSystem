using Microsoft.AspNetCore.Identity;

namespace Casa_Purita_RentalManagementSystem.Services
{
    public class BCryptPasswordHasher<TUser> : IPasswordHasher<TUser> where TUser : class
    {
        private const int WorkFactor = 12;

        public string HashPassword(TUser user, string password)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password, WorkFactor);
        }

        public PasswordVerificationResult VerifyHashedPassword(
            TUser user, string hashedPassword, string providedPassword)
        {
            bool isValid = BCrypt.Net.BCrypt.EnhancedVerify(providedPassword, hashedPassword);

            if (isValid)
            {
                bool needsRehash = BCrypt.Net.BCrypt.PasswordNeedsRehash(hashedPassword, WorkFactor);
                return needsRehash
                    ? PasswordVerificationResult.SuccessRehashNeeded
                    : PasswordVerificationResult.Success;
            }

            return PasswordVerificationResult.Failed;
        }
    }
}
