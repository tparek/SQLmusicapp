# SQLmusicapp
This program, when finished, is a database for browsing selected artists and info about their albums, tracks. Also there will be a youtube player built-in for listening added tracks. There is a form on the interface for adding albums to database. Form for adding tracks to database is yet to come. 

Overview of the operations to achieve this result:

1) Created a database called "music" in phpMyAdmin page and added tables for albums and tracks

2) Added some data of albums and tracks to the tables

3) Created Visual Studio project and started with creating Forms in Form Designer. 

4) Added code in Form1.cs for following items:
 -DataGridView control (dataGridView1) to display albums and another DataGridView control (dataGridView2) to display tracks.
--PictureBox control (pictureBox1) to display the album cover image.
--Two BindingSource objects (albumBindingSource and tracksBindingSource) are used to bind the data from the database to the DataGridView controls.
--button1_Click: Retrieves all albums from the database using the getAllAlbums method of AlbumsDAO and binds the data to dataGridView1.
--button2_Click: Searches for albums in the database based on the text entered in textBox1 using the searchTitles method of AlbumsDAO and binds the data to dataGridView1.
--dataGridView1_CellClick: Event handler for clicking a cell in dataGridView1. It retrieves the selected album's image URL and displays the corresponding image in pictureBox1. It also binds the tracks of the selected album to dataGridView2.
--button3_Click: Adds a new album to the database using the values entered in txt_album, txt_artist, txt_year, txt_image, and txt_desc TextBox controls. It calls the addOneAlbum method of AlbumsDAO to insert the album into the database.
--button4_Click: Deletes a track from the database based on the selected row in dataGridView2. It retrieves the track ID and calls the deleteTrack method of AlbumsDAO to remove the track from the database.

5) Created AlbumDAO class which contains following methods for interacting with MySQL database:
--getAllAlbums(): Retrieves all albums from the database, along with their associated tracks. It returns a list of Album objects.
--searchTitles(String searchTerm): Searches for albums in the database based on a provided search term. It performs a partial match search on the album title and returns a list of matching Album objects.
--addOneAlbum(Album album): Inserts a new album into the database. It takes an Album object as a parameter and adds the album's details to the "albums" table. It returns the number of rows affected.
--deleteTrack(int trackID): Deletes a track from the database based on the provided track ID. It removes the track from the "tracks" table. It returns the number of affected rows.
--getTracksForAlbum(int albumID): Retrieves all tracks associated with a specific album from the database. It takes the album ID as a parameter and returns a list of Track objects.
--getTracksUsingJoin(int albumID): Retrieves tracks using a join operation between the "tracks" and "albums" tables. It retrieves the track ID, album title, track title, video URL, and lyrics for a given album ID. It returns a list of JObject objects representing the tracks.

6) There are 2 additonal classes: Track.cs and Album.cs which allow music application to store and access information about albums and tracks.

This is a final project for Databases course in Estonian Entrepreneurship University of Applied Sciences (EEK Mainor)
