namespace Million.Application.Services.Transversal
{
    using Million.Application.Interfaces.Transversal;
    using Million.Domain.Entities.Dto.Transversal;
    using Million.Domain.Entities.Enums;
    using Million.Domain.Entities.ErrorHandler;
    using Million.Domain.Entities.Response;
    using Million.Domain.Interfaces.Services;
    using Million.Domain.Interfaces.Services.Transversal;
    using Million.Domain.Services.Utilities;

    /// <summary>
    /// The seguridad aplicaciones.
    /// </summary>
    public class AuthenticationApplication : IAuthenticationApplication
    {
        private readonly IUserService userService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuarioServicio"></param>
        public AuthenticationApplication(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="credencialDto"></param>
        /// <returns></returns>
        public GenericResponse<LoginResponse> Authentication(CredentialDto credencialDto)
        {
            var loginResponse = new LoginResponse();
            var userAux = this.userService.SignIn(credencialDto.userName, credencialDto.password);
            if(userAux != null) {
                loginResponse.user = new UserDto
                {
                    id = userAux.Id,
                    name = userAux.Name,
                    userName = userAux.Email,
                    email = userAux.Email,
                    token = this.userService.GetToken(userAux.Email, userAux.Id)
                }; 
            }
            return HelperGeneric<LoginResponse>.CastToGenericResponse(Helper.ManageResponse(loginResponse));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public GenericResponse<UserDto> GetUserAuth(string userName)
        {
            var userAuth = new UserDto();
            var userAux = this.userService.GetUserAuth(userName);
            if (userAux == null)
            {
                throw new ExceptionGeneric(ExceptionGenericTypes.Authentication, "Use not found");
            }else
            {
                userAuth = new UserDto
                {
                    id = userAux.Id,
                    name = userAux.Name,
                    userName = userAux.Email,
                    email = userAux.Email,
                    token = this.userService.GetToken(userAux.Email, userAux.Id)
                };
            }
            return HelperGeneric<UserDto>.CastToGenericResponse(Helper.ManageResponse(userAuth));
        }
    }
}