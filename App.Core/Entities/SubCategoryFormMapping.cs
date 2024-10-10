using App.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Entities
{
    public class SubCategoryFormMapping : BaseEntity
    {
        public string SubCategoryId { get; set; }
        public string SentenceFormId { get; set; }
        // Navigation properties
        public SubCategory SubCategory { get; set; }
        public SentenceForms SentenceForm { get; set; }
    }
}
