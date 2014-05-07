﻿using System;
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
                m_subtitles.Add(new Subtitles { ID = 1, MediaName = "Hangover 3", YearPublished = "2012" });
                m_subtitles.Add(new Subtitles { ID = 2, MediaName = "Rocky", YearPublished = "0000" });
                m_subtitles.Add(new Subtitles { ID = 3, MediaName = "The Amzing Spider-Man 2", YearPublished = "2014" });
                m_subtitles.Add(new Subtitles { ID = 4, MediaName = "Hangover 1", YearPublished = "2010" });
            }
            return m_subtitles;
        }
    }
}