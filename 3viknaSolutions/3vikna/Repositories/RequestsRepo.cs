using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _3vikna.Models;

namespace _3vikna.Repositories
{
    public class RequestsRepo
    {
        private static List<Requests> m_requests = new List<Requests>();

        public List<Requests> GetAllRequests()
        {
            if (m_requests.Count == 0)
            {
                m_requests.Add(new Requests { ID = 1, MediaName = "Screen 1", YearPublished = "1999" });
                m_requests.Add(new Requests { ID = 2, MediaName = "Scary Movie 2", YearPublished = "2002" });
                m_requests.Add(new Requests { ID = 3, MediaName = "Lion King", YearPublished = "1998" });
                m_requests.Add(new Requests { ID = 4, MediaName = "Catch Me If You Can", YearPublished = "2001" });
            }
            return m_requests;
        }
    }
}