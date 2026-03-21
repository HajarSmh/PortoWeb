using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WEB_HS.Data;
using WEB_HS.Entities;
using WEB_HS.Shered;

namespace WEB_HS.Repository  
{
    public class CompetenceRepos
    {
        private readonly MyDbContextPortfolio _db;

        public CompetenceRepos(MyDbContextPortfolio context)
        {
            _db = context;
        }


        // Delete
        public void Delete(int id)
        {
            var ob = _db.Competences.Find(id);
            if (ob != null)
            {
                _db.Competences.Remove(ob);
                _db.SaveChanges();
            }
        }

        // Read by Id
        public Competence? GetById(int id)
        {
            return _db.Competences.Find(id);
        }

        public List<Competence> GetAll()
        {
            return _db.Competences.OrderBy(c => c.Titre).ToList();
        }

        public List<Competence> GetAllByNiveau(NiveauCompetence niveauCompetence)
        {
            return _db.Competences
                .Where(c => c.Niveau == (int)niveauCompetence)
                .OrderBy(c => c.Titre)
                .ToList();
        }

        // Create
        public Competence Create(Competence obj)
        {
            _db.Competences.Add(obj);
            _db.SaveChanges();
            return obj;
        }

        // Update
        public Competence Update(Competence obj)
        {
            _db.Competences.Update(obj);
            _db.SaveChanges();
            return obj;
        }
    }
}