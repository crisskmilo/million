namespace Smartwyre.Application.Services.Transversal
{
    using Smartwyre.Application.Interfaces.Transversal;
    using Smartwyre.Domain.Entities.Dto.Transversal;
    using Smartwyre.Domain.Entities.Response;
    using Smartwyre.Domain.Interfaces.Services;
    using Smartwyre.Domain.Services.Utilities;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="AuthorizationApplication" />
    /// </summary>
    public class AuthorizationApplication : IAuthorizationApplication
    {
        /// <summary>
        /// Defines the authorizationService
        /// </summary>
        private readonly IAuthorizationService authorizationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationApplication"/> class.
        /// </summary>
        /// <param name="authorizationService">The authorizationService<see cref="IAuthorizationService"/></param>
        public AuthorizationApplication(IAuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestAuthorization"></param>
        /// <returns></returns>
        public GenericResponse<IEnumerable<AuthorizationDto>> RequestAuthorization(RequestAuthorizationDto requestAuthorization)
        {
            var auth = authorizationService.RequestAuthorization(requestAuthorization);
            return HelperGeneric<IEnumerable<AuthorizationDto>>.CastToGenericResponse(Helper.ManageResponse(auth));
        }
    }
}
