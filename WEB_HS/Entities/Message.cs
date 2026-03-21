using System;
using System.ComponentModel.DataAnnotations;

namespace WEB_HS.Entities
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Le contenu est obligatoire")]
        [StringLength(500, ErrorMessage = "Le commentaire ne peut dépasser 500 caractères")]
        [Display(Name = "Message")]
        public string Contenu { get; set; }

        [Required]
        [Display(Name = "Destinataire")]
        public int PersonneId { get; set; } // Personne qui reçoit le message

        [StringLength(100)]
        [Display(Name = "Nom")]
        public string NomAuteur { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string EmailAuteur { get; set; }

        public DateTime DateCreation { get; set; } = DateTime.Now;

        [Display(Name = "Lu")]
        public bool EstLu { get; set; } = false;

        // Navigation property (une seule relation)
        public virtual Personne Personne { get; set; } // Destinataire seulement
    }
}