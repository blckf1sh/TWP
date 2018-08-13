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
namespace ToastWalletC
{
    public partial class reset_form : Form
    {
        public reset_form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text + textBox2.Text + textBox3.Text) && textBox2.Text == textBox3.Text)
            {
                
                
                string exeDir = Directory.GetCurrentDirectory();
                Environment.CurrentDirectory = exeDir;
                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Porto\VeraCrypt-x64.exe");
                string cpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Porto\VeraCrypt Format-x64.exe");
                string arg = "/l T /hash sha512 /c no /q /s /v Store\\store /s /Password " + textBox1.Text;
                string arg2 = "/l Y /hash sha512 /c no /q /s /v Store\\store0 /s /Password " + textBox2.Text;
                string narg = "/create Store\\Store0 /silent /hash sha512 /encryption serpent /filesystem NTFS /size 100M /force /password " + textBox2.Text;

                
                //mount old containers
                Process b = new Process();
                b.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                b.StartInfo.FileName = path;
                b.StartInfo.Arguments = arg;
                b.Start();
                this.Hide();
                b.WaitForExit();
                if (!Directory.Exists("T:"))
                {
                    MessageBox.Show("Wrong Password, Please Try Again.");
                    var decrypt_form = new reset_form();
                    decrypt_form.Closed += (s, args) => this.Close();
                    decrypt_form.Show();
                }
                else
                {
                   //create new container
                    Process a = new Process();
                    a.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    a.StartInfo.FileName = cpath;
                    a.StartInfo.Arguments = narg;
                    a.Start();
                    a.WaitForExit();
                    //mount new container
                    Process c = new Process();
                    c.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    c.StartInfo.FileName = path;
                    c.StartInfo.Arguments = arg2;
                    c.Start();
                    c.WaitForExit();
                    //link old containers to folders
                    var link1 = @"C:\Windows\System32\cmd.exe";
                    string cd = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Porto";
                    string link_arg = " /C mklink /d /j " + cd + @"\1 T:";
                    string link_arg2 = " /C mklink /d /j " + cd + @"\0 Y:";
                    Process d = new Process();
                    d.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    d.StartInfo.FileName = link1;
                    d.StartInfo.Arguments = link_arg;
                    d.Start();
                    d.WaitForExit();
                    //link new container
                    Process f = new Process();
                    f.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    f.StartInfo.FileName = link1;
                    f.StartInfo.Arguments = link_arg2;
                    f.Start();
                    f.WaitForExit();
                    //copy files
                    string copy_arg = " /C xcopy /E /Q /H " + cd + @"\1\* " + cd + @"\0\";
                    Process g = new Process();
                    g.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    g.StartInfo.FileName = link1;
                    g.StartInfo.Arguments = copy_arg;
                    g.Start();
                    g.WaitForExit();
                    //unmounting containers
                    string um0 = @" /q /s /d Y";
                    string um1 = @" /q /s /d T";
                    Process h = new Process();
                    h.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    h.StartInfo.FileName = path;
                    h.StartInfo.Arguments = um0;
                    h.Start();
                    h.WaitForExit();
                    Process h2 = new Process();
                    h2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    h2.StartInfo.FileName = path;
                    h2.StartInfo.Arguments = um1;
                    h2.Start();
                    h2.WaitForExit();
                    //remove old container
                    Process rm = new Process();
                    rm.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    rm.StartInfo.FileName = link1;
                    rm.StartInfo.Arguments = " /C move Store\\store0 Store\\Store";
                    rm.Start();
                    rm.WaitForExit();
                    Directory.Delete(cd + @"\0");
                    Directory.Delete(cd + @"\1");
                    MessageBox.Show("Container Was Reseted Succesfully,");

                    var decrypt_form = new decrypt_form();
                    decrypt_form.Closed += (s, args) => this.Close();
                    decrypt_form.Show();
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox1.PasswordChar = '\0';
                textBox2.PasswordChar = '\0';
                textBox3.PasswordChar = '\0';
            }
            else
            {
                textBox1.PasswordChar = '*';
                textBox2.PasswordChar = '*';
                textBox3.PasswordChar = '*';
            }
        }

        private void button3_Click(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            var decrypt_form = new decrypt_form();
            decrypt_form.Closed += (s, args) => this.Close();
            decrypt_form.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("If you like this project and would like to support it." + "\n" + "XRP: rUaESERZHHjE7duW8ZhWo4yjXsjiDGQ1Ws");

        }
    }
}