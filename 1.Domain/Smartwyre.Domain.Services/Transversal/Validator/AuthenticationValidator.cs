namespace Smartwyre.Domain.Services.Transversal.Validator
{
    using FluentValidation;
    using Smartwyre.Domain.Entities.Model.Authentication;
    using Smartwyre.Domain.Interfaces.Repositories.Transversal;
    using Smartwyre.Domain.Services.Utilities;

    /// <summary>
    /// Defines the <see cref="AuthenticationValidator" />
    /// </summary>
    public class AuthenticationValidator : BaseValidator<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationValidator"/> class.
        /// </summary>
        /// <param name="userRepository">The userRepository<see cref="IUsuarioRepositorio"/></param>
        /// <param name="password">The password<see cref="string"/></param>
        public AuthenticationValidator(IUserRepository userRepository, string password) : base(userRepository)
        {
            RuleFor(r => r).Must(e => this.ArePasswordsEquals(e.contrasena, password)).WithMessage("Usuario o contraseña incorrectos.");
            RuleFor(r => r.activo).Must(IsActive).WithMessage("El usuario no se encuentra activo.");
           //RuleFor(r => r.rol).Must(IsRolActive).WithMessage("El rol del usuario esta inactivo.");
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

        private bool IsActive(string active)
        {
            return active.Equals("True");
        }

        private bool IsRolActive(int? active)
        {
            return active.Equals("True");
        }
    }
}
