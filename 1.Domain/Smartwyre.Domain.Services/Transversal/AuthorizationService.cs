namespace Smartwyre.Domain.Services.Transversal
{
    using Smartwyre.Domain.Entities.Dto.Transversal;
    using Smartwyre.Domain.Entities.Enums;
    using Smartwyre.Domain.Entities.Model.Transversal;
    using Smartwyre.Domain.Interfaces.Repositories.Transversal;
    using Smartwyre.Domain.Interfaces.Services;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="AuthorizationService" />
    /// </summary>
    public class AuthorizationService : IAuthorizationService
    {

        /// <summary>
        /// The Usuario repositorio
        /// </summary>
        private readonly IUserRepository userRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rolPermissionsRepository"></param>
        /// <param name="userRepository"></param>
        public AuthorizationService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestAuthorization"></param>
        /// <returns></returns>
        public IEnumerable<AuthorizationDto> RequestAuthorization(RequestAuthorizationDto requestAuthorization)
        {
            if (this.UserIsAdmin(requestAuthorization))
            {
                return this.AuthorizeAllFeatures(requestAuthorization.requestFuncionalities);
            }
            return FuctionalityAuth(requestAuthorization);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestAuthorization"></param>
        /// <returns></returns>
        private bool UserIsAdmin(RequestAuthorizationDto requestAuthorization)
        {
            if (!requestAuthorization.isAuth)
            {
                return false;
            }

            var usuario = this.userRepository.GetUserAuth(requestAuthorization.userName);
            return  true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="functionalities"></param>
        /// <returns></returns>
        private IEnumerable<AuthorizationDto> AuthorizeAllFeatures(IEnumerable<FunctionalityDto> functionalities)
        {
            return functionalities.Select(x => new AuthorizationDto()
            {
                authorizationCode = AuthorizationCode.Authorized,
                functionalityName = x.name
            }).ToList();
        }


        /// <summary>
        /// The AutorizaFunctionalityePermitidad
        /// </summary>
        /// <param name="requestAuthorization">The requestAuthorization<see cref="RequestAuthorizationDto"/></param>
        /// <returns>The <see cref="IEnumerable{Authorization}"/></returns>
        private IEnumerable<AuthorizationDto> FuctionalityAuth(RequestAuthorizationDto requestAuthorization)
        {        
            var authorizationes = new List<AuthorizationDto>();
            requestAuthorization.requestFuncionalities
            .ToList()
            .ForEach(x =>
            {
                var authorization = new AuthorizationDto()
                {
                    authorizationCode = AuthorizationCode.Authorized,
                    functionalityName = x.name
                };

                if (!requestAuthorization.isAuth && x.requireRequestAccess)
                {
                    authorization.authorizationCode = AuthorizationCode.UnAuthenticated;
                }
                else if (requestAuthorization.isAuth &&
                            x.requireRequestAccess)
                {
                    authorization.authorizationCode = authorization.authorizationCode;
                }

                authorizationes.Add(authorization);
            });

            return authorizationes;
        }
    }
}
