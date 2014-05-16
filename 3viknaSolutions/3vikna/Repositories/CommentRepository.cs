using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _3vikna.Models;

namespace _3vikna.Repositories
{
    public class CommentRepository
    {
        private static CommentRepository _instance;
        //private List<Requests> _instance = new List<Requests>();
        AppDataContext db = new AppDataContext();

        public static CommentRepository Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CommentRepository();
                return _instance;
            }
        }

        public List<Comment> m_comments = null;

        public CommentRepository()
        {
            this.m_comments = new List<Comment>();
            Comment commment1 = new Comment { ID = 1, CommentText = "Great Video!", UserName = "Patrekur" };
            Comment commment2 = new Comment { ID = 2, CommentText = "Amazing content!", UserName = "Siggi" };
            this.m_comments.Add(commment1);
            this.m_comments.Add(commment2);

        }
        public IEnumerable<Comment> GetComments(int? id)
        {
            var result = from c in db.Comment
                         where c.subtitleID == id
                         select c;
            return result;
        }

        public IEnumerable<Comment> Gettingcomments(int id)
        {
            var result = (from c in db.Comment
                          where c.subtitleID == id
                          select c);
            return result;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void AddComment(Comment c)
        {
            //int newID = 1;
            //if (db.Comment.Count() > 0)
            //{
            //    newID = db.Comment.Max(x => x.ID) + 1;
            //}
            //c.ID = newID;

            db.Comment.Add(c);
        }
    }
}