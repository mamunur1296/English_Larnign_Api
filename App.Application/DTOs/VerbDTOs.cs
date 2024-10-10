using App.Application.Common;

namespace App.Application.DTOs
{
    public class VerbDTOs : BaseDTOs
    {
        public string Name { get; set; }
        public string BanglaName { get; set; }
        public string BaseForm { get; set; }
        public string ThirdPersonSingular { get; set; }
        public string PastSimple { get; set; }
        public string PastParticiple { get; set; }
        public string PresentParticiple { get; set; }
        public string Gerund { get; set; }
    }
}
