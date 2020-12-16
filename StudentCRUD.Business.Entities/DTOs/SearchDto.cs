using System;
using System.Collections.Generic;
using System.Text;

namespace StudentCRUD.Business.Entities.DTOs {
    public class SearchDto {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SearchTerms { get; set; }
        public string SortField { get; set; }
        public bool SortDesc { get; set; }

    }
}
