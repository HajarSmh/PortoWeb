using Microsoft.EntityFrameworkCore;
using WEB_HS.Data;
using WEB_HS.Entities;
using System.Collections.Generic;
using System.Linq;

namespace WEB_HS.Services
{
    public class ProjetService
    {
        private readonly MyDbContextPortfolio _context;

        public ProjetService(MyDbContextPortfolio context)
        {
            _context = context;
        }

        public List<Projet> GetAllProjets()
        {
            return _context.Projets.OrderByDescending(p => p.DateCreation).ToList();
        }

        public Projet GetProjetById(int id)
        {
            return _context.Projets.Find(id);
        }

        public void CreateProjet(Projet projet)
        {
            _context.Projets.Add(projet);
            _context.SaveChanges();
        }

        public void UpdateProjet(Projet projet)
        {
            _context.Entry(projet).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteProjet(int id)
        {
            var projet = _context.Projets.Find(id);
            if (projet != null)
            {
                _context.Projets.Remove(projet);
                _context.SaveChanges();
            }
        }

        public bool ProjetExists(int id)
        {
            return _context.Projets.Any(e => e.Id == id);
        }
    }
}