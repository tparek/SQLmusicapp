using Microsoft.Web.WebView2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseSQLMusicApp
{
    public partial class Form1 : Form

    {
        BindingSource albumBindingSource =new BindingSource();
        BindingSource tracksBindingSource = new BindingSource();

        List<Album> albums = new List<Album>();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AlbumsDAO albumsDAO = new AlbumsDAO();
           

            //connect the list to grid view control
            albums= albumsDAO.getAllAlbums();
            albumBindingSource.DataSource = albums;
            dataGridView1.DataSource = albumBindingSource;
            
            pictureBox1.Load("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTmkwokXYS9cvOL87q7P8pjLs6gp2A1FdgBfQ&usqp=CAU");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            {
                AlbumsDAO albumsDAO = new AlbumsDAO();


                //connect the list to grid view control
                albumBindingSource.DataSource = albumsDAO.searchTitles
                    (textBox1.Text);
                dataGridView1.DataSource = albumBindingSource;

                
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            //get the row number clicked

            int rowClicked = dataGridView.CurrentRow.Index;
            //MessageBox.Show("You clicked row" + rowClicked);

            String ImageURL = dataGridView.Rows[rowClicked].Cells[4].Value.ToString();
            // MessageBox.Show("URL=" +imageURL);
            pictureBox1.Load(ImageURL);


            tracksBindingSource.DataSource = albums[rowClicked].Tracks;
            dataGridView2.DataSource = tracksBindingSource;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //add a new item to the database
            Album album = new Album
            {
                AlbumName = txt_album.Text,
                ArtistName = txt_artist.Text,
                Year = Int32.Parse(txt_year.Text),
                ImageURL = txt_image.Text,
                Description = txt_desc.Text
            };

            AlbumsDAO albumsDAO = new AlbumsDAO();
            int result = albumsDAO.addOneAlbum(album);
            MessageBox.Show(result + "new row(s) inserted");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //get the row number clicked

            int rowClicked = dataGridView2.CurrentRow.Index;
            MessageBox.Show("You clicked row" + rowClicked);
            int trackID = (int)dataGridView2.Rows[rowClicked].Cells[0].Value;
            MessageBox.Show("ID of track" + trackID);

            AlbumsDAO albumsDao = new AlbumsDAO();
            int result = albumsDao.deleteTrack(trackID);

            MessageBox.Show("Result" + result);
            dataGridView2.DataSource = null;
            albums = albumsDao.getAllAlbums();
        }
        
    }
}
