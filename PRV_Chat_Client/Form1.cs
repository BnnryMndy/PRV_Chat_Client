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
using System.Threading;

namespace PRV_Chat_Client
{
    public partial class Form1 : Form
    {
        NamedPipeClientStream cs;
        StreamReader sr;
        StreamWriter sw;
        Task<string> task;

        public Form1()
        {
            InitializeComponent();

            cs = new NamedPipeClientStream(".", "server", PipeDirection.InOut, PipeOptions.Asynchronous);
            cs.Connect();
            
            sr = new StreamReader(cs);
            sw = new StreamWriter(cs);
            task = ReadLineAsync(sr);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            sw.WriteLine(inputTextBox.Text);
            sw.Flush();
            inputTextBox.Text = "";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (task.IsCompleted || task.IsFaulted || task.IsCanceled)
            {
                task = ReadLineAsync(sr);
            }

        }

        private async Task<string> ReadLineAsync(StreamReader sr)
        {

            string st = await sr.ReadLineAsync();
            if (!string.IsNullOrEmpty(st))
            {
               AddMsgToList(st);
            }

            return st;
        }

        private void AddMsgToList(string msg)
        {
            if (this.InvokeRequired)
            {
                Action<string> tmp = this.AddMsgToList;
                this.Invoke(tmp, msg);
                return;
            }
            listBox1.Items.Add(msg);

        }
    }
}
