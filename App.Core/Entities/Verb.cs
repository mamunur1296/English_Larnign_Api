using App.Domain.Entities.Base;

namespace App.Domain.Entities
{
    public class Verb : BaseEntity
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
