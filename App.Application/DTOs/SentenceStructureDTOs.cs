﻿using App.Application.Common;

namespace App.Application.DTOs
{
    public class SentenceStructureDTOs : BaseDTOs
    {
        public string BanglaSentence { get; set; }
        public string EnglistSentence { get; set; }
        public string ?SubCatagoryID { get; set; }
        public string? FormsId { get; set; }
        public bool? isAssaindByforms { get; set; } = false;
    }
}
