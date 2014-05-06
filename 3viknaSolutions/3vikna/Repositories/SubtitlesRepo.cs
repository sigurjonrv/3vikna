using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _3vikna.Models;

namespace _3vikna.Repositories
{
    public class SubtitlesRepo
    {
        private static List<Subtitles> m_subtitles = new List<Subtitles>();

        public List<Subtitles> GetAllSubtitles()
        {
            if (m_subtitles.Count == 0)
            {
                m_subtitles.Add(new Subtitles { ID = 1, MediaName = "Hangover 3", YearPublished = "2013" });
                m_subtitles.Add(new Subtitles { ID = 1, MediaName = "Hangover 3", YearPublished = "2013" });
                m_subtitles.Add(new Subtitles { ID = 1, MediaName = "Hangover 3", YearPublished = "2013" });
                m_subtitles.Add(new Subtitles { ID = 1, MediaName = "Hangover 3", YearPublished = "2013" });
            }
            return m_subtitles;
        }
    }
}