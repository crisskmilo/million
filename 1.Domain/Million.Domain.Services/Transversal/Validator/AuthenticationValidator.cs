namespace Million.Domain.Services.Transversal.Validator
{
    using FluentValidation;
    using Million.Domain.Entities.Model.Transversal;
    using Million.Domain.Interfaces.Repositories.Transversal;
    using Million.Domain.Services.Utilities;

    /// <summary>
    /// Defines the <see cref="AuthenticationValidator" />
    /// </summary>
    public class AuthenticationValidator : BaseValidator<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationValidator"/> class.
        /// </summary>
        /// <param name="userRepository">The userRepository<see cref="UserRepository"/></param>
        /// <param name="password">The password<see cref="string"/></param>
        public AuthenticationValidator(IUserRepository userRepository, string password) : base(userRepository)
        {
            RuleFor(r => r).Must(e => this.ArePasswordsEquals(e.Password, password)).WithMessage("user or password is wrong.");
            RuleFor(r => r.Active).Must(IsActive).WithMessage("user is inactive.");
        }

        /// <summary>
        /// The passwordIgual
        /// </summary>
        /// <param name="password">The password<see cref="string"/></param>
        /// <returns>The <see cref="bool"/></returns>
        private bool ArePasswordsEqualsSignature(string password, string pass)
        {
            return Util.ComparePassword(password, pass);
        }

        private bool ArePasswordsEquals(string pass, string password)
        {
            return pass.Trim().Equals(password.Trim());
        }

        private bool IsActive(bool active)
        {
            return active==true;
        }
    }
}
