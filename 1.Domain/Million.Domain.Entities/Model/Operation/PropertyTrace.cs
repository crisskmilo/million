using System;

namespace Million.Domain.Entities.Model.Operation
{
    [Dapper.Contrib.Extensions.Table("dbo.[PropertyTrace]")]
    [Serializable]
    public class PropertyTrace
    {
        [Dapper.Contrib.Extensions.Key]
        public int IdPropertyTrace { get; set; }
        public DateTime? DateSale { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public decimal Tax { get; set; }
        public int IdProperty { get; set; }
        [Dapper.Contrib.Extensions.Computed]
        public Property Property { get; set; }
    }
}
