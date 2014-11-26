using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Threading;


    class MTVerifyCodeClass
    {
        private static PictureBox m_pictureBox=null;
        private static TextBox m_textBox=null;
        private static List<verifyCodeParam> m_list = new List<verifyCodeParam>();

        delegate void delegateSetPictureBox(Image pImage);
        delegate void delegateSetTextBox(bool enable);

        private static void SetPictureBox(Image pImage)
        {
            if (m_pictureBox.InvokeRequired)
            {
                m_pictureBox.Invoke(new delegateSetPictureBox(SetPictureBox), new object[] { pImage });
                return;
            }
            m_pictureBox.Image =pImage;
        }
        private static void SetTextBox(bool enable)
        {
            if (m_textBox.InvokeRequired)
            {
                m_textBox.Invoke(new delegateSetTextBox(SetTextBox), new object[] { enable });
                return;
            }

            m_textBox.Enabled = enable;
            if (enable)
            {
                m_textBox.Text = string.Empty;
                m_textBox.Focus();
            }

            
        }

        public static void Init(PictureBox pPicBox,TextBox pTextBox)
        {
            m_pictureBox = pPicBox;
            m_textBox = pTextBox;

            m_textBox.KeyUp += new KeyEventHandler(m_textBox_KeyUp);
        }

        static void m_textBox_KeyUp(object sender, KeyEventArgs e)
        {
            //throw new Exception("The method or operation is not implemented.");
            if (e.KeyCode!=Keys.Enter)
            {
                return;
            }
            SetVerifyCode(m_textBox.Text.Trim());
        }
        public static void AddVerifyImage(AutoResetEvent pEvent,Image pImage,verifyCodeBaseClass pSender)
        {
            verifyCodeParam param = new verifyCodeParam();
            param.theadEvent = pEvent;
            param.verifyImage = (Image)pImage.Clone();
            param.Sender = pSender;
            lock(m_list)
            {
                m_list.Add(param);
                if (m_list.Count==1)
                {
                    SetPictureBox(param.verifyImage);
                    SetTextBox(true);
                }
            }
        }
        public static void clear()
        {
            foreach (verifyCodeParam param in m_list.ToArray())
            {
                param.theadEvent.Set();
            }
        }

        public static void SetVerifyCode(string pVerifyCode)
        {
            lock(m_list)
            {
                verifyCodeParam param = m_list[0];
                param.Sender.m_verifyCode = pVerifyCode;
                param.theadEvent.Set();

                m_list.Remove(param);
                if (m_list.Count<=0)
                {
                    SetTextBox(false);
                } 
                else
                {
                    SetPictureBox(m_list[0].verifyImage);
                    SetTextBox(true);
                }

            }
        }

    }

    class verifyCodeParam
    {
        public verifyCodeBaseClass Sender = null;
        public AutoResetEvent theadEvent = null;
        public Image verifyImage = null;
    }
   public  class verifyCodeBaseClass
    {
        public string m_verifyCode = string.Empty;
    }

