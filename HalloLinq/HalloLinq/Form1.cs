﻿using Bogus;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace HalloLinq
{
    public partial class Form1 : Form
    {

        event Action<object, int> MeinTollesEvent;

        private void button5_Click(object sender, EventArgs e)
        {
            //   if (MeinTollesEvent != null)
            //       MeinTollesEvent.Invoke(this, DateTime.Now.Second);
            MeinTollesEvent?.Invoke(this, DateTime.Now.Second);
        }

        public List<Person> Personen { get; set; } = new List<Person>();

        public Form1()
        {
            InitializeComponent();
            this.MeinTollesEvent += CoolerHandler;
            this.MeinTollesEvent += (o, i) => MessageBox.Show("ANNNOOO " + i.ToString());
            this.MeinTollesEvent -= CoolerHandler;

            var faker = new Faker<Person>();
            faker.RuleFor(x => x.Name, (f, u) => f.Person.FullName);
            faker.RuleFor(x => x.GebDatum, (f, u) => f.Date.Past(50));
            faker.RuleFor(x => x.Ort, (f, u) => f.Address.Country());

            for (int i = 0; i < 5000; i++)
            {
                var p = faker.Generate();
                p.Id = i;
                Personen.Add(p);
                //Personen.Add(new Person()
                //{
                //    Name = $"Fred #{i:000}",
                //    Id = i,
                //    Ort = "Hier und da",
                //    GebDatum = DateTime.Now.AddYears(-50).AddHours(i * 27)
                //});
            }

            var xDoc = new XDocument(); //linq-to-xml
            xDoc.Root.DescendantNodes().Where(x => x.NodeType == System.Xml.XmlNodeType.Element);

            var xmlDoc = new XmlDocument(); //alles im Speicher

            var xmlReader = XmlReader.Create(""); //elementweise (schnell)
           

        }

        private void CoolerHandler(object arg1, int arg2)
        {
            MessageBox.Show(arg2.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Personen;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //linq expression syntax
            var query = from p in Personen
                        where p.GebDatum.Month == 5 && p.Ort.StartsWith("M")
                        orderby p.GebDatum.Day, p.GebDatum.Year descending
                        select p;

            dataGridView1.DataSource = query.ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Personen.Where(x => x.GebDatum.Month == 5 &&
                                                           x.Ort.StartsWith("M"))
                                               .OrderBy(x => x.GebDatum.Day)
                                               .ThenByDescending(x => x.GebDatum.Year)
                                               .ToList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //var groups = Personen.GroupBy(x => x.GebDatum.Year);

            // var count = Personen.Count(x => x.GebDatum.Year == 1999);
            bool hatJemand1999 = Personen.Any(x => x.GebDatum.Year == 1999);
            bool habenAlle1999 = Personen.All(x => x.GebDatum.Year == 1999);

            var p = Personen.FirstOrDefault(x => x.GebDatum.Year == 1999);
            //var p2 = Personen.Single(x => x.GebDatum.Year == 1999);
            //var avgJahr = Personen.Average(x => x.GebDatum.Year);
            var orte = Personen.Select(x => x.Ort).Distinct();

            MessageBox.Show(string.Join(", ", orte));
        }


    }

    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ort { get; set; }
        public DateTime GebDatum { get; set; }
    }
}
