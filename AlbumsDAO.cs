﻿using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseSQLMusicApp
{
    class AlbumsDAO
    {

        string connectionString = "datasource=localhost;port=3306;username=root;password=;database=Music";


        public List<Album> getAllAlbums()
        {
            //start with an empty list
            List<Album> returnThese = new List<Album>();
            // connect to the mysql server

            MySqlConnection connection = new MySqlConnection
                (connectionString);
            connection.Open();

            //define the sql statement to fetch all albums
            MySqlCommand command = new MySqlCommand("SELECT ID, ALBUM_TITLE, ARTIST," +
                "YEAR, IMAGE_NAME, DESCRIPTION FROM ALBUMS", connection);

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Album a = new Album
                    {
                        ID = reader.GetInt32(0),
                        AlbumName = reader.GetString(1),
                        ArtistName = reader.GetString(2),
                        Year = reader.GetInt32(3),
                        ImageURL = reader.GetString(4),
                        Description = reader.GetString(5)


                    };

                    a.Tracks = getTracksForAlbum(a.ID);
                    returnThese.Add(a);
                }
            }

            connection.Close();

            return returnThese;

        }

        public List<Album> searchTitles(String searchTerm)
        {
            //start with an empty list
            List<Album> returnThese = new List<Album>();
            // connect to the mysql server

            MySqlConnection connection = new MySqlConnection
                (connectionString);
            connection.Open();
            String searchWildPhrase = "%" + searchTerm + "%";
            //define the sql statement to fetch all albums
            MySqlCommand command = new MySqlCommand();
            command.CommandText = "SELECT ID, ALBUM_TITLE, ARTIST, " +
                "YEAR, IMAGE_NAME, DESCRIPTION FROM ALBUMS WHERE ALBUM_TITLE LIKE @search";
            command.Parameters.AddWithValue("@search", searchWildPhrase);
            command.Connection = connection;

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Album a = new Album
                    {
                        ID = reader.GetInt32(0),
                        AlbumName = reader.GetString(1),
                        ArtistName = reader.GetString(2),
                        Year = reader.GetInt32(3),
                        ImageURL = reader.GetString(4),
                        Description = reader.GetString(5)

                    };
                    

                    returnThese.Add(a);
                }
            }

            connection.Close();
            return returnThese;



        }

        internal int addOneAlbum(Album album)
        {
            // connect to the mysql server

            MySqlConnection connection = new MySqlConnection
                (connectionString);
            connection.Open();

            //define the sql statement to fetch all albums
            MySqlCommand command = new MySqlCommand("INSERT INTO `albums`(`ALBUM_TITLE`, `ARTIST`, `YEAR`, `IMAGE_NAME`, `DESCRIPTION`) VALUES (@albumtitle,@artist,@year,@imageURL,@description)", connection);

            command.Parameters.AddWithValue("@albumtitle", album.AlbumName);

            command.Parameters.AddWithValue("@artist", album.ArtistName);

            command.Parameters.AddWithValue("@year", album.Year);

            command.Parameters.AddWithValue("@imageURL", album.ImageURL);

            command.Parameters.AddWithValue("@Description", album.Description);




            int newRows = command.ExecuteNonQuery();
            connection.Close();


            return newRows;

        }

        internal int deleteTrack(int trackID)
        {
            // connect to the mysql server

            MySqlConnection connection = new MySqlConnection
                (connectionString);
            connection.Open();

            //define the sql statement to fetch all albums
            MySqlCommand command = new MySqlCommand("DELETE FROM `tracks` WHERE `tracks`.`ID` = @trackID;", connection);

            command.Parameters.AddWithValue("@trackID", trackID);

           

            int result = command.ExecuteNonQuery();
            connection.Close();


            return result;

        }
    

        public List<Track> getTracksForAlbum(int albumID)
        {
            //start with an empty list
            List<Track> returnThese = new List<Track>();
            // connect to the mysql server

            MySqlConnection connection = new MySqlConnection
                (connectionString);
            connection.Open();

            //define the sql statement to fetch all albums
            MySqlCommand command = new MySqlCommand();
            command.CommandText = "SELECT * FROM TRACKS WHERE albums_ID=@albumid";
            command.Parameters.AddWithValue("@albumid", albumID);
            command.Connection = connection;

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Track t = new Track
                    {
                        ID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Number = reader.GetInt32(2),
                        VideoURL = reader.GetString(3),
                        Lyrics = reader.GetString(4),



                    };
                    returnThese.Add(t);


                }
            }
            connection.Close();

            return returnThese;



        }



        public List<JObject> getTracksUsingJoin(int albumID)
        {
            //start with an empty list
            List<JObject> returnThese = new List<JObject>();
            // connect to the mysql server

            MySqlConnection connection = new MySqlConnection
                (connectionString);
            connection.Open();

            //define the sql statement to fetch all albums
            MySqlCommand command = new MySqlCommand();
            command.CommandText = "SELECT  tracks.ID as trackID, albums.ALBUM_TITLE, `track_title`, `video_url`, `lyrics` FROM `tracks` JOIN albums ON albums_ID=albums.ID WHERE albums_ID=@albumid";
            command.Parameters.AddWithValue("@albumid", albumID);
            command.Connection = connection;

            using (MySqlDataReader reader = command.ExecuteReader())
            {

                while (reader.Read())
                {

                    JObject newTrack = new JObject();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        newTrack.Add(reader.GetName(i).ToString(), reader.GetValue(i).ToString());
                    }

                    returnThese.Add(newTrack);

                
                   
                }
            }
            connection.Close();

            return returnThese;



        }

    }
}