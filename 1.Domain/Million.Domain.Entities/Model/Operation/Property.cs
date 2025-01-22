using System;
using System.Collections.Generic;

namespace Million.Domain.Entities.Model.Operation
{
    [Dapper.Contrib.Extensions.Table("dbo.[Property]")]
    [Serializable]
    public class Property
    {
        [Dapper.Contrib.Extensions.Key]
        public int IdProperty { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public int CodeInternal { get; set; }
        public DateTime? Year { get; set; }
        public int IdOwner { get; set; }
        [Dapper.Contrib.Extensions.Computed]
        public Owner Owner { get; set; }
        [Dapper.Contrib.Extensions.Computed]
        public ICollection<PropertyImage> PropertyImages { get; set; }
        [Dapper.Contrib.Extensions.Computed]
        public ICollection<PropertyTrace> PropertyTraces { get; set; }
    }
}
