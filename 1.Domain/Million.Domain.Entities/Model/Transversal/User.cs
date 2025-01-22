using System;

namespace Million.Domain.Entities.Model.Transversal
{    
    [Dapper.Contrib.Extensions.Table("dbo.[User]")]
    [Serializable]
    public class User
    {
        [Dapper.Contrib.Extensions.Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool Active { get; set; }

    }
}
