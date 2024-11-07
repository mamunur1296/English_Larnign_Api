using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.OthersDto
{
    public class InputSentenceItem
    {
        public string EnglishSentences { get; set; }
        public string BanglaSentences { get; set; }
        public string SubCatagoryName { get; set; }
        public string FormName { get; set; }
        public string? SubCatagoryID { get; set; } // Added for binding
        public string? FormsId { get; set; } // Added for binding
        public int SrNumber { get; set; }
    }
}
