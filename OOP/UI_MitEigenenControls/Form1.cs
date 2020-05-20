
using OOP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_MitEigenenControls
{
    delegate void EigenerHandler();
    public partial class Form1 : Form
    {
        event EventHandler<int> MeinTollesEvent;
        event EigenerHandler EventMitEigenerHandler;

        protected void OnEventMitEigenerHandler()
        {
            EventMitEigenerHandler?.Invoke();
        }
        public Form1()
        {
            InitializeComponent();
            MeinTollesEvent += Form1_MeinTollesEvent;
            Click += Form1_Click;
            MouseClick += Form1_MouseClick;
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            EventMitEigenerHandler.Invoke();

        }

        private void Form1_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Form1_MeinTollesEvent(object sender, int e)
        {
            throw new NotImplementedException();
        }

        private void meinButton1_TripleClick(object sender, int e)
        {
            MessageBox.Show($"KLickt nummer{e}");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var k = new OOP.Katze();

        }
    }

    public class SpezialKatze : Tier
    {
        public SpezialKatze()
        {
            geheim = "lala";
        }

        public override void MachLaut()
        {
            throw new NotImplementedException();
        }
    }
}
