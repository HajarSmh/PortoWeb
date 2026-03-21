using Microsoft.EntityFrameworkCore;
using WEB_HS.Data;
using WEB_HS.Entities;
using System.Linq;

namespace WEB_HS.Services
{
    public class ProfilService
    {
        private readonly MyDbContextPortfolio _context;

        public ProfilService(MyDbContextPortfolio context)
        {
            _context = context;
        }

        // Récupérer tous les profils publics
        public List<Personne> GetAllProfils()
        {
            return _context.Personnes
                .Include(p => p.Competences)
                .Include(p => p.Projets)
                .Where(p => p.Role != RoleUtilisateur.Admin)
                .OrderByDescending(p => p.DateInscription)
                .ToList();
        }

        // Récupérer un profil par ID
        public Personne GetProfilById(int id)
        {
            return _context.Personnes
                .Include(p => p.Competences)
                .Include(p => p.Projets)
                .Include(p => p.MessagesRecus)
                .FirstOrDefault(p => p.Id == id);
        }

        // Mettre à jour le profil
        public void UpdateProfil(Personne personne)
        {
            _context.Entry(personne).State = EntityState.Modified;
            _context.SaveChanges();
        }

        // Ajouter un commentaire
        public void AddCommentaire(Message commentaire)
        {
            _context.Messages.Add(commentaire);
            _context.SaveChanges();
        }
    }
}