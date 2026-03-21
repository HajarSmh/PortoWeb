using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEB_HS.Entities
{
    public class Projet
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Le titre est obligatoire")]
        [StringLength(100, ErrorMessage = "Le titre ne peut dépasser 100 caractères")]
        [Display(Name = "Titre du projet")]
        public string Titre { get; set; }

        [Required(ErrorMessage = "La description est obligatoire")]
        [StringLength(1000, ErrorMessage = "La description ne peut dépasser 1000 caractères")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [StringLength(500, ErrorMessage = "Les technologies ne peuvent dépasser 500 caractères")]
        [Display(Name = "Technologies utilisées")]
        public string Technologies { get; set; }

        [Display(Name = "Image du projet")]
        public string ImageUrl { get; set; } = "/images/project-default.jpg";

        [Url(ErrorMessage = "URL invalide")]
        [Display(Name = "Lien de démonstration")]
        public string LienDemo { get; set; }

        [Url(ErrorMessage = "URL invalide")]
        [Display(Name = "Lien du code source")]
        public string LienSource { get; set; }

        [Required(ErrorMessage = "La date est obligatoire")]
        [DataType(DataType.Date)]
        [Display(Name = "Date de création")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateCreation { get; set; } = DateTime.Now;

        // Relation avec Personne 
        [Required]
        [Display(Name = "Propriétaire")]
        public int PersonneId { get; set; }

        [ForeignKey("PersonneId")]
        [Display(Name = "Propriétaire")]
        public virtual Personne Personne { get; set; }

        // Méthodes utilitaires
        [NotMapped]
        [Display(Name = "Année")]
        public int Annee => DateCreation.Year;

        [NotMapped]
        [Display(Name = "Afficher les technologies")]
        public string[] TechnologiesListe =>
            !string.IsNullOrEmpty(Technologies) ?
            Technologies.Split(',', StringSplitOptions.RemoveEmptyEntries) :
            Array.Empty<string>();

        // Pour faciliter l'affichage
        public string GetTechnologieTags()
        {
            if (string.IsNullOrEmpty(Technologies))
                return string.Empty;

            var tags = Technologies.Split(',')
                .Select(t => t.Trim())
                .Where(t => !string.IsNullOrEmpty(t))
                .Select(t => $"<span class='badge bg-info me-1'>{t}</span>");

            return string.Join(" ", tags);
        }
    }
}