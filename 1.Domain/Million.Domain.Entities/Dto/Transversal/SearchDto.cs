using System.Collections.Generic;

namespace Million.Domain.Entities.Dto.Transversal
{
    public class SearchDto
    {
        public string Page { get; set; }

        public string Offset { get; set; }

        public int? PageSize { get; set; }

        public string ObjectId { get; set; }

        public string OrderByField { get; set; }

        public bool OrderByAscending { get; set; }

        public Dictionary<string, string[]> Filters { get; set; }
    }
}
