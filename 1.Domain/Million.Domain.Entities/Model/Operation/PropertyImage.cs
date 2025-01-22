using System;

namespace Million.Domain.Entities.Model.Operation
{
    [Dapper.Contrib.Extensions.Table("dbo.[PropertyImage]")]
    [Serializable]
    public class PropertyImage
    {
        [Dapper.Contrib.Extensions.Key]
        public int IdPropertyImage { get; set; }
        public int IdProperty { get; set; }
        [Dapper.Contrib.Extensions.Computed]
        public Property Property { get; set; }
        public string File { get; set; }
        public bool Enabled { get; set; }
    }
}
