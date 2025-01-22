namespace Million.Application.Interfaces.Transversal
{
    using Million.Domain.Entities.Dto.Transversal;
    using Million.Domain.Entities.Response;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="IAuthorizationApplication" />
    /// </summary>
    public interface IAuthorizationApplication
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestAuthorization"></param>
        /// <returns></returns>
        GenericResponse<IEnumerable<AuthorizationDto>> RequestAuthorization(RequestAuthorizationDto requestAuthorization);
    }
}
