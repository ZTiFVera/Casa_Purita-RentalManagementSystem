using Microsoft.AspNetCore.Identity;

namespace Casa_Purita_RentalManagementSystem.Services
{
    public class CustomPasswordValidator<TUser> : IPasswordValidator<TUser> where TUser : class
    {
        public Task<IdentityResult> ValidateAsync(
            UserManager<TUser> manager, TUser user, string? password)
        {
            var errors = new List<IdentityError>();

            if (string.IsNullOrWhiteSpace(password))
            {
                errors.Add(new IdentityError
                {
                    Code = "PasswordEmpty",
                    Description = "Password cannot be empty."
                });
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }

            if (password.Length < 8)
            {
                errors.Add(new IdentityError
                {
                    Code = "PasswordTooShort",
                    Description = "Password must be at least 8 characters."
                });
            }

            if (!password.Any(char.IsUpper))
            {
                errors.Add(new IdentityError
                {
                    Code = "PasswordNoUpper",
                    Description = "Password must contain at least one uppercase letter."
                });
            }

            if (!password.Any(char.IsLower))
            {
                errors.Add(new IdentityError
                {
                    Code = "PasswordNoLower",
                    Description = "Password must contain at least one lowercase letter."
                });
            }

            if (!password.Any(char.IsDigit))
            {
                errors.Add(new IdentityError
                {
                    Code = "PasswordNoDigit",
                    Description = "Password must contain at least one number."
                });
            }

            return errors.Count == 0
                ? Task.FromResult(IdentityResult.Success)
                : Task.FromResult(IdentityResult.Failed(errors.ToArray()));
        }
    }
}
