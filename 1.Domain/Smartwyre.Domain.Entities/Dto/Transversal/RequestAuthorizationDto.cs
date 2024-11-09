namespace Smartwyre.Domain.Entities.Dto.Transversal
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="RequestAuthorizationDto" />
    /// </summary>
    public class RequestAuthorizationDto
    {
        /// <summary>
        /// Gets or sets the Usuario
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// Gets or sets the CamposSolicitados
        /// </summary>
        public IEnumerable<FunctionalityDto> requestFuncionalities { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether EstaAutenticado
        /// </summary>
        public bool isAuth { get; set; }
    }
}
