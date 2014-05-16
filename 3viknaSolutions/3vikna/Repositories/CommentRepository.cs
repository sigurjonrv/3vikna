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

        public IEnumerable<Comment> GetCommentsById(int? id)
        {
            var result = from c in db.Comment
                         where c.subtitleID == id
                         select c;
            return result;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void AddComment(Comment c)
        {
            db.Comment.Add(c);
        }
    }
}