using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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
        double timeStart, timeStop;
        string timeStartSring= "00:00", timeStopString, stop;
        int ID;
        DataTable dt = new DataTable();
        public Form1()
        {
            InitializeComponent();
            timer1.Start();
        }

        private async void listFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            MediaFile file = listFile.SelectedItem as MediaFile;
            if(file != null){
                axWindowsMediaPlayer1.URL = file.Path;
                lblName.Text = file.FileName;
                //axWindowsMediaPlayer1.Ctlcontrols.play();
                timeStart = 0;
                timeStop = 0;
                timeStartSring = "00:00";
                findbyName(file.FileName);
                //FirebaseResponse response = await client.GetTaskAsync(file.FileName);
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
            dt.Columns.Add("ID");
            dt.Columns.Add("Start");
            dt.Columns.Add("Stop");
            dt.Columns.Add("SEC");
            dataGridView1.DataSource = dt;
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
        int indexRow;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            indexRow = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[indexRow];
            double sec = Convert.ToDouble(row.Cells[3].Value.ToString());
            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = sec;
            axWindowsMediaPlayer1.Ctlcontrols.play();
            axWindowsMediaPlayer1.Ctlcontrols.pause();
            stop = row.Cells[2].Value.ToString();
            ID = Convert.ToInt32(row.Cells[0].Value);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            FirebaseResponse response1 = await client.DeleteTaskAsync("VIDEO/" + lblName.Text + "/" + ID);
            MessageBox.Show("Delete marker of ID: " + ID);
            findbyName(lblName.Text);
        }

        private async void btnEDIT_Click(object sender, EventArgs e)
        {
            string newStop = axWindowsMediaPlayer1.Ctlcontrols.currentPositionString;
            if (stop != newStop)
            {
                FirebaseResponse response1 = await client.GetTaskAsync("VIDEO/" + lblName.Text + "/" + ID);
                Data obj1 = response1.ResultAs<Data>();
                var data = new Data
                {
                    ID = ID.ToString(),
                    Start = obj1.Start,
                    Stop = newStop,
                    StopSec = axWindowsMediaPlayer1.Ctlcontrols.currentPosition.ToString()
                };
                SetResponse response = await client.SetTaskAsync("VIDEO/" + lblName.Text + "/" + ID, data);
                Data result = response.ResultAs<Data>();

                FirebaseResponse response2 = await client.GetTaskAsync("VIDEO/" + lblName.Text + "/" + (ID+1));
                Data obj2 = response1.ResultAs<Data>();
                var data1 = new Data
                {
                    ID = (ID+1).ToString(),
                    Start = newStop,
                    Stop = obj2.Stop,
                    StopSec = obj2.StopSec
                };
                SetResponse response3 = await client.SetTaskAsync("VIDEO/" + lblName.Text + "/" + (ID + 1), data1);
                Data result2 = response.ResultAs<Data>();
                findbyName(lblName.Text);
            }
        }

        private async void btnMarker_Click(object sender, EventArgs e)
        {
            timeStop = axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
            if (timeStop > timeStart) {
                timeStopString = axWindowsMediaPlayer1.Ctlcontrols.currentPositionString;
                Caunter_Class get;
                try
                {
                    FirebaseResponse response1 = await client.GetTaskAsync("Counter/" + lblName.Text);
                    get = response1.ResultAs<Caunter_Class>();
                }
                catch (NullReferenceException)
                {
                    var obj2 = new Caunter_Class
                    {
                        cnt = Convert.ToInt32(0).ToString()
                    };
                    SetResponse response3 = await client.SetTaskAsync("Counter/" + lblName.Text, obj2);
                }
                finally
                {
                    FirebaseResponse response1 = await client.GetTaskAsync("Counter/" + lblName.Text);
                    get = response1.ResultAs<Caunter_Class>();
                }
                var data = new Data
                {   
                    ID = (Convert.ToInt32(get.cnt) + 1).ToString(),
                    Start = timeStartSring,
                    Stop = timeStopString,
                    StopSec = timeStop.ToString()
                };
                var obj = new Caunter_Class
                {
                    cnt = (Convert.ToInt32(get.cnt) + 1).ToString()
                };
                SetResponse response = await client.SetTaskAsync("VIDEO/"+lblName.Text+"/"+(Convert.ToInt32(get.cnt)+1).ToString(), data);
                Data result = response.ResultAs<Data>();
                SetResponse response2 = await client.SetTaskAsync("Counter/" + lblName.Text, obj);
                findbyName(lblName.Text);
                timeStartSring = timeStopString;
                timeStart = timeStop;
            }
           
        }
        private async void findbyName(string Name) {
            int i, cnt;
            dt.Rows.Clear();
            try
            {
                FirebaseResponse response = await client.GetTaskAsync("Counter/" + Name);
                Caunter_Class obj = response.ResultAs<Caunter_Class>();
                cnt = Convert.ToInt32(obj.cnt);
            }
            catch (NullReferenceException)
            {
                cnt = 0;
            }
            for (i = 0; i <= cnt; i++) {
                try
                {
                    FirebaseResponse response = await client.GetTaskAsync("VIDEO/" + Name + "/" + i);
                    Data obj1 = response.ResultAs<Data>();
                    DataRow row = dt.NewRow();
                    row["ID"] = obj1.ID;
                    row["Start"] = obj1.Start;
                    row["Stop"] = obj1.Stop;
                    row["SEC"] = obj1.StopSec;
                    dt.Rows.Add(row);
                }
                catch { 
                }
            }
        }
        
    }
}
