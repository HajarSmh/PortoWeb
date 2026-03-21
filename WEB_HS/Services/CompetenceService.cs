using Microsoft.EntityFrameworkCore;
using WEB_HS.Data;
using WEB_HS.Entities;
using System.Collections.Generic;
using System.Linq;

namespace WEB_HS.Services
{
    public class CompetenceService
    {
        private readonly MyDbContextPortfolio _context;

        public CompetenceService(MyDbContextPortfolio context)
        {
            _context = context;
        }

        public List<Competence> GetAllCompetences()
        {
            return _context.Competences.OrderBy(c => c.Titre).ToList();
        }

        public Competence GetCompetenceById(int id)
        {
            return _context.Competences.Find(id);
        }

        public void CreateCompetence(Competence competence)
        {
            _context.Competences.Add(competence);
            _context.SaveChanges();
        }

        public void UpdateCompetence(Competence competence)
        {
            _context.Entry(competence).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteCompetence(int id)
        {
            var competence = _context.Competences.Find(id);
            if (competence != null)
            {
                _context.Competences.Remove(competence);
                _context.SaveChanges();
            }
        }

        public bool CompetenceExists(int id)
        {
            return _context.Competences.Any(e => e.Id == id);
        }
    }
}