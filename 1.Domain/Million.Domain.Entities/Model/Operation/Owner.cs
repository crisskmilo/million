using System;
using System.Collections.Generic;

namespace Million.Domain.Entities.Model.Operation
{
    [Dapper.Contrib.Extensions.Table("dbo.[Owner]")]
    [Serializable]
    public class Owner
    {
        [Dapper.Contrib.Extensions.Key]
        public int IdOwner { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
        public DateTime? Birthday { get; set; }
        [Dapper.Contrib.Extensions.Computed]
        public ICollection<Property> Properties { get; set; }
    }
}
