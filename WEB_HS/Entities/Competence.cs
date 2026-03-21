using System.ComponentModel.DataAnnotations.Schema;
using WEB_HS.Shered;

namespace WEB_HS.Entities;

[Table("Competence")]
public class Competence : Base
{
    public int PersonneId { get; set; }
    public string Titre { get; set; } = string.Empty;
    public string? Description { get; set; } // Ajoutez cette propriété
    public int Niveau { get; set; }
    public string? Categorie { get; set; }

    public virtual Personne Personne { get; set; }
}
