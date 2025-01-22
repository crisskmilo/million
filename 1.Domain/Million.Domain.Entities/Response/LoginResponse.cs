using Million.Domain.Entities.Dto.Transversal;

namespace Million.Domain.Entities.Response
{
    /// <summary>
    /// The login response.
    /// </summary>
    public class LoginResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginResponse"/> class.
        /// </summary>
        public LoginResponse()
        {
            this.user = new UserDto();
        }

        /// <summary>
        /// Gets or sets the usuario.
        /// </summary>
        public UserDto user { get; set; }
    }
}