using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _3vikna.Models;

namespace _3vikna.Repositories
{
    public class SubtitlesRepo
    {
        private List<Subtitles> m_subtitles = new List<Subtitles>();
        AppDataContext db = new AppDataContext();
        public List<Subtitles> GetAllSubtitles()
        {
            if (m_subtitles.Count == 0)
            {
                m_subtitles.Add(new Subtitles { ID = 1, MediaNameSub = "Hangover 3", YearPublished = "2012" });
                m_subtitles.Add(new Subtitles { ID = 2, MediaNameSub = "Rocky", YearPublished = "0000" });
                m_subtitles.Add(new Subtitles { ID = 3, MediaNameSub = "The Amzing Spider-Man 2", YearPublished = "2014" });
                m_subtitles.Add(new Subtitles { ID = 4, MediaNameSub = "Hangover 1", YearPublished = "2010" });
            }
            return m_subtitles;
        }

        public IQueryable<Subtitles> GetNewest()
        {
            var result = (from s in db.Subtitles
                          orderby s.DateCreated descending
                          select s).Take(5);
            return result;
        }

        public string getNameById(int id)
        {
            var result = (from s in db.Subtitles
                          where s.ID == id
                          select s.MediaNameSub).SingleOrDefault();
            return result;
        }

        public Subtitles GetByID(int id)
        {
            var results = (from r in db.Subtitles
                           where r.ID == id
                           select r).SingleOrDefault();
            return results;
        }
        
        public string GetFileFromDB(int id)
        {
            var result = (from s in db.Subtitles
                          where s.ID == id
                          select s.File).SingleOrDefault();
            return result;
        }
        public IEnumerable<string> GetCommentByID(int id)
        {
            var result = (from s in db.Subtitles
                          where s.ID == id
                          select s.Comments);
            return result;
        }

        public void UpdateALL(int id, Subtitles sub)
        {
            
        }

        public void AddSubtitle(Subtitles s)
        {
            db.Subtitles.Add(s);
            //m_db.SaveChanges();
        }

        public void UpdateDB(int id, Subtitles sub)
        {
            var prev = (from a in db.Subtitles
                        where a.ID == id
                        select a).SingleOrDefault();

            if (prev != null)
            {
                prev.File = sub.File;
                prev.DateCreated = DateTime.Now;
            }
            else
                return;

            db.SaveChanges();
        }

        public void Save()
        {
            db.SaveChanges();
        }

    }
}