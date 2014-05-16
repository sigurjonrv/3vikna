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

        public IQueryable<Subtitles> GetNewestSubtitles()
        {
            var result = (from s in db.Subtitles
                          where s.IsFinished == true
                          orderby s.DateCreated descending
                          select s).Take(5);
            return result;
        }

        public string GetSubtitleNameById(int? id)
        {
            var result = (from s in db.Subtitles
                          where s.ID == id
                          select s.MediaNameSub).SingleOrDefault();
            return result;
        }
        
        public IEnumerable<Subtitles> NotFinishedSubtitles()
        {
            var result = (from s in db.Subtitles
                          where s.IsFinished == false
                          orderby s.DateCreated descending
                          select s).Take(5);
            return result;
        }

        public Subtitles GetSubtitleByID(int? id)
        {
            var results = (from r in db.Subtitles
                           where r.ID == id
                           select r).SingleOrDefault();
            return results;
        }
        
        public string GetSubtitleFileFromDB(int? id)
        {
            var result = (from s in db.Subtitles
                          where s.ID == id
                          select s.File).SingleOrDefault();
            return result;
        }
        /*public IEnumerable<string> GetCommentByID(int id)
        {
            var result = (from s in db.Subtitles
                          where s.ID == id
                          select s.Comments);
            return result;
        }*/

        public void UpdateSubtitleDB(int id, Subtitles sub)
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

        public void AddSubtitle(Subtitles s)
        {
            db.Subtitles.Add(s);
            //m_db.SaveChanges();
        }

        public void Save()
        {
            db.SaveChanges();
        }

    }
}