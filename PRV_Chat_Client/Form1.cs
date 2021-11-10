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
using System.IO.Pipes;

namespace PRV_Chat_Client
{
    public partial class Form1 : Form
    {
        NamedPipeClientStream cs;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cs = new NamedPipeClientStream(".", "server", PipeDirection.InOut);
            cs.Connect();
            StreamReader sr = new StreamReader(cs);
            StreamWriter sw = new StreamWriter(cs);
            sw.WriteLine("сервер");
            sw.Flush();
            string inp = sr.ReadLine();
            //responseLabel.Text = inp;
        }
    }
}
