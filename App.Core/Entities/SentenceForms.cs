﻿using App.Domain.Entities.Base;

namespace App.Domain.Entities
{
    public class SentenceForms : BaseEntity
    {
        public string Name { get; set; }
        public bool? isAssaindBySubCatagory { get; set; } = false;
        public virtual ICollection<SentenceFormStructureMapping>? SentenceFormStructureMapping { get; set; } 
    }
}
