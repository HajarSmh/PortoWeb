using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEB_HS.Entities
{
    public enum RoleUtilisateur
    {
        Visiteur = 0,
        Utilisateur = 1,
        Admin = 2
    }

    public class Personne
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom est obligatoire")]
        [StringLength(100)]
        [Display(Name = "Nom ")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Le prenom est obligatoire")]
        [StringLength(100)]
        [Display(Name = "Prenom")]
        public string Prenom { get; set; } = "";


        [Required(ErrorMessage = "L'email est obligatoire")]
        [EmailAddress(ErrorMessage = "Format d'email invalide")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Le mot de passe est obligatoire")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Le mot de passe doit avoir au moins 6 caractères")]
        public string MotDePasse { get; set; }

        [Display(Name = "Photo de profil")]
        public string? Photo { get; set; } = "/images/default-avatar.jpg";

        [StringLength(1000)]
        [Display(Name = "Biographie")]
        public string? Bio { get; set; }

        [Display(Name = "Lien LinkedIn")]
        public string? LinkedIn { get; set; }

        [Display(Name = "Lien GitHub")]
        public string? GitHub { get; set; }

        [Display(Name = "Titre professionnel")]
        [StringLength(100)]
        public string? Titre { get; set; }

        public RoleUtilisateur Role { get; set; } = RoleUtilisateur.Utilisateur;
        public DateTime DateInscription { get; set; } = DateTime.Now;

        public virtual ICollection<Competence> Competences { get; set; }
        public virtual ICollection<Projet> Projets { get; set; }
        public virtual ICollection<Message> MessagesRecus { get; set; }
        public Personne()
        {
            Competences = new List<Competence>();
            Projets = new List<Projet>();
            MessagesRecus = new List<Message>();
        }
    }
}