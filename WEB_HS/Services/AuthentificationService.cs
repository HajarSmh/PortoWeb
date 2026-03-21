using WEB_HS.Data;
using WEB_HS.Entities;
using WEB_HS.Models;
using System.Security.Cryptography;
using System.Text;

namespace WEB_HS.Services
{
    public class AuthentificationService
    {
        private readonly MyDbContextPortfolio _context;

        public AuthentificationService(MyDbContextPortfolio context)
        {
            _context = context;
        }

        // Inscription
        public Personne Register(FormulaireCreationViewModel model)
        {
            // Vérifier si l'email existe déjà
            if (_context.Personnes.Any(p => p.Email == model.Email))
            {
                return null;
            }

            var personne = new Personne
            {
                Nom = model.Nom,
                Prenom = model.Prenom,
                Email = model.Email,
                MotDePasse = HashPassword(model.Password),
                Bio = model.Bio,
                Titre = model.Titre,
                Role = RoleUtilisateur.Utilisateur,
                DateInscription = DateTime.Now
            };

            _context.Personnes.Add(personne);
            _context.SaveChanges();

            return personne;
        }

        // Connexion
        public Personne Login(string email, string password)
        {
            var hashedPassword = HashPassword(password);
            return _context.Personnes
                .FirstOrDefault(p => p.Email == email && p.MotDePasse == hashedPassword);
        }

        // Hash password
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}