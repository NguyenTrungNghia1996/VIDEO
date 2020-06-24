using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace WindowsFormsApp1
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
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);
            if (client != null) {
                MessageBox.Show("Kết nối Thành Công ");
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            var data = new Data
            {   
                ID = txtID.Text,
                Name = txtName.Text
            };
            SetResponse response = await client.SetTaskAsync("INFO/",data);
            Data result = response.ResultAs<Data>();
            MessageBox.Show("Data Insert");
        }
    }
}
