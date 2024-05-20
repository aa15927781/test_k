using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            comboBox1.Items.Add(new MyItem("南投A學校", "OLY3UWIxYhuRunv5hHUUzfRspDIO5z8QEmfEerZvYma"));
            comboBox1.Items.Add(new MyItem("南投B學校", "CeGNf3elQgU7wz5PaNOC1mpEDtsuFVmkgWDniX9SfIj"));
            comboBox1.Items.Add(new MyItem("南投C學校", "WwwnMNNy5SkMn5RDprFbVKqLP0UsijgKXgUkkThh1je"));
            comboBox1.Items.Add(new MyItem("個人", "FV5vakUhDAaR8OcPuoPKrdIrC0F02S9Lau6n7OQnAvI"));


        }

        private void button1_Click(object sender, EventArgs e)
        {
            lineNotify(textBox1.Text.ToString());
        }

        private void lineNotify(string msg) //執行Line
        {
            string token = ((MyItem)comboBox1.SelectedItem).RealValue; 
            try
            {
                var request = (HttpWebRequest)WebRequest.Create("https://notify-api.line.me/api/notify");
                var postData = string.Format("message={0}", "\r\n" + msg);
                var data = Encoding.UTF8.GetBytes(postData);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                request.Headers.Add("Authorization", "Bearer " + token);

                using (var stream = request.GetRequestStream()) stream.Write(data, 0, data.Length);
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // remember cast to a string type
           // textBox1.Text = ((MyItem)comboBox1.SelectedItem).RealValue; 測試token有沒有傳遞
        }

        struct MyItem   
        {
            public MyItem(string displayName, string realValue)
                : this()  //舊版C#默認構造函數，然後才能分配自動實現的屬性
            {
                DisplayName = displayName;
                RealValue = realValue;
            }
            public string DisplayName { get; set; }
            public string RealValue { get; set; }
            
            // must have this override method to display the right string.
            public override string ToString()
            {
                return DisplayName;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "HH:mm";

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            
        }
    }
}
