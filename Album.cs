﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseSQLMusicApp
{
    class Album
    {
        public int ID { get; set; }
        public String AlbumName { get; set; }
        public String ArtistName { get; set; }
        public int Year { get; set; }
        public String ImageURL { get; set; }
        public String Description { get; set; }
        // later make a List<Track> songs
        public List<Track> Tracks { get; set; }
    }
}
