namespace Million.Domain.Services.Transversal
{
    using Microsoft.IdentityModel.Tokens;
    using Million.Domain.Entities.Enums;
    using Million.Domain.Entities.ErrorHandler;
    using Million.Domain.Interfaces.Repositories.Transversal;
    using Million.Domain.Interfaces.Services;
    using Million.Domain.Services.Transversal.Validator;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using Microsoft.Extensions.Options;
    using Million.Domain.Entities.Config;
    using Million.Domain.Entities.Model.Transversal;

    /// <summary>
    /// Defines the <see cref="UserService" />
    /// </summary>
    public class UserService : BaseService<User>, IUserService
    {
        /// <summary>
        /// The User repository
        /// </summary>
        private readonly IUserRepository userRepository;


        private readonly AppSettings appSettings;
        

        public UserService(IOptions<AppSettings> appSettings, IUserRepository userRepository
           ) 
            : base(userRepository)
        {
            this.userRepository = userRepository;
            this.appSettings = appSettings.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User SignIn(string userName, string password)
        {
            User userAuth = this.userRepository.GetUserAuth(userName);
            //password = Encoding.UTF8.GetString(Convert.FromBase64String(password));
            if (userAuth == null)
            {
                throw new ExceptionGeneric(ExceptionGenericTypes.Validations, "User o password wrong.");
            }
            var validator = new AuthenticationValidator(this.userRepository, password);
            var validations = validator.Validate(userAuth);
            if (!validations.IsValid)
            {
                throw new ExceptionGeneric(ExceptionGenericTypes.Validations, validations.Errors.First().ErrorMessage);
            }
            return userAuth;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetToken(string user, int? id)
        {
            var claims = new[] {
                new Claim (ClaimTypes.Name, user.Trim()),
                new Claim(ClaimTypes.NameIdentifier, id.Value.ToString())
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddYears(1),
                SigningCredentials = creds
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            /*    const string authenticationType = "Cookies";
                var claimsIdentity = new ClaimsIdentity(claims, authenticationType);
                _httpContextAccessor.HttpContext.SignInAsync(authenticationType, new ClaimsPrincipal(claimsIdentity), authProperties);*/
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// The GetUserAuth
        /// </summary>
        /// <param name="user">The user<see cref="string"/></param>
        /// <returns>The <see cref="User"/></returns>
        public User GetUserAuth(string userName)
        {
            var userIn = this.userRepository.GetUserAuth(userName);
            
            if (userIn == null)
            {
                throw new ExceptionGeneric(ExceptionGenericTypes.Authentication, "User not found.");
            }
            return userIn;
        }


        /// <summary>
        /// Obteners the por identifier.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public User GetUserById(int? userId) {
          return  this.userRepository.GetUserById(userId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> GetUsers()
        {
            return this.userRepository.GetObjectAll();
        }
    }
}
