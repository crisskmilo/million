namespace Million.Domain.Entities.Dto.Transversal
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class UserDto
    {
        /// <summary>
        /// Gets or sets the id
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Gets or sets the userName
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// Gets or sets the email
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// Gets or sets the token
        /// </summary>
        public string token { get; set; }
    }
}
