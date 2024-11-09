namespace Smartwyre.Domain.Entities.Dto.Transversal
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="FunctionalityDto" />
    /// </summary>
    public class FunctionalityDto
    {
        /// <summary>
        /// Gets or sets the Nombre
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Gets or sets the menuId
        /// </summary>
        public int? menuId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether requireRequestAccess
        /// </summary>
        public bool requireRequestAccess { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether requireWritePermissions
        /// </summary>
        public bool requireWritePermissions { get; set; }

        /// <summary>
        /// Gets or sets the functionalityChild
        /// </summary>
        public IEnumerable<FunctionalityDto> functionalityChild { get; set; }
    }
}
