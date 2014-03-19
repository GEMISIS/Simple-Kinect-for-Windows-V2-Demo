using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Manual_Progress_Bar
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                float percent = float.Parse(textBox1.Text) / 2000.0f;
                progressBar1.Value = (int)Math.Min(percent * 100.0f, 100.0f);
                label3.Text = "$" + textBox1.Text + " - " + Math.Round(percent * 100.0f, 2) + "%";
            }
            catch (Exception)
            {
            }
        }
    }
}
