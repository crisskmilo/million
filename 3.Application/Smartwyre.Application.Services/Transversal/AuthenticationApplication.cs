namespace Smartwyre.Application.Services.Transversal
{
    using Smartwyre.Application.Interfaces.Transversal;
    using Smartwyre.Domain.Entities.Dto.Transversal;
    using Smartwyre.Domain.Entities.Enums;
    using Smartwyre.Domain.Entities.ErrorHandler;
    using Smartwyre.Domain.Entities.Response;
    using Smartwyre.Domain.Interfaces.Services;
    using Smartwyre.Domain.Interfaces.Services.Transversal;
    using Smartwyre.Domain.Services.Utilities;

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
                    id = userAux.id,
                    name = userAux.nombre,
                    userName = userAux.usuario,
                    email = userAux.correoelectronico,
                    token = this.userService.GetToken(userAux.usuario, userAux.id)
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
                    id = userAux.id,
                    name = userAux.nombre,
                    userName = userAux.usuario,
                    email = userAux.correoelectronico,
                    token = this.userService.GetToken(userAux.usuario, userAux.id)
                };
            }
            return HelperGeneric<UserDto>.CastToGenericResponse(Helper.ManageResponse(userAuth));
        }
    }
}