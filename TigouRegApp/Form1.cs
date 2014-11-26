using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace TigouRegApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        

        LoggerClass myLog = null;

        int TypeIndex = 0;

        string saveFilePath = string.Empty;
        string failFilePath = string.Empty;
        List<string> lsEmail = new List<string>();
        StreamWriter srAccount = null;
        StreamWriter srFail = null;



        private void Form1_Load(object sender, EventArgs e)
        {
            this.backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            this.backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
            this.backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;

            myLog = new LoggerClass(this.listBoxLog);
            MTVerifyCodeClass.Init(this.pictureBoxVerify, this.textBoxVerifyCode);

            TypeIndex = UtilityClass.LoadLibFromFile("ocr.lib", "123");
            if (TypeIndex<0)
            {
                myLog.writeLogger("载入验证码识别库错误！！！！");
            }
            else
            {
                TigouLib.OnVeifyImageOk += TigouLib_OnVeifyImageOk;
            }
        }

        string TigouLib_OnVeifyImageOk(Image arg)
        {
            //throw new NotImplementedException();
            StringBuilder Result = new StringBuilder('\0', 256);
            MemoryStream memory = new MemoryStream();
            string code = string.Empty;
            try
            {
                arg.Save(memory, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] Buffer = memory.ToArray();

                if (UtilityClass.GetCodeFromBuffer(TypeIndex,Buffer,Buffer.Length,Result))
                {
                    code = Result.ToString();
                }
                else
                {
                    code = "OcrError";
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                code = "Error";
            }
            return code;
            
        }

        void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //throw new NotImplementedException();

            UserInfor objResult = e.UserState as UserInfor;
            if (objResult!=null)
            {
                if (string.IsNullOrEmpty(objResult.Hr))
                {
                    try
                    {
                        srAccount.WriteLine(string.Format(@"{0}    {1}", objResult.Account, objResult.PassWord));
                        myLog.writeLogger("注册成功√√√");
                        srAccount.Flush();
                    }
                    catch (System.Exception ex)
                    {
                        myLog.writeLogger(ex.Message);
                    }
                } 
                else
                {
                    
                    if (!(objResult.Hr.Contains("Already Register")||objResult.Hr.Contains("65538")))
                    {
                        myLog.writeLogger("注册失败：：：" + objResult.Hr);
                        srFail.WriteLine(string.Format(@"{0}    {1}", objResult.Account, objResult.PassWord));
                        srFail.Flush();
                    }
                    else
                    {
                        myLog.writeLogger("注册失败：：：该用户名或者邮箱已被注册！");
                    }

                }
            }


        }

        void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //throw new NotImplementedException();
            this.toolStripButtonSetAccountFilePath.Enabled =true;
            this.toolStripButtonImportEmail.Enabled =true;
            this.toolStripButtonBeginReg.Enabled = true;
            this.toolStripButtonStopReg.Enabled = false;

            MessageBox.Show("停止注册成功！！！");

            try
            {
                if (srAccount!=null)
                {
                    srAccount.Close();
                }
            }
            catch (System.Exception ex)
            {
                myLog.writeLogger(ex.Message);
            }
        }

        void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //lsEmail.Add("sytest01@sina.com");
            //lsEmail.Add("sytest02@sina.com");
            //lsEmail.Add("sytest03@sina.com");
            //lsEmail.Add("sytest04@sina.com");
            //lsEmail.Add("sytest05@sina.com");
            //lsEmail.Add("sytest06@sina.com");
            //lsEmail.Add("sytest07@sina.com");
            //lsEmail.Add("sytest09@sina.com");
            //lsEmail.Add("sytest10@sina.com");
            //lsEmail.Add("sytest11@sina.com");

            //throw new NotImplementedException();
            int i=0;
            while (i<lsEmail.Count && !this.backgroundWorker1.CancellationPending)
            {
                
                try
                {
                    string account = Regex.Replace(lsEmail[i],@"@\S+",string.Empty,RegexOptions.IgnoreCase);
                    string password =RandomPassword.MakeRandomPassword("NUMCHAR",9);
                    string email = lsEmail[i];
                    string psn = UtilityClass.GenPinCode();
                    string hr = string.Empty;

                    TigouLib libObj = new TigouLib();
                    int tryAgain = 0;
                    while (tryAgain<10)
                    {
                        hr = libObj.RegAccount(account, password, email, psn);
                        
                        if (hr != "验证码不正确")
                        {
                            break;
                        }
                        tryAgain++;
                    }

                    this.backgroundWorker1.ReportProgress(i, new UserInfor { Account = account, PassWord = password, Hr = hr });

                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                i++;
            }

            


        }

        private void toolStripButtonImportEmail_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog()!=DialogResult.OK)
            {
                return;
            }
            lsEmail.Clear();
            try
            {
                using(StreamReader sr=new StreamReader(this.openFileDialog1.FileName))
                {
                    string words = sr.ReadToEnd();
                    MatchCollection mc=Regex.Matches(words,@"\S+?@\S+?\.\S+",RegexOptions.IgnoreCase);
                    foreach (Match _m in mc)
                    {
                        lsEmail.Add(_m.Value);
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripButtonBeginReg_Click(object sender, EventArgs e)
        {
           
            try
            {
                if (string.IsNullOrEmpty(saveFilePath) || lsEmail.Count <= 0)
                {
                    throw new Exception
                    ("请导入邮箱或者指定帐号保存文件路径");
                }
                if (srAccount!=null)
                {
                    srAccount.Close();
                }
                if (srFail!=null)
                {
                    srFail.Close();
                }

                srAccount = new StreamWriter(saveFilePath, true, Encoding.Default);
                srFail = new StreamWriter(failFilePath, true, Encoding.Default);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.toolStripButtonSetAccountFilePath.Enabled = false;
            this.toolStripButtonImportEmail.Enabled = false;
            this.toolStripButtonBeginReg.Enabled = false;
            this.toolStripButtonStopReg.Enabled = true;

            this.backgroundWorker1.RunWorkerAsync();
        }

        private void toolStripButtonSetAccountFilePath_Click(object sender, EventArgs e)
        {
            if (this.saveFileDialog1.ShowDialog()!=DialogResult.OK)
            {
                return;
            }
            saveFilePath = this.saveFileDialog1.FileName;
            failFilePath = string.Format(@"{2}\{0}_fail{1}", Path.GetFileNameWithoutExtension(saveFilePath), Path.GetExtension(saveFilePath),Path.GetDirectoryName(saveFilePath));
        }

        private void toolStripButtonStopReg_Click(object sender, EventArgs e)
        {
            myLog.writeLogger("请求停止注册，请等待注册线程结束");
            this.toolStripButtonSetAccountFilePath.Enabled = false;
            this.toolStripButtonImportEmail.Enabled = false;
            this.toolStripButtonBeginReg.Enabled = false;
            this.toolStripButtonStopReg.Enabled = false;
        }
    }
}
