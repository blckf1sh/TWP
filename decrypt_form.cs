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
    public partial class decrypt_form : Form
    {
        public decrypt_form()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox1.PasswordChar = '\0';
            }
            else
            {
                textBox1.PasswordChar = '*';
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                string exeDir = Directory.GetCurrentDirectory();
                Environment.CurrentDirectory = exeDir;

                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Porto\VeraCrypt-x64.exe");
                string arg = "/l I /hash sha512 /c no /q /v Store\\store /s /Password " + textBox1.Text;
                Process pross = new Process();
                pross.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                pross.StartInfo.FileName = path;
                pross.StartInfo.Arguments = arg;
                pross.Start();
                this.Hide();
                pross.WaitForExit();

                if (!Directory.Exists("I:"))
                {
                    MessageBox.Show("Wrong Password, Please Try Again.");
                    var decrypt_form = new decrypt_form();
                    decrypt_form.Closed += (s, args) => this.Close();
                    decrypt_form.Show();
                }
                else
                {
                    var link = @"C:\Windows\System32\cmd.exe";
                    string cd = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ToastWallet");
                    string link_arg = " /C mklink /d /j " + cd + " I:";
                    Process c = new Process();
                    c.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    c.StartInfo.FileName = link;
                    c.StartInfo.Arguments = link_arg;
                    c.Start();
                    c.WaitForExit();

                    //link Toast Folder to Crypted container


                    //start ToastWallet
                    var Toast = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Porto\ToastWallet.exe");
                    Process run = new Process();
                    run.StartInfo.FileName = Toast;
                    run.Start();
                    run.WaitForExit();

                    //closing crypted container
                    var close_vera = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Porto\VeraCrypt-x64.exe");
                    string close_arg = " /d I /q /s";
                    Process close = new Process();
                    close.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    close.StartInfo.FileName = close_vera;
                    close.StartInfo.Arguments = close_arg;
                    close.Start();
                    close.WaitForExit();

                    //unloading veracrypt service
                    var unload_sys = @"C:\Windows\System32\Cmd.exe";
                    string unload_arg = " /C net stop veracrypt";
                    Process unload = new Process();
                    unload.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    unload.StartInfo.FileName = unload_sys;
                    unload.StartInfo.Arguments = unload_arg;
                    unload.Start();
                    unload.WaitForExit();

                    //Delete Temp Files
                    string d = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Porto");
                    File.Delete(d + @"\VeraCrypt-x64.exe");
                    File.Delete(d + @"\VeraCrypt Format-x64.exe");
                    File.Delete(d + @"\VeraCrypt-x64.sys");
                    File.Delete(d + @"\ToastWallet.exe");
                    Directory.Delete(d);
                    Directory.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Toastwallet"));
                    //restore old ToastWallet if it was there

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
                        Application.Exit();
                    }
                    else
                    {
                        MessageBox.Show("Encrypted Folder Succesfully Unmounted");
                        Application.Exit();
                    }

                }
            }
            else MessageBox.Show("Password Can Not Be Empty");

                
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
            //unloading veracrypt service
            var unload_sys = @"C:\Windows\System32\Cmd.exe";
            string unload_arg = " /C net stop veracrypt";
            Process unload = new Process();
            unload.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            unload.StartInfo.FileName = unload_sys;
            unload.StartInfo.Arguments = unload_arg;
            unload.Start();
            unload.WaitForExit();
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
            //go back
            this.Hide();
            var reset_form = new reset_form();
            reset_form.Closed += (s, args) => this.Close();
            reset_form.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("If you like this project and would like to support it." + "\n" + "XRP: rUaESERZHHjE7duW8ZhWo4yjXsjiDGQ1Ws");

        }
    }

}

