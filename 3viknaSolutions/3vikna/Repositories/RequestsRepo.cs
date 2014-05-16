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

        public IEnumerable<Requests> GetAllByDate()
        {
            var results = (from r in db.Requests
                           orderby r.Date ascending
                           select r).Take(5);
            return results;
        }

        /*public IQueryable<string> GetCommentsByID()
        {
            var result = (from c in db.Comment
                          orderby c.UserName
                          select c.UserName);
            return result;
        }*/
        //SRV
        public Requests GetRequestsByID(int id)
        {
            var results = (from r in db.Requests
                           where r.ID == id
                           select r).SingleOrDefault();
            return results;
        }

        public IEnumerable<string> UserHasLiked(int? id)
        {
            var result = (from r in db.Upvote
                          where r.ReqID == id
                          select r.UserName);
            return result;
        }

        public void AddUpvotes(int id, string strUser)
        {
            Upvote item = new Upvote();
            item.ReqID = id;
            item.UserName = strUser;
            db.Upvote.Add(item);
            db.SaveChanges();
        }

        public IEnumerable<Requests> SortByUpvotes()
        {
            var result = (from r in db.Requests
                          orderby r.UpvoteID descending
                          select r);
            return result;
        }

        public IEnumerable<Requests> GetUpvotes()
        {
            var result = from c in db.Requests
                         orderby c.UpvoteID ascending
                         select c;
            return result;
        }

        public void UpdateRequestDB(int id, Requests req)
        {
            var prev = (from a in db.Requests
                        where a.ID == id
                        select a).SingleOrDefault();

            if (prev != null)
            {
                prev.Date = DateTime.Now;
            }
            else
                return;

            db.SaveChanges();
        }

        public void AddRequest(Requests s)
        {
            db.Requests.Add(s);
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}