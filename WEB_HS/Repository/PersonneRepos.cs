using System;
using System.Collections.Generic;
using System.Linq;
using WEB_HS.Entities;

namespace WEB_HS.Repository
{
    public class PersonneRepos
    {
        private readonly Data.MyDbContextPortfolio _db;

        public PersonneRepos(Data.MyDbContextPortfolio context)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Personne Get(int id)
        {
            var personne = new Personne();
            personne.Id = id;
            personne.Nom = "SAMOUH";
            personne.Prenom = "HAJAR";
            personne.DateInscription = DateTime.Now;

            return personne;
        }

        public bool Delete(int id)
        {
            if (id <= 0)
            {
                return false;
            }
            return true;
        }

        public bool Update(Personne personne)
        {
            if (personne.Id <= 0)
            {
                return false;
            }
            return true;
        }

        public bool Create(Personne personne)
        {
            _db.Personnes.Add(personne);
            _db.SaveChanges();
            return true;
        }

        public List<Personne> GetAll()
        {
                var liste = _db.Personnes.ToList();
            return liste;
        }
    }
}
