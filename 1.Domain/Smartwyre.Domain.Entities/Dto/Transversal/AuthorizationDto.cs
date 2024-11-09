namespace Smartwyre.Domain.Entities.Dto.Transversal
{
    using Smartwyre.Domain.Entities.Enums;

    /// <summary>
    /// Defines the <see cref="AuthorizationDto" />
    /// </summary>
    public class AuthorizationDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string functionalityName { get; set; }

        /// <summary>
        /// Gets or sets the CodigoAutorizacion
        /// </summary>
        public string authorizationCode { get; set; }

        /// <summary>
        /// The Equals
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public override bool Equals(object obj)
        {
            var authorization = (AuthorizationDto)obj;
            return authorizationCode == authorization.authorizationCode &&
                    functionalityName == authorization.functionalityName;
        }

        /// <summary>
        /// The GetHashCode
        /// </summary>
        /// <returns>The <see cref="int"/></returns>
        public override int GetHashCode()
        {
            return functionalityName.GetHashCode();
        }
    }
}
