using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _3vikna.Models;

namespace _3vikna.Repositories
{
    public class RequestsRepo
    {
        private List<Requests> m_requests = new List<Requests>();
        AppDataContext db = new AppDataContext();
        public List<Requests> GetAllRequests()
        {
            if (m_requests.Count == 0)
            {
                m_requests.Add(new Requests { ID = 1, MediaName = "Screen 1", YearPublished = "1999"/*, IsFinished = true*/});
                m_requests.Add(new Requests { ID = 2, MediaName = "Scary Movie 2", YearPublished = "2002"/*, IsFinished = true */});
                m_requests.Add(new Requests { ID = 3, MediaName = "Lion King", YearPublished = "1998" /*,IsFinished = true"*/ });
                m_requests.Add(new Requests { ID = 4, MediaName = "Catch Me If You Can", YearPublished = "2001" /*, IsFinished = true*/ });
                m_requests.Add(new Requests { ID = 5, MediaName = "X-Men: Days of Future Past", YearPublished = "2014"/*, IsFinished = false*/ });
                m_requests.Add(new Requests { ID = 6, MediaName = "Bad Neighbours", YearPublished = "2014"/*, IsFinished = false*/ });
                m_requests.Add(new Requests { ID = 7, MediaName = "Fed Up", YearPublished = "2014"/*, IsFinished = false */});
            }
            return m_requests;
        }

        /*public IEnumerable<Requests> GetAllUnFinishedRequests()
        {
            var result = (from req in m_requests
                          where req.IsFinished == false
                          orderby req.MediaName ascending
                          select req);
            return result;
        }*/

        public IEnumerable<Requests> GetAllByDate()
        {
            var results = (from r in db.Requests
                           orderby r.Date ascending
                           select r).Take(5);
            return results;
        }

        public IQueryable<string> GetCommentsByID()
        {
            var result = (from c in db.Comment
                          orderby c.UserName
                          select c.UserName);
            return result;
        }
        //SRV
        public Requests GetByID(int id)
        {
            var results = (from r in db.Requests
                           where r.ID == id
                           select r).SingleOrDefault();
            return results;
        }

        public IEnumerable<Requests> GetUpvotes()
        {
            var result = from c in db.Requests
                         orderby c.UpvoteID ascending
                         select c;
            return result;
        }

        public void AddRequest(Requests s)
        {
            db.Requests.Add(s);
            //m_db.SaveChanges();
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}