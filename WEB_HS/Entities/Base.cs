using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEB_HS.Entities
{
    public class Base
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime DateCreation { get; set; }
        public int CreePar { get; set; }
        public bool? EstSupperime { get; set; }
        public DateTime? SupperimeA { get; set; }
        public int? SupperimePar { get; set; }

    }
}
