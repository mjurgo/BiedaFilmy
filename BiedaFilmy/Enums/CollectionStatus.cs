using System.ComponentModel.DataAnnotations;

namespace BiedaFilmy.Enums
{
    public enum CollectionStatus
    {
        [Display(Name = "Nieobejrzany")]
        NotSeen,
        [Display(Name = "Obejrzany")]
        Seen,
        [Display(Name = "Chcę obejrzeć")]
        WantToSee,
    }
}
