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
using System.Diagnostics;
using System.Resources;
namespace ToastWalletC
{
    public partial class create_form : Form
    {
        public create_form()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox1.PasswordChar = '\0';
                textBox2.PasswordChar = '\0';
            }
            else
            {
                textBox1.PasswordChar = '*';
                textBox2.PasswordChar = '*';                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text + textBox2) && textBox1.Text == textBox2.Text)
            {
                //change Directory
                string exeDir = Directory.GetCurrentDirectory();
                Environment.CurrentDirectory = exeDir;
                //set path and arguments
                string arg = "/create Store\\Store /silent /hash sha512 /encryption serpent /filesystem NTFS /size 100M /force /password " + textBox1.Text;
                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Porto\VeraCrypt Format-x64.exe");
                Process pross = new Process();
                pross.StartInfo.FileName = path;
                pross.StartInfo.Arguments = arg;
                pross.Start();
                this.Hide();
                pross.WaitForExit();
                MessageBox.Show("Container Generated Succesfully.");
                var decrypt_form = new decrypt_form();
                decrypt_form.Closed += (s, args) => this.Close();
                decrypt_form.Show();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string dir = (Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            if (Directory.Exists(dir + @"\ToastWallet.bak"))
            {
                var cmd = @"C:\Windows\System32\cmd.exe";
                string argm = "/C move " + dir + @"\ToastWallet.bak " + dir + @"\ToastWallet";
                Process mm = new Process();
                mm.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                mm.StartInfo.FileName = cmd;
                mm.StartInfo.Arguments = argm;
                mm.Start();
                mm.WaitForExit();
            }
            //cleanup
            string d = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Porto");
            File.Delete(d + @"\VeraCrypt-x64.exe");
            File.Delete(d + @"\VeraCrypt Format-x64.exe");
            File.Delete(d + @"\VeraCrypt-x64.sys");
            File.Delete(d + @"\ToastWallet.exe");
            Directory.Delete(d);
            Application.Exit();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("If you like this project and would like to support it." + "\n" + "XRP: rUaESERZHHjE7duW8ZhWo4yjXsjiDGQ1Ws");
        }
    }

}
    

