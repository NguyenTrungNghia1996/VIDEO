using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;


namespace VIDEO
{
    public partial class Form1 : Form
    {
        IFirebaseConfig config = new FirebaseConfig()
        {
            AuthSecret = "QnH4I1pg4HVvkouVT6oYYwGfxRsRuZ2Ltjab87Ks",
            BasePath = "https://video-ea5fa.firebaseio.com/"

        };
        IFirebaseClient client;
        public Form1()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void listFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            MediaFile file = listFile.SelectedItem as MediaFile;
            if(file != null){
                axWindowsMediaPlayer1.URL = file.Path;
                lblName.Text = "Name: "+file.FileName;
                //axWindowsMediaPlayer1.Ctlcontrols.play();
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog { Multiselect = true, ValidateNames = true, Filter = "MP4|*.mp4|MKV|*.mkv" }) {
                if (ofd.ShowDialog() == DialogResult.OK) {
                    List<MediaFile> files = new List<MediaFile>();
                    foreach (string fileName in ofd.FileNames) {
                        FileInfo fi = new FileInfo(fileName);
                        files.Add(new MediaFile() { FileName = Path.GetFileNameWithoutExtension(fi.FullName), Path = fi.FullName });
                    }
                    listFile.DataSource = files;
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listFile.ValueMember = "Path";
            listFile.DisplayMember = "FileName";
            client = new FireSharp.FirebaseClient(config);
            if (client != null)
            {
                MessageBox.Show("Kết nối Thành Công ");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string TNS = axWindowsMediaPlayer1.Ctlcontrols.currentPositionString;
            lblTime.Text = "Time: " +TNS;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            double TND = axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
            double TB = TND - 1;
            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = TB;
            axWindowsMediaPlayer1.Ctlcontrols.play();
            axWindowsMediaPlayer1.Ctlcontrols.pause();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            double TND = axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
            double TB = TND + 1;
            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = TB;
            axWindowsMediaPlayer1.Ctlcontrols.play();
            axWindowsMediaPlayer1.Ctlcontrols.pause();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.pause();
        }

        private void btnMarker_Click(object sender, EventArgs e)
        {

        }
       /** private async void btnSave_Click(object sender, EventArgs e)
       {
           var data = new Data
            {
               ID = txtID.Text,
               Name = txtName.Text
            };
            SetResponse response = await client.SetTaskAsync("INFO/", data);
            Data result = response.ResultAs<Data>();
            MessageBox.Show("Data Insert");
        }**/
    }
}
