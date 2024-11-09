namespace Smartwyre.Application.Interfaces.Transversal
{
    using Smartwyre.Domain.Entities.Dto.Transversal;
    using Smartwyre.Domain.Entities.Response;

    /// <summary>
    /// The Authentication Application interface.
    /// </summary>
    public interface IAuthenticationApplication
    {
       /// <summary>
       /// 
       /// </summary>
       /// <param name="credencialDto"></param>
       /// <returns></returns>
        GenericResponse<LoginResponse> Authentication(CredentialDto credencialDto);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        GenericResponse<UserDto> GetUserAuth(string userName);
    }
}