namespace Smartwyre.Domain.Interfaces.Services
{
    using Smartwyre.Domain.Entities.Dto.Transversal;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="IAuthorizationService" />
    /// </summary>
    public interface IAuthorizationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestAuthorization"></param>
        /// <returns></returns>
        IEnumerable<AuthorizationDto> RequestAuthorization(RequestAuthorizationDto requestAuthorization);
    }
}
