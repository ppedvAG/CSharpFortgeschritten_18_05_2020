using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace HalloSerializer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            var url = $"https://www.googleapis.com/books/v1/volumes?q={textBox1.Text}";

            var web = new HttpClient();

            var json = await web.GetStringAsync(url);

            textBox2.Text = json;

            BooksResult result = JsonConvert.DeserializeObject<BooksResult>(json);

            dataGridView1.DataSource = result.items.Select(x => x.volumeInfo).ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dlg = new SaveFileDialog();
            dlg.Filter = "JSON Datei|*.json|Alle Datein|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                using (var sw = new StreamWriter(dlg.FileName))
                {
                    sw.Write(JsonConvert.SerializeObject(dataGridView1.DataSource));
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            var dlg = new SaveFileDialog();
            dlg.Filter = "XML Datei|*.xml|Alle Datein|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                using (var sw = new StreamWriter(dlg.FileName))
                {
                    var serial = new XmlSerializer(typeof(List<Volumeinfo>));
                    serial.Serialize(sw, dataGridView1.DataSource);
                } //.Dispose(); ->.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "XML Datei|*.xml|Alle Datein|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                using (var sr = new StreamReader(dlg.FileName))
                {
                    var serial = new XmlSerializer(typeof(List<Volumeinfo>));
                    dataGridView1.DataSource = serial.Deserialize(sr);
                } 
            }
        }
    }
}
