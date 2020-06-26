using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;

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
        string timeStartSring= "00:00", timeStopString ;
        int ID, stop;
        DataTable dt = new DataTable();
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
            dt.Columns.Add("FileName");
            dt.Columns.Add("Stop-time-[ms]");
            dt.Columns.Add("MainCategory");
            dt.Columns.Add("SubCategory1");
            dt.Columns.Add("SubCategory2");
            dt.Columns.Add("SubCategory3");
            dt.Columns.Add("SubCategory4");
            dt.Columns.Add("SubCategory5");
            dt.Columns.Add("SubCategory6");
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Width = 40;
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[2].Width = 110;
            dataGridView1.Columns[3].Width = 135;
            dataGridView1.Columns[4].Width = 135;
            dataGridView1.Columns[5].Width = 135;
            dataGridView1.Columns[6].Width = 135;
            dataGridView1.Columns[7].Width = 135;
            dataGridView1.Columns[8].Width = 135;
            dataGridView1.Columns[9].Width = 135;
            cbMain.Items.Add("Static");
            cbMain.Items.Add("Pan");
            cbMain.Items.Add("Tilt");
            cbMain.Items.Add("Dolly");
            cbMain.Items.Add("Truck");
            cbMain.Items.Add("Pedestal");
            cbMain.Items.Add("Zoom");
            cbMain.Items.Add("Roll");
            cbSub1.Enabled = false;
            cbSub2.Enabled = false;
            cbSub3.Enabled = false;
            cbSub4.Enabled = false;
            cbSub5.Enabled = false;
            cbSub6.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string TNS = Convert.ToInt32(axWindowsMediaPlayer1.Ctlcontrols.currentPosition * 1000).ToString();
            lblTime.Text = "Time: " +TNS;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            double TND = axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
            double TB = TND - 0.03;
            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = TB;
            axWindowsMediaPlayer1.Ctlcontrols.play();
            axWindowsMediaPlayer1.Ctlcontrols.pause();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            double TND = axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
            double TB = TND + 0.03;
            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = TB;
            axWindowsMediaPlayer1.Ctlcontrols.play();
            axWindowsMediaPlayer1.Ctlcontrols.pause();
        }
        int indexRow;
        private async void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            indexRow = e.RowIndex;
            DataGridViewRow row;
            try {  row = dataGridView1.Rows[indexRow]; }
            catch {  row = dataGridView1.Rows[0]; }
            int sec = (Convert.ToInt32(row.Cells[2].Value))/1000;
            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = sec;
            axWindowsMediaPlayer1.Ctlcontrols.play();
            axWindowsMediaPlayer1.Ctlcontrols.pause();
            stop = Convert.ToInt32(row.Cells[2].Value);
            ID = Convert.ToInt32(row.Cells[0].Value);
            FirebaseResponse response1 = await client.GetTaskAsync("VIDEO/" + lblName.Text + "/" + ID);
            Data obj1 = response1.ResultAs<Data>();
            cbMain.SelectedIndex = cbMain.FindStringExact(obj1.Main);
            cbSub1.SelectedIndex = cbSub1.FindStringExact(obj1.Sub1);
            cbSub2.SelectedIndex = cbSub2.FindStringExact(obj1.Sub2);
            cbSub3.SelectedIndex = cbSub3.FindStringExact(obj1.Sub3);
            cbSub4.SelectedIndex = cbSub4.FindStringExact(obj1.Sub4);
            cbSub5.SelectedIndex = cbSub5.FindStringExact(obj1.Sub5);
            cbSub6.SelectedIndex = cbSub6.FindStringExact(obj1.Sub6);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            int t = 0;
             try
             {
                 FirebaseResponse response = await client.GetTaskAsync("Counter/" + lblName.Text);
                 Caunter_Class get = response.ResultAs<Caunter_Class>();
                 var obj = new Caunter_Class
                 {
                     cnt = (Convert.ToInt32(get.cnt)-1).ToString()
                 };
                 SetResponse response3 = await client.SetTaskAsync("Counter/" + lblName.Text, obj);
                 FirebaseResponse response1 = await client.DeleteTaskAsync("VIDEO/" + lblName.Text + "/" + ID);
            }
             catch { }
           
            findbyName(lblName.Text);
        }

        private void cbMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbSub1.Items.Clear();
            cbSub2.Items.Clear();
            cbSub3.Items.Clear();
            cbSub4.Items.Clear();
            cbSub5.Items.Clear();
            cbSub6.Items.Clear(); 
            cbSub1.Enabled = true;
            cbSub2.Enabled = true;
            cbSub3.Enabled = true;
            cbSub4.Enabled = true;
            cbSub5.Enabled = true;
            cbSub6.Enabled = true;
            if (cbMain.SelectedItem == "Static")
            {
                cbSub1.Items.Add("Handheld");
                cbSub1.Items.Add("Tripod");

                cbSub2.Items.Add("Follow");
                cbSub2.Items.Add("NoFollow");

                cbSub3.Items.Add("InCar");
                cbSub3.Items.Add("CarRig");
                cbSub4.Enabled = false;
                cbSub5.Enabled = false;
                cbSub6.Enabled = false;
            }
            if (cbMain.SelectedItem == "Pan")
            {
                cbSub1.Items.Add("LeftToRight");
                cbSub1.Items.Add("RightToLeft");

                cbSub2.Items.Add("Follow");
                cbSub2.Items.Add("NoFollow");

                cbSub3.Items.Add("Slow");
                cbSub3.Items.Add("Normal");
                cbSub3.Items.Add("Fast");

                cbSub4.Items.Add("WithTruck");
                cbSub4.Items.Add("WithDolly");

                cbSub5.Items.Add("Handheld");
                cbSub5.Items.Add("Tripod");

                cbSub6.Items.Add("Parallax");
                cbSub6.Items.Add("Diagonal	");
            }
            if (cbMain.SelectedItem == "Tilt")
            {
                cbSub1.Items.Add("Up");
                cbSub1.Items.Add("Down");

                cbSub2.Items.Add("Follow");
                cbSub2.Items.Add("NoFollow");

                cbSub3.Items.Add("Slow");
                cbSub3.Items.Add("Normal");
                cbSub3.Items.Add("Fast");

                cbSub4.Items.Add("WithDolly");
                cbSub4.Items.Add("WithPedestal");
               
                cbSub5.Items.Add("Handheld");
                cbSub5.Items.Add("Tripod");

                cbSub6.Items.Add("Diagonal");
            }
            if (cbMain.SelectedItem == "Dolly")
            {
                cbSub1.Items.Add("Forwards");
                cbSub1.Items.Add("Backwards");

                cbSub2.Items.Add("Follow");
                cbSub2.Items.Add("NoFollow");

                cbSub3.Items.Add("Slow");
                cbSub3.Items.Add("Normal");
                cbSub3.Items.Add("Fast");

                cbSub4.Items.Add("WithPan");
                cbSub4.Items.Add("WithTilt");
                cbSub4.Items.Add("Car-rig");
                cbSub4.Items.Add("In-car");

                cbSub5.Items.Add("Gimbal");
                cbSub5.Items.Add("Handheld");

                cbSub6.Enabled = false;
            }
            if (cbMain.SelectedItem == "Truck")
            {
                cbSub1.Items.Add("LeftToRight");
                cbSub1.Items.Add("RightToLeft");

                cbSub2.Items.Add("Follow");
                cbSub2.Items.Add("NoFollow");

                cbSub3.Items.Add("Slow");
                cbSub3.Items.Add("Normal");
                cbSub3.Items.Add("Fast");

                cbSub4.Items.Add("WithPan");
                cbSub4.Items.Add("InCar");
                cbSub4.Items.Add("CarRig");

                cbSub5.Items.Add("Gimbal");
                cbSub5.Items.Add("Handheld");

                cbSub6.Items.Add("Parallax");
            }
            if (cbMain.SelectedItem == "Pedestal")
            {
                cbSub1.Items.Add("Up");
                cbSub1.Items.Add("Down");

                cbSub2.Items.Add("Follow");
                cbSub2.Items.Add("NoFollow");

                cbSub3.Items.Add("Slow");
                cbSub3.Items.Add("Normal");
                cbSub3.Items.Add("Fast");

                cbSub4.Items.Add("WithTilt");

                cbSub5.Enabled = false;
                cbSub6.Enabled = false;
            }
            if (cbMain.SelectedItem == "Zoom")
            {
                cbSub1.Items.Add("In");
                cbSub1.Items.Add("Out");

                cbSub2.Items.Add("Follow");
                cbSub2.Items.Add("NoFollow");

                cbSub3.Items.Add("Slow");
                cbSub3.Items.Add("Normal");
                cbSub3.Items.Add("Fast");


                cbSub4.Enabled = false;
                cbSub5.Enabled = false;
                cbSub6.Enabled = false;
            }
            if (cbMain.SelectedItem == "Roll")
            {
                cbSub1.Items.Add("Clockwise");
                cbSub1.Items.Add("Counter-Clockwise");

                cbSub2.Items.Add("Follow");
                cbSub2.Items.Add("NoFollow");

                cbSub3.Items.Add("Slow");
                cbSub3.Items.Add("Normal");
                cbSub3.Items.Add("Fast");
                cbSub4.Enabled = false;
                cbSub5.Enabled = false;
                cbSub6.Enabled = false;
            }

        }

        public async void button2_Click(object sender, EventArgs e)
        {
            List<Data> listData = new List<Data>();
            int cnt;
            try
            {
                FirebaseResponse response = await client.GetTaskAsync("Counter/" + lblName.Text);
                Caunter_Class obj = response.ResultAs<Caunter_Class>();
                cnt = Convert.ToInt32(obj.cnt);
            }
            catch (NullReferenceException)
            {
                cnt = 0;
            }
            for (int i = 0; i <= cnt; i++)
            {
                try
                {
                    FirebaseResponse response1 = await client.GetTaskAsync("VIDEO/" + lblName.Text + "/" + i);
                    Data obj1 = response1.ResultAs<Data>();
                    Data d = new Data();
                    d.ID = obj1.ID;
                    d.StopSec = obj1.StopSec;
                    d.Main = obj1.Main;
                    d.Sub1 = obj1.Sub1;
                    d.Sub2 = obj1.Sub2;
                    d.Sub3 = obj1.Sub3;
                    d.Sub4 = obj1.Sub4;
                    d.Sub5 = obj1.Sub5;
                    d.Sub6 = obj1.Sub6;
                    listData.Add(d);
                }
                catch
                {
                }
            }

            listData.Sort(new SortData());
            string output = JsonConvert.SerializeObject(listData);
            TextWriter txt = new StreamWriter("D:\\" + lblName.Text + ".txt");
            txt.Write(output);
            txt.Close();
            for (int i = 0; i <= cnt; i++)
            {
                var datass = new Data
                {
                    ID = (i+1).ToString(),
                    StopSec = Convert.ToInt32(listData[i].StopSec),
                    Main = listData[i].Main,
                    Sub1 = listData[i].Sub1,
                    Sub2 = listData[i].Sub2,
                    Sub3 = listData[i].Sub3,
                    Sub4 = listData[i].Sub4,
                    Sub5 = listData[i].Sub5,
                    Sub6 = listData[i].Sub6
                };
                SetResponse respo = await client.SetTaskAsync("VIDEO/" + lblName.Text + "/" + (i + 1).ToString(), datass);
                _ = respo.ResultAs<Data>();
            }
            
            findbyName(lblName.Text);
        }

        class SortData : IComparer<Data>
        {
            public int Compare(Data x, Data y)
            {
                if (x.StopSec.CompareTo(y.StopSec)!=0)
                {
                    return x.StopSec.CompareTo(y.StopSec);
                }
                else
                {
                    return 0;
                }
            }
        }

        private async void btnEDIT_Click(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                try
                {
                    FirebaseResponse res = await client.GetTaskAsync("VIDEO/" + lblName.Text + "/" + (ID + 1));
                    Data obj = res.ResultAs<Data>();
                    int timeSt = Convert.ToInt32(obj.StopSec);
                    int newStop = Convert.ToInt32((axWindowsMediaPlayer1.Ctlcontrols.currentPosition) * 1000);
                    if ( newStop < timeSt)
                    {
                        FirebaseResponse response1 = await client.GetTaskAsync("VIDEO/" + lblName.Text + "/" + ID);
                        Data obj1 = response1.ResultAs<Data>();
                        var data = new Data
                        {
                            ID = ID.ToString(),
                            StopSec = newStop,
                            Main = cbMain.Text,
                            Sub1 = cbSub1.Text,
                            Sub2 = cbSub2.Text,
                            Sub3 = cbSub3.Text,
                            Sub4 = cbSub4.Text,
                            Sub5 = cbSub5.Text,
                            Sub6 = cbSub6.Text
                        };
                        SetResponse response = await client.SetTaskAsync("VIDEO/" + lblName.Text + "/" + ID, data);
                        Data result = response.ResultAs<Data>();
                        findbyName(lblName.Text);
                    }
                    else
                    {
                        MessageBox.Show("Stop_Time ID:" + ID + " Lớn hơn ID: " + (ID + 1) + " ( " + newStop + " > " + timeSt + " )");
                    }
                }
                catch
                {
                }
            }
            else {
                MessageBox.Show("ID: " + ID + " Không tồn tại");
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
                    StopSec = Convert.ToInt32(timeStop * 1000),
                    Main = cbMain.Text,
                    Sub1 = cbSub1.Text,
                    Sub2= cbSub2.Text,
                    Sub3 =cbSub3.Text,
                    Sub4 =cbSub4.Text,
                    Sub5 =cbSub5.Text,
                    Sub6 =cbSub6.Text
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
            for (i = 0; i <= cnt+1; i++) {
                try
                {
                    FirebaseResponse response = await client.GetTaskAsync("VIDEO/" + Name + "/" + i);
                    Data obj1 = response.ResultAs<Data>();
                    DataRow row = dt.NewRow();
                    row["ID"] = obj1.ID;
                    row["FileName"] = Name ;
                    row["Stop-time-[ms]"] = obj1.StopSec;
                    row["MainCategory"] = obj1.Main;
                    row["SubCategory1"] = obj1.Sub1;
                    row["SubCategory2"] = obj1.Sub2;
                    row["SubCategory3"] = obj1.Sub3;
                    row["SubCategory4"] = obj1.Sub4;
                    row["SubCategory5"] = obj1.Sub5;
                    row["SubCategory6"] = obj1.Sub6;
                    dt.Rows.Add(row);
                }
                catch { 
                }
            }
        }
        
    }
}
