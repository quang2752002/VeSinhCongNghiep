﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class PagedRequestDTO
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public string? SearchTerm { get; set; }
        public string? SortBy { get; set; }
        public bool? Descending { get; set; } = true;
    }
}
