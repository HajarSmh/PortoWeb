using Microsoft.EntityFrameworkCore;
using WEB_HS.Data;
using WEB_HS.Entities;
using System.Collections.Generic;
using System.Linq;

namespace WEB_HS.Repository
{
    public interface IProjetRepository
    {
        IEnumerable<Projet> GetAll();
        Projet GetById(int id);
        void Add(Projet projet);
        void Update(Projet projet);
        void Delete(int id);
        bool Exists(int id);
    }

    public class ProjetRepository : IProjetRepository
    {
        private readonly MyDbContextPortfolio _context;

        public ProjetRepository(MyDbContextPortfolio context)
        {
            _context = context;
        }

        public IEnumerable<Projet> GetAll()
        {
            return _context.Projets.OrderByDescending(p => p.DateCreation).ToList();
        }

        public Projet GetById(int id)
        {
            return _context.Projets.Find(id);
        }

        public void Add(Projet projet)
        {
            _context.Projets.Add(projet);
            _context.SaveChanges();
        }

        public void Update(Projet projet)
        {
            _context.Entry(projet).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var projet = _context.Projets.Find(id);
            if (projet != null)
            {
                _context.Projets.Remove(projet);
                _context.SaveChanges();
            }
        }

        public bool Exists(int id)
        {
            return _context.Projets.Any(e => e.Id == id);
        }
    }
}