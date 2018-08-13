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
    public partial class load_form : Form
    {
        public load_form()
        {
            InitializeComponent();
        }

        private void Welkom_Load(object sender, EventArgs e)
        {


            //Moving Existing ToastWallet in appdata to bak
            if (Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)) + @"\ToastWallet"))
            {
                string dir = (Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
                var cmd = @"C:\Windows\System32\cmd.exe";
                string arg = "/C move " + dir + @"\ToastWallet " + dir + @"\ToastWallet.bak";
                Process mm = new Process();
                mm.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                mm.StartInfo.FileName = cmd;
                mm.StartInfo.Arguments = arg;
                mm.Start();
                mm.WaitForExit();
            }
                //Set Executable Directory as Working Directory
                string exeDir = Directory.GetCurrentDirectory();
                Environment.CurrentDirectory = exeDir;

                //Extracting Temp files
                string appdata = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                Directory.CreateDirectory(appdata + @"\Porto");
                string temp = appdata + @"\Porto";
                byte[] a = TWP.Properties.Resources.VeraCrypt_Format_x64;
                byte[] b = TWP.Properties.Resources.VeraCrypt_x64;
                byte[] c = TWP.Properties.Resources.veracrypt_x641;
                byte[] t = TWP.Properties.Resources.ToastWallet;
                string tempa = temp + @"\VeraCrypt format-x64.exe";
                string tempb = temp + @"\VeraCrypt-x64.exe";
                string tempc = temp + @"\VeraCrypt-x64.sys";
                string tempt = temp + @"\ToastWallet.exe";
                File.WriteAllBytes(tempa, a);
                File.WriteAllBytes(tempb, b);
                File.WriteAllBytes(tempc, c);
                File.WriteAllBytes(tempt, t);
                //If Store folder does not exist make one.
                if (!Directory.Exists("Store"))
            {
                Directory.CreateDirectory("Store");
            }
                
                //chack if Store File Exists If not Create One Else Goto Unlock.form
                if (!File.Exists(@"Store\Store"))
                {
                    this.Hide();
                    var create_form = new create_form();
                    create_form.Closed += (s, args) => this.Close();
                    create_form.Show();
                }
                else
                {
                    this.Hide();
                    var decrypt_form = new decrypt_form();
                    decrypt_form.Closed += (s, args) => this.Close();
                    decrypt_form.Show();
                }
        }
    }
}

